using System;
using System.Drawing;
using System.Windows.Forms;

namespace LEARN_WF
{
    public partial class Form1
    {
        // Chỉ lưu trạng thái LẦN TRƯỚC để so sánh ghi log.
        // Trạng thái hiện tại lấy trực tiếp từ PlcMap.
        private bool lastHomedAxis1;
        private bool lastHomedAxis2;
        private bool lastHomedAxis3;

        private bool lastBusyAxis1;
        private bool lastBusyAxis2;
        private bool lastBusyAxis3;

        private bool lastErrorAxis1;
        private bool lastErrorAxis2;
        private bool lastErrorAxis3;

        private bool lastInPosAxis1;
        private bool lastInPosAxis2;
        private bool lastInPosAxis3;

        // Chưa có mẫu trước thì không kết luận đã có chuyển trạng thái.
        private bool hasPreviousStatus;

        // Dùng để không ghi lặp log mất dữ liệu mỗi lượt Timer.
        private bool displayHadValidData;


        // 1. Chuẩn bị giao diện khi mở Form.
        private void InitializeDisplay()
        {
            ShowConnection(false);
            ShowDataUnavailable();

            hasPreviousStatus = false;
            displayHadValidData = false;
        }


        // 2. Ghi log hệ thống.
        private void AddLog(string message)
        {
            lstLog.Items.Add(
                DateTime.Now.ToString("HH:mm:ss")
                + " | " + message);

            if (lstLog.Items.Count > 500)
                lstLog.Items.RemoveAt(0);

            lstLog.TopIndex = lstLog.Items.Count - 1;
        }


        // 3. Hiển thị kết nối.
        // Gọi sau khi Connect/Disconnect hoàn tất.
        // Hàm này KHÔNG tự kết nối hoặc ngắt PLC.
        private void ShowConnection(bool connected)
        {
            lblConnection.Text =
                connected ? "Đã kết nối" : "Đã ngắt kết nối";

            lblConnection.BackColor =
                connected ? Color.LimeGreen : Color.LightGray;

            btnConnect.Enabled = !connected;
            btnDisconnect.Enabled = connected;
            txtIP.Enabled = !connected;

            if (!connected)
            {
                ShowDataUnavailable();

                hasPreviousStatus = false;
                displayHadValidData = false;
            }
        }


        // 4. Cho phép hoặc khóa nút chuyển động.
        private void SetMotionButtons(
            bool canJog,
            bool canHome,
            bool canMove)
        {
            btnJogUpAxis1.Enabled = canJog;
            btnJogDownAxis1.Enabled = canJog;

            btnJogUpAxis2.Enabled = canJog;
            btnJogDownAxis2.Enabled = canJog;

            btnJogUpAxis3.Enabled = canJog;
            btnJogDownAxis3.Enabled = canJog;

            btnHomeAxis.Enabled = canHome;

            btnExecuteAbsAxis.Enabled = canMove;
            btnExecuteTGXYZ.Enabled = canMove;

            // Không khóa Stop theo Busy/Homed.
            // Chức năng Stop phải được gắn đúng địa chỉ PLC.
        }


        // 5. Hiển thị vị trí hiện tại của ba trục.
        private void UpdatePosAxis(
            float positionAxis1,
            float positionAxis2,
            float positionAxis3)
        {
            lblCurrentPosAxis1.Text = positionAxis1.ToString("F2");
            lblCurrentPosAxis2.Text = positionAxis2.ToString("F2");
            lblCurrentPosAxis3.Text = positionAxis3.ToString("F2");
        }


        // 6. Hiển thị tọa độ XYZ hiện tại.
        private void UpdateCoordinates(
            float positionX,
            float positionY,
            float positionZ)
        {
            lblCurrentPositionX.Text = positionX.ToString("F2");
            lblCurrentPositionY.Text = positionY.ToString("F2");
            lblCurrentPositionZ.Text = positionZ.ToString("F2");
        }


        // 7. Đổi màu một Label trạng thái.
        private void SetStatusLabel(
            Label label,
            bool active,
            Color activeColor)
        {
            label.BackColor =
                active ? activeColor : Color.LightGray;

            label.ForeColor =
                active ? Color.White : Color.Black;
        }


        // 8. Hiển thị trạng thái của một trục.
        private void UpdateStatusAxis(
            int axis,
            bool homed,
            bool error,
            bool busy,
            bool inpos)
        {
            switch (axis)
            {
                case 1:
                    SetStatusLabel(lblHomedAxis1, homed, Color.Green);
                    SetStatusLabel(lblErrorAxis1, error, Color.Red);
                    SetStatusLabel(lblBusyAxis1, busy, Color.Green);
                    SetStatusLabel(lblInPosAxis1, inpos, Color.Green);
                    break;

                case 2:
                    SetStatusLabel(lblHomedAxis2, homed, Color.Green);
                    SetStatusLabel(lblErrorAxis2, error, Color.Red);
                    SetStatusLabel(lblBusyAxis2, busy, Color.Green);
                    SetStatusLabel(lblInPosAxis2, inpos, Color.Green);
                    break;

                case 3:
                    SetStatusLabel(lblHomedAxis3, homed, Color.Green);
                    SetStatusLabel(lblErrorAxis3, error, Color.Red);
                    SetStatusLabel(lblBusyAxis3, busy, Color.Green);
                    SetStatusLabel(lblInPosAxis3, inpos, Color.Green);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(axis));
            }
        }


