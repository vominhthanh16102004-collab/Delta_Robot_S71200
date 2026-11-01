namespace LEARN_WF
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            txtIP = new TextBox();
            btnConnect = new Button();
            btnDisconnect = new Button();
            label2 = new Label();
            btnJogDownAxis1 = new Button();
            btnJogUpAxis1 = new Button();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            btnJogDownAxis2 = new Button();
            btnJogUpAxis2 = new Button();
            btnJogDownAxis3 = new Button();
            btnJogUpAxis3 = new Button();
            numSpeedJogAxis1 = new NumericUpDown();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            numSpeedJogAxis2 = new NumericUpDown();
            label9 = new Label();
            numSpeedJogAxis3 = new NumericUpDown();
            label10 = new Label();
            btnHomeAxis = new Button();
            label11 = new Label();
            label12 = new Label();
            label13 = new Label();
            label14 = new Label();
            label15 = new Label();
            numTGPAxis1 = new NumericUpDown();
            numTGPAxis2 = new NumericUpDown();
            numTGPAxis3 = new NumericUpDown();
            label16 = new Label();
            label17 = new Label();
            label18 = new Label();
            label19 = new Label();
            numSpeedAbsAxis3 = new NumericUpDown();
            label20 = new Label();
            numSpeedAbsAxis2 = new NumericUpDown();
            label21 = new Label();
            label22 = new Label();
            label23 = new Label();
            btnExecuteTGXYZ = new Button();
            label28 = new Label();
            label29 = new Label();
            label30 = new Label();
            numTargetZ = new NumericUpDown();
            numTargetY = new NumericUpDown();
            numTargetX = new NumericUpDown();
            label31 = new Label();
            label32 = new Label();
            label33 = new Label();
            label34 = new Label();
            label24 = new Label();
            numSpeedZ = new NumericUpDown();
            label25 = new Label();
            numSpeedY = new NumericUpDown();
            label26 = new Label();
            label27 = new Label();
            numSpeedX = new NumericUpDown();
            label35 = new Label();
            label36 = new Label();
            label37 = new Label();
            lblCurrentPosAxis1 = new Label();
            label39 = new Label();
            label38 = new Label();
            lblCurrentPosAxis2 = new Label();
            label41 = new Label();
            lblCurrentPosAxis3 = new Label();
            label43 = new Label();
            lblHomedAxis1 = new Label();
            lblHomedAxis2 = new Label();
            lblHomedAxis3 = new Label();
            lblBusyAxis1 = new Label();
            lblBusyAxis2 = new Label();
            lblBusyAxis3 = new Label();
            lblInPosAxis1 = new Label();
            lblInPosAxis3 = new Label();
            lblInPosAxis2 = new Label();
            label49 = new Label();
            lblCurrentPositionZ = new Label();
            label51 = new Label();
            lblCurrentPositionY = new Label();
            label53 = new Label();
            lblCurrentPositionX = new Label();
            label55 = new Label();
            lblConnection = new Label();
            lblErrorAxis1 = new Label();
            lblErrorAxis2 = new Label();
            lblErrorAxis3 = new Label();
            btnStopAxis = new Button();
            label40 = new Label();
            label42 = new Label();
            btnExecuteAbsAxis = new Button();
            label44 = new Label();
            lstLog = new ListBox();
            label45 = new Label();
            numSpeedAbsAxis1 = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)numSpeedJogAxis1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSpeedJogAxis2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSpeedJogAxis3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numTGPAxis1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numTGPAxis2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numTGPAxis3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSpeedAbsAxis3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSpeedAbsAxis2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numTargetZ).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numTargetY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numTargetX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSpeedZ).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSpeedY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSpeedX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSpeedAbsAxis1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(40, 27);
            label1.Name = "label1";
            label1.Size = new Size(48, 20);
            label1.TabIndex = 0;
            label1.Text = "IP PLC";
            // 
            // txtIP
            // 
            txtIP.Location = new Point(94, 24);
            txtIP.Name = "txtIP";
            txtIP.Size = new Size(125, 27);
            txtIP.TabIndex = 1;
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(404, 23);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(94, 29);
            btnConnect.TabIndex = 2;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // btnDisconnect
            // 
            btnDisconnect.Location = new Point(520, 22);
            btnDisconnect.Name = "btnDisconnect";
            btnDisconnect.Size = new Size(94, 29);
            btnDisconnect.TabIndex = 3;
            btnDisconnect.Text = "Disconnect";
            btnDisconnect.UseVisualStyleBackColor = true;
            btnDisconnect.Click += btnDisconnect_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(40, 66);
            label2.Name = "label2";
            label2.Size = new Size(114, 20);
            label2.TabIndex = 6;
            label2.Text = "Manual Control:";
            // 
            // btnJogDownAxis1
            // 
            btnJogDownAxis1.Location = new Point(231, 102);
            btnJogDownAxis1.Name = "btnJogDownAxis1";
            btnJogDownAxis1.Size = new Size(94, 29);
            btnJogDownAxis1.TabIndex = 5;
            btnJogDownAxis1.Text = "Jog-";
            btnJogDownAxis1.UseVisualStyleBackColor = true;
            btnJogDownAxis1.MouseDown += btnJogDownAxis1_MouseDown;
            btnJogDownAxis1.MouseUp += btnJogDownAxis1_MouseUp;
            // 
            // btnJogUpAxis1
            // 
            btnJogUpAxis1.Location = new Point(114, 102);
            btnJogUpAxis1.Name = "btnJogUpAxis1";
            btnJogUpAxis1.Size = new Size(94, 29);
            btnJogUpAxis1.TabIndex = 4;
            btnJogUpAxis1.Text = "Jog+";
            btnJogUpAxis1.UseVisualStyleBackColor = true;
            btnJogUpAxis1.MouseDown += btnJogUpAxis1_MouseDown;
            btnJogUpAxis1.MouseUp += btnJogUpAxis1_MouseUp;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(40, 106);
            label3.Name = "label3";
            label3.Size = new Size(51, 20);
            label3.TabIndex = 7;
            label3.Text = "Axis 1:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(40, 154);
            label4.Name = "label4";
            label4.Size = new Size(51, 20);
            label4.TabIndex = 10;
            label4.Text = "Axis 2:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(40, 202);
            label5.Name = "label5";
            label5.Size = new Size(51, 20);
            label5.TabIndex = 13;
            label5.Text = "Axis 3:";
            // 
            // btnJogDownAxis2
            // 
            btnJogDownAxis2.Location = new Point(231, 150);
            btnJogDownAxis2.Name = "btnJogDownAxis2";
            btnJogDownAxis2.Size = new Size(94, 29);
            btnJogDownAxis2.TabIndex = 15;
            btnJogDownAxis2.Text = "Jog-";
            btnJogDownAxis2.UseVisualStyleBackColor = true;
            btnJogDownAxis2.MouseDown += btnJogDownAxis2_MouseDown;
            btnJogDownAxis2.MouseUp += btnJogDownAxis2_MouseUp;
            // 
            // btnJogUpAxis2
            // 
            btnJogUpAxis2.Location = new Point(114, 150);
            btnJogUpAxis2.Name = "btnJogUpAxis2";
            btnJogUpAxis2.Size = new Size(94, 29);
            btnJogUpAxis2.TabIndex = 14;
            btnJogUpAxis2.Text = "Jog+";
            btnJogUpAxis2.UseVisualStyleBackColor = true;
            btnJogUpAxis2.MouseDown += btnJogUpAxis2_MouseDown;
            btnJogUpAxis2.MouseUp += btnJogUpAxis2_MouseUp;
            // 
            // btnJogDownAxis3
            // 
            btnJogDownAxis3.Location = new Point(231, 198);
            btnJogDownAxis3.Name = "btnJogDownAxis3";
            btnJogDownAxis3.Size = new Size(94, 29);
            btnJogDownAxis3.TabIndex = 17;
            btnJogDownAxis3.Text = "Jog-";
            btnJogDownAxis3.UseVisualStyleBackColor = true;
            btnJogDownAxis3.MouseDown += btnJogDownAxis3_MouseDown;
            btnJogDownAxis3.MouseUp += btnJogDownAxis3_MouseUp;
            // 
            // btnJogUpAxis3
            // 
            btnJogUpAxis3.Location = new Point(114, 198);
            btnJogUpAxis3.Name = "btnJogUpAxis3";
            btnJogUpAxis3.Size = new Size(94, 29);
            btnJogUpAxis3.TabIndex = 16;
            btnJogUpAxis3.Text = "Jog+";
            btnJogUpAxis3.UseVisualStyleBackColor = true;
            btnJogUpAxis3.MouseDown += btnJogUpAxis3_MouseDown;
            btnJogUpAxis3.MouseUp += btnJogUpAxis3_MouseUp;
            // 
            // numSpeedJogAxis1
            // 
            numSpeedJogAxis1.DecimalPlaces = 2;
            numSpeedJogAxis1.Location = new Point(355, 103);
            numSpeedJogAxis1.Name = "numSpeedJogAxis1";
            numSpeedJogAxis1.Size = new Size(90, 27);
            numSpeedJogAxis1.TabIndex = 18;
            numSpeedJogAxis1.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(451, 106);
            label6.Name = "label6";
            label6.Size = new Size(47, 20);
            label6.TabIndex = 19;
            label6.Text = "mm/s";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(355, 66);
            label7.Name = "label7";
            label7.Size = new Size(81, 20);
            label7.TabIndex = 20;
            label7.Text = "Jog Speed:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(451, 154);
            label8.Name = "label8";
            label8.Size = new Size(47, 20);
            label8.TabIndex = 22;
            label8.Text = "mm/s";
            // 
            // numSpeedJogAxis2
            // 
            numSpeedJogAxis2.DecimalPlaces = 2;
            numSpeedJogAxis2.Location = new Point(355, 151);
            numSpeedJogAxis2.Name = "numSpeedJogAxis2";
            numSpeedJogAxis2.Size = new Size(90, 27);
            numSpeedJogAxis2.TabIndex = 21;
            numSpeedJogAxis2.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(451, 202);
            label9.Name = "label9";
            label9.Size = new Size(47, 20);
            label9.TabIndex = 24;
            label9.Text = "mm/s";
            // 
            // numSpeedJogAxis3
            // 
            numSpeedJogAxis3.DecimalPlaces = 2;
            numSpeedJogAxis3.Location = new Point(355, 199);
            numSpeedJogAxis3.Name = "numSpeedJogAxis3";
            numSpeedJogAxis3.Size = new Size(90, 27);
            numSpeedJogAxis3.TabIndex = 23;
            numSpeedJogAxis3.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(530, 66);
            label10.Name = "label10";
            label10.Size = new Size(84, 20);
            label10.TabIndex = 25;
            label10.Text = "Home Axis:";
            // 
            // btnHomeAxis
            // 
            btnHomeAxis.Location = new Point(525, 102);
            btnHomeAxis.Name = "btnHomeAxis";
            btnHomeAxis.Size = new Size(94, 29);
            btnHomeAxis.TabIndex = 26;
            btnHomeAxis.Text = "Home";
            btnHomeAxis.UseVisualStyleBackColor = true;
            btnHomeAxis.Click += btnHomeAxis_Click;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(40, 242);
            label11.Name = "label11";
            label11.Size = new Size(80, 20);
            label11.TabIndex = 29;
            label11.Text = "Move ABS:";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(40, 411);
            label12.Name = "label12";
            label12.Size = new Size(51, 20);
            label12.TabIndex = 32;
            label12.Text = "Axis 3:";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(40, 363);
            label13.Name = "label13";
            label13.Size = new Size(51, 20);
            label13.TabIndex = 31;
            label13.Text = "Axis 2:";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(40, 315);
            label14.Name = "label14";
            label14.Size = new Size(51, 20);
            label14.TabIndex = 30;
            label14.Text = "Axis 1:";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(94, 273);
            label15.Name = "label15";
            label15.Size = new Size(140, 20);
            label15.TabIndex = 33;
            label15.Text = "ABS Target Position:";
            // 
            // numTGPAxis1
            // 
            numTGPAxis1.DecimalPlaces = 2;
            numTGPAxis1.Location = new Point(114, 312);
            numTGPAxis1.Maximum = new decimal(new int[] { 27, 0, 0, 0 });
            numTGPAxis1.Name = "numTGPAxis1";
            numTGPAxis1.Size = new Size(90, 27);
            numTGPAxis1.TabIndex = 34;
            // 
            // numTGPAxis2
            // 
            numTGPAxis2.DecimalPlaces = 2;
            numTGPAxis2.Location = new Point(114, 360);
            numTGPAxis2.Maximum = new decimal(new int[] { 27, 0, 0, 0 });
            numTGPAxis2.Name = "numTGPAxis2";
            numTGPAxis2.Size = new Size(90, 27);
            numTGPAxis2.TabIndex = 35;
            // 
            // numTGPAxis3
            // 
            numTGPAxis3.DecimalPlaces = 2;
            numTGPAxis3.Location = new Point(114, 408);
            numTGPAxis3.Maximum = new decimal(new int[] { 27, 0, 0, 0 });
            numTGPAxis3.Name = "numTGPAxis3";
            numTGPAxis3.Size = new Size(90, 27);
            numTGPAxis3.TabIndex = 36;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(217, 315);
            label16.Name = "label16";
            label16.Size = new Size(35, 20);
            label16.TabIndex = 37;
            label16.Text = "mm";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(217, 363);
            label17.Name = "label17";
            label17.Size = new Size(35, 20);
            label17.TabIndex = 38;
            label17.Text = "mm";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(217, 411);
            label18.Name = "label18";
            label18.Size = new Size(35, 20);
            label18.TabIndex = 39;
            label18.Text = "mm";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(389, 411);
            label19.Name = "label19";
            label19.Size = new Size(47, 20);
            label19.TabIndex = 49;
            label19.Text = "mm/s";
            // 
            // numSpeedAbsAxis3
            // 
            numSpeedAbsAxis3.DecimalPlaces = 2;
            numSpeedAbsAxis3.Location = new Point(293, 408);
            numSpeedAbsAxis3.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            numSpeedAbsAxis3.Name = "numSpeedAbsAxis3";
            numSpeedAbsAxis3.Size = new Size(90, 27);
            numSpeedAbsAxis3.TabIndex = 48;
            numSpeedAbsAxis3.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(389, 363);
            label20.Name = "label20";
            label20.Size = new Size(47, 20);
            label20.TabIndex = 47;
            label20.Text = "mm/s";
            // 
            // numSpeedAbsAxis2
            // 
            numSpeedAbsAxis2.DecimalPlaces = 2;
            numSpeedAbsAxis2.Location = new Point(293, 360);
            numSpeedAbsAxis2.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            numSpeedAbsAxis2.Name = "numSpeedAbsAxis2";
            numSpeedAbsAxis2.Size = new Size(90, 27);
            numSpeedAbsAxis2.TabIndex = 46;
            numSpeedAbsAxis2.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new Point(293, 273);
            label21.Name = "label21";
            label21.Size = new Size(85, 20);
            label21.TabIndex = 45;
            label21.Text = "ABS Speed:";
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new Point(389, 315);
            label22.Name = "label22";
            label22.Size = new Size(47, 20);
            label22.TabIndex = 44;
            label22.Text = "mm/s";
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Location = new Point(40, 460);
            label23.Name = "label23";
            label23.Size = new Size(91, 20);
            label23.TabIndex = 50;
            label23.Text = "XYZ Control:";
            // 
            // btnExecuteTGXYZ
            // 
            btnExecuteTGXYZ.Location = new Point(469, 529);
            btnExecuteTGXYZ.Name = "btnExecuteTGXYZ";
            btnExecuteTGXYZ.Size = new Size(94, 29);
            btnExecuteTGXYZ.TabIndex = 61;
            btnExecuteTGXYZ.Text = "Execute";
            btnExecuteTGXYZ.UseVisualStyleBackColor = true;
            btnExecuteTGXYZ.Click += btnExecuteTGXYZ_Click;
            // 
            // label28
            // 
            label28.AutoSize = true;
            label28.Location = new Point(217, 629);
            label28.Name = "label28";
            label28.Size = new Size(35, 20);
            label28.TabIndex = 60;
            label28.Text = "mm";
            // 
            // label29
            // 
            label29.AutoSize = true;
            label29.Location = new Point(217, 581);
            label29.Name = "label29";
            label29.Size = new Size(35, 20);
            label29.TabIndex = 59;
            label29.Text = "mm";
            // 
            // label30
            // 
            label30.AutoSize = true;
            label30.Location = new Point(217, 533);
            label30.Name = "label30";
            label30.Size = new Size(35, 20);
            label30.TabIndex = 58;
            label30.Text = "mm";
            // 
            // numTargetZ
            // 
            numTargetZ.DecimalPlaces = 2;
            numTargetZ.Location = new Point(114, 626);
            numTargetZ.Maximum = new decimal(new int[] { 27, 0, 0, 0 });
            numTargetZ.Name = "numTargetZ";
            numTargetZ.Size = new Size(90, 27);
            numTargetZ.TabIndex = 57;
            // 
            // numTargetY
            // 
            numTargetY.DecimalPlaces = 2;
            numTargetY.Location = new Point(114, 578);
            numTargetY.Maximum = new decimal(new int[] { 150, 0, 0, 0 });
            numTargetY.Name = "numTargetY";
            numTargetY.Size = new Size(90, 27);
            numTargetY.TabIndex = 56;
            // 
            // numTargetX
            // 
            numTargetX.DecimalPlaces = 2;
            numTargetX.Location = new Point(114, 530);
            numTargetX.Maximum = new decimal(new int[] { 150, 0, 0, 0 });
            numTargetX.Name = "numTargetX";
            numTargetX.Size = new Size(90, 27);
            numTargetX.TabIndex = 55;
            // 
            // label31
            // 
            label31.AutoSize = true;
            label31.Location = new Point(94, 491);
            label31.Name = "label31";
            label31.Size = new Size(109, 20);
            label31.TabIndex = 54;
            label31.Text = "Target Position:";
            // 
            // label32
            // 
            label32.AutoSize = true;
            label32.Location = new Point(40, 629);
            label32.Name = "label32";
            label32.Size = new Size(21, 20);
            label32.TabIndex = 53;
            label32.Text = "Z:";
            // 
            // label33
            // 
            label33.AutoSize = true;
            label33.Location = new Point(40, 581);
            label33.Name = "label33";
            label33.Size = new Size(20, 20);
            label33.TabIndex = 52;
            label33.Text = "Y:";
            // 
            // label34
            // 
            label34.AutoSize = true;
            label34.Location = new Point(40, 533);
            label34.Name = "label34";
            label34.Size = new Size(21, 20);
            label34.TabIndex = 51;
            label34.Text = "X:";
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Location = new Point(389, 629);
            label24.Name = "label24";
            label24.Size = new Size(47, 20);
            label24.TabIndex = 70;
            label24.Text = "mm/s";
            // 
            // numSpeedZ
            // 
            numSpeedZ.DecimalPlaces = 2;
            numSpeedZ.Location = new Point(293, 626);
            numSpeedZ.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            numSpeedZ.Name = "numSpeedZ";
            numSpeedZ.Size = new Size(90, 27);
            numSpeedZ.TabIndex = 69;
            numSpeedZ.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Location = new Point(389, 581);
            label25.Name = "label25";
            label25.Size = new Size(47, 20);
            label25.TabIndex = 68;
            label25.Text = "mm/s";
            // 
            // numSpeedY
            // 
            numSpeedY.DecimalPlaces = 2;
            numSpeedY.Location = new Point(293, 578);
            numSpeedY.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            numSpeedY.Name = "numSpeedY";
            numSpeedY.Size = new Size(90, 27);
            numSpeedY.TabIndex = 67;
            numSpeedY.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.Location = new Point(293, 491);
            label26.Name = "label26";
            label26.Size = new Size(54, 20);
            label26.TabIndex = 66;
            label26.Text = "Speed:";
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.Location = new Point(389, 533);
            label27.Name = "label27";
            label27.Size = new Size(47, 20);
            label27.TabIndex = 65;
            label27.Text = "mm/s";
            // 
            // numSpeedX
            // 
            numSpeedX.DecimalPlaces = 2;
            numSpeedX.Location = new Point(293, 530);
            numSpeedX.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            numSpeedX.Name = "numSpeedX";
            numSpeedX.Size = new Size(90, 27);
            numSpeedX.TabIndex = 64;
            numSpeedX.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // label35
            // 
            label35.AutoSize = true;
            label35.Location = new Point(663, 26);
            label35.Name = "label35";
            label35.Size = new Size(72, 20);
            label35.TabIndex = 71;
            label35.Text = "Axis Data";
            // 
            // label36
            // 
            label36.AutoSize = true;
            label36.Location = new Point(663, 65);
            label36.Name = "label36";
            label36.Size = new Size(147, 20);
            label36.TabIndex = 72;
            label36.Text = "Current Axis Position:";
            // 
            // label37
            // 
            label37.AutoSize = true;
            label37.Location = new Point(686, 141);
            label37.Name = "label37";
            label37.Size = new Size(0, 20);
            label37.TabIndex = 73;
            // 
            // lblCurrentPosAxis1
            // 
            lblCurrentPosAxis1.AutoSize = true;
            lblCurrentPosAxis1.BorderStyle = BorderStyle.Fixed3D;
            lblCurrentPosAxis1.Location = new Point(693, 104);
            lblCurrentPosAxis1.Name = "lblCurrentPosAxis1";
            lblCurrentPosAxis1.Size = new Size(65, 22);
            lblCurrentPosAxis1.TabIndex = 74;
            lblCurrentPosAxis1.Text = "---------";
            // 
            // label39
            // 
            label39.AutoSize = true;
            label39.Location = new Point(776, 105);
            label39.Name = "label39";
            label39.Size = new Size(35, 20);
            label39.TabIndex = 75;
            label39.Text = "mm";
            // 
            // label38
            // 
            label38.AutoSize = true;
            label38.Location = new Point(776, 153);
            label38.Name = "label38";
            label38.Size = new Size(35, 20);
            label38.TabIndex = 77;
            label38.Text = "mm";
            // 
            // lblCurrentPosAxis2
            // 
            lblCurrentPosAxis2.AutoSize = true;
            lblCurrentPosAxis2.BorderStyle = BorderStyle.Fixed3D;
            lblCurrentPosAxis2.Location = new Point(693, 152);
            lblCurrentPosAxis2.Name = "lblCurrentPosAxis2";
            lblCurrentPosAxis2.Size = new Size(65, 22);
            lblCurrentPosAxis2.TabIndex = 76;
            lblCurrentPosAxis2.Text = "---------";
            // 
            // label41
            // 
            label41.AutoSize = true;
            label41.Location = new Point(776, 201);
            label41.Name = "label41";
            label41.Size = new Size(35, 20);
            label41.TabIndex = 79;
            label41.Text = "mm";
            // 
            // lblCurrentPosAxis3
            // 
            lblCurrentPosAxis3.AutoSize = true;
            lblCurrentPosAxis3.BorderStyle = BorderStyle.Fixed3D;
            lblCurrentPosAxis3.Location = new Point(693, 200);
            lblCurrentPosAxis3.Name = "lblCurrentPosAxis3";
            lblCurrentPosAxis3.Size = new Size(65, 22);
            lblCurrentPosAxis3.TabIndex = 78;
            lblCurrentPosAxis3.Text = "---------";
            // 
            // label43
            // 
            label43.AutoSize = true;
            label43.Location = new Point(663, 273);
            label43.Name = "label43";
            label43.Size = new Size(80, 20);
            label43.TabIndex = 80;
            label43.Text = "Axis Status";
            // 
            // lblHomedAxis1
            // 
            lblHomedAxis1.AutoSize = true;
            lblHomedAxis1.BorderStyle = BorderStyle.Fixed3D;
            lblHomedAxis1.Location = new Point(693, 314);
            lblHomedAxis1.Name = "lblHomedAxis1";
            lblHomedAxis1.Size = new Size(61, 22);
            lblHomedAxis1.TabIndex = 81;
            lblHomedAxis1.Text = "Homed";
            lblHomedAxis1.Click += lblHomedAxis1_Click;
            // 
            // lblHomedAxis2
            // 
            lblHomedAxis2.AutoSize = true;
            lblHomedAxis2.BorderStyle = BorderStyle.Fixed3D;
            lblHomedAxis2.Location = new Point(693, 362);
            lblHomedAxis2.Name = "lblHomedAxis2";
            lblHomedAxis2.Size = new Size(61, 22);
            lblHomedAxis2.TabIndex = 82;
            lblHomedAxis2.Text = "Homed";
            // 
            // lblHomedAxis3
            // 
            lblHomedAxis3.AutoSize = true;
            lblHomedAxis3.BorderStyle = BorderStyle.Fixed3D;
            lblHomedAxis3.Location = new Point(693, 410);
            lblHomedAxis3.Name = "lblHomedAxis3";
            lblHomedAxis3.Size = new Size(61, 22);
            lblHomedAxis3.TabIndex = 83;
            lblHomedAxis3.Text = "Homed";
            // 
            // lblBusyAxis1
            // 
            lblBusyAxis1.BorderStyle = BorderStyle.Fixed3D;
            lblBusyAxis1.Location = new Point(791, 314);
            lblBusyAxis1.Name = "lblBusyAxis1";
            lblBusyAxis1.Size = new Size(61, 22);
            lblBusyAxis1.TabIndex = 84;
            lblBusyAxis1.Text = "Busy";
            // 
            // lblBusyAxis2
            // 
            lblBusyAxis2.BorderStyle = BorderStyle.Fixed3D;
            lblBusyAxis2.Location = new Point(791, 362);
            lblBusyAxis2.Name = "lblBusyAxis2";
            lblBusyAxis2.Size = new Size(61, 22);
            lblBusyAxis2.TabIndex = 85;
            lblBusyAxis2.Text = "Busy";
            // 
            // lblBusyAxis3
            // 
            lblBusyAxis3.BorderStyle = BorderStyle.Fixed3D;
            lblBusyAxis3.Location = new Point(791, 410);
            lblBusyAxis3.Name = "lblBusyAxis3";
            lblBusyAxis3.Size = new Size(61, 22);
            lblBusyAxis3.TabIndex = 86;
            lblBusyAxis3.Text = "Busy";
            // 
            // lblInPosAxis1
            // 
            lblInPosAxis1.BorderStyle = BorderStyle.Fixed3D;
            lblInPosAxis1.Location = new Point(889, 314);
            lblInPosAxis1.Name = "lblInPosAxis1";
            lblInPosAxis1.Size = new Size(61, 22);
            lblInPosAxis1.TabIndex = 87;
            lblInPosAxis1.Text = "InPos";
            // 
            // lblInPosAxis3
            // 
            lblInPosAxis3.BorderStyle = BorderStyle.Fixed3D;
            lblInPosAxis3.Location = new Point(889, 410);
            lblInPosAxis3.Name = "lblInPosAxis3";
            lblInPosAxis3.Size = new Size(61, 22);
            lblInPosAxis3.TabIndex = 88;
            lblInPosAxis3.Text = "InPos";
            // 
            // lblInPosAxis2
            // 
            lblInPosAxis2.BorderStyle = BorderStyle.Fixed3D;
            lblInPosAxis2.Location = new Point(889, 362);
            lblInPosAxis2.Name = "lblInPosAxis2";
            lblInPosAxis2.Size = new Size(61, 22);
            lblInPosAxis2.TabIndex = 89;
            lblInPosAxis2.Text = "InPos";
            // 
            // label49
            // 
            label49.AutoSize = true;
            label49.Location = new Point(776, 630);
            label49.Name = "label49";
            label49.Size = new Size(35, 20);
            label49.TabIndex = 103;
            label49.Text = "mm";
            // 
            // lblCurrentPositionZ
            // 
            lblCurrentPositionZ.AutoSize = true;
            lblCurrentPositionZ.BorderStyle = BorderStyle.Fixed3D;
            lblCurrentPositionZ.Location = new Point(693, 630);
            lblCurrentPositionZ.Name = "lblCurrentPositionZ";
            lblCurrentPositionZ.Size = new Size(65, 22);
            lblCurrentPositionZ.TabIndex = 102;
            lblCurrentPositionZ.Text = "---------";
            // 
            // label51
            // 
            label51.AutoSize = true;
            label51.Location = new Point(776, 575);
            label51.Name = "label51";
            label51.Size = new Size(35, 20);
            label51.TabIndex = 101;
            label51.Text = "mm";
            // 
            // lblCurrentPositionY
            // 
            lblCurrentPositionY.AutoSize = true;
            lblCurrentPositionY.BorderStyle = BorderStyle.Fixed3D;
            lblCurrentPositionY.Location = new Point(693, 582);
            lblCurrentPositionY.Name = "lblCurrentPositionY";
            lblCurrentPositionY.Size = new Size(65, 22);
            lblCurrentPositionY.TabIndex = 100;
            lblCurrentPositionY.Text = "---------";
            // 
            // label53
            // 
            label53.AutoSize = true;
            label53.Location = new Point(776, 525);
            label53.Name = "label53";
            label53.Size = new Size(35, 20);
            label53.TabIndex = 99;
            label53.Text = "mm";
            // 
            // lblCurrentPositionX
            // 
            lblCurrentPositionX.AutoSize = true;
            lblCurrentPositionX.BorderStyle = BorderStyle.Fixed3D;
            lblCurrentPositionX.Location = new Point(693, 525);
            lblCurrentPositionX.Name = "lblCurrentPositionX";
            lblCurrentPositionX.Size = new Size(65, 22);
            lblCurrentPositionX.TabIndex = 98;
            lblCurrentPositionX.Text = "---------";
            // 
            // label55
            // 
            label55.AutoSize = true;
            label55.Location = new Point(663, 491);
            label55.Name = "label55";
            label55.Size = new Size(144, 20);
            label55.TabIndex = 97;
            label55.Text = "Current Coordinates:";
            // 
            // lblConnection
            // 
            lblConnection.AutoSize = true;
            lblConnection.BorderStyle = BorderStyle.Fixed3D;
            lblConnection.Location = new Point(231, 29);
            lblConnection.Name = "lblConnection";
            lblConnection.Size = new Size(95, 22);
            lblConnection.TabIndex = 104;
            lblConnection.Text = "--------------";
            // 
            // lblErrorAxis1
            // 
            lblErrorAxis1.BorderStyle = BorderStyle.Fixed3D;
            lblErrorAxis1.Location = new Point(985, 315);
            lblErrorAxis1.Name = "lblErrorAxis1";
            lblErrorAxis1.Size = new Size(61, 22);
            lblErrorAxis1.TabIndex = 105;
            lblErrorAxis1.Text = "Error";
            // 
            // lblErrorAxis2
            // 
            lblErrorAxis2.BorderStyle = BorderStyle.Fixed3D;
            lblErrorAxis2.Location = new Point(985, 359);
            lblErrorAxis2.Name = "lblErrorAxis2";
            lblErrorAxis2.Size = new Size(61, 22);
            lblErrorAxis2.TabIndex = 106;
            lblErrorAxis2.Text = "Error";
            // 
            // lblErrorAxis3
            // 
            lblErrorAxis3.BorderStyle = BorderStyle.Fixed3D;
            lblErrorAxis3.Location = new Point(985, 407);
            lblErrorAxis3.Name = "lblErrorAxis3";
            lblErrorAxis3.Size = new Size(61, 22);
            lblErrorAxis3.TabIndex = 107;
            lblErrorAxis3.Text = "Error";
            // 
            // btnStopAxis
            // 
            btnStopAxis.Location = new Point(530, 193);
            btnStopAxis.Name = "btnStopAxis";
            btnStopAxis.Size = new Size(94, 29);
            btnStopAxis.TabIndex = 109;
            btnStopAxis.Text = "Stop";
            btnStopAxis.UseVisualStyleBackColor = true;
            btnStopAxis.Click += btnStopAxis_Click;
            // 
            // label40
            // 
            label40.AutoSize = true;
            label40.Location = new Point(530, 153);
            label40.Name = "label40";
            label40.Size = new Size(74, 20);
            label40.TabIndex = 108;
            label40.Text = "Stop Axis:";
            // 
            // label42
            // 
            label42.AutoSize = true;
            label42.Location = new Point(469, 273);
            label42.Name = "label42";
            label42.Size = new Size(63, 20);
            label42.TabIndex = 110;
            label42.Text = "Execute:";
            // 
            // btnExecuteAbsAxis
            // 
            btnExecuteAbsAxis.Location = new Point(469, 306);
            btnExecuteAbsAxis.Name = "btnExecuteAbsAxis";
            btnExecuteAbsAxis.Size = new Size(94, 29);
            btnExecuteAbsAxis.TabIndex = 40;
            btnExecuteAbsAxis.Text = "Execute";
            btnExecuteAbsAxis.UseVisualStyleBackColor = true;
            btnExecuteAbsAxis.Click += btnExecuteAbsAxis_Click;
            // 
            // label44
            // 
            label44.AutoSize = true;
            label44.Location = new Point(469, 491);
            label44.Name = "label44";
            label44.Size = new Size(63, 20);
            label44.TabIndex = 111;
            label44.Text = "Execute:";
            // 
            // lstLog
            // 
            lstLog.FormattingEnabled = true;
            lstLog.Location = new Point(40, 719);
            lstLog.Name = "lstLog";
            lstLog.Size = new Size(920, 104);
            lstLog.TabIndex = 112;
            // 
            // label45
            // 
            label45.AutoSize = true;
            label45.Location = new Point(40, 680);
            label45.Name = "label45";
            label45.Size = new Size(94, 20);
            label45.TabIndex = 113;
            label45.Text = "System Logs:";
            // 
            // numSpeedAbsAxis1
            // 
            numSpeedAbsAxis1.DecimalPlaces = 2;
            numSpeedAbsAxis1.Location = new Point(293, 313);
            numSpeedAbsAxis1.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            numSpeedAbsAxis1.Name = "numSpeedAbsAxis1";
            numSpeedAbsAxis1.Size = new Size(90, 27);
            numSpeedAbsAxis1.TabIndex = 114;
            numSpeedAbsAxis1.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1100, 908);
            Controls.Add(numSpeedAbsAxis1);
            Controls.Add(label45);
            Controls.Add(lstLog);
            Controls.Add(label44);
            Controls.Add(label42);
            Controls.Add(btnStopAxis);
            Controls.Add(label40);
            Controls.Add(lblErrorAxis3);
            Controls.Add(lblErrorAxis2);
            Controls.Add(lblErrorAxis1);
            Controls.Add(lblConnection);
            Controls.Add(label49);
            Controls.Add(lblCurrentPositionZ);
            Controls.Add(label51);
            Controls.Add(lblCurrentPositionY);
            Controls.Add(label53);
            Controls.Add(lblCurrentPositionX);
            Controls.Add(label55);
            Controls.Add(lblInPosAxis2);
            Controls.Add(lblInPosAxis3);
            Controls.Add(lblInPosAxis1);
            Controls.Add(lblBusyAxis3);
            Controls.Add(lblBusyAxis2);
            Controls.Add(lblBusyAxis1);
            Controls.Add(lblHomedAxis3);
            Controls.Add(lblHomedAxis2);
            Controls.Add(lblHomedAxis1);
            Controls.Add(label43);
            Controls.Add(label41);
            Controls.Add(lblCurrentPosAxis3);
            Controls.Add(label38);
            Controls.Add(lblCurrentPosAxis2);
            Controls.Add(label39);
            Controls.Add(lblCurrentPosAxis1);
            Controls.Add(label37);
            Controls.Add(label36);
            Controls.Add(label35);
            Controls.Add(label24);
            Controls.Add(numSpeedZ);
            Controls.Add(label25);
            Controls.Add(numSpeedY);
            Controls.Add(label26);
            Controls.Add(label27);
            Controls.Add(numSpeedX);
            Controls.Add(btnExecuteTGXYZ);
            Controls.Add(label28);
            Controls.Add(label29);
            Controls.Add(label30);
            Controls.Add(numTargetZ);
            Controls.Add(numTargetY);
            Controls.Add(numTargetX);
            Controls.Add(label31);
            Controls.Add(label32);
            Controls.Add(label33);
            Controls.Add(label34);
            Controls.Add(label23);
            Controls.Add(label19);
            Controls.Add(numSpeedAbsAxis3);
            Controls.Add(label20);
            Controls.Add(numSpeedAbsAxis2);
            Controls.Add(label21);
            Controls.Add(label22);
            Controls.Add(btnExecuteAbsAxis);
            Controls.Add(label18);
            Controls.Add(label17);
            Controls.Add(label16);
            Controls.Add(numTGPAxis3);
            Controls.Add(numTGPAxis2);
            Controls.Add(numTGPAxis1);
            Controls.Add(label15);
            Controls.Add(label12);
            Controls.Add(label13);
            Controls.Add(label14);
            Controls.Add(label11);
            Controls.Add(btnHomeAxis);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(numSpeedJogAxis3);
            Controls.Add(label8);
            Controls.Add(numSpeedJogAxis2);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(numSpeedJogAxis1);
            Controls.Add(btnJogDownAxis3);
            Controls.Add(btnJogUpAxis3);
            Controls.Add(btnJogDownAxis2);
            Controls.Add(btnJogUpAxis2);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(btnJogDownAxis1);
            Controls.Add(btnJogUpAxis1);
            Controls.Add(btnDisconnect);
            Controls.Add(btnConnect);
            Controls.Add(txtIP);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)numSpeedJogAxis1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSpeedJogAxis2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSpeedJogAxis3).EndInit();
            ((System.ComponentModel.ISupportInitialize)numTGPAxis1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numTGPAxis2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numTGPAxis3).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSpeedAbsAxis3).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSpeedAbsAxis2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numTargetZ).EndInit();
            ((System.ComponentModel.ISupportInitialize)numTargetY).EndInit();
            ((System.ComponentModel.ISupportInitialize)numTargetX).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSpeedZ).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSpeedY).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSpeedX).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSpeedAbsAxis1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtIP;
        private Button btnConnect;
        private Button btnDisconnect;
        private Label label2;
        private Button btnJogDownAxis1;
        private Button btnJogUpAxis1;
        private Label label3;
        private Label label4;
        private Label label5;
        private Button btnJogDownAxis2;
        private Button btnJogUpAxis2;
        private Button btnJogDownAxis3;
        private Button btnJogUpAxis3;
        private NumericUpDown numSpeedJogAxis1;
        private Label label6;
        private Label label7;
        private Label label8;
        private NumericUpDown numSpeedJogAxis2;
        private Label label9;
        private NumericUpDown numSpeedJogAxis3;
        private Label label10;
        private Button btnHomeAxis;
        private Button btnHomeAxis1;
        private Button btnHomeAxis2;
        private Button btnHomeAxis3;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label15;
        private NumericUpDown numTGPAxis1;
        private NumericUpDown numTGPAxis2;
        private NumericUpDown numTGPAxis3;
        private Label label16;
        private Label label17;
        private Label label18;
        private Label label19;
        private NumericUpDown numSpeedAbsAxis3;
        private Label label20;
        private NumericUpDown numSpeedAbsAxis2;
        private Label label21;
        private Label label22;
        private Label label23;
        private Button btnExecuteTGXYZ;
        private Label label28;
        private Label label29;
        private Label label30;
        private NumericUpDown numTargetZ;
        private NumericUpDown numTargetY;
        private NumericUpDown numTargetX;
        private Label label31;
        private Label label32;
        private Label label33;
        private Label label34;
        private Label label24;
        private NumericUpDown numSpeedZ;
        private Label label25;
        private NumericUpDown numSpeedY;
        private Label label26;
        private Label label27;
        private NumericUpDown numSpeedX;
        private Label label35;
        private Label label36;
        private Label label37;
        private Label lblCurrentPosAxis1;
        private Label label39;
        private Label label38;
        private Label lblCurrentPosAxis2;
        private Label label41;
        private Label lblCurrentPosAxis3;
        private Label label43;
        private Label lblHomedAxis1;
        private Label lblHomedAxis2;
        private Label lblHomedAxis3;
        private Label lblBusyAxis1;
        private Label lblBusyAxis2;
        private Label lblBusyAxis3;
        private Label lblInPosAxis1;
        private Label lblInPosAxis3;
        private Label lblInPosAxis2;
        private Label label49;
        private Label lblCurrentPositionZ;
        private Label label51;
        private Label lblCurrentPositionY;
        private Label label53;
        private Label lblCurrentPositionX;
        private Label label55;
        private Label lblConnection;
        private Label lblErrorAxis1;
        private Label lblErrorAxis2;
        private Label lblErrorAxis3;
        private Button button1;
        private Button button2;
        private Button btnStopAxis;
        private Label label40;
        private Label label42;
        private Button btnExecuteAbsAxis;
        private Label label44;
        private ListBox lstLog;
        private Label label45;
        private NumericUpDown numSpeedAbsAxis1;
    }
}
