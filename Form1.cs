using S7.Net;
using System;
using System.Windows.Forms;

namespace LEARN_WF
{
    public partial class Form1 : Form
    {
        private Plc plc;

        private readonly System.Windows.Forms.Timer timerRead =
            new System.Windows.Forms.Timer();

        private bool changingConnection;
        private bool shuttingDown;
        private bool allowClose;

        public Form1()
        {
            InitializeComponent();
            InitializeDisplay();

            // Gắn nút Connect.
            btnConnect.Click -= btnConnect_Click;
            btnConnect.Click += btnConnect_Click;

            // Gắn nút Disconnect.
            btnDisconnect.Click -= btnDisconnect_Click;
            btnDisconnect.Click += btnDisconnect_Click;

            // Đọc PLC định kỳ.
            timerRead.Interval = 250;
            timerRead.Tick += timerRead_Tick;

            // Xử lý đóng cửa sổ.
            FormClosing -= Form1_FormClosing;
            FormClosing += Form1_FormClosing;

            // Homming
            btnHomeAxis.Click -= btnHomeAxis_Click;

            // Excute TGXYZ
            btnExecuteTGXYZ.Click -= btnExecuteTGXYZ_Click;

            // Stop axis
            btnStopAxis.Click -= btnStopAxis_Click;

            // Excute ABS axis
            btnExecuteAbsAxis.Click -= btnExecuteAbsAxis_Click;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void lblHomedAxis1_Click(object sender, EventArgs e)
        {

        }
    }
}