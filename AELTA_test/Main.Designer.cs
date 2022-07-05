namespace ControlUI
{
    partial class GripPosition_
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.serialPort2 = new System.IO.Ports.SerialPort(this.components);
            this.serialPort3 = new System.IO.Ports.SerialPort(this.components);
            this.serialPort4 = new System.IO.Ports.SerialPort(this.components);
            this.oo = new System.IO.Ports.SerialPort(this.components);
            this.btn_Connect = new System.Windows.Forms.Button();
            this.btn_Disconnect = new System.Windows.Forms.Button();
            this.lab_Data = new System.Windows.Forms.Label();
            this.TB_SendData = new System.Windows.Forms.TextBox();
            this.CB_Listen = new System.Windows.Forms.CheckBox();
            this.btn_ClearSendData = new System.Windows.Forms.Button();
            this.btn_Send = new System.Windows.Forms.Button();
            this.TB_Command = new System.Windows.Forms.TextBox();
            this.lab_ReData = new System.Windows.Forms.Label();
            this.TB_RecvData = new System.Windows.Forms.TextBox();
            this.btn_ClearRecvData = new System.Windows.Forms.Button();
            this.CB_Customized = new System.Windows.Forms.ComboBox();
            this.btn_TMtest = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Image_checkBox = new System.Windows.Forms.CheckBox();
            this.ImageGrip_CheckBox = new System.Windows.Forms.CheckBox();
            this.TB_Port = new System.Windows.Forms.TextBox();
            this.TB_IPAddress = new System.Windows.Forms.TextBox();
            this.LB_ConnectionStatus = new System.Windows.Forms.Label();
            this.lab_Status = new System.Windows.Forms.Label();
            this.lab_Port = new System.Windows.Forms.Label();
            this.lab_IP = new System.Windows.Forms.Label();
            this.XGE32_groupBox = new System.Windows.Forms.GroupBox();
            this.button35 = new System.Windows.Forms.Button();
            this.GripCnt_Btn = new System.Windows.Forms.Button();
            this.XEG32_Close_Other = new System.Windows.Forms.Button();
            this.XEG32_Close_bt = new System.Windows.Forms.Button();
            this.XEG32_Open_bt = new System.Windows.Forms.Button();
            this.XEG32_Reset_bt = new System.Windows.Forms.Button();
            this.XEG32_Enable_bt = new System.Windows.Forms.Button();
            this.XEG32_VelC_lab = new System.Windows.Forms.Label();
            this.XEG32_Info_lab = new System.Windows.Forms.Label();
            this.XEG32_VelL_lab = new System.Windows.Forms.Label();
            this.XEG32_PosStkL_lab = new System.Windows.Forms.Label();
            this.XEG32_PosStkC_lab = new System.Windows.Forms.Label();
            this.XEG32_Vel_text = new System.Windows.Forms.TextBox();
            this.XEG32_PosStk_text = new System.Windows.Forms.TextBox();
            this.XEG32 = new System.IO.Ports.SerialPort(this.components);
            this.txtStatus = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.AX12A_Close = new System.Windows.Forms.Button();
            this.AX12A_Open = new System.Windows.Forms.Button();
            this.AX12A_Status = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.TB_sp_abs = new System.Windows.Forms.TextBox();
            this.TB_sp_pc = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Img_Btn = new System.Windows.Forms.Button();
            this.socketmsg = new System.Windows.Forms.RichTextBox();
            this.timer_rec = new System.Windows.Forms.Timer(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Grip_Position = new System.Windows.Forms.TextBox();
            this.StopMove = new System.Windows.Forms.Button();
            this.Delete = new System.Windows.Forms.Button();
            this.Excel = new System.Windows.Forms.CheckBox();
            this.ExprotDataGrid = new System.Windows.Forms.Button();
            this.ActionBtn = new System.Windows.Forms.Button();
            this.Clr_btn = new System.Windows.Forms.Button();
            this.exportExcel = new System.Windows.Forms.Button();
            this.reMove = new System.Windows.Forms.Button();
            this.PointDataGrid = new System.Windows.Forms.DataGridView();
            this.Arm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GripObject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.command = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.x = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.z = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rx = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ry = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rz = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.CB_Customized1 = new System.Windows.Forms.ComboBox();
            this.btn_ClearRecvData1 = new System.Windows.Forms.Button();
            this.TB_RecvData1 = new System.Windows.Forms.TextBox();
            this.lab_ReData1 = new System.Windows.Forms.Label();
            this.TB_Command1 = new System.Windows.Forms.TextBox();
            this.btn_Send1 = new System.Windows.Forms.Button();
            this.btn_ClearSendData1 = new System.Windows.Forms.Button();
            this.CB_Listen1 = new System.Windows.Forms.CheckBox();
            this.TB_SendData1 = new System.Windows.Forms.TextBox();
            this.lab_Data1 = new System.Windows.Forms.Label();
            this.btn_Disconnect1 = new System.Windows.Forms.Button();
            this.btn_Connect1 = new System.Windows.Forms.Button();
            this.TB_Port1 = new System.Windows.Forms.TextBox();
            this.TB_IPAddress1 = new System.Windows.Forms.TextBox();
            this.LB_ConnectionStatus1 = new System.Windows.Forms.Label();
            this.lab_Status1 = new System.Windows.Forms.Label();
            this.lab_Port1 = new System.Windows.Forms.Label();
            this.lab_IP1 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.TB_sp_abs1 = new System.Windows.Forms.TextBox();
            this.TB_sp_pc1 = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.button9 = new System.Windows.Forms.Button();
            this.BaisLB = new System.Windows.Forms.Label();
            this.聯軸器 = new System.Windows.Forms.Button();
            this.萬象軸 = new System.Windows.Forms.Button();
            this.固定座 = new System.Windows.Forms.Button();
            this.img_Position = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.NowPositionLb = new System.Windows.Forms.Label();
            this.img_Label = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.墊高板 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.PutDown = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.XGE32_groupBox.SuspendLayout();
            this.txtStatus.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PointDataGrid)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = 921600;
            this.serialPort1.PortName = "COM2";
            // 
            // serialPort2
            // 
            this.serialPort2.BaudRate = 921600;
            this.serialPort2.PortName = "COM3";
            // 
            // serialPort3
            // 
            this.serialPort3.BaudRate = 921600;
            this.serialPort3.PortName = "COM15";
            // 
            // serialPort4
            // 
            this.serialPort4.BaudRate = 57142;
            this.serialPort4.PortName = "COM1700";
            // 
            // oo
            // 
            this.oo.PortName = "COM10";
            // 
            // btn_Connect
            // 
            this.btn_Connect.Location = new System.Drawing.Point(222, 21);
            this.btn_Connect.Name = "btn_Connect";
            this.btn_Connect.Size = new System.Drawing.Size(75, 21);
            this.btn_Connect.TabIndex = 60;
            this.btn_Connect.Text = "Connect";
            this.btn_Connect.UseVisualStyleBackColor = true;
            this.btn_Connect.Click += new System.EventHandler(this.btn_Connect_Click);
            // 
            // btn_Disconnect
            // 
            this.btn_Disconnect.Location = new System.Drawing.Point(222, 47);
            this.btn_Disconnect.Name = "btn_Disconnect";
            this.btn_Disconnect.Size = new System.Drawing.Size(75, 21);
            this.btn_Disconnect.TabIndex = 61;
            this.btn_Disconnect.Text = "Disconnect";
            this.btn_Disconnect.UseVisualStyleBackColor = true;
            this.btn_Disconnect.Click += new System.EventHandler(this.btn_Disconnect_Click);
            // 
            // lab_Data
            // 
            this.lab_Data.AutoSize = true;
            this.lab_Data.Location = new System.Drawing.Point(4, 109);
            this.lab_Data.Name = "lab_Data";
            this.lab_Data.Size = new System.Drawing.Size(74, 12);
            this.lab_Data.TabIndex = 62;
            this.lab_Data.Text = "Edit/Send Data";
            // 
            // TB_SendData
            // 
            this.TB_SendData.Location = new System.Drawing.Point(5, 122);
            this.TB_SendData.Multiline = true;
            this.TB_SendData.Name = "TB_SendData";
            this.TB_SendData.Size = new System.Drawing.Size(292, 36);
            this.TB_SendData.TabIndex = 63;
            this.TB_SendData.Text = "1,PTP(\"CPP\",450,-122,300,180,0,90,100,200,0,false)";
            this.TB_SendData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_SendData_KeyDown);
            // 
            // CB_Listen
            // 
            this.CB_Listen.AutoSize = true;
            this.CB_Listen.Checked = true;
            this.CB_Listen.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CB_Listen.Location = new System.Drawing.Point(5, 167);
            this.CB_Listen.Name = "CB_Listen";
            this.CB_Listen.Size = new System.Drawing.Size(52, 16);
            this.CB_Listen.TabIndex = 64;
            this.CB_Listen.Text = "Listen";
            this.CB_Listen.UseVisualStyleBackColor = true;
            this.CB_Listen.CheckedChanged += new System.EventHandler(this.CB_Listen_CheckedChanged);
            // 
            // btn_ClearSendData
            // 
            this.btn_ClearSendData.Location = new System.Drawing.Point(141, 163);
            this.btn_ClearSendData.Name = "btn_ClearSendData";
            this.btn_ClearSendData.Size = new System.Drawing.Size(75, 21);
            this.btn_ClearSendData.TabIndex = 65;
            this.btn_ClearSendData.Text = "Clear";
            this.btn_ClearSendData.UseVisualStyleBackColor = true;
            this.btn_ClearSendData.Click += new System.EventHandler(this.btn_ClearSendData_Click);
            // 
            // btn_Send
            // 
            this.btn_Send.Location = new System.Drawing.Point(222, 163);
            this.btn_Send.Name = "btn_Send";
            this.btn_Send.Size = new System.Drawing.Size(75, 21);
            this.btn_Send.TabIndex = 66;
            this.btn_Send.Text = "Send";
            this.btn_Send.UseVisualStyleBackColor = true;
            this.btn_Send.Click += new System.EventHandler(this.btn_Send_Click);
            // 
            // TB_Command
            // 
            this.TB_Command.Location = new System.Drawing.Point(5, 189);
            this.TB_Command.Multiline = true;
            this.TB_Command.Name = "TB_Command";
            this.TB_Command.Size = new System.Drawing.Size(292, 31);
            this.TB_Command.TabIndex = 67;
            // 
            // lab_ReData
            // 
            this.lab_ReData.AutoSize = true;
            this.lab_ReData.Location = new System.Drawing.Point(4, 226);
            this.lab_ReData.Name = "lab_ReData";
            this.lab_ReData.Size = new System.Drawing.Size(53, 12);
            this.lab_ReData.TabIndex = 68;
            this.lab_ReData.Text = "Recv Data";
            // 
            // TB_RecvData
            // 
            this.TB_RecvData.Location = new System.Drawing.Point(5, 241);
            this.TB_RecvData.Multiline = true;
            this.TB_RecvData.Name = "TB_RecvData";
            this.TB_RecvData.Size = new System.Drawing.Size(292, 40);
            this.TB_RecvData.TabIndex = 69;
            this.TB_RecvData.Text = "1,Move_Line(\"CPP\", 0, 0, -5, 0, 0, 0, 125, 200, 0, false)";
            // 
            // btn_ClearRecvData
            // 
            this.btn_ClearRecvData.Location = new System.Drawing.Point(222, 287);
            this.btn_ClearRecvData.Name = "btn_ClearRecvData";
            this.btn_ClearRecvData.Size = new System.Drawing.Size(75, 21);
            this.btn_ClearRecvData.TabIndex = 70;
            this.btn_ClearRecvData.Text = "Clear";
            this.btn_ClearRecvData.UseVisualStyleBackColor = true;
            this.btn_ClearRecvData.Click += new System.EventHandler(this.btn_ClearRecvData_Click);
            // 
            // CB_Customized
            // 
            this.CB_Customized.FormattingEnabled = true;
            this.CB_Customized.Items.AddRange(new object[] {
            "$TMSCT",
            "$TMSTA"});
            this.CB_Customized.Location = new System.Drawing.Point(61, 164);
            this.CB_Customized.Name = "CB_Customized";
            this.CB_Customized.Size = new System.Drawing.Size(67, 20);
            this.CB_Customized.TabIndex = 71;
            // 
            // btn_TMtest
            // 
            this.btn_TMtest.Location = new System.Drawing.Point(194, 18);
            this.btn_TMtest.Name = "btn_TMtest";
            this.btn_TMtest.Size = new System.Drawing.Size(75, 23);
            this.btn_TMtest.TabIndex = 72;
            this.btn_TMtest.Text = "TM歸位";
            this.btn_TMtest.UseVisualStyleBackColor = true;
            this.btn_TMtest.Click += new System.EventHandler(this.btn_TMtest_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Image_checkBox);
            this.groupBox1.Controls.Add(this.ImageGrip_CheckBox);
            this.groupBox1.Controls.Add(this.CB_Customized);
            this.groupBox1.Controls.Add(this.btn_ClearRecvData);
            this.groupBox1.Controls.Add(this.TB_RecvData);
            this.groupBox1.Controls.Add(this.lab_ReData);
            this.groupBox1.Controls.Add(this.TB_Command);
            this.groupBox1.Controls.Add(this.btn_Send);
            this.groupBox1.Controls.Add(this.btn_ClearSendData);
            this.groupBox1.Controls.Add(this.CB_Listen);
            this.groupBox1.Controls.Add(this.TB_SendData);
            this.groupBox1.Controls.Add(this.lab_Data);
            this.groupBox1.Controls.Add(this.btn_Disconnect);
            this.groupBox1.Controls.Add(this.btn_Connect);
            this.groupBox1.Controls.Add(this.TB_Port);
            this.groupBox1.Controls.Add(this.TB_IPAddress);
            this.groupBox1.Controls.Add(this.LB_ConnectionStatus);
            this.groupBox1.Controls.Add(this.lab_Status);
            this.groupBox1.Controls.Add(this.lab_Port);
            this.groupBox1.Controls.Add(this.lab_IP);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(307, 314);
            this.groupBox1.TabIndex = 73;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "TM_TCP";
            // 
            // Image_checkBox
            // 
            this.Image_checkBox.AutoSize = true;
            this.Image_checkBox.Location = new System.Drawing.Point(217, 86);
            this.Image_checkBox.Name = "Image_checkBox";
            this.Image_checkBox.Size = new System.Drawing.Size(53, 16);
            this.Image_checkBox.TabIndex = 72;
            this.Image_checkBox.Text = "Image";
            this.Image_checkBox.UseVisualStyleBackColor = true;
            // 
            // ImageGrip_CheckBox
            // 
            this.ImageGrip_CheckBox.AutoSize = true;
            this.ImageGrip_CheckBox.Location = new System.Drawing.Point(217, 105);
            this.ImageGrip_CheckBox.Name = "ImageGrip_CheckBox";
            this.ImageGrip_CheckBox.Size = new System.Drawing.Size(74, 16);
            this.ImageGrip_CheckBox.TabIndex = 72;
            this.ImageGrip_CheckBox.Text = "ImageGrip";
            this.ImageGrip_CheckBox.UseVisualStyleBackColor = true;
            // 
            // TB_Port
            // 
            this.TB_Port.Location = new System.Drawing.Point(141, 46);
            this.TB_Port.Name = "TB_Port";
            this.TB_Port.Size = new System.Drawing.Size(46, 22);
            this.TB_Port.TabIndex = 59;
            this.TB_Port.Text = "5890";
            // 
            // TB_IPAddress
            // 
            this.TB_IPAddress.Location = new System.Drawing.Point(6, 46);
            this.TB_IPAddress.Name = "TB_IPAddress";
            this.TB_IPAddress.Size = new System.Drawing.Size(91, 22);
            this.TB_IPAddress.TabIndex = 58;
            this.TB_IPAddress.Text = "169.254.119.180";
            // 
            // LB_ConnectionStatus
            // 
            this.LB_ConnectionStatus.AutoSize = true;
            this.LB_ConnectionStatus.Location = new System.Drawing.Point(139, 82);
            this.LB_ConnectionStatus.Name = "LB_ConnectionStatus";
            this.LB_ConnectionStatus.Size = new System.Drawing.Size(70, 12);
            this.LB_ConnectionStatus.TabIndex = 57;
            this.LB_ConnectionStatus.Text = "Disconnected.";
            // 
            // lab_Status
            // 
            this.lab_Status.AutoSize = true;
            this.lab_Status.Location = new System.Drawing.Point(5, 82);
            this.lab_Status.Name = "lab_Status";
            this.lab_Status.Size = new System.Drawing.Size(92, 12);
            this.lab_Status.TabIndex = 24;
            this.lab_Status.Text = "Connection Status:";
            // 
            // lab_Port
            // 
            this.lab_Port.AutoSize = true;
            this.lab_Port.Location = new System.Drawing.Point(139, 23);
            this.lab_Port.Name = "lab_Port";
            this.lab_Port.Size = new System.Drawing.Size(24, 12);
            this.lab_Port.TabIndex = 25;
            this.lab_Port.Text = "Port";
            // 
            // lab_IP
            // 
            this.lab_IP.AutoSize = true;
            this.lab_IP.Location = new System.Drawing.Point(6, 23);
            this.lab_IP.Name = "lab_IP";
            this.lab_IP.Size = new System.Drawing.Size(55, 12);
            this.lab_IP.TabIndex = 26;
            this.lab_IP.Text = "IP Address";
            // 
            // XGE32_groupBox
            // 
            this.XGE32_groupBox.Controls.Add(this.button35);
            this.XGE32_groupBox.Controls.Add(this.GripCnt_Btn);
            this.XGE32_groupBox.Controls.Add(this.XEG32_Close_Other);
            this.XGE32_groupBox.Controls.Add(this.XEG32_Close_bt);
            this.XGE32_groupBox.Controls.Add(this.XEG32_Open_bt);
            this.XGE32_groupBox.Controls.Add(this.XEG32_Reset_bt);
            this.XGE32_groupBox.Controls.Add(this.XEG32_Enable_bt);
            this.XGE32_groupBox.Controls.Add(this.XEG32_VelC_lab);
            this.XGE32_groupBox.Controls.Add(this.XEG32_Info_lab);
            this.XGE32_groupBox.Controls.Add(this.XEG32_VelL_lab);
            this.XGE32_groupBox.Controls.Add(this.XEG32_PosStkL_lab);
            this.XGE32_groupBox.Controls.Add(this.XEG32_PosStkC_lab);
            this.XGE32_groupBox.Controls.Add(this.XEG32_Vel_text);
            this.XGE32_groupBox.Controls.Add(this.XEG32_PosStk_text);
            this.XGE32_groupBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.XGE32_groupBox.Location = new System.Drawing.Point(8, 82);
            this.XGE32_groupBox.Margin = new System.Windows.Forms.Padding(2);
            this.XGE32_groupBox.Name = "XGE32_groupBox";
            this.XGE32_groupBox.Padding = new System.Windows.Forms.Padding(2);
            this.XGE32_groupBox.Size = new System.Drawing.Size(321, 160);
            this.XGE32_groupBox.TabIndex = 110;
            this.XGE32_groupBox.TabStop = false;
            this.XGE32_groupBox.Text = "XEG-32夾爪控制";
            // 
            // button35
            // 
            this.button35.Location = new System.Drawing.Point(8, 55);
            this.button35.Name = "button35";
            this.button35.Size = new System.Drawing.Size(75, 28);
            this.button35.TabIndex = 20;
            this.button35.Text = "Disconnect";
            this.button35.UseVisualStyleBackColor = true;
            this.button35.Click += new System.EventHandler(this.button35_Click);
            // 
            // GripCnt_Btn
            // 
            this.GripCnt_Btn.Location = new System.Drawing.Point(8, 21);
            this.GripCnt_Btn.Name = "GripCnt_Btn";
            this.GripCnt_Btn.Size = new System.Drawing.Size(75, 28);
            this.GripCnt_Btn.TabIndex = 19;
            this.GripCnt_Btn.Text = "Connect";
            this.GripCnt_Btn.UseVisualStyleBackColor = true;
            this.GripCnt_Btn.Click += new System.EventHandler(this.button34_Click);
            // 
            // XEG32_Close_Other
            // 
            this.XEG32_Close_Other.Location = new System.Drawing.Point(89, 127);
            this.XEG32_Close_Other.Name = "XEG32_Close_Other";
            this.XEG32_Close_Other.Size = new System.Drawing.Size(75, 28);
            this.XEG32_Close_Other.TabIndex = 18;
            this.XEG32_Close_Other.Text = "關夾爪_200";
            this.XEG32_Close_Other.UseVisualStyleBackColor = true;
            this.XEG32_Close_Other.Click += new System.EventHandler(this.XEG32_Close_Other_Click);
            // 
            // XEG32_Close_bt
            // 
            this.XEG32_Close_bt.Location = new System.Drawing.Point(8, 123);
            this.XEG32_Close_bt.Name = "XEG32_Close_bt";
            this.XEG32_Close_bt.Size = new System.Drawing.Size(75, 28);
            this.XEG32_Close_bt.TabIndex = 18;
            this.XEG32_Close_bt.Text = "關夾爪_600";
            this.XEG32_Close_bt.UseVisualStyleBackColor = true;
            this.XEG32_Close_bt.Click += new System.EventHandler(this.XEG32_Close_bt_Click);
            // 
            // XEG32_Open_bt
            // 
            this.XEG32_Open_bt.Location = new System.Drawing.Point(89, 90);
            this.XEG32_Open_bt.Name = "XEG32_Open_bt";
            this.XEG32_Open_bt.Size = new System.Drawing.Size(75, 28);
            this.XEG32_Open_bt.TabIndex = 18;
            this.XEG32_Open_bt.Text = "開夾爪";
            this.XEG32_Open_bt.UseVisualStyleBackColor = true;
            this.XEG32_Open_bt.Click += new System.EventHandler(this.XEG32_Open_bt_Click);
            // 
            // XEG32_Reset_bt
            // 
            this.XEG32_Reset_bt.Location = new System.Drawing.Point(8, 92);
            this.XEG32_Reset_bt.Name = "XEG32_Reset_bt";
            this.XEG32_Reset_bt.Size = new System.Drawing.Size(75, 28);
            this.XEG32_Reset_bt.TabIndex = 17;
            this.XEG32_Reset_bt.Text = "Reset";
            this.XEG32_Reset_bt.UseVisualStyleBackColor = true;
            this.XEG32_Reset_bt.Click += new System.EventHandler(this.XEG32_Reset_bt_Click);
            // 
            // XEG32_Enable_bt
            // 
            this.XEG32_Enable_bt.Location = new System.Drawing.Point(170, 90);
            this.XEG32_Enable_bt.Name = "XEG32_Enable_bt";
            this.XEG32_Enable_bt.Size = new System.Drawing.Size(80, 34);
            this.XEG32_Enable_bt.TabIndex = 16;
            this.XEG32_Enable_bt.Text = "Enable";
            this.XEG32_Enable_bt.UseVisualStyleBackColor = true;
            this.XEG32_Enable_bt.Click += new System.EventHandler(this.XEG32_Enable_bt_Click);
            // 
            // XEG32_VelC_lab
            // 
            this.XEG32_VelC_lab.AutoSize = true;
            this.XEG32_VelC_lab.Location = new System.Drawing.Point(97, 65);
            this.XEG32_VelC_lab.Name = "XEG32_VelC_lab";
            this.XEG32_VelC_lab.Size = new System.Drawing.Size(29, 12);
            this.XEG32_VelC_lab.TabIndex = 13;
            this.XEG32_VelC_lab.Text = "速度";
            // 
            // XEG32_Info_lab
            // 
            this.XEG32_Info_lab.AutoSize = true;
            this.XEG32_Info_lab.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.XEG32_Info_lab.Location = new System.Drawing.Point(4, 14);
            this.XEG32_Info_lab.Name = "XEG32_Info_lab";
            this.XEG32_Info_lab.Size = new System.Drawing.Size(101, 12);
            this.XEG32_Info_lab.TabIndex = 14;
            this.XEG32_Info_lab.Text = "夾爪無反應時使用";
            // 
            // XEG32_VelL_lab
            // 
            this.XEG32_VelL_lab.AutoSize = true;
            this.XEG32_VelL_lab.Location = new System.Drawing.Point(252, 65);
            this.XEG32_VelL_lab.Name = "XEG32_VelL_lab";
            this.XEG32_VelL_lab.Size = new System.Drawing.Size(63, 12);
            this.XEG32_VelL_lab.TabIndex = 14;
            this.XEG32_VelL_lab.Text = "10~80 mm/s";
            // 
            // XEG32_PosStkL_lab
            // 
            this.XEG32_PosStkL_lab.AutoSize = true;
            this.XEG32_PosStkL_lab.Location = new System.Drawing.Point(252, 37);
            this.XEG32_PosStkL_lab.Name = "XEG32_PosStkL_lab";
            this.XEG32_PosStkL_lab.Size = new System.Drawing.Size(44, 12);
            this.XEG32_PosStkL_lab.TabIndex = 14;
            this.XEG32_PosStkL_lab.Text = "0~3200 ";
            // 
            // XEG32_PosStkC_lab
            // 
            this.XEG32_PosStkC_lab.AutoSize = true;
            this.XEG32_PosStkC_lab.Location = new System.Drawing.Point(97, 37);
            this.XEG32_PosStkC_lab.Name = "XEG32_PosStkC_lab";
            this.XEG32_PosStkC_lab.Size = new System.Drawing.Size(29, 12);
            this.XEG32_PosStkC_lab.TabIndex = 15;
            this.XEG32_PosStkC_lab.Text = "位置";
            // 
            // XEG32_Vel_text
            // 
            this.XEG32_Vel_text.Location = new System.Drawing.Point(146, 62);
            this.XEG32_Vel_text.Name = "XEG32_Vel_text";
            this.XEG32_Vel_text.Size = new System.Drawing.Size(100, 22);
            this.XEG32_Vel_text.TabIndex = 7;
            this.XEG32_Vel_text.Text = "80";
            // 
            // XEG32_PosStk_text
            // 
            this.XEG32_PosStk_text.Location = new System.Drawing.Point(146, 34);
            this.XEG32_PosStk_text.Name = "XEG32_PosStk_text";
            this.XEG32_PosStk_text.Size = new System.Drawing.Size(100, 22);
            this.XEG32_PosStk_text.TabIndex = 8;
            this.XEG32_PosStk_text.Text = "0";
            // 
            // XEG32
            // 
            this.XEG32.BaudRate = 115200;
            this.XEG32.PortName = "COM5";
            // 
            // txtStatus
            // 
            this.txtStatus.Controls.Add(this.groupBox5);
            this.txtStatus.Controls.Add(this.XGE32_groupBox);
            this.txtStatus.Controls.Add(this.label6);
            this.txtStatus.Controls.Add(this.label5);
            this.txtStatus.Controls.Add(this.TB_sp_abs);
            this.txtStatus.Controls.Add(this.TB_sp_pc);
            this.txtStatus.Controls.Add(this.label4);
            this.txtStatus.Controls.Add(this.label3);
            this.txtStatus.Controls.Add(this.btn_TMtest);
            this.txtStatus.Location = new System.Drawing.Point(325, 17);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtStatus.Size = new System.Drawing.Size(338, 318);
            this.txtStatus.TabIndex = 111;
            this.txtStatus.TabStop = false;
            this.txtStatus.Text = "TM_動作";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.AX12A_Close);
            this.groupBox5.Controls.Add(this.AX12A_Open);
            this.groupBox5.Controls.Add(this.AX12A_Status);
            this.groupBox5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.groupBox5.Location = new System.Drawing.Point(8, 246);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox5.Size = new System.Drawing.Size(179, 72);
            this.groupBox5.TabIndex = 110;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "AX12A";
            // 
            // AX12A_Close
            // 
            this.AX12A_Close.Location = new System.Drawing.Point(-2, 20);
            this.AX12A_Close.Name = "AX12A_Close";
            this.AX12A_Close.Size = new System.Drawing.Size(75, 28);
            this.AX12A_Close.TabIndex = 18;
            this.AX12A_Close.Text = "關夾爪";
            this.AX12A_Close.UseVisualStyleBackColor = true;
            this.AX12A_Close.Click += new System.EventHandler(this.AX12A_Close_Click);
            // 
            // AX12A_Open
            // 
            this.AX12A_Open.Location = new System.Drawing.Point(79, 20);
            this.AX12A_Open.Name = "AX12A_Open";
            this.AX12A_Open.Size = new System.Drawing.Size(75, 28);
            this.AX12A_Open.TabIndex = 18;
            this.AX12A_Open.Text = "開夾爪";
            this.AX12A_Open.UseVisualStyleBackColor = true;
            this.AX12A_Open.Click += new System.EventHandler(this.AX12A_Open_Click);
            // 
            // AX12A_Status
            // 
            this.AX12A_Status.AutoSize = true;
            this.AX12A_Status.Location = new System.Drawing.Point(3, 51);
            this.AX12A_Status.Name = "AX12A_Status";
            this.AX12A_Status.Size = new System.Drawing.Size(44, 12);
            this.AX12A_Status.TabIndex = 14;
            this.AX12A_Status.Text = "0~3200 ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(141, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 80;
            this.label6.Text = "0.1~1.0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(141, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 79;
            this.label5.Text = "0.1~1.0";
            // 
            // TB_sp_abs
            // 
            this.TB_sp_abs.Location = new System.Drawing.Point(80, 44);
            this.TB_sp_abs.Name = "TB_sp_abs";
            this.TB_sp_abs.Size = new System.Drawing.Size(55, 22);
            this.TB_sp_abs.TabIndex = 78;
            this.TB_sp_abs.Text = "1.0";
            this.TB_sp_abs.TextChanged += new System.EventHandler(this.TB_sp_abs_TextChanged);
            // 
            // TB_sp_pc
            // 
            this.TB_sp_pc.Location = new System.Drawing.Point(80, 21);
            this.TB_sp_pc.Name = "TB_sp_pc";
            this.TB_sp_pc.Size = new System.Drawing.Size(55, 22);
            this.TB_sp_pc.TabIndex = 77;
            this.TB_sp_pc.Text = "1.0";
            this.TB_sp_pc.TextChanged += new System.EventHandler(this.TB_sp_pc_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 12);
            this.label4.TabIndex = 76;
            this.label4.Text = "絕對速度:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 12);
            this.label3.TabIndex = 75;
            this.label3.Text = "百分比速度:";
            // 
            // Img_Btn
            // 
            this.Img_Btn.Location = new System.Drawing.Point(338, 486);
            this.Img_Btn.Name = "Img_Btn";
            this.Img_Btn.Size = new System.Drawing.Size(92, 23);
            this.Img_Btn.TabIndex = 72;
            this.Img_Btn.Text = "影像辨識測試";
            this.Img_Btn.UseVisualStyleBackColor = true;
            this.Img_Btn.Click += new System.EventHandler(this.Img_Btn_Click);
            // 
            // socketmsg
            // 
            this.socketmsg.Location = new System.Drawing.Point(6, 15);
            this.socketmsg.Name = "socketmsg";
            this.socketmsg.Size = new System.Drawing.Size(262, 93);
            this.socketmsg.TabIndex = 113;
            this.socketmsg.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.StopMove);
            this.groupBox2.Controls.Add(this.Delete);
            this.groupBox2.Controls.Add(this.Excel);
            this.groupBox2.Controls.Add(this.ExprotDataGrid);
            this.groupBox2.Controls.Add(this.ActionBtn);
            this.groupBox2.Controls.Add(this.socketmsg);
            this.groupBox2.Controls.Add(this.Clr_btn);
            this.groupBox2.Controls.Add(this.exportExcel);
            this.groupBox2.Controls.Add(this.reMove);
            this.groupBox2.Controls.Add(this.PointDataGrid);
            this.groupBox2.Location = new System.Drawing.Point(669, 17);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(601, 617);
            this.groupBox2.TabIndex = 115;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "顯示介面";
            // 
            // Grip_Position
            // 
            this.Grip_Position.Location = new System.Drawing.Point(635, 515);
            this.Grip_Position.Name = "Grip_Position";
            this.Grip_Position.Size = new System.Drawing.Size(28, 22);
            this.Grip_Position.TabIndex = 125;
            this.Grip_Position.Text = "0";
            // 
            // StopMove
            // 
            this.StopMove.Location = new System.Drawing.Point(520, 70);
            this.StopMove.Name = "StopMove";
            this.StopMove.Size = new System.Drawing.Size(75, 23);
            this.StopMove.TabIndex = 129;
            this.StopMove.Text = "StopMove";
            this.StopMove.UseVisualStyleBackColor = true;
            this.StopMove.Click += new System.EventHandler(this.StopMove_Click);
            // 
            // Delete
            // 
            this.Delete.Location = new System.Drawing.Point(6, 498);
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(75, 23);
            this.Delete.TabIndex = 128;
            this.Delete.Text = "delete";
            this.Delete.UseVisualStyleBackColor = true;
            this.Delete.Click += new System.EventHandler(this.Delete_Click);
            // 
            // Excel
            // 
            this.Excel.AutoSize = true;
            this.Excel.Location = new System.Drawing.Point(545, 16);
            this.Excel.Name = "Excel";
            this.Excel.Size = new System.Drawing.Size(50, 16);
            this.Excel.TabIndex = 72;
            this.Excel.Text = "Excel";
            this.Excel.UseVisualStyleBackColor = true;
            // 
            // ExprotDataGrid
            // 
            this.ExprotDataGrid.Location = new System.Drawing.Point(274, 71);
            this.ExprotDataGrid.Name = "ExprotDataGrid";
            this.ExprotDataGrid.Size = new System.Drawing.Size(120, 21);
            this.ExprotDataGrid.TabIndex = 127;
            this.ExprotDataGrid.Text = "FromExcelImportData";
            this.ExprotDataGrid.UseVisualStyleBackColor = true;
            this.ExprotDataGrid.Click += new System.EventHandler(this.ExprotDataGrid_Click);
            // 
            // ActionBtn
            // 
            this.ActionBtn.Location = new System.Drawing.Point(520, 42);
            this.ActionBtn.Name = "ActionBtn";
            this.ActionBtn.Size = new System.Drawing.Size(75, 21);
            this.ActionBtn.TabIndex = 127;
            this.ActionBtn.Text = "Action";
            this.ActionBtn.UseVisualStyleBackColor = true;
            this.ActionBtn.Click += new System.EventHandler(this.ActionBtn_Click);
            // 
            // Clr_btn
            // 
            this.Clr_btn.Location = new System.Drawing.Point(87, 498);
            this.Clr_btn.Name = "Clr_btn";
            this.Clr_btn.Size = new System.Drawing.Size(75, 23);
            this.Clr_btn.TabIndex = 126;
            this.Clr_btn.Text = "Clear";
            this.Clr_btn.UseVisualStyleBackColor = true;
            this.Clr_btn.Click += new System.EventHandler(this.Clr_btn_Click);
            // 
            // exportExcel
            // 
            this.exportExcel.Location = new System.Drawing.Point(274, 42);
            this.exportExcel.Name = "exportExcel";
            this.exportExcel.Size = new System.Drawing.Size(75, 23);
            this.exportExcel.TabIndex = 126;
            this.exportExcel.Text = "ExportExcel";
            this.exportExcel.UseVisualStyleBackColor = true;
            this.exportExcel.Click += new System.EventHandler(this.exportExcel_Click);
            // 
            // reMove
            // 
            this.reMove.Location = new System.Drawing.Point(274, 13);
            this.reMove.Name = "reMove";
            this.reMove.Size = new System.Drawing.Size(75, 23);
            this.reMove.TabIndex = 115;
            this.reMove.Text = "重新發送";
            this.reMove.UseVisualStyleBackColor = true;
            this.reMove.Click += new System.EventHandler(this.armReMove);
            // 
            // PointDataGrid
            // 
            this.PointDataGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PointDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PointDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Arm,
            this.Count,
            this.GripObject,
            this.command,
            this.x,
            this.y,
            this.z,
            this.Rx,
            this.Ry,
            this.Rz});
            this.PointDataGrid.Location = new System.Drawing.Point(6, 114);
            this.PointDataGrid.Name = "PointDataGrid";
            this.PointDataGrid.RowTemplate.Height = 24;
            this.PointDataGrid.Size = new System.Drawing.Size(589, 379);
            this.PointDataGrid.TabIndex = 125;
            // 
            // Arm
            // 
            this.Arm.Frozen = true;
            this.Arm.HeaderText = "Arm";
            this.Arm.Name = "Arm";
            this.Arm.Width = 31;
            // 
            // Count
            // 
            this.Count.Frozen = true;
            this.Count.HeaderText = "Count";
            this.Count.Name = "Count";
            this.Count.Width = 40;
            // 
            // GripObject
            // 
            this.GripObject.Frozen = true;
            this.GripObject.HeaderText = "GripObject";
            this.GripObject.Name = "GripObject";
            this.GripObject.Width = 60;
            // 
            // command
            // 
            this.command.Frozen = true;
            this.command.HeaderText = "command";
            this.command.Name = "command";
            this.command.Width = 60;
            // 
            // x
            // 
            this.x.Frozen = true;
            this.x.HeaderText = "x";
            this.x.Name = "x";
            this.x.Width = 55;
            // 
            // y
            // 
            this.y.HeaderText = "y";
            this.y.Name = "y";
            this.y.Width = 55;
            // 
            // z
            // 
            this.z.HeaderText = "z";
            this.z.Name = "z";
            this.z.Width = 55;
            // 
            // Rx
            // 
            this.Rx.HeaderText = "Rx";
            this.Rx.Name = "Rx";
            this.Rx.Width = 55;
            // 
            // Ry
            // 
            this.Ry.HeaderText = "Ry";
            this.Ry.Name = "Ry";
            this.Ry.Width = 55;
            // 
            // Rz
            // 
            this.Rz.HeaderText = "Rz";
            this.Rz.Name = "Rz";
            this.Rz.Width = 55;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.CB_Customized1);
            this.groupBox3.Controls.Add(this.btn_ClearRecvData1);
            this.groupBox3.Controls.Add(this.TB_RecvData1);
            this.groupBox3.Controls.Add(this.lab_ReData1);
            this.groupBox3.Controls.Add(this.TB_Command1);
            this.groupBox3.Controls.Add(this.btn_Send1);
            this.groupBox3.Controls.Add(this.btn_ClearSendData1);
            this.groupBox3.Controls.Add(this.CB_Listen1);
            this.groupBox3.Controls.Add(this.TB_SendData1);
            this.groupBox3.Controls.Add(this.lab_Data1);
            this.groupBox3.Controls.Add(this.btn_Disconnect1);
            this.groupBox3.Controls.Add(this.btn_Connect1);
            this.groupBox3.Controls.Add(this.TB_Port1);
            this.groupBox3.Controls.Add(this.TB_IPAddress1);
            this.groupBox3.Controls.Add(this.LB_ConnectionStatus1);
            this.groupBox3.Controls.Add(this.lab_Status1);
            this.groupBox3.Controls.Add(this.lab_Port1);
            this.groupBox3.Controls.Add(this.lab_IP1);
            this.groupBox3.Location = new System.Drawing.Point(12, 326);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(307, 308);
            this.groupBox3.TabIndex = 73;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "TM_TCP";
            // 
            // CB_Customized1
            // 
            this.CB_Customized1.FormattingEnabled = true;
            this.CB_Customized1.Items.AddRange(new object[] {
            "$TMSCT",
            "$TMSTA"});
            this.CB_Customized1.Location = new System.Drawing.Point(61, 164);
            this.CB_Customized1.Name = "CB_Customized1";
            this.CB_Customized1.Size = new System.Drawing.Size(67, 20);
            this.CB_Customized1.TabIndex = 71;
            // 
            // btn_ClearRecvData1
            // 
            this.btn_ClearRecvData1.Location = new System.Drawing.Point(222, 276);
            this.btn_ClearRecvData1.Name = "btn_ClearRecvData1";
            this.btn_ClearRecvData1.Size = new System.Drawing.Size(75, 21);
            this.btn_ClearRecvData1.TabIndex = 70;
            this.btn_ClearRecvData1.Text = "Clear";
            this.btn_ClearRecvData1.UseVisualStyleBackColor = true;
            this.btn_ClearRecvData1.Click += new System.EventHandler(this.btn_ClearRecvData1_Click);
            // 
            // TB_RecvData1
            // 
            this.TB_RecvData1.Location = new System.Drawing.Point(5, 241);
            this.TB_RecvData1.Multiline = true;
            this.TB_RecvData1.Name = "TB_RecvData1";
            this.TB_RecvData1.Size = new System.Drawing.Size(292, 29);
            this.TB_RecvData1.TabIndex = 69;
            // 
            // lab_ReData1
            // 
            this.lab_ReData1.AutoSize = true;
            this.lab_ReData1.Location = new System.Drawing.Point(4, 226);
            this.lab_ReData1.Name = "lab_ReData1";
            this.lab_ReData1.Size = new System.Drawing.Size(53, 12);
            this.lab_ReData1.TabIndex = 68;
            this.lab_ReData1.Text = "Recv Data";
            // 
            // TB_Command1
            // 
            this.TB_Command1.Location = new System.Drawing.Point(5, 189);
            this.TB_Command1.Multiline = true;
            this.TB_Command1.Name = "TB_Command1";
            this.TB_Command1.Size = new System.Drawing.Size(292, 31);
            this.TB_Command1.TabIndex = 67;
            // 
            // btn_Send1
            // 
            this.btn_Send1.Location = new System.Drawing.Point(222, 163);
            this.btn_Send1.Name = "btn_Send1";
            this.btn_Send1.Size = new System.Drawing.Size(75, 21);
            this.btn_Send1.TabIndex = 66;
            this.btn_Send1.Text = "Send";
            this.btn_Send1.UseVisualStyleBackColor = true;
            this.btn_Send1.Click += new System.EventHandler(this.btn_Send1_Click);
            // 
            // btn_ClearSendData1
            // 
            this.btn_ClearSendData1.Location = new System.Drawing.Point(141, 163);
            this.btn_ClearSendData1.Name = "btn_ClearSendData1";
            this.btn_ClearSendData1.Size = new System.Drawing.Size(75, 21);
            this.btn_ClearSendData1.TabIndex = 65;
            this.btn_ClearSendData1.Text = "Clear";
            this.btn_ClearSendData1.UseVisualStyleBackColor = true;
            this.btn_ClearSendData1.Click += new System.EventHandler(this.btn_ClearSendData1_Click);
            // 
            // CB_Listen1
            // 
            this.CB_Listen1.AutoSize = true;
            this.CB_Listen1.Checked = true;
            this.CB_Listen1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CB_Listen1.Location = new System.Drawing.Point(5, 167);
            this.CB_Listen1.Name = "CB_Listen1";
            this.CB_Listen1.Size = new System.Drawing.Size(52, 16);
            this.CB_Listen1.TabIndex = 64;
            this.CB_Listen1.Text = "Listen";
            this.CB_Listen1.UseVisualStyleBackColor = true;
            this.CB_Listen1.CheckedChanged += new System.EventHandler(this.CB_Listen1_CheckedChanged);
            // 
            // TB_SendData1
            // 
            this.TB_SendData1.Location = new System.Drawing.Point(5, 122);
            this.TB_SendData1.Multiline = true;
            this.TB_SendData1.Name = "TB_SendData1";
            this.TB_SendData1.Size = new System.Drawing.Size(292, 36);
            this.TB_SendData1.TabIndex = 63;
            this.TB_SendData1.Text = "1,PTP(\"CPP\",450,-122,300,180,0,90,100,200,0,false)";
            this.TB_SendData1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_SendData1_KeyDown);
            // 
            // lab_Data1
            // 
            this.lab_Data1.AutoSize = true;
            this.lab_Data1.Location = new System.Drawing.Point(4, 109);
            this.lab_Data1.Name = "lab_Data1";
            this.lab_Data1.Size = new System.Drawing.Size(74, 12);
            this.lab_Data1.TabIndex = 62;
            this.lab_Data1.Text = "Edit/Send Data";
            // 
            // btn_Disconnect1
            // 
            this.btn_Disconnect1.Location = new System.Drawing.Point(222, 47);
            this.btn_Disconnect1.Name = "btn_Disconnect1";
            this.btn_Disconnect1.Size = new System.Drawing.Size(75, 21);
            this.btn_Disconnect1.TabIndex = 61;
            this.btn_Disconnect1.Text = "Disconnect";
            this.btn_Disconnect1.UseVisualStyleBackColor = true;
            this.btn_Disconnect1.Click += new System.EventHandler(this.btn_Disconnect1_Click);
            // 
            // btn_Connect1
            // 
            this.btn_Connect1.Location = new System.Drawing.Point(222, 21);
            this.btn_Connect1.Name = "btn_Connect1";
            this.btn_Connect1.Size = new System.Drawing.Size(75, 21);
            this.btn_Connect1.TabIndex = 60;
            this.btn_Connect1.Text = "Connect";
            this.btn_Connect1.UseVisualStyleBackColor = true;
            this.btn_Connect1.Click += new System.EventHandler(this.btn_Connect1_Click);
            // 
            // TB_Port1
            // 
            this.TB_Port1.Location = new System.Drawing.Point(141, 46);
            this.TB_Port1.Name = "TB_Port1";
            this.TB_Port1.Size = new System.Drawing.Size(46, 22);
            this.TB_Port1.TabIndex = 59;
            this.TB_Port1.Text = "5890";
            // 
            // TB_IPAddress1
            // 
            this.TB_IPAddress1.Location = new System.Drawing.Point(6, 46);
            this.TB_IPAddress1.Name = "TB_IPAddress1";
            this.TB_IPAddress1.Size = new System.Drawing.Size(91, 22);
            this.TB_IPAddress1.TabIndex = 58;
            this.TB_IPAddress1.Text = "169.254.30.194";
            // 
            // LB_ConnectionStatus1
            // 
            this.LB_ConnectionStatus1.AutoSize = true;
            this.LB_ConnectionStatus1.Location = new System.Drawing.Point(139, 82);
            this.LB_ConnectionStatus1.Name = "LB_ConnectionStatus1";
            this.LB_ConnectionStatus1.Size = new System.Drawing.Size(70, 12);
            this.LB_ConnectionStatus1.TabIndex = 57;
            this.LB_ConnectionStatus1.Text = "Disconnected.";
            // 
            // lab_Status1
            // 
            this.lab_Status1.AutoSize = true;
            this.lab_Status1.Location = new System.Drawing.Point(5, 82);
            this.lab_Status1.Name = "lab_Status1";
            this.lab_Status1.Size = new System.Drawing.Size(92, 12);
            this.lab_Status1.TabIndex = 24;
            this.lab_Status1.Text = "Connection Status:";
            // 
            // lab_Port1
            // 
            this.lab_Port1.AutoSize = true;
            this.lab_Port1.Location = new System.Drawing.Point(139, 23);
            this.lab_Port1.Name = "lab_Port1";
            this.lab_Port1.Size = new System.Drawing.Size(24, 12);
            this.lab_Port1.TabIndex = 25;
            this.lab_Port1.Text = "Port";
            // 
            // lab_IP1
            // 
            this.lab_IP1.AutoSize = true;
            this.lab_IP1.Location = new System.Drawing.Point(6, 23);
            this.lab_IP1.Name = "lab_IP1";
            this.lab_IP1.Size = new System.Drawing.Size(55, 12);
            this.lab_IP1.TabIndex = 26;
            this.lab_IP1.Text = "IP Address";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.TB_sp_abs1);
            this.groupBox4.Controls.Add(this.TB_sp_pc1);
            this.groupBox4.Controls.Add(this.label17);
            this.groupBox4.Controls.Add(this.label18);
            this.groupBox4.Controls.Add(this.button9);
            this.groupBox4.Location = new System.Drawing.Point(330, 350);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBox4.Size = new System.Drawing.Size(324, 79);
            this.groupBox4.TabIndex = 111;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "TM_動作";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(141, 48);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(41, 12);
            this.label15.TabIndex = 80;
            this.label15.Text = "0.1~1.0";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(141, 24);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(41, 12);
            this.label16.TabIndex = 79;
            this.label16.Text = "0.1~1.0";
            // 
            // TB_sp_abs1
            // 
            this.TB_sp_abs1.Location = new System.Drawing.Point(80, 44);
            this.TB_sp_abs1.Name = "TB_sp_abs1";
            this.TB_sp_abs1.Size = new System.Drawing.Size(55, 22);
            this.TB_sp_abs1.TabIndex = 78;
            this.TB_sp_abs1.Text = "1.0";
            this.TB_sp_abs1.TextChanged += new System.EventHandler(this.TB_sp_abs_TextChanged);
            // 
            // TB_sp_pc1
            // 
            this.TB_sp_pc1.Location = new System.Drawing.Point(80, 21);
            this.TB_sp_pc1.Name = "TB_sp_pc1";
            this.TB_sp_pc1.Size = new System.Drawing.Size(55, 22);
            this.TB_sp_pc1.TabIndex = 77;
            this.TB_sp_pc1.Text = "1.0";
            this.TB_sp_pc1.TextChanged += new System.EventHandler(this.TB_sp_pc_TextChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 47);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(56, 12);
            this.label17.TabIndex = 76;
            this.label17.Text = "絕對速度:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 24);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(68, 12);
            this.label18.TabIndex = 75;
            this.label18.Text = "百分比速度:";
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(188, 19);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(75, 23);
            this.button9.TabIndex = 72;
            this.button9.Text = "TM歸位";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.btn_TMtest_Click1);
            // 
            // BaisLB
            // 
            this.BaisLB.AutoSize = true;
            this.BaisLB.Location = new System.Drawing.Point(355, 567);
            this.BaisLB.Name = "BaisLB";
            this.BaisLB.Size = new System.Drawing.Size(25, 12);
            this.BaisLB.TabIndex = 119;
            this.BaisLB.Text = "Bais";
            // 
            // 聯軸器
            // 
            this.聯軸器.Location = new System.Drawing.Point(430, 435);
            this.聯軸器.Name = "聯軸器";
            this.聯軸器.Size = new System.Drawing.Size(67, 23);
            this.聯軸器.TabIndex = 120;
            this.聯軸器.Text = "聯軸器";
            this.聯軸器.UseVisualStyleBackColor = true;
            this.聯軸器.Click += new System.EventHandler(this.聯軸器_Click);
            // 
            // 萬象軸
            // 
            this.萬象軸.Location = new System.Drawing.Point(512, 435);
            this.萬象軸.Name = "萬象軸";
            this.萬象軸.Size = new System.Drawing.Size(67, 23);
            this.萬象軸.TabIndex = 120;
            this.萬象軸.Text = "萬象軸";
            this.萬象軸.UseVisualStyleBackColor = true;
            this.萬象軸.Click += new System.EventHandler(this.萬象軸_Click);
            // 
            // 固定座
            // 
            this.固定座.Location = new System.Drawing.Point(587, 435);
            this.固定座.Name = "固定座";
            this.固定座.Size = new System.Drawing.Size(67, 23);
            this.固定座.TabIndex = 120;
            this.固定座.Text = "固定座";
            this.固定座.UseVisualStyleBackColor = true;
            this.固定座.Click += new System.EventHandler(this.固定座_Click);
            // 
            // img_Position
            // 
            this.img_Position.AutoSize = true;
            this.img_Position.Location = new System.Drawing.Point(342, 464);
            this.img_Position.Name = "img_Position";
            this.img_Position.Size = new System.Drawing.Size(96, 16);
            this.img_Position.TabIndex = 121;
            this.img_Position.Text = "影像辨識位置";
            this.img_Position.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(437, 487);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 122;
            this.button1.Text = "GripRotation";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // NowPositionLb
            // 
            this.NowPositionLb.AutoSize = true;
            this.NowPositionLb.Location = new System.Drawing.Point(331, 552);
            this.NowPositionLb.Name = "NowPositionLb";
            this.NowPositionLb.Size = new System.Drawing.Size(77, 12);
            this.NowPositionLb.TabIndex = 118;
            this.NowPositionLb.Text = "NowPositionLb";
            // 
            // img_Label
            // 
            this.img_Label.AutoSize = true;
            this.img_Label.Location = new System.Drawing.Point(336, 534);
            this.img_Label.Name = "img_Label";
            this.img_Label.Size = new System.Drawing.Size(55, 12);
            this.img_Label.TabIndex = 117;
            this.img_Label.Text = "img_Label";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(518, 487);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 122;
            this.button2.Text = "downRatation";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // 墊高板
            // 
            this.墊高板.Location = new System.Drawing.Point(341, 435);
            this.墊高板.Name = "墊高板";
            this.墊高板.Size = new System.Drawing.Size(75, 23);
            this.墊高板.TabIndex = 123;
            this.墊高板.Text = "墊高板位置";
            this.墊高板.UseVisualStyleBackColor = true;
            this.墊高板.Click += new System.EventHandler(this.墊高板_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(599, 487);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(60, 23);
            this.button3.TabIndex = 124;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // PutDown
            // 
            this.PutDown.AutoSize = true;
            this.PutDown.Location = new System.Drawing.Point(587, 548);
            this.PutDown.Name = "PutDown";
            this.PutDown.Size = new System.Drawing.Size(67, 16);
            this.PutDown.TabIndex = 126;
            this.PutDown.Text = "PutDown";
            this.PutDown.UseVisualStyleBackColor = true;
            // 
            // GripPosition_
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1282, 644);
            this.Controls.Add(this.PutDown);
            this.Controls.Add(this.Grip_Position);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.墊高板);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.img_Position);
            this.Controls.Add(this.固定座);
            this.Controls.Add(this.萬象軸);
            this.Controls.Add(this.聯軸器);
            this.Controls.Add(this.BaisLB);
            this.Controls.Add(this.NowPositionLb);
            this.Controls.Add(this.img_Label);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Img_Btn);
            this.Name = "GripPosition_";
            this.Text = " ";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.XGE32_groupBox.ResumeLayout(false);
            this.XGE32_groupBox.PerformLayout();
            this.txtStatus.ResumeLayout(false);
            this.txtStatus.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PointDataGrid)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort1;
        private System.IO.Ports.SerialPort serialPort2;
        private System.IO.Ports.SerialPort serialPort3;
        private System.IO.Ports.SerialPort serialPort4;
        private System.IO.Ports.SerialPort oo;
        private System.Windows.Forms.Button btn_Connect;
        private System.Windows.Forms.Button btn_Disconnect;
        private System.Windows.Forms.Label lab_Data;
        private System.Windows.Forms.TextBox TB_SendData;
        private System.Windows.Forms.CheckBox CB_Listen;
        private System.Windows.Forms.Button btn_ClearSendData;
        private System.Windows.Forms.Button btn_Send;
        private System.Windows.Forms.TextBox TB_Command;
        private System.Windows.Forms.Label lab_ReData;
        private System.Windows.Forms.TextBox TB_RecvData;
        private System.Windows.Forms.Button btn_ClearRecvData;
        private System.Windows.Forms.ComboBox CB_Customized;
        private System.Windows.Forms.Button btn_TMtest;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox XGE32_groupBox;
        private System.Windows.Forms.Button XEG32_Close_bt;
        private System.Windows.Forms.Button XEG32_Open_bt;
        private System.Windows.Forms.Button XEG32_Reset_bt;
        private System.Windows.Forms.Button XEG32_Enable_bt;
        private System.Windows.Forms.Label XEG32_VelC_lab;
        private System.Windows.Forms.Label XEG32_Info_lab;
        private System.Windows.Forms.Label XEG32_VelL_lab;
        private System.Windows.Forms.Label XEG32_PosStkL_lab;
        private System.Windows.Forms.Label XEG32_PosStkC_lab;
        private System.Windows.Forms.TextBox XEG32_Vel_text;
        private System.Windows.Forms.TextBox XEG32_PosStk_text;
        private System.Windows.Forms.Button GripCnt_Btn;
        private System.Windows.Forms.Button button35;
        private System.IO.Ports.SerialPort XEG32;
        private System.Windows.Forms.GroupBox txtStatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TB_sp_abs;
        private System.Windows.Forms.TextBox TB_sp_pc;
        private System.Windows.Forms.Timer timer_rec;
        private System.Windows.Forms.RichTextBox socketmsg;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView PointDataGrid;
        private System.Windows.Forms.Button reMove;
        private System.Windows.Forms.Button exportExcel;
        private System.Windows.Forms.Button ActionBtn;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox CB_Customized1;
        private System.Windows.Forms.Button btn_ClearRecvData1;
        private System.Windows.Forms.TextBox TB_RecvData1;
        private System.Windows.Forms.Label lab_ReData1;
        private System.Windows.Forms.TextBox TB_Command1;
        private System.Windows.Forms.Button btn_Send1;
        private System.Windows.Forms.Button btn_ClearSendData1;
        private System.Windows.Forms.CheckBox CB_Listen1;
        private System.Windows.Forms.TextBox TB_SendData1;
        private System.Windows.Forms.Label lab_Data1;
        private System.Windows.Forms.Button btn_Disconnect1;
        private System.Windows.Forms.Button btn_Connect1;
        private System.Windows.Forms.TextBox TB_Port1;
        private System.Windows.Forms.TextBox TB_IPAddress1;
        private System.Windows.Forms.Label LB_ConnectionStatus1;
        private System.Windows.Forms.Label lab_Status1;
        private System.Windows.Forms.Label lab_Port1;
        private System.Windows.Forms.Label lab_IP1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox TB_sp_abs1;
        private System.Windows.Forms.TextBox TB_sp_pc1;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.TextBox TB_Port;
        private System.Windows.Forms.TextBox TB_IPAddress;
        private System.Windows.Forms.Label LB_ConnectionStatus;
        private System.Windows.Forms.Label lab_Status;
        private System.Windows.Forms.Label lab_Port;
        private System.Windows.Forms.Label lab_IP;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button AX12A_Close;
        private System.Windows.Forms.Button AX12A_Open;
        private System.Windows.Forms.Button XEG32_Close_Other;
        private System.Windows.Forms.CheckBox ImageGrip_CheckBox;
        private System.Windows.Forms.Button Img_Btn;
        private System.Windows.Forms.Label BaisLB;
        private System.Windows.Forms.Button 聯軸器;
        private System.Windows.Forms.Button 萬象軸;
        private System.Windows.Forms.Button 固定座;
        private System.Windows.Forms.CheckBox img_Position;
        private System.Windows.Forms.CheckBox Image_checkBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label AX12A_Status;
        private System.Windows.Forms.Label NowPositionLb;
        private System.Windows.Forms.Label img_Label;
        private System.Windows.Forms.DataGridViewTextBoxColumn Arm;
        private System.Windows.Forms.DataGridViewTextBoxColumn Count;
        private System.Windows.Forms.DataGridViewTextBoxColumn GripObject;
        private System.Windows.Forms.DataGridViewTextBoxColumn command;
        private System.Windows.Forms.DataGridViewTextBoxColumn x;
        private System.Windows.Forms.DataGridViewTextBoxColumn y;
        private System.Windows.Forms.DataGridViewTextBoxColumn z;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rx;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ry;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rz;
        private System.Windows.Forms.Button Clr_btn;
        private System.Windows.Forms.Button ExprotDataGrid;
        private System.Windows.Forms.CheckBox Excel;
        private System.Windows.Forms.Button Delete;
        private System.Windows.Forms.Button StopMove;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button 墊高板;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox Grip_Position;
        private System.Windows.Forms.CheckBox PutDown;
    }
}

