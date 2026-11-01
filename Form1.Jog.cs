using S7.Net;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LEARN_WF
{
    public partial class Form1
    {
        private enum JogDirection
        {
            Up,
            Down
        }

        // Chọn địa chỉ và lấy tốc độ từ ô nhập của đúng trục.
        private (string SpeedAddress, string JogAddress, decimal SpeedValue)
            GetInfAxis(int axis, JogDirection dirJog)
        {
            if (dirJog != JogDirection.Up &&
                dirJog != JogDirection.Down)
            {
                throw new ArgumentOutOfRangeException(nameof(dirJog));
            }

            switch (axis)
            {
                case 1:
                    return (
                        PlcMap.JogSpeedAxis1Address,
                        dirJog == JogDirection.Up
                            ? PlcMap.JogUpAxis1Address
                            : PlcMap.JogDownAxis1Address,
                        numSpeedJogAxis1.Value);

                case 2:
                    return (
                        PlcMap.JogSpeedAxis2Address,
                        dirJog == JogDirection.Up
                            ? PlcMap.JogUpAxis2Address
                            : PlcMap.JogDownAxis2Address,
                        numSpeedJogAxis2.Value);

                case 3:
                    return (
                        PlcMap.JogSpeedAxis3Address,
                        dirJog == JogDirection.Up
                            ? PlcMap.JogUpAxis3Address
                            : PlcMap.JogDownAxis3Address,
                        numSpeedJogAxis3.Value);

                default:
                    throw new ArgumentOutOfRangeException(
                        nameof(axis), "Trục phải là 1, 2 hoặc 3.");
            }
        }

        // Đây là bước xử lý trên PC, không phải trạng thái động cơ.
        private enum JogPhase
        {
            Idle,
            WritingSpeed,
            SendingOn,
            Holding,
            SendingOff,
            Fault
        }

        private class JogState
        {
            // null: đã thả hoặc yêu cầu bị hủy.
            public JogDirection? RequestedDirection = null;

            public JogPhase Phase = JogPhase.Idle;

            // Giữ đúng đối tượng PLC của lượt nhấn này.
            public Plc Connection = null;

            // Giữ đúng địa chỉ bit cần bật và tắt.
            public string ActiveJogAddress = "";

            // Chặn lượt mới khi lỗi chưa được xử lý.
            public bool NeedsReset = false;
        }

        private readonly JogState[] jogStates =
        {
            new JogState(), // Trục 1: phần tử 0.
            new JogState(), // Trục 2: phần tử 1.
            new JogState()  // Trục 3: phần tử 2.
        };

        private async Task StartJogAsync(int axis, JogDirection dirJog)
        {
            if (axis < 1 || axis > 3)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(axis), "Trục phải là 1, 2 hoặc 3.");
            }

            if (plc == null || changingConnection || shuttingDown)
                return;

            if (!PlcMap.Connected || !PlcMap.DataValid)
                return;

            // Không nhận lượt mới khi lượt trước chưa kết thúc.
            // Đồng thời chặn đổi Up/Down trên cùng trục khi đang giữ.
            if (jogStates[axis - 1].Phase != JogPhase.Idle ||
                jogStates[axis - 1].NeedsReset)
            {
                return;
            }

            var info = GetInfAxis(axis, dirJog);

            // PLC INT cần số nguyên; tốc độ Jog yêu cầu lớn hơn 0.
            // Maximum của ô num vẫn phải đặt theo giới hạn thực tế máy.
            if (info.SpeedValue <= 0 ||
                info.SpeedValue > short.MaxValue ||
                info.SpeedValue != decimal.Truncate(info.SpeedValue))
            {
                AddLog("Tốc độ Jog phải là số nguyên từ 1 đến 32767.");
                return;
            }

            jogStates[axis - 1].RequestedDirection = dirJog;
            jogStates[axis - 1].Connection = plc;
            jogStates[axis - 1].ActiveJogAddress = info.JogAddress;
            jogStates[axis - 1].Phase = JogPhase.WritingSpeed;

            // Ghi nhớ đã bắt đầu gửi true chưa.
            // Nếu phát sinh ngoại lệ sau đó, cần thử gửi false.
            bool onAttempted = false;

            try
            {
                // BƯỚC 1: Ghi tốc độ rồi chờ kết quả.
                bool WrittenSpeedJog = await PlcMap.WriteIntAsync(
                    jogStates[axis - 1].Connection,
                    info.SpeedAddress,
                    info.SpeedValue);

                if (!WrittenSpeedJog)
                {
                    jogStates[axis - 1].RequestedDirection = null;
                    jogStates[axis - 1].Phase = JogPhase.Fault;
                    jogStates[axis - 1].NeedsReset = true;

                    AddLog("Ghi tốc độ trục " + axis + " lỗi: "
                        + PlcMap.LastWriteError);
                    return;
                }

                // Người dùng đã thả trong lúc ghi tốc độ:
                // chưa gửi true nên kết thúc lượt này ngay.
                if (jogStates[axis - 1].RequestedDirection == null)
                {
                    jogStates[axis - 1].Phase = JogPhase.Idle;
                    return;
                }

                // Kiểm tra lại sau await vì kết nối có thể đã thay đổi.
                if (jogStates[axis - 1].NeedsReset ||
                    changingConnection ||
                    shuttingDown ||
                    !ReferenceEquals(
                        plc, jogStates[axis - 1].Connection) ||
                    !PlcMap.Connected ||
                    !PlcMap.DataValid)
                {
                    jogStates[axis - 1].RequestedDirection = null;
                    jogStates[axis - 1].Phase = JogPhase.Fault;
                    jogStates[axis - 1].NeedsReset = true;

                    AddLog("Đã hủy bắt đầu Jog trục " + axis + ".");
                    return;
                }

                // BƯỚC 2: Đặt SendingOn ngay trước khi gửi true.
                jogStates[axis - 1].Phase = JogPhase.SendingOn;
                onAttempted = true;

                bool CmdJog = await PlcMap.WriteBoolAsync(
                    jogStates[axis - 1].Connection,
                    jogStates[axis - 1].ActiveJogAddress,
                    true);

                if (!CmdJog)
                {
                    // Lưu lỗi trước, vì gửi false có thể đổi LastWriteError.
                    string error = PlcMap.LastWriteError;

                    jogStates[axis - 1].RequestedDirection = null;
                    jogStates[axis - 1].Phase = JogPhase.Fault;
                    jogStates[axis - 1].NeedsReset = true;

                    AddLog("Gửi bật Jog trục " + axis + " lỗi: " + error);

                    // Ghi lỗi không chứng minh PLC chưa nhận true.
                    // Thử xóa yêu cầu bằng false.
                    await SendJogOffAsyncc(axis);
                    return;
                }

                // Đã thả hoặc điều kiện thay đổi trong lúc gửi true:
                // gửi false sau khi thao tác true hoàn tất.
                if (jogStates[axis - 1].RequestedDirection == null ||
                    jogStates[axis - 1].NeedsReset ||
                    changingConnection ||
                    shuttingDown ||
                    !ReferenceEquals(
                        plc, jogStates[axis - 1].Connection) ||
                    !PlcMap.Connected ||
                    !PlcMap.DataValid)
                {
                    await SendJogOffAsyncc(axis);
                    return;
                }

                // BƯỚC 3: Đã gửi bật và vẫn còn giữ nút.
                // Hàm bắt đầu kết thúc ở đây.
                // Khi thả sau thời điểm này, ReleaseJogAsync sẽ gửi tắt.
                jogStates[axis - 1].Phase = JogPhase.Holding;
                AddLog("Đã gửi yêu cầu Jog trục " + axis + ".");
            }
            catch (Exception ex)
            {
                jogStates[axis - 1].RequestedDirection = null;
                jogStates[axis - 1].Phase = JogPhase.Fault;
                jogStates[axis - 1].NeedsReset = true;

                // Ghi đúng ngoại lệ ban đầu, không dùng LastWriteError.
                AddLog("Lỗi xử lý Jog trục " + axis + ": " + ex.Message);

                if (onAttempted)
                    await SendJogOffAsyncc(axis);
            }
        }

        // Nhận sự kiện thả nút.
        private async Task ReleaseJogAsync(int axis, JogDirection dirJog)
        {
            if (axis < 1 || axis > 3)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(axis), "Trục phải là 1, 2 hoặc 3.");
            }

            // Không để thả nút hướng khác hủy yêu cầu hiện tại.
            if (jogStates[axis - 1].RequestedDirection != dirJog)
                return;

            // Phải gán ngay trước await.
            jogStates[axis - 1].RequestedDirection = null;

            if (jogStates[axis - 1].Phase == JogPhase.Holding)
            {
                // Start đã kết thúc nên Release chịu trách nhiệm gửi tắt.
                await SendJogOffAsyncc(axis);
            }

            // Nếu đang WritingSpeed hoặc SendingOn:
            // StartJogAsync sẽ đọc thấy null và xử lý tiếp.
            // Không tự đặt Idle ở đây khi Start vẫn đang chạy.
        }

        // Hàm gửi false.
        // Không gọi trực tiếp hàm này khi lệnh true vẫn đang gửi.
        private async Task SendJogOffAsyncc(int axis)
        {
            if (axis < 1 || axis > 3)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(axis), "Trục phải là 1, 2 hoặc 3.");
            }

            // Đã có lượt gửi tắt thì không tạo thêm lượt trùng.
            // Lưu ý: return này KHÔNG chờ lượt gửi tắt cũ.
            if (jogStates[axis - 1].Phase == JogPhase.SendingOff)
                return;

            jogStates[axis - 1].RequestedDirection = null;
            jogStates[axis - 1].Phase = JogPhase.SendingOff;

            try
            {
                if (jogStates[axis - 1].Connection == null ||
                    string.IsNullOrWhiteSpace(
                        jogStates[axis - 1].ActiveJogAddress))
                {
                    jogStates[axis - 1].Phase = JogPhase.Fault;
                    jogStates[axis - 1].NeedsReset = true;

                    AddLog("Thiếu thông tin gửi tắt Jog trục " + axis);
                    return;
                }

                // Không chặn gửi tắt bằng DataValid/Busy/Homed.
                // PlcMap sẽ kiểm tra khả năng giao tiếp.
                bool CmdJog = await PlcMap.WriteBoolAsync(
                    jogStates[axis - 1].Connection,
                    jogStates[axis - 1].ActiveJogAddress,
                    false);

                if (CmdJog)
                {
                    // Chỉ về Idle sau khi ghi false thành công.
                    jogStates[axis - 1].Phase = JogPhase.Idle;
                    jogStates[axis - 1].NeedsReset = false;

                    AddLog("Đã xóa yêu cầu Jog trục " + axis + ".");
                }
                else
                {
                    jogStates[axis - 1].Phase = JogPhase.Fault;
                    jogStates[axis - 1].NeedsReset = true;

                    AddLog("Gửi tắt Jog trục " + axis + " lỗi: "
                        + PlcMap.LastWriteError);
                }
            }
            catch (Exception ex)
            {
                jogStates[axis - 1].RequestedDirection = null;
                jogStates[axis - 1].Phase = JogPhase.Fault;
                jogStates[axis - 1].NeedsReset = true;

                AddLog("Lỗi gửi tắt Jog trục " + axis + ": " + ex.Message);
            }
        }

        // Giữ tên các sự kiện để Designer không mất liên kết.
        // Không gắn thêm lần nữa nếu Designer đã đăng ký chúng.

        private async void btnJogDownAxis1_MouseDown(
            object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            try
            {
                await StartJogAsync(1, JogDirection.Down);
            }
            catch (Exception ex)
            {
                AddLog("Lỗi nhấn Down trục 1: " + ex.Message);
            }
        }

        private async void btnJogDownAxis1_MouseUp(
            object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            try
            {
                await ReleaseJogAsync(1, JogDirection.Down);
            }
            catch (Exception ex)
            {
                AddLog("Lỗi thả Down trục 1: " + ex.Message);
            }
        }

        private async void btnJogUpAxis1_MouseDown(
            object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            try
            {
                await StartJogAsync(1, JogDirection.Up);
            }
            catch (Exception ex)
            {
                AddLog("Lỗi nhấn Up trục 1: " + ex.Message);
            }
        }

        private async void btnJogUpAxis1_MouseUp(
            object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            try
            {
                await ReleaseJogAsync(1, JogDirection.Up);
            }
            catch (Exception ex)
            {
                AddLog("Lỗi thả Up trục 1: " + ex.Message);
            }
        }

        private async void btnJogDownAxis2_MouseDown(
            object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            try
            {
                await StartJogAsync(2, JogDirection.Down);
            }
            catch (Exception ex)
            {
                AddLog("Lỗi nhấn Down trục 2: " + ex.Message);
            }
        }

        private async void btnJogDownAxis2_MouseUp(
            object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            try
            {
                await ReleaseJogAsync(2, JogDirection.Down);
            }
            catch (Exception ex)
            {
                AddLog("Lỗi thả Down trục 2: " + ex.Message);
            }
        }

        private async void btnJogUpAxis2_MouseDown(
            object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            try
            {
                await StartJogAsync(2, JogDirection.Up);
            }
            catch (Exception ex)
            {
                AddLog("Lỗi nhấn Up trục 2: " + ex.Message);
            }
        }

        private async void btnJogUpAxis2_MouseUp(
            object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            try
            {
                await ReleaseJogAsync(2, JogDirection.Up);
            }
            catch (Exception ex)
            {
                AddLog("Lỗi thả Up trục 2: " + ex.Message);
            }
        }

        private async void btnJogDownAxis3_MouseDown(
            object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            try
            {
                await StartJogAsync(3, JogDirection.Down);
            }
            catch (Exception ex)
            {
                AddLog("Lỗi nhấn Down trục 3: " + ex.Message);
            }
        }

        private async void btnJogDownAxis3_MouseUp(
            object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            try
            {
                await ReleaseJogAsync(3, JogDirection.Down);
            }
            catch (Exception ex)
            {
                AddLog("Lỗi thả Down trục 3: " + ex.Message);
            }
        }

        private async void btnJogUpAxis3_MouseDown(
            object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            try
            {
                await StartJogAsync(3, JogDirection.Up);
            }
            catch (Exception ex)
            {
                AddLog("Lỗi nhấn Up trục 3: " + ex.Message);
            }
        }

        private async void btnJogUpAxis3_MouseUp(
            object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            try
            {
                await ReleaseJogAsync(3, JogDirection.Up);
            }
            catch (Exception ex)
            {
                AddLog("Lỗi thả Up trục 3: " + ex.Message);
            }
        }
    }
}