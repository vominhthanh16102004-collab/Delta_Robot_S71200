// PlcMap.cs — BẢN ĐỌC/GHI DÙNG CHUNG CHO LEARN_WF
// Thay toàn bộ file cũ bằng file này. Cần package S7netplus, namespace S7.Net.
// Không giữ class PlcMap trùng tên ở file khác.
//
// CÁCH DÙNG (các dòng dưới là ví dụ, đặt trong hàm async của Form):
// Kết nối:     bool ok = await PlcMap.ConnectAsync(plc);
// Đọc Timer:   bool ok = await PlcMap.ReadDataAsync(plc);
// Lấy vị trí:  lblCurrentPosAxis1.Text = PlcMap.PositionAxis1.ToString("F2");
// Lấy bit:     bool homed = PlcMap.HomedAxis1;
// Ghi ô số:    bool ok = await PlcMap.WriteIntAsync(plc,
//                  PlcMap.JogSpeedAxis1Address, numSpeedJogAxis1.Value);
// Ghi bit:     bool ok = await PlcMap.WriteBoolAsync(plc,
//                  PlcMap.JogUpAxis1Address, true);
// Ngắt:        timerRead.Stop(); bool ok = await PlcMap.DisconnectAsync(plc);
//
// PHẢI kiểm tra bool trả về và DataValid trước khi dùng dữ liệu điều khiển.
// Bên ngoài chỉ được đọc giá trị trạng thái (private set), không gán lại.
// Dùng các hàm trên từ luồng UI WinForms; không gọi từ nhiều luồng nền.
// Mọi Open/Read/Write/Close đi qua file này; bỏ các lệnh plc.* trực tiếp ở Form.
// Timer: đọc thành công mới UpdateDisplay; dữ liệu lỗi hiển thị mất dữ liệu.
// Ghi số: kiểm tra thêm giới hạn cơ khí tại Form; chặn ValueChanged lúc khởi tạo.
// Đối với Execute: ghi đủ bộ số thành công rồi mới kích; không dựa vào các lần
// ValueChanged cũ để kết luận bộ số hiện tại đã ghi đủ. PLC vẫn kiểm tra liên động.
//
// Các hàm ghi là truyền dữ liệu, KHÔNG phải bộ điều khiển chuyển động hoàn chỉnh:
// - Home/Execute: cần thống nhất cách PLC xác nhận/xóa bit, chưa tự tạo xung.
// - Jog: cần MouseUp, mất capture/cửa sổ và watchdog PLC; chưa có lớp Jog hoàn chỉnh.
// - Một lệnh đang gửi không thể được thu hồi bằng Disconnect.
// - Stop và XYZ mục tiêu còn thiếu địa chỉ, để rỗng và chặn ghi.
// - Hai vùng đọc có thể khác chu kỳ PLC; chưa phải snapshot nguyên tử của PLC.
// - Dùng một PLC; Connected phản ánh truyền thông gần nhất, không phải heartbeat.
//
// API ReadBytes/Write đối chiếu với mã nguồn chính thức:
// https://github.com/S7NetPlus/s7netplus/blob/main/S7.Net/PlcSynchronous.cs
// Chưa thử trên PLC thật. Quyền truy cập DB/PUT-GET và cấu hình PLC phải phù hợp.

using System;
using System.Threading;
using System.Threading.Tasks;
using S7.Net;

namespace LEARN_WF
{
    public static class PlcMap
    {
        // Dùng tất cả hàm public từ luồng UI WinForms và await kết quả.
        // Một ứng dụng / một PLC. Không dùng .Wait() hoặc .Result trên luồng UI.
        public static bool Connected { get; private set; }
        public static bool DataValid { get; private set; }
        public static string LastReadError { get; private set; } = "";
        public static string LastWriteError { get; private set; } = "";
        public static string LastConnectionError { get; private set; } = "";
        public static DateTime? LastReadTime { get; private set; }

        private static readonly SemaphoreSlim Gate = new SemaphoreSlim(1, 1);
        private static bool closing;
        private static int generation;
        private static Plc activePlc;

