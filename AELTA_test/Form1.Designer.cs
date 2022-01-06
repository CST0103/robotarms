namespace ControlUI
{
    partial class Form1
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
            this.lab_Status1 = new System.Windows.Forms.Label();
            this.lab_Port = new System.Windows.Forms.Label();
            this.lab_IP = new System.Windows.Forms.Label();
            this.serialPort4 = new System.IO.Ports.SerialPort(this.components);
            this.oo = new System.IO.Ports.SerialPort(this.components);
            this.LB_ConnectionStatus = new System.Windows.Forms.Label();
            this.TB_IPAddress = new System.Windows.Forms.TextBox();
            this.TB_Port = new System.Windows.Forms.TextBox();
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
            this.XGE32_groupBox = new System.Windows.Forms.GroupBox();
            this.button35 = new System.Windows.Forms.Button();
            this.button34 = new System.Windows.Forms.Button();
            this.XEG32_Close_bt = new System.Windows.Forms.Button();
            this.XEG32_Open_bt = new System.Windows.Forms.Button();
            this.XEG32_Reset_bt = new System.Windows.Forms.Button();
            this.XEG32_Enable_bt = new System.Windows.Forms.Button();
            this.XEG32_PushPosStkC_lab = new System.Windows.Forms.Label();
            this.XEG32_PushVelC_lab = new System.Windows.Forms.Label();
            this.XEG32_Forcec_lab = new System.Windows.Forms.Label();
            this.XEG32_CJogC_lab = new System.Windows.Forms.Label();
            this.XEG32_VelC_lab = new System.Windows.Forms.Label();
            this.XEG32_Info_lab = new System.Windows.Forms.Label();
            this.XEG32_VelL_lab = new System.Windows.Forms.Label();
            this.XEG32_PosStkL_lab = new System.Windows.Forms.Label();
            this.XEG32_ForceL_lab = new System.Windows.Forms.Label();
            this.XEG32_PosStkC_lab = new System.Windows.Forms.Label();
            this.XEG32_PushPosStk_text = new System.Windows.Forms.TextBox();
            this.XEG32_PushVel_text = new System.Windows.Forms.TextBox();
            this.XEG32_CJog_text = new System.Windows.Forms.TextBox();
            this.XEG32_Vel_text = new System.Windows.Forms.TextBox();
            this.XEG32_PosStk_text = new System.Windows.Forms.TextBox();
            this.XEG32_Force_text = new System.Windows.Forms.TextBox();
            this.XEG32 = new System.IO.Ports.SerialPort(this.components);
            this.txtStatus = new System.Windows.Forms.GroupBox();
            this.button39 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.TB_sp_abs = new System.Windows.Forms.TextBox();
            this.TB_sp_pc = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button38 = new System.Windows.Forms.Button();
            this.button37 = new System.Windows.Forms.Button();
            this.socketmsg = new System.Windows.Forms.RichTextBox();
            this.timer_rec = new System.Windows.Forms.Timer(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.PointDataGrid = new System.Windows.Forms.DataGridView();
            this.number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.x = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.z = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rx = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ry = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rz = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reMove = new System.Windows.Forms.Button();
            this.exportExcel = new System.Windows.Forms.Button();
            this.ActionBtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.XGE32_groupBox.SuspendLayout();
            this.txtStatus.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PointDataGrid)).BeginInit();
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
            // lab_Status1
            // 
            this.lab_Status1.AutoSize = true;
            this.lab_Status1.Location = new System.Drawing.Point(5, 82);
            this.lab_Status1.Name = "lab_Status1";
            this.lab_Status1.Size = new System.Drawing.Size(92, 12);
            this.lab_Status1.TabIndex = 24;
            this.lab_Status1.Text = "Connection Status:";
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
            // serialPort4
            // 
            this.serialPort4.BaudRate = 57142;
            this.serialPort4.PortName = "COM17";
            // 
            // oo
            // 
            this.oo.PortName = "COM10";
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
            // TB_IPAddress
            // 
            this.TB_IPAddress.Location = new System.Drawing.Point(6, 46);
            this.TB_IPAddress.Name = "TB_IPAddress";
            this.TB_IPAddress.Size = new System.Drawing.Size(91, 22);
            this.TB_IPAddress.TabIndex = 58;
            this.TB_IPAddress.Text = "169.254.119.180";
            // 
            // TB_Port
            // 
            this.TB_Port.Location = new System.Drawing.Point(141, 46);
            this.TB_Port.Name = "TB_Port";
            this.TB_Port.Size = new System.Drawing.Size(46, 22);
            this.TB_Port.TabIndex = 59;
            this.TB_Port.Text = "5890";
            // 
            // btn_Connect
            // 
            this.btn_Connect.Location = new System.Drawing.Point(258, 24);
            this.btn_Connect.Name = "btn_Connect";
            this.btn_Connect.Size = new System.Drawing.Size(75, 21);
            this.btn_Connect.TabIndex = 60;
            this.btn_Connect.Text = "Connect";
            this.btn_Connect.UseVisualStyleBackColor = true;
            this.btn_Connect.Click += new System.EventHandler(this.btn_Connect_Click);
            // 
            // btn_Disconnect
            // 
            this.btn_Disconnect.Location = new System.Drawing.Point(258, 48);
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
            this.TB_SendData.Size = new System.Drawing.Size(328, 53);
            this.TB_SendData.TabIndex = 63;
            this.TB_SendData.Text = "1,PTP(\"CPP\",519,-122,458,185,0,90,100,200,0,false)";
            this.TB_SendData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_SendData_KeyDown_1);
            // 
            // CB_Listen
            // 
            this.CB_Listen.AutoSize = true;
            this.CB_Listen.Checked = true;
            this.CB_Listen.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CB_Listen.Location = new System.Drawing.Point(5, 184);
            this.CB_Listen.Name = "CB_Listen";
            this.CB_Listen.Size = new System.Drawing.Size(52, 16);
            this.CB_Listen.TabIndex = 64;
            this.CB_Listen.Text = "Listen";
            this.CB_Listen.UseVisualStyleBackColor = true;
            this.CB_Listen.CheckedChanged += new System.EventHandler(this.CB_Listen_CheckedChanged);
            // 
            // btn_ClearSendData
            // 
            this.btn_ClearSendData.Location = new System.Drawing.Point(177, 181);
            this.btn_ClearSendData.Name = "btn_ClearSendData";
            this.btn_ClearSendData.Size = new System.Drawing.Size(75, 21);
            this.btn_ClearSendData.TabIndex = 65;
            this.btn_ClearSendData.Text = "Clear";
            this.btn_ClearSendData.UseVisualStyleBackColor = true;
            this.btn_ClearSendData.Click += new System.EventHandler(this.btn_ClearSendData_Click);
            // 
            // btn_Send
            // 
            this.btn_Send.Location = new System.Drawing.Point(258, 181);
            this.btn_Send.Name = "btn_Send";
            this.btn_Send.Size = new System.Drawing.Size(75, 21);
            this.btn_Send.TabIndex = 66;
            this.btn_Send.Text = "Send";
            this.btn_Send.UseVisualStyleBackColor = true;
            this.btn_Send.Click += new System.EventHandler(this.btn_Send_Click);
            // 
            // TB_Command
            // 
            this.TB_Command.Location = new System.Drawing.Point(5, 211);
            this.TB_Command.Multiline = true;
            this.TB_Command.Name = "TB_Command";
            this.TB_Command.Size = new System.Drawing.Size(328, 40);
            this.TB_Command.TabIndex = 67;
            // 
            // lab_ReData
            // 
            this.lab_ReData.AutoSize = true;
            this.lab_ReData.Location = new System.Drawing.Point(8, 254);
            this.lab_ReData.Name = "lab_ReData";
            this.lab_ReData.Size = new System.Drawing.Size(53, 12);
            this.lab_ReData.TabIndex = 68;
            this.lab_ReData.Text = "Recv Data";
            // 
            // TB_RecvData
            // 
            this.TB_RecvData.Location = new System.Drawing.Point(5, 269);
            this.TB_RecvData.Multiline = true;
            this.TB_RecvData.Name = "TB_RecvData";
            this.TB_RecvData.Size = new System.Drawing.Size(328, 52);
            this.TB_RecvData.TabIndex = 69;
            // 
            // btn_ClearRecvData
            // 
            this.btn_ClearRecvData.Location = new System.Drawing.Point(258, 330);
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
            this.CB_Customized.Location = new System.Drawing.Point(61, 181);
            this.CB_Customized.Name = "CB_Customized";
            this.CB_Customized.Size = new System.Drawing.Size(67, 20);
            this.CB_Customized.TabIndex = 71;
            // 
            // btn_TMtest
            // 
            this.btn_TMtest.Location = new System.Drawing.Point(188, 19);
            this.btn_TMtest.Name = "btn_TMtest";
            this.btn_TMtest.Size = new System.Drawing.Size(75, 23);
            this.btn_TMtest.TabIndex = 72;
            this.btn_TMtest.Text = "TM歸位";
            this.btn_TMtest.UseVisualStyleBackColor = true;
            this.btn_TMtest.Click += new System.EventHandler(this.btn_TMtest_Click);
            // 
            // groupBox1
            // 
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
            this.groupBox1.Controls.Add(this.lab_Status1);
            this.groupBox1.Controls.Add(this.lab_Port);
            this.groupBox1.Controls.Add(this.lab_IP);
            this.groupBox1.Location = new System.Drawing.Point(12, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(339, 363);
            this.groupBox1.TabIndex = 73;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "TM_TCP";
            // 
            // XGE32_groupBox
            // 
            this.XGE32_groupBox.Controls.Add(this.button35);
            this.XGE32_groupBox.Controls.Add(this.button34);
            this.XGE32_groupBox.Controls.Add(this.XEG32_Close_bt);
            this.XGE32_groupBox.Controls.Add(this.XEG32_Open_bt);
            this.XGE32_groupBox.Controls.Add(this.XEG32_Reset_bt);
            this.XGE32_groupBox.Controls.Add(this.XEG32_Enable_bt);
            this.XGE32_groupBox.Controls.Add(this.XEG32_PushPosStkC_lab);
            this.XGE32_groupBox.Controls.Add(this.XEG32_PushVelC_lab);
            this.XGE32_groupBox.Controls.Add(this.XEG32_Forcec_lab);
            this.XGE32_groupBox.Controls.Add(this.XEG32_CJogC_lab);
            this.XGE32_groupBox.Controls.Add(this.XEG32_VelC_lab);
            this.XGE32_groupBox.Controls.Add(this.XEG32_Info_lab);
            this.XGE32_groupBox.Controls.Add(this.XEG32_VelL_lab);
            this.XGE32_groupBox.Controls.Add(this.XEG32_PosStkL_lab);
            this.XGE32_groupBox.Controls.Add(this.XEG32_ForceL_lab);
            this.XGE32_groupBox.Controls.Add(this.XEG32_PosStkC_lab);
            this.XGE32_groupBox.Controls.Add(this.XEG32_PushPosStk_text);
            this.XGE32_groupBox.Controls.Add(this.XEG32_PushVel_text);
            this.XGE32_groupBox.Controls.Add(this.XEG32_CJog_text);
            this.XGE32_groupBox.Controls.Add(this.XEG32_Vel_text);
            this.XGE32_groupBox.Controls.Add(this.XEG32_PosStk_text);
            this.XGE32_groupBox.Controls.Add(this.XEG32_Force_text);
            this.XGE32_groupBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.XGE32_groupBox.Location = new System.Drawing.Point(8, 82);
            this.XGE32_groupBox.Margin = new System.Windows.Forms.Padding(2);
            this.XGE32_groupBox.Name = "XGE32_groupBox";
            this.XGE32_groupBox.Padding = new System.Windows.Forms.Padding(2);
            this.XGE32_groupBox.Size = new System.Drawing.Size(339, 227);
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
            // button34
            // 
            this.button34.Location = new System.Drawing.Point(8, 21);
            this.button34.Name = "button34";
            this.button34.Size = new System.Drawing.Size(75, 28);
            this.button34.TabIndex = 19;
            this.button34.Text = "Connect";
            this.button34.UseVisualStyleBackColor = true;
            this.button34.Click += new System.EventHandler(this.button34_Click);
            // 
            // XEG32_Close_bt
            // 
            this.XEG32_Close_bt.Enabled = false;
            this.XEG32_Close_bt.Location = new System.Drawing.Point(8, 174);
            this.XEG32_Close_bt.Name = "XEG32_Close_bt";
            this.XEG32_Close_bt.Size = new System.Drawing.Size(75, 28);
            this.XEG32_Close_bt.TabIndex = 18;
            this.XEG32_Close_bt.Text = "關夾爪";
            this.XEG32_Close_bt.UseVisualStyleBackColor = true;
            this.XEG32_Close_bt.Click += new System.EventHandler(this.XEG32_Close_bt_Click);
            // 
            // XEG32_Open_bt
            // 
            this.XEG32_Open_bt.Location = new System.Drawing.Point(8, 138);
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
            this.XEG32_Enable_bt.Location = new System.Drawing.Point(254, 149);
            this.XEG32_Enable_bt.Name = "XEG32_Enable_bt";
            this.XEG32_Enable_bt.Size = new System.Drawing.Size(75, 48);
            this.XEG32_Enable_bt.TabIndex = 16;
            this.XEG32_Enable_bt.Text = "Enable";
            this.XEG32_Enable_bt.UseVisualStyleBackColor = true;
            this.XEG32_Enable_bt.Click += new System.EventHandler(this.XEG32_Enable_bt_Click);
            // 
            // XEG32_PushPosStkC_lab
            // 
            this.XEG32_PushPosStkC_lab.AutoSize = true;
            this.XEG32_PushPosStkC_lab.Location = new System.Drawing.Point(83, 177);
            this.XEG32_PushPosStkC_lab.Name = "XEG32_PushPosStkC_lab";
            this.XEG32_PushPosStkC_lab.Size = new System.Drawing.Size(58, 12);
            this.XEG32_PushPosStkC_lab.TabIndex = 9;
            this.XEG32_PushPosStkC_lab.Text = "PushPosStk";
            // 
            // XEG32_PushVelC_lab
            // 
            this.XEG32_PushVelC_lab.AutoSize = true;
            this.XEG32_PushVelC_lab.Location = new System.Drawing.Point(89, 149);
            this.XEG32_PushVelC_lab.Name = "XEG32_PushVelC_lab";
            this.XEG32_PushVelC_lab.Size = new System.Drawing.Size(43, 12);
            this.XEG32_PushVelC_lab.TabIndex = 10;
            this.XEG32_PushVelC_lab.Text = "PushVel";
            // 
            // XEG32_Forcec_lab
            // 
            this.XEG32_Forcec_lab.AutoSize = true;
            this.XEG32_Forcec_lab.Location = new System.Drawing.Point(102, 107);
            this.XEG32_Forcec_lab.Name = "XEG32_Forcec_lab";
            this.XEG32_Forcec_lab.Size = new System.Drawing.Size(17, 12);
            this.XEG32_Forcec_lab.TabIndex = 11;
            this.XEG32_Forcec_lab.Text = "力";
            // 
            // XEG32_CJogC_lab
            // 
            this.XEG32_CJogC_lab.AutoSize = true;
            this.XEG32_CJogC_lab.Location = new System.Drawing.Point(97, 93);
            this.XEG32_CJogC_lab.Name = "XEG32_CJogC_lab";
            this.XEG32_CJogC_lab.Size = new System.Drawing.Size(29, 12);
            this.XEG32_CJogC_lab.TabIndex = 12;
            this.XEG32_CJogC_lab.Text = "CJog";
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
            // XEG32_ForceL_lab
            // 
            this.XEG32_ForceL_lab.AutoSize = true;
            this.XEG32_ForceL_lab.Location = new System.Drawing.Point(252, 121);
            this.XEG32_ForceL_lab.Name = "XEG32_ForceL_lab";
            this.XEG32_ForceL_lab.Size = new System.Drawing.Size(14, 12);
            this.XEG32_ForceL_lab.TabIndex = 14;
            this.XEG32_ForceL_lab.Text = "%";
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
            // XEG32_PushPosStk_text
            // 
            this.XEG32_PushPosStk_text.Location = new System.Drawing.Point(146, 174);
            this.XEG32_PushPosStk_text.Name = "XEG32_PushPosStk_text";
            this.XEG32_PushPosStk_text.Size = new System.Drawing.Size(100, 22);
            this.XEG32_PushPosStk_text.TabIndex = 3;
            this.XEG32_PushPosStk_text.Text = "0";
            // 
            // XEG32_PushVel_text
            // 
            this.XEG32_PushVel_text.Location = new System.Drawing.Point(146, 146);
            this.XEG32_PushVel_text.Name = "XEG32_PushVel_text";
            this.XEG32_PushVel_text.Size = new System.Drawing.Size(100, 22);
            this.XEG32_PushVel_text.TabIndex = 4;
            this.XEG32_PushVel_text.Text = "0";
            // 
            // XEG32_CJog_text
            // 
            this.XEG32_CJog_text.Location = new System.Drawing.Point(146, 90);
            this.XEG32_CJog_text.Name = "XEG32_CJog_text";
            this.XEG32_CJog_text.Size = new System.Drawing.Size(100, 22);
            this.XEG32_CJog_text.TabIndex = 6;
            this.XEG32_CJog_text.Text = "0";
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
            // XEG32_Force_text
            // 
            this.XEG32_Force_text.Location = new System.Drawing.Point(146, 118);
            this.XEG32_Force_text.Name = "XEG32_Force_text";
            this.XEG32_Force_text.Size = new System.Drawing.Size(100, 22);
            this.XEG32_Force_text.TabIndex = 5;
            this.XEG32_Force_text.Text = "70";
            // 
            // XEG32
            // 
            this.XEG32.BaudRate = 115200;
            this.XEG32.PortName = "COM3";
            // 
            // txtStatus
            // 
            this.txtStatus.Controls.Add(this.XGE32_groupBox);
            this.txtStatus.Controls.Add(this.button39);
            this.txtStatus.Controls.Add(this.label6);
            this.txtStatus.Controls.Add(this.label5);
            this.txtStatus.Controls.Add(this.TB_sp_abs);
            this.txtStatus.Controls.Add(this.TB_sp_pc);
            this.txtStatus.Controls.Add(this.label4);
            this.txtStatus.Controls.Add(this.label3);
            this.txtStatus.Controls.Add(this.button38);
            this.txtStatus.Controls.Add(this.btn_TMtest);
            this.txtStatus.Controls.Add(this.button37);
            this.txtStatus.Location = new System.Drawing.Point(357, 12);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtStatus.Size = new System.Drawing.Size(357, 364);
            this.txtStatus.TabIndex = 111;
            this.txtStatus.TabStop = false;
            this.txtStatus.Text = "TM_動作";
            // 
            // button39
            // 
            this.button39.Location = new System.Drawing.Point(267, 45);
            this.button39.Name = "button39";
            this.button39.Size = new System.Drawing.Size(75, 23);
            this.button39.TabIndex = 81;
            this.button39.Text = "TM_Line2";
            this.button39.UseVisualStyleBackColor = true;
            this.button39.Click += new System.EventHandler(this.button39_Click);
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
            // button38
            // 
            this.button38.Location = new System.Drawing.Point(188, 46);
            this.button38.Name = "button38";
            this.button38.Size = new System.Drawing.Size(75, 23);
            this.button38.TabIndex = 74;
            this.button38.Text = "Circle_test2";
            this.button38.UseVisualStyleBackColor = true;
            this.button38.Click += new System.EventHandler(this.button38_Click);
            // 
            // button37
            // 
            this.button37.Location = new System.Drawing.Point(267, 19);
            this.button37.Name = "button37";
            this.button37.Size = new System.Drawing.Size(75, 23);
            this.button37.TabIndex = 73;
            this.button37.Text = "TM_Line1";
            this.button37.UseVisualStyleBackColor = true;
            this.button37.Click += new System.EventHandler(this.button37_Click);
            // 
            // socketmsg
            // 
            this.socketmsg.Location = new System.Drawing.Point(6, 48);
            this.socketmsg.Name = "socketmsg";
            this.socketmsg.Size = new System.Drawing.Size(366, 133);
            this.socketmsg.TabIndex = 113;
            this.socketmsg.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.socketmsg);
            this.groupBox2.Location = new System.Drawing.Point(728, 17);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(378, 188);
            this.groupBox2.TabIndex = 115;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // PointDataGrid
            // 
            this.PointDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PointDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.number,
            this.x,
            this.y,
            this.z,
            this.Rx,
            this.Ry,
            this.Rz});
            this.PointDataGrid.Location = new System.Drawing.Point(728, 211);
            this.PointDataGrid.Name = "PointDataGrid";
            this.PointDataGrid.RowTemplate.Height = 24;
            this.PointDataGrid.Size = new System.Drawing.Size(527, 229);
            this.PointDataGrid.TabIndex = 125;
            // 
            // number
            // 
            this.number.HeaderText = "unmber";
            this.number.Name = "number";
            this.number.Width = 50;
            // 
            // x
            // 
            this.x.HeaderText = "x";
            this.x.Name = "x";
            this.x.Width = 70;
            // 
            // y
            // 
            this.y.HeaderText = "y";
            this.y.Name = "y";
            this.y.Width = 70;
            // 
            // z
            // 
            this.z.HeaderText = "z";
            this.z.Name = "z";
            this.z.Width = 70;
            // 
            // Rx
            // 
            this.Rx.HeaderText = "Rx";
            this.Rx.Name = "Rx";
            this.Rx.Width = 70;
            // 
            // Ry
            // 
            this.Ry.HeaderText = "Ry";
            this.Ry.Name = "Ry";
            this.Ry.Width = 70;
            // 
            // Rz
            // 
            this.Rz.HeaderText = "Rz";
            this.Rz.Name = "Rz";
            this.Rz.Width = 70;
            // 
            // reMove
            // 
            this.reMove.Location = new System.Drawing.Point(1112, 37);
            this.reMove.Name = "reMove";
            this.reMove.Size = new System.Drawing.Size(75, 23);
            this.reMove.TabIndex = 115;
            this.reMove.Text = "重新發送";
            this.reMove.UseVisualStyleBackColor = true;
            this.reMove.Click += new System.EventHandler(this.armReMove);
            // 
            // exportExcel
            // 
            this.exportExcel.Location = new System.Drawing.Point(1112, 165);
            this.exportExcel.Name = "exportExcel";
            this.exportExcel.Size = new System.Drawing.Size(75, 23);
            this.exportExcel.TabIndex = 126;
            this.exportExcel.Text = "ExportExcel";
            this.exportExcel.UseVisualStyleBackColor = true;
            this.exportExcel.Click += new System.EventHandler(this.exportExcel_Click);
            // 
            // ActionBtn
            // 
            this.ActionBtn.Location = new System.Drawing.Point(1112, 134);
            this.ActionBtn.Name = "ActionBtn";
            this.ActionBtn.Size = new System.Drawing.Size(75, 21);
            this.ActionBtn.TabIndex = 127;
            this.ActionBtn.Text = "ActionButton";
            this.ActionBtn.UseVisualStyleBackColor = true;
            this.ActionBtn.Click += new System.EventHandler(this.ActionBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1303, 639);
            this.Controls.Add(this.ActionBtn);
            this.Controls.Add(this.exportExcel);
            this.Controls.Add(this.reMove);
            this.Controls.Add(this.PointDataGrid);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = " ";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.XGE32_groupBox.ResumeLayout(false);
            this.XGE32_groupBox.PerformLayout();
            this.txtStatus.ResumeLayout(false);
            this.txtStatus.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PointDataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort1;
        private System.IO.Ports.SerialPort serialPort2;
        private System.IO.Ports.SerialPort serialPort3;
        private System.Windows.Forms.Label lab_Status1;
        private System.Windows.Forms.Label lab_Port;
        private System.Windows.Forms.Label lab_IP;
        private System.IO.Ports.SerialPort serialPort4;
        private System.IO.Ports.SerialPort oo;
        private System.Windows.Forms.Label LB_ConnectionStatus;
        private System.Windows.Forms.TextBox TB_IPAddress;
        private System.Windows.Forms.TextBox TB_Port;
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
        private System.Windows.Forms.Label XEG32_PushPosStkC_lab;
        private System.Windows.Forms.Label XEG32_PushVelC_lab;
        private System.Windows.Forms.Label XEG32_Forcec_lab;
        private System.Windows.Forms.Label XEG32_CJogC_lab;
        private System.Windows.Forms.Label XEG32_VelC_lab;
        private System.Windows.Forms.Label XEG32_Info_lab;
        private System.Windows.Forms.Label XEG32_VelL_lab;
        private System.Windows.Forms.Label XEG32_PosStkL_lab;
        private System.Windows.Forms.Label XEG32_ForceL_lab;
        private System.Windows.Forms.Label XEG32_PosStkC_lab;
        private System.Windows.Forms.TextBox XEG32_PushPosStk_text;
        private System.Windows.Forms.TextBox XEG32_PushVel_text;
        private System.Windows.Forms.TextBox XEG32_CJog_text;
        private System.Windows.Forms.TextBox XEG32_Vel_text;
        private System.Windows.Forms.TextBox XEG32_PosStk_text;
        private System.Windows.Forms.TextBox XEG32_Force_text;
        private System.Windows.Forms.Button button34;
        private System.Windows.Forms.Button button35;
        private System.IO.Ports.SerialPort XEG32;
        private System.Windows.Forms.GroupBox txtStatus;
        private System.Windows.Forms.Button button37;
        private System.Windows.Forms.Button button38;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TB_sp_abs;
        private System.Windows.Forms.TextBox TB_sp_pc;
        private System.Windows.Forms.Button button39;
        private System.Windows.Forms.Timer timer_rec;
        private System.Windows.Forms.RichTextBox socketmsg;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView PointDataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn number;
        private System.Windows.Forms.DataGridViewTextBoxColumn x;
        private System.Windows.Forms.DataGridViewTextBoxColumn y;
        private System.Windows.Forms.DataGridViewTextBoxColumn z;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rx;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ry;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rz;
        private System.Windows.Forms.Button reMove;
        private System.Windows.Forms.Button exportExcel;
        private System.Windows.Forms.Button ActionBtn;
    }
}