        // 9. Hiển thị khi chưa có dữ liệu hoặc đọc lỗi.
        private void ShowDataUnavailable()
        {
            lblCurrentPosAxis1.Text = "--";
            lblCurrentPosAxis2.Text = "--";
            lblCurrentPosAxis3.Text = "--";

            lblCurrentPositionX.Text = "--";
            lblCurrentPositionY.Text = "--";
            lblCurrentPositionZ.Text = "--";

            UpdateStatusAxis(1, false, false, false, false);
            UpdateStatusAxis(2, false, false, false, false);
            UpdateStatusAxis(3, false, false, false, false);

            SetMotionButtons(false, false, false);
        }


        // 10. Cập nhật toàn bộ phần hiển thị sau một lượt đọc.
        private void UpdateDisplay()
        {
            if (!PlcMap.DataValid)
            {
                ShowDataUnavailable();

                lblConnection.Text = "Chưa có dữ liệu PLC";
                lblConnection.BackColor = Color.Orange;

                if (displayHadValidData)
                {
                    AddLog("Mất dữ liệu PLC: " + PlcMap.LastReadError);
                }

                displayHadValidData = false;

                // Sau gián đoạn, lấy lại mẫu đầu tiên làm mốc.
                hasPreviousStatus = false;
                return;
            }

            if (!displayHadValidData)
            {
                AddLog("Đã nhận dữ liệu PLC");
            }

            displayHadValidData = true;

            // Chỉ cập nhật nhãn tại đây.
            // Không tự thay trạng thái nút Connect/Disconnect trong mỗi Tick.
            lblConnection.Text = "Đã kết nối";
            lblConnection.BackColor = Color.LimeGreen;

            UpdatePosAxis(
                PlcMap.PositionAxis1,
                PlcMap.PositionAxis2,
                PlcMap.PositionAxis3);

            UpdateCoordinates(
                PlcMap.CurrentX,
                PlcMap.CurrentY,
                PlcMap.CurrentZ);

            // Dùng tên tham số để dễ nhìn biến nào đưa vào đâu.
            UpdateStatusAxis(
                axis: 1,
                homed: PlcMap.HomedAxis1,
                error: PlcMap.ErrorAxis1,
                busy: PlcMap.BusyAxis1,
                inpos: PlcMap.InPosAxis1);

            UpdateStatusAxis(
                axis: 2,
                homed: PlcMap.HomedAxis2,
                error: PlcMap.ErrorAxis2,
                busy: PlcMap.BusyAxis2,
                inpos: PlcMap.InPosAxis2);

            UpdateStatusAxis(
                axis: 3,
                homed: PlcMap.HomedAxis3,
                error: PlcMap.ErrorAxis3,
                busy: PlcMap.BusyAxis3,
                inpos: PlcMap.InPosAxis3);

            UpdateStatusLogs();

            // Chưa tự mở nút chuyển động ở đây:
            // phần điều khiển sẽ gọi SetMotionButtons(...)
            // khi đã kiểm tra đủ điều kiện chạy.
        }


        // 11. Ghi log khi trạng thái thay đổi.
        private void LogStatusChange(
            bool current,
            ref bool previous,
            string onMessage,
            string offMessage)
        {
            if (hasPreviousStatus && current != previous)
            {
                AddLog(current ? onMessage : offMessage);
            }

            previous = current;
        }


        // 12. Theo dõi thay đổi của ba trục.
        private void UpdateStatusLogs()
        {
            // Báo lỗi đang có ngay ở mẫu đầu,
            // nhưng không gọi đó là lỗi "vừa phát sinh".
            if (!hasPreviousStatus)
            {
                if (PlcMap.ErrorAxis1)
                    AddLog("Trục 1 đang có lỗi");

                if (PlcMap.ErrorAxis2)
                    AddLog("Trục 2 đang có lỗi");

                if (PlcMap.ErrorAxis3)
                    AddLog("Trục 3 đang có lỗi");
            }

            LogStatusChange(
                PlcMap.HomedAxis1, ref lastHomedAxis1,
                "Trục 1: Homed bật", "Trục 1: Homed tắt");

            LogStatusChange(
                PlcMap.HomedAxis2, ref lastHomedAxis2,
                "Trục 2: Homed bật", "Trục 2: Homed tắt");

            LogStatusChange(
                PlcMap.HomedAxis3, ref lastHomedAxis3,
                "Trục 3: Homed bật", "Trục 3: Homed tắt");

            LogStatusChange(
                PlcMap.BusyAxis1, ref lastBusyAxis1,
                "Trục 1: Busy bật", "Trục 1: Busy tắt");

            LogStatusChange(
                PlcMap.BusyAxis2, ref lastBusyAxis2,
                "Trục 2: Busy bật", "Trục 2: Busy tắt");

            LogStatusChange(
                PlcMap.BusyAxis3, ref lastBusyAxis3,
                "Trục 3: Busy bật", "Trục 3: Busy tắt");

            LogStatusChange(
                PlcMap.ErrorAxis1, ref lastErrorAxis1,
                "Trục 1: Error bật", "Trục 1: Error tắt");

            LogStatusChange(
                PlcMap.ErrorAxis2, ref lastErrorAxis2,
                "Trục 2: Error bật", "Trục 2: Error tắt");

            LogStatusChange(
                PlcMap.ErrorAxis3, ref lastErrorAxis3,
                "Trục 3: Error bật", "Trục 3: Error tắt");

            LogStatusChange(
                PlcMap.InPosAxis1, ref lastInPosAxis1,
                "Trục 1: InPosition bật", "Trục 1: InPosition tắt");

            LogStatusChange(
                PlcMap.InPosAxis2, ref lastInPosAxis2,
                "Trục 2: InPosition bật", "Trục 2: InPosition tắt");

            LogStatusChange(
                PlcMap.InPosAxis3, ref lastInPosAxis3,
                "Trục 3: InPosition bật", "Trục 3: InPosition tắt");

            hasPreviousStatus = true;
        }
    }
}