        // Tạo plc ở Form rồi truyền vào đây; loại CPU/rack/slot do Form chọn.
        // Không tự kết nối lại hoặc gửi lại lệnh cũ.
        public static async Task<bool> ConnectAsync(Plc plc)
        {
            await Gate.WaitAsync();
            try
            {
                if (plc == null) throw new ArgumentNullException(nameof(plc));
                if (activePlc != null && !ReferenceEquals(activePlc, plc))
                    throw new InvalidOperationException("Hãy Disconnect PLC cũ trước khi đổi kết nối.");
                activePlc = plc;
                closing = true;
                generation++;
                DataValid = false;
                await Task.Run(() => { if (!plc.IsConnected) plc.Open(); });
                Connected = plc.IsConnected;
                closing = false;
                LastConnectionError = "";
                return Connected;
            }
            catch (Exception ex)
            {
                Connected = false;
                DataValid = false;
                closing = true;
                LastConnectionError = ex.Message;
                return false;
            }
            finally { Gate.Release(); }
        }

        // Đọc 21 giá trị. Bận ghi/kết nối thì bỏ lượt đọc, không xếp hàng Timer.
        // false + LastReadError rỗng có thể chỉ là bỏ lượt vì bận.
        public static async Task<bool> ReadDataAsync(Plc plc)
        {
            if (closing || !await Gate.WaitAsync(0)) return false;
            int ticket = generation;
            try
            {
                RequireConnection(plc);
                Snapshot result = await Task.Run(() => ReadSnapshot(plc));
                if (closing || ticket != generation) return false;
                Apply(result); // Trở lại luồng UI sau await.
                return true;
            }
            catch (Exception ex)
            {
                DataValid = false;
                Connected = false;
                LastReadError = ex.Message;
                generation++; // Hủy yêu cầu ghi đang chờ từ trước lỗi.
                return false;
            }
            finally { Gate.Release(); }
        }

        // Ghi BOOL tại các địa chỉ lệnh đã khai báo. Không tự tạo xung/reset bit.
        public static Task<bool> WriteBoolAsync(Plc plc, string address, bool value)
        {
            return WriteValueAsync(plc, address, value, true);
        }

        // Nhận trực tiếp num.Value (decimal). Chặn số lẻ/vượt phạm vi INT.
        // Sau kiểm tra, ghi short 2 byte; không ghi float vào vùng INT.
        public static Task<bool> WriteIntAsync(Plc plc, string address, decimal value)
        {
            if (value != decimal.Truncate(value) || value < short.MinValue || value > short.MaxValue)
            {
                LastWriteError = "Giá trị INT phải là số nguyên trong [-32768, 32767].";
                return Task.FromResult(false);
            }
            return WriteValueAsync(plc, address, (short)value, false);
        }

