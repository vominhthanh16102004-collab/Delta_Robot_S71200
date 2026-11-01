using S7.Net;
using System;
using System.Windows.Forms;

namespace LEARN_WF
{
    public partial class Form1
    {
        private async void timerRead_Tick(object sender, EventArgs e)
        {
            if (changingConnection || shuttingDown || plc == null)
                return;

            bool readok = await PlcMap.ReadDataAsync(plc);

            if (changingConnection || shuttingDown || !timerRead.Enabled)
                return;

            if (readok || !PlcMap.DataValid)
                UpdateDisplay();
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            if (changingConnection || shuttingDown)
                return;

            changingConnection = true;

            try
            {
                timerRead.Stop();

                btnConnect.Enabled = false;
                btnDisconnect.Enabled = false;
                txtIP.Enabled = false;

                SetMotionButtons(false, false, false);
                ShowDataUnavailable();

                lblConnection.Text = "Đang kết nối...";
                lblConnection.BackColor = System.Drawing.Color.Khaki;

                if (plc == null)
                {
                    plc = new Plc(
                        CpuType.S71200,
                        txtIP.Text.Trim(),
                        0,
                        1);
                }

                bool connectok = await PlcMap.ConnectAsync(plc);

                if (connectok)
                {
                    ShowConnection(true);
                    AddLog("Đã kết nối PLC.");

                    timerRead.Start();
                }
                else
                {
                    // Ghi lỗi trước vì Disconnect có thể đổi lỗi này.
                    AddLog("Kết nối thất bại: "
                        + PlcMap.LastConnectionError);

                    bool closed = await PlcMap.DisconnectAsync(plc);

                    if (closed)
                    {
                        plc = null;

                        lblConnection.Text = "Kết nối thất bại";
                        lblConnection.BackColor =
                            System.Drawing.Color.OrangeRed;

                        btnConnect.Enabled = true;
                        btnDisconnect.Enabled = false;
                        txtIP.Enabled = true;
                    }
                    else
                    {
                        lblConnection.Text = "Lỗi, hãy Disconnect";
                        lblConnection.BackColor =
                            System.Drawing.Color.OrangeRed;

                        btnConnect.Enabled = false;
                        btnDisconnect.Enabled = true;
                        txtIP.Enabled = false;

                        AddLog("Dọn kết nối thất bại: "
                            + PlcMap.LastConnectionError);
                    }
                }
            }
            catch (Exception ex)
            {
                timerRead.Stop();

                SetMotionButtons(false, false, false);
                ShowDataUnavailable();

                lblConnection.BackColor =
                    System.Drawing.Color.OrangeRed;

                if (plc == null)
                {
                    lblConnection.Text = "Lỗi kết nối";

                    btnConnect.Enabled = true;
                    btnDisconnect.Enabled = false;
                    txtIP.Enabled = true;
                }
                else
                {
                    // Giữ đối tượng để thử Disconnect dọn kết nối.
                    lblConnection.Text = "Lỗi, hãy Disconnect";

                    btnConnect.Enabled = false;
                    btnDisconnect.Enabled = true;
                    txtIP.Enabled = false;
                }

                AddLog("Lỗi: " + ex.Message);
            }
            finally
            {
                changingConnection = false;
            }
        }

        private async void btnDisconnect_Click(
            object sender, EventArgs e)
        {
            if (changingConnection || shuttingDown)
                return;

            changingConnection = true;

            try
            {
                timerRead.Stop();

                btnConnect.Enabled = false;
                btnDisconnect.Enabled = false;
                txtIP.Enabled = false;

                SetMotionButtons(false, false, false);
                ShowDataUnavailable();

                lblConnection.Text = "Đang ngắt kết nối...";
                lblConnection.BackColor =
                    System.Drawing.Color.Khaki;

                bool disconnectok =
                    await PlcMap.DisconnectAsync(plc);

                if (disconnectok)
                {
                    plc = null;

                    ShowConnection(false);
                    AddLog("Đã ngắt kết nối PLC.");
                }
                else
                {
                    lblConnection.Text = "Lỗi, hãy Disconnect lại";
                    lblConnection.BackColor =
                        System.Drawing.Color.OrangeRed;

                    btnConnect.Enabled = false;
                    btnDisconnect.Enabled = true;
                    txtIP.Enabled = false;

                    AddLog("Ngắt kết nối thất bại: "
                        + PlcMap.LastConnectionError);
                }
            }
            catch (Exception ex)
            {
                lblConnection.Text = "Lỗi, hãy Disconnect lại";
                lblConnection.BackColor =
                    System.Drawing.Color.OrangeRed;

                btnConnect.Enabled = false;
                btnDisconnect.Enabled = true;
                txtIP.Enabled = false;

                AddLog("Lỗi: " + ex.Message);
            }
            finally
            {
                changingConnection = false;
            }
        }

        private async void Form1_FormClosing(
            object sender,
            FormClosingEventArgs e)
        {
            // Đã dọn xong: cho phép đóng cửa sổ.
            if (allowClose)
                return;

            // Giữ cửa sổ mở trong lúc chờ ngắt kết nối.
            e.Cancel = true;

            if (shuttingDown)
                return;

            if (changingConnection)
            {
                AddLog(
                    "Đang xử lý kết nối. Hãy đóng lại khi hoàn tất.");
                return;
            }

            shuttingDown = true;

            try
            {
                timerRead.Stop();

                btnConnect.Enabled = false;
                btnDisconnect.Enabled = false;
                txtIP.Enabled = false;

                SetMotionButtons(false, false, false);
                ShowDataUnavailable();

                lblConnection.Text = "Đang đóng kết nối...";
                lblConnection.BackColor =
                    System.Drawing.Color.Khaki;

                bool closed = await PlcMap.DisconnectAsync(plc);

                if (closed)
                {
                    plc = null;

                    timerRead.Dispose();

                    allowClose = true;

                    // Lần đóng này được phép vì allowClose = true.
                    Close();
                }
                else
                {
                    lblConnection.Text = "Lỗi, hãy Disconnect lại";
                    lblConnection.BackColor =
                        System.Drawing.Color.OrangeRed;

                    btnDisconnect.Enabled = true;

                    AddLog("Chưa đóng được kết nối: "
                        + PlcMap.LastConnectionError);
                }
            }
            catch (Exception ex)
            {
                lblConnection.Text = "Lỗi khi đóng chương trình";
                lblConnection.BackColor =
                    System.Drawing.Color.OrangeRed;

                btnDisconnect.Enabled = true;

                AddLog("Lỗi: " + ex.Message);
            }
            finally
            {
                if (!allowClose)
                    shuttingDown = false;
            }
        }
    }
}