        private static async Task<bool> WriteValueAsync(Plc plc, string address,
            object value, bool isBit)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                LastWriteError = "Chưa khai báo địa chỉ ghi.";
                return false;
            }
            string[] allowed = isBit ? WritableBits : WritableInts;
            if (Array.IndexOf(allowed, address) < 0)
            {
                LastWriteError = "Địa chỉ không thuộc nhóm ghi đúng kiểu: " + address;
                return false;
            }
            if (closing)
            {
                LastWriteError = "Đang ngắt hoặc chưa kết nối PLC.";
                return false;
            }
            int ticket = generation;
            await Gate.WaitAsync();
            try
            {
                if (closing || ticket != generation)
                {
                    LastWriteError = "Đã hủy yêu cầu ghi cũ do thay đổi/lỗi kết nối.";
                    return false;
                }
                RequireConnection(plc);
                await Task.Run(() => plc.Write(address, value));
                LastWriteError = "";
                // Thành công = PLC đã trả lời ghi, không phải chuyển động đã hoàn tất.
                return true;
            }
            catch (Exception ex)
            {
                DataValid = false;
                Connected = false;
                LastWriteError = ex.Message;
                generation++;
                return false;
            }
            finally { Gate.Release(); }
        }

        // Form gọi timerRead.Stop() trước hàm này.
        // Hủy lệnh chưa gửi; chờ I/O đang chạy xong rồi Close.
        // Không thay thế Stop máy, không bảo đảm xóa bit Jog nếu mạng đã mất.
        public static async Task<bool> DisconnectAsync(Plc plc)
        {
            closing = true;
            generation++;
            Connected = false;
            DataValid = false;
            await Gate.WaitAsync();
            try
            {
                if (activePlc != null && !ReferenceEquals(activePlc, plc))
                    throw new InvalidOperationException("Phải ngắt đúng đối tượng PLC đang dùng.");
                if (plc != null) await Task.Run(() => plc.Close());
                activePlc = null;
                LastConnectionError = "";
                return true;
            }
            catch (Exception ex)
            {
                LastConnectionError = ex.Message;
                return false;
            }
            finally { Gate.Release(); }
        }

        private static void RequireConnection(Plc plc)
        {
            if (plc == null || !ReferenceEquals(plc, activePlc) || !plc.IsConnected)
                throw new InvalidOperationException("Hãy kết nối bằng PlcMap.ConnectAsync(plc) trước.");
        }

        private static readonly string[] WritableBits =
        {
            JogUpAxis1Address, JogUpAxis2Address, JogUpAxis3Address,
            JogDownAxis1Address, JogDownAxis2Address, JogDownAxis3Address,
            ResetAxisAddress, HomeAxisAddress, PcMoveRequestAddress,
            ExecuteAbsAddress, ExecuteXYZAddress, StopAxisAddress
        };
        private static readonly string[] WritableInts =
        {
            TargetPositionAxis1Address, TargetPositionAxis2Address, TargetPositionAxis3Address,
            MoveSpeedAxis1Address, MoveSpeedAxis2Address, MoveSpeedAxis3Address,
            JogSpeedAxis1Address, JogSpeedAxis2Address, JogSpeedAxis3Address
        };

        private sealed class Snapshot
        {
            public bool[] Bits;
            public float[] Numbers;
        }

        // Danh sách liên kết dữ liệu. Địa chỉ vẫn chỉ khai báo ở các hằng phía dưới.
        private static readonly string[] BitAddresses =
        {
            BusyAxis1Address, BusyAxis2Address, BusyAxis3Address,
            ErrorAxis1Address, ErrorAxis2Address, ErrorAxis3Address,
            HomedAxis1Address, HomedAxis2Address, HomedAxis3Address,
            InPosAxis1Address, InPosAxis2Address, InPosAxis3Address
        };
        private static readonly string[] RealAddresses =
        {
            PositionAxis1Address, PositionAxis2Address, PositionAxis3Address,
            ActualSpeedAxis1Address, ActualSpeedAxis2Address, ActualSpeedAxis3Address,
            CurrentXAddress, CurrentYAddress, CurrentZAddress
        };

        private static Snapshot ReadSnapshot(Plc plc)
        {
            if (plc == null || !plc.IsConnected)
                throw new InvalidOperationException("Chưa mở kết nối PLC.");

            int bitStart, realStart;
            // Hiện tại: đọc DB6 byte 576..584 và byte 760..795.
            // Tự suy ra vùng đọc từ Address, không phải sửa offset ở hai nơi.
            byte[] bits = ReadWindow(plc, BitAddresses, 1, out bitStart);
            byte[] reals = ReadWindow(plc, RealAddresses, 4, out realStart);
            var result = new Snapshot
            {
                Bits = new bool[BitAddresses.Length],
                Numbers = new float[RealAddresses.Length]
            };
            for (int i = 0; i < BitAddresses.Length; i++)
            {
                int db, offset, bit;
                ParseAddress(BitAddresses[i], out db, out offset, out bit);
                result.Bits[i] = (bits[offset - bitStart] & (1 << bit)) != 0;
            }
            for (int i = 0; i < RealAddresses.Length; i++)
            {
                int db, offset, bit;
                ParseAddress(RealAddresses[i], out db, out offset, out bit);
                byte[] four = new byte[4];
                Array.Copy(reals, offset - realStart, four, 0, 4);
                // Siemens REAL lưu byte lớn trước (big endian).
                if (BitConverter.IsLittleEndian) Array.Reverse(four);
                float value = BitConverter.ToSingle(four, 0);
                if (float.IsNaN(value) || float.IsInfinity(value))
                    throw new InvalidOperationException("REAL không hợp lệ: " + RealAddresses[i]);
                result.Numbers[i] = value;
            }
            return result;
        }

        private static void Apply(Snapshot data)
        {
            // Chỉ gán sau khi ĐỌC VÀ GIẢI MÃ toàn bộ thành công.
            BusyAxis1 = data.Bits[0];
            BusyAxis2 = data.Bits[1];
            BusyAxis3 = data.Bits[2];
            ErrorAxis1 = data.Bits[3];
            ErrorAxis2 = data.Bits[4];
            ErrorAxis3 = data.Bits[5];
            HomedAxis1 = data.Bits[6];
            HomedAxis2 = data.Bits[7];
            HomedAxis3 = data.Bits[8];
            InPosAxis1 = data.Bits[9];
            InPosAxis2 = data.Bits[10];
            InPosAxis3 = data.Bits[11];
            PositionAxis1 = data.Numbers[0];
            PositionAxis2 = data.Numbers[1];
            PositionAxis3 = data.Numbers[2];
            ActualSpeedAxis1 = data.Numbers[3];
            ActualSpeedAxis2 = data.Numbers[4];
            ActualSpeedAxis3 = data.Numbers[5];
            CurrentX = data.Numbers[6];
            CurrentY = data.Numbers[7];
            CurrentZ = data.Numbers[8];
            LastReadTime = DateTime.Now;
            LastReadError = "";
            Connected = true;
            DataValid = true;
        }

        private static byte[] ReadWindow(Plc plc, string[] addresses, int width,
            out int start)
        {
            int db, first, unused;
            ParseAddress(addresses[0], out db, out first, out unused);
            start = first;
            int end = first + width;
            foreach (string address in addresses)
            {
                int otherDb, offset, bit;
                ParseAddress(address, out otherDb, out offset, out bit);
                if (otherDb != db)
                    throw new InvalidOperationException("Các địa chỉ trong một nhóm đọc phải cùng DB.");
                start = Math.Min(start, offset);
                end = Math.Max(end, offset + width);
            }
            byte[] data = plc.ReadBytes(DataType.DataBlock, db, start, end - start);
            if (data == null || data.Length != end - start)
                throw new InvalidOperationException("PLC trả về thiếu dữ liệu.");
            return data;
        }

        // Hỗ trợ địa chỉ BOOL DBn.DBXbyte.bit và REAL DBn.DBDbyte của map này.
        private static void ParseAddress(string address, out int db,
            out int offset, out int bit)
        {
            if (string.IsNullOrWhiteSpace(address))
                throw new InvalidOperationException("Chưa khai báo địa chỉ PLC.");
            string[] parts = address.Split('.');
            if (parts.Length < 2 || !parts[0].StartsWith("DB"))
                throw new FormatException("Sai địa chỉ: " + address);
            db = int.Parse(parts[0].Substring(2));
            if (parts[1].StartsWith("DBX") && parts.Length == 3)
            {
                offset = int.Parse(parts[1].Substring(3));
                bit = int.Parse(parts[2]);
                if (bit < 0 || bit > 7) throw new FormatException("Sai bit: " + address);
            }
            else if (parts[1].StartsWith("DBD") && parts.Length == 2)
            {
                offset = int.Parse(parts[1].Substring(3));
                bit = 0;
            }
            else throw new FormatException("Sai kiểu địa chỉ đọc: " + address);
            if (db < 1 || offset < 0) throw new FormatException("Sai địa chỉ: " + address);
        }

        // ===== 1. PLC → PC: TRẠNG THÁI BOOL, DB6 =====

        // Đang chạy.
        public const string BusyAxis1Address = "DB6.DBX576.0";
        public static bool BusyAxis1 { get; private set; } // lblBusyAxis1
        public const string BusyAxis2Address = "DB6.DBX576.1";
        public static bool BusyAxis2 { get; private set; } // lblBusyAxis2
        public const string BusyAxis3Address = "DB6.DBX576.2";
        public static bool BusyAxis3 { get; private set; } // lblBusyAxis3

        // Lỗi trục.
        public const string ErrorAxis1Address = "DB6.DBX580.0";
        public static bool ErrorAxis1 { get; private set; } // lblErrorAxis1
        public const string ErrorAxis2Address = "DB6.DBX580.1";
        public static bool ErrorAxis2 { get; private set; } // lblErrorAxis2
        public const string ErrorAxis3Address = "DB6.DBX580.2";
        public static bool ErrorAxis3 { get; private set; } // lblErrorAxis3

        // Đã Home.
        public const string HomedAxis1Address = "DB6.DBX582.0";
        public static bool HomedAxis1 { get; private set; } // lblHomedAxis1
        public const string HomedAxis2Address = "DB6.DBX582.1";
        public static bool HomedAxis2 { get; private set; } // lblHomedAxis2
        public const string HomedAxis3Address = "DB6.DBX582.2";
        public static bool HomedAxis3 { get; private set; } // lblHomedAxis3

        // Đã tới vị trí.
        public const string InPosAxis1Address = "DB6.DBX584.0";
        public static bool InPosAxis1 { get; private set; } // lblInPosAxis1
        public const string InPosAxis2Address = "DB6.DBX584.1";
        public static bool InPosAxis2 { get; private set; } // lblInPosAxis2
        public const string InPosAxis3Address = "DB6.DBX584.2";
        public static bool InPosAxis3 { get; private set; } // lblInPosAxis3

        // ===== 2. PLC → PC: GIÁ TRỊ REAL, DB6 =====

        // Axis_Position_Current; hiển thị lblCurrentPosAxis. Đơn vị cần kiểm tra logic PLC.
        public const string PositionAxis1Address = "DB6.DBD760";
        public static float PositionAxis1 { get; private set; }
        public const string PositionAxis2Address = "DB6.DBD764";
        public static float PositionAxis2 { get; private set; }
        public const string PositionAxis3Address = "DB6.DBD768";
        public static float PositionAxis3 { get; private set; }

        // Axis_Velocity_Current; tốc độ thực tế trục. Đơn vị cần kiểm tra logic PLC.
        public const string ActualSpeedAxis1Address = "DB6.DBD772";
        public static float ActualSpeedAxis1 { get; private set; }
        public const string ActualSpeedAxis2Address = "DB6.DBD776";
        public static float ActualSpeedAxis2 { get; private set; }
        public const string ActualSpeedAxis3Address = "DB6.DBD780";
        public static float ActualSpeedAxis3 { get; private set; }

        // Robot_Position_Current: XYZ HIỆN TẠI, không phải mục tiêu.
        public const string CurrentXAddress = "DB6.DBD784";
        public static float CurrentX { get; private set; }
        public const string CurrentYAddress = "DB6.DBD788";
        public static float CurrentY { get; private set; }
        public const string CurrentZAddress = "DB6.DBD792";
        public static float CurrentZ { get; private set; }

        // REAL phải đọc theo kiểu REAL (float) bằng hàm đọc có kiểu hoặc giải mã
        // đúng 4 byte. Không ép (float) trực tiếp đối tượng uint từ Read("DBx.DBDy").
        // Chọn Axis_Position_Current ở 760, không chọn ActualPos_deg ở 606.

        // ===== 3. PC → PLC: Ô NHẬP INT, DB6 =====
        // INT PLC = short C# (2 byte), KHÔNG ghi float 4 byte vào các ô này.
        // Ô num đặt DecimalPlaces=0; giới hạn theo máy và trong [-32768,32767].
        // Nếu cần số lẻ phải đổi kiểu/quy đổi bên PLC rồi cập nhật lại map.

        // Position_Target_Abs[1..3].
        // Offset phần tử suy ra từ INT[1..3] bắt đầu 742: 742,744,746.
        public const string TargetPositionAxis1Address = "DB6.DBW742"; // numTGPAxis1
        public const string TargetPositionAxis2Address = "DB6.DBW744"; // numTGPAxis2
        public const string TargetPositionAxis3Address = "DB6.DBW746"; // numTGPAxis3

        // Velocity_Abs[1..3].
        public const string MoveSpeedAxis1Address = "DB6.DBW748"; // numSpeedAbsAxis1
        public const string MoveSpeedAxis2Address = "DB6.DBW750"; // numSpeedAbsAxis2
        public const string MoveSpeedAxis3Address = "DB6.DBW752"; // numSpeedAbsAxis3

        // Velocity_Jog[1..3].
        public const string JogSpeedAxis1Address = "DB6.DBW754"; // numSpeedJogAxis1
        public const string JogSpeedAxis2Address = "DB6.DBW756"; // numSpeedJogAxis2
        public const string JogSpeedAxis3Address = "DB6.DBW758"; // numSpeedJogAxis3

        // ===== 4. PC → PLC: NÚT JOG BOOL, DB2 =====
        // Ghép JogUp=Forward, JogDown=Backward; kiểm tra chiều thực tế của máy.
        // MouseDown yêu cầu true, MouseUp yêu cầu false.
        public const string JogUpAxis1Address = "DB2.DBX666.0"; // btnJogUpAxis1
        public const string JogUpAxis2Address = "DB2.DBX666.1"; // btnJogUpAxis2
        public const string JogUpAxis3Address = "DB2.DBX666.2"; // btnJogUpAxis3
        public const string JogDownAxis1Address = "DB2.DBX668.0"; // btnJogDownAxis1
        public const string JogDownAxis2Address = "DB2.DBX668.1"; // btnJogDownAxis2
        public const string JogDownAxis3Address = "DB2.DBX668.2"; // btnJogDownAxis3

        // ===== 5. PC → PLC: LỆNH CHUNG BOOL, DB2 =====
        public const string ResetAxisAddress = "DB2.DBX664.0"; // Reset_Axis; KHÔNG phải Stop.
        public const string HomeAxisAddress = "DB2.DBX670.0"; // btnHomeAxis.
        public const string PcMoveRequestAddress = "DB2.DBX674.0";
        public const string ExecuteAbsAddress = "DB2.DBX674.1"; // btnExecuteAbsAxis.
        public const string ExecuteXYZAddress = "DB2.DBX674.2"; // btnExecuteTGXYZ.

        // Cần đọc logic PLC để biết đặt/xóa và phối hợp 3 bit tại byte 674.
        // Không tự coi PcMoveRequest là Stop hoặc tự kích cả 3 bit cùng lúc.
        // Home/Execute: chưa xác định cơ chế xác nhận/xóa lệnh từ ảnh khai báo.

        // ===== 6. CHƯA CÓ ĐỊA CHỈ: TUYỆT ĐỐI KHÔNG GỌI GHI KHI CÒN RỖNG =====
        public const string StopAxisAddress = ""; // btnStopAxis: cần ảnh phần Stop.
        public const string TargetXAddress = ""; // numTargetX: XYZ mục tiêu chưa thấy.
        public const string TargetYAddress = ""; // numTargetY.
        public const string TargetZAddress = ""; // numTargetZ.
        public const string SpeedXAddress = ""; // numSpeedX: chưa thấy kiểu/offset.
        public const string SpeedYAddress = ""; // numSpeedY.
        public const string SpeedZAddress = ""; // numSpeedZ.

        // Nếu PC tự tính XYZ thành mục tiêu trục thì có thể dùng nhóm ABS,
        // nhưng phải thống nhất logic PLC trước khi chọn ExecuteAbs hay ExecuteXYZ.
    }
}

