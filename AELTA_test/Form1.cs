﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
//使用XEG-32夾爪 請加入此參考
using System.IO.Ports;
//
using System.Runtime.InteropServices;
using System.Reflection;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Net;
using System.Collections;


using ClosedXML;
using System.Net.Sockets;
using ClosedXML.Excel;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Input;
//using System.Windows.Markup;

#region --SDK--

/*------------兼容ZLG的数据类型---------------------------------*/

//1.ZLGCAN系列接口卡信息的数据类型。
public struct VCI_BOARD_INFO
{
    public UInt16 hw_Version;
    public UInt16 fw_Version;
    public UInt16 dr_Version;
    public UInt16 in_Version;
    public UInt16 irq_Num;
    public byte can_Num;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
    public byte[] str_Serial_Num;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
    public byte[] str_hw_Type;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public byte[] Reserved;
}

/////////////////////////////////////////////////////
//2.定义CAN信息帧的数据类型。
unsafe public struct VCI_CAN_OBJ  //使用不安全代码
{
    public uint ID;
    public uint TimeStamp;        //时间标识
    public byte TimeFlag;         //是否使用时间标识
    public byte SendType;         //发送标志。保留，未用
    public byte RemoteFlag;       //是否是远程帧
    public byte ExternFlag;       //是否是扩展帧
    public byte DataLen;          //数据长度
    public fixed byte Data[8];    //数据
    public fixed byte Reserved[3];//保留位

}

//3.定义初始化CAN的数据类型
public struct VCI_INIT_CONFIG
{
    public UInt32 AccCode;
    public UInt32 AccMask;
    public UInt32 Reserved;
    public byte Filter;   //0或1接收所有帧。2标准帧滤波，3是扩展帧滤波。
    public byte Timing0;  //波特率参数，具体配置，请查看二次开发库函数说明书。
    public byte Timing1;
    public byte Mode;     //模式，0表示正常模式，1表示只听模式,2自测模式
}

/*------------其他数据结构描述---------------------------------*/
//4.USB-CAN总线适配器板卡信息的数据类型1，该类型为VCI_FindUsbDevice函数的返回参数。
public struct VCI_BOARD_INFO1
{
    public UInt16 hw_Version;
    public UInt16 fw_Version;
    public UInt16 dr_Version;
    public UInt16 in_Version;
    public UInt16 irq_Num;
    public byte can_Num;
    public byte Reserved;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public byte[] str_Serial_Num;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
    public byte[] str_hw_Type;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
    public byte[] str_Usb_Serial;
}

/*------------数据结构描述完成---------------------------------*/

public struct CHGDESIPANDPORT
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
    public byte[] szpwd;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
    public byte[] szdesip;
    public Int32 desport;

    public void Init()
    {
        szpwd = new byte[10];
        szdesip = new byte[20];
    }
}
#endregion

namespace ControlUI
{
    public partial class Form1 : Form
    {
        #region 宣告
        //TM_1
        private StringBuilder showRecvDataLog = new StringBuilder();
        private SocketClientObject TCPClientObject;
        //TM_2
        private StringBuilder showRecvDataLog1 = new StringBuilder();
        private SocketClientObject TCPClientObject1;

        //Declare and Initialize the IP Adress
        static IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
        static IPAddress ipAd = host.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);

        //Declare and Initilize the Port Number;
        static int PortNumber = 1001;
        string[] data = new string[10];
        double[] dataint = new double[] { 0, 0, 0, 0, 0, 0 };

        /* Initializes the Listener */
        TCP_Listener FirstListener = new TCP_Listener(ipAd.ToString(), PortNumber);
        //TCP_Listener FirstListener = new TCP_Listener("192.168.100.213", PortNumber);
        TCP_Listener SecondListener = new TCP_Listener(ipAd.ToString(), PortNumber + 1);
        TcpClient clientSocket = default(TcpClient);

        private delegate void AddConnectDataDelegate(string _connectstatus, Label _label);
        private delegate void AddReceiveDataDelegate(string _receivedata, TextBox _textbox);
        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CB_Customized.SelectedIndex = 0;
            CB_Customized1.SelectedIndex = 0;

            Thread FirstArmSever = new Thread(FirstSever);
            Thread SecondArmSever = new Thread(SecondSever);
            FirstArmSever.Start();
            SecondArmSever.Start();
        }

        #region --通訊--
        private void FirstSever()
        {
            int Count = 0;
            Action<String> ModifyText = SockMsg;
            Action<double, double, double, double, double, double,bool> Tolist = CoordinateConversion;
            string Declare;
            bool _open_flag = FirstListener.Start();
            Declare = _open_flag == true ? "FirstSever Open" : "FirstSever Not Open";
            Invoke(ModifyText, Declare);

            bool _connect_flag = FirstListener.Connect();
            Declare = _connect_flag == true ? "FirstSever Connect" : "FirstSever Not Connect";
            Invoke(ModifyText, Declare);
            try
            {

                while (true)
                {
                    string reciveData = FirstListener.Recive();
                    Invoke(ModifyText, Count.ToString() + "  First Command");
                    string[] Point = reciveData.Split('$');
                    Invoke(Tolist,
                        Convert.ToDouble(Point[1]),
                        Convert.ToDouble(Point[2]),
                        Convert.ToDouble(Point[3]),
                        Convert.ToDouble(Point[4]),
                        Convert.ToDouble(Point[5]),
                        Convert.ToDouble(Point[6]),
                        true);
                }
            }
            catch { }
        }

        private void SecondSever()
        {
            int Count = 0;
            Action<String> ModifyText = SockMsg;
            Action<double, double, double, double, double, double,bool> Tolist = CoordinateConversion;
            string Declare;
            bool _open_flag = SecondListener.Start();
            Declare = _open_flag == true ? "SecondSever Open" : "SecondSever Not Open";
            Invoke(ModifyText, Declare);

            bool _connect_flag = SecondListener.Connect();
            Declare = _connect_flag == true ? "SecondSever Connect" : "SecondSever Not Connect";
            Invoke(ModifyText, Declare);
            while (true)
            {
                string reciveData = SecondListener.Recive();
                Invoke(ModifyText, Count.ToString() + "  Second Command");
                string[] Point = reciveData.Split('$');
                Invoke(Tolist,
                    Convert.ToDouble(Point[1]),
                    Convert.ToDouble(Point[2]),
                    Convert.ToDouble(Point[3]),
                    Convert.ToDouble(Point[4]),
                    Convert.ToDouble(Point[5]),
                    Convert.ToDouble(Point[6]),
                    false);
            }
        }
        protected void SockMsg<T>(T teste)
        {
            socketmsg.Text += Environment.NewLine + teste.ToString();

            socketmsg.SelectionStart = socketmsg.TextLength;
            socketmsg.ScrollToCaret();
        }

        #endregion

        #region --座標修正--
        /*
         * zeropoint 450 -122 300 180 0 90
         * 
         * x 70~600
         * y -600~600
         * z -45~400
         * 
         *      左       前       右       後       正上
         * Rx   180     -90     -180       93
         * Ry   90      -6       -90        -7
         * Rz   90       92      -50        89       90
         *         
         * 
         * 
         * z(-90 > 0 > 90) = x(-90 > 180 > 90)
         * x(-90 > 0 > 90) = y(-90 > 0 > 90)
         * y(-90 > 0 > 90) = z(180 > 90 > 0)
         * 
         * Rz > Rx  
         * Rx > -Ry
         * Ry > Rz
         * 
         * Rz Rx
         * 0~-180 = 180 ~ 0
         * 0 ~ 180 = -180 ~ 0
         * 
         * >0 Rx = -180 + Rz
         * <0 Rx = 180 + Rz
         * 
         * Ry = -Rx
         * 
         * Ry Rz
         * Rz = 90-Ry
         * 
         * */
        int CoordinateConversionCount = 0;
        private void CoordinateConversion(double d1, double d2, double d3, double d4, double d5, double d6, bool ArmFlag)
        {
            double[] origin = new double[6] { 450, -122, 300, 180, 0, 90 };

            Action<int, double, double, double, double, double, double,bool> DataToGrid = WriteDataGrid;
            Action<string, string, string, string, string, string,bool> Toarm = armMove;



            int xmax, xmin, ymax, ymin, zmax, zmin, xbias, ybias, zbias;

            if (ArmFlag)
            {
                xmax = 750;
                xmin = 70;
                ymax = 550;
                ymin = -550;
                zmax = 400;
                zmin = -45;

                xbias = 450;
                ybias = -122;
                zbias = 300;
            }
            else
            {
                xmax = 750;
                xmin = 70;
                ymax = 550;
                ymin = -500;
                zmax = 600;
                zmin = -45;
                xbias = 450;
                ybias = -122;
                zbias = 430;
            }

            double RotateBuffer_x = 180, RotateBuffer_y = 0, RotateBuffer_z = 90;

            int x, y, z;

            x = Convert.ToInt32(d1) + xbias;
            y = Convert.ToInt32(d2) + ybias;
            z = Convert.ToInt32(d3) + zbias;

            d1 = x;
            d2 = y;
            d3 = z;

            RotateBuffer_x = 180 - d4;
            RotateBuffer_y = d5;
            RotateBuffer_z = 90 - d6;

            d4 = RotateBuffer_x;
            d5 = RotateBuffer_y;
            d6 = RotateBuffer_z;


            if (d1 > xmax)
                d1 = xmax;
            if (d1 < xmin)
                d1 = xmin;

            if (d2 > ymax)
                d2 = ymax;
            if (d2 < ymin)
                d2 = ymin;

            if (d3 > zmax)
                d3 = zmax;
            if (d3 < zmin)
                d3 = zmin;

            Invoke(DataToGrid, CoordinateConversionCount, d1, d2, d3, d4, d5, d6,ArmFlag);
            Invoke(Toarm, d1.ToString(), d2.ToString(), d3.ToString(), d4.ToString(), d5.ToString(), d6.ToString(), ArmFlag);
            CoordinateConversionCount++;
        }


        #endregion

        #region --點位表格--

        private void WriteDataGrid(int count, double d1, double d2, double d3, double d4, double d5, double d6,bool ArmFlag)
        {
            if (ArmFlag)
            { this.PointDataGrid.Rows.Add(count, d1, d2, d3, d4, d5, d6); }
            else
            { this.PointDataGrid1.Rows.Add(count, d1, d2, d3, d4, d5, d6); }
        }

        #endregion

        #region --移動--

        private void armMove(String d1, String d2, String d3, String d4, String d5, String d6,bool ArmFlag)
        {

            int speed = 100;
            var item = (d1: dataint[0], speed: string.Format("{0:000}", speed * sp_abs));

            //Line(CAP
            string test_string = @"1,PTP(""CPP""" + "," + d1 + "," + d2 + "," + d3 + "," + d4 + "," + d5 + "," + d6 + "," + $"{item.speed} ,200,0,false)";
            TB_SendData.Text = test_string;
            if(ArmFlag)
                TM_send(test_string);
            else
                TM_send1(test_string);

        }

        private void armReMove(object sender, EventArgs e)
        {
            Action<string> ModifyText = SockMsg;
            Action<String, String, String, String, String, String> Toarm = armMove;

            for (int i = 0; i < PointDataGrid.RowCount - 1; i++)
            {

                String rd1 = Convert.ToString(PointDataGrid[1, i].Value);
                String rd2 = Convert.ToString(PointDataGrid[2, i].Value);
                String rd3 = Convert.ToString(PointDataGrid[3, i].Value);
                String rd4 = Convert.ToString(PointDataGrid[4, i].Value);
                String rd5 = Convert.ToString(PointDataGrid[5, i].Value);
                String rd6 = Convert.ToString(PointDataGrid[6, i].Value);

                Invoke(ModifyText, "ReMove" + rd1 + "," + rd2 + "," + rd3 + "," + rd4 + "," + rd5 + "," + rd6);
                Invoke(Toarm, rd1, rd2, rd3, rd4, rd5, rd6);
            }
        }
        #endregion


        #region --TM_TCP--
        #region --TM1--

        private void btn_ClearSendData_Click(object sender, EventArgs e)
        {
            this.TB_SendData.Text = string.Empty;
        }

        private void btn_ClearRecvData_Click(object sender, EventArgs e)
        {
            this.TB_RecvData.Text = string.Empty;
            this.showRecvDataLog.Clear();
        }

        private void btn_Connect_Click(object sender, EventArgs e)
        {
            ThreadStart start = null;
            try
            {
                string text = this.TB_IPAddress.Text;
                //169.254.119.180
                string str2 = this.TB_Port.Text;
                int result = 0;
                if (this.checkIPAddressValid(text) && (!string.IsNullOrEmpty(str2) && int.TryParse(str2, out result)))
                {
                    this.TCPClientObject = new SocketClientObject(text, result);
                    if (this.TCPClientObject != null)
                    {
                        this.TCPClientObject.ConnectStatusResponse += new SocketClientObject.TCPConnectStatusResponse(this.showConnectStatus);
                        if (start == null)
                        {
                            start = delegate
                            {
                                if (this.TCPClientObject.Connect(0))
                                {
                                    TCPClientObject.ReceiveData += new SocketClientObject.TCPReceiveData(this.showReceiveData);
                                }
                            };
                        }
                        new Thread(start) { IsBackground = true }.Start();
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        private void btn_Disconnect_Click(object sender, EventArgs e)
        {
            if ((this.TCPClientObject != null) && this.TCPClientObject.Disconnect())
            {
                this.TCPClientObject.ReceiveData -= new SocketClientObject.TCPReceiveData(this.showReceiveData);
                this.TCPClientObject = null;
            }
        }

        private void btn_Send_Click(object sender, EventArgs e)
        {
            string s = string.Empty;
            if (this.CB_Listen.Checked == true)
            {
                s = SocketClientObject.DataToPacket(CB_Customized.SelectedItem.ToString(), this.TB_SendData.Text);
            }
            else
            {
                s = this.TB_SendData.Text;
            }
            this.TB_Command.Text = s;
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            if (this.TCPClientObject != null)
            {
                this.TCPClientObject.WriteSyncData(bytes);
            }
        }
        private void showConnectStatus(object sender, string resp)
        {
            AddConnectStatus(resp, LB_ConnectionStatus);
        }

        public void showReceiveData(object sender, string recv_data)
        {
            string str = string.Format("[{0}] {1}", DateTime.Now.ToString("HH:mm:ss:fff"), recv_data);
            this.showRecvDataLog.AppendLine(str);
            AddReceiveData(showRecvDataLog.ToString(), TB_RecvData);
        }

        private void TB_SendData_KeyDown(object sender, KeyEventArgs e)
        {
            int selectionStart = this.TB_SendData.SelectionStart;
            if (e.KeyCode == Keys.Enter)
            {
                this.TB_SendData.Text = this.TB_SendData.Text.Insert(selectionStart, Environment.NewLine);
                this.TB_SendData.Select(selectionStart + 2, 0);
            }
        }

        private void CB_Listen_CheckedChanged(object sender, EventArgs e)
        {
            if (this.CB_Listen.Checked == true)
            {
                this.CB_Customized.Visible = true;
            }
            else
            {
                this.CB_Customized.Visible = false;
            }
        }

        #endregion
        #region --TM2--

        private void btn_Connect1_Click(object sender, EventArgs e)
        {
            ThreadStart start = null;
            try
            {
                string text = this.TB_IPAddress1.Text;
                //169.254.119.180
                string str2 = this.TB_Port1.Text;
                int result = 0;
                if (this.checkIPAddressValid(text) && (!string.IsNullOrEmpty(str2) && int.TryParse(str2, out result)))
                {
                    this.TCPClientObject1 = new SocketClientObject(text, result);
                    if (this.TCPClientObject1 != null)
                    {
                        this.TCPClientObject1.ConnectStatusResponse += new SocketClientObject.TCPConnectStatusResponse(this.showConnectStatus1);
                        if (start == null)
                        {
                            start = delegate
                            {
                                if (this.TCPClientObject1.Connect(0))
                                {
                                    TCPClientObject1.ReceiveData += new SocketClientObject.TCPReceiveData(this.showReceiveData1);
                                }
                            };
                        }
                        new Thread(start) { IsBackground = true }.Start();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void btn_Disconnect1_Click(object sender, EventArgs e)
        {

            if ((this.TCPClientObject1 != null) && this.TCPClientObject1.Disconnect())
            {
                this.TCPClientObject1.ReceiveData -= new SocketClientObject.TCPReceiveData(this.showReceiveData1);
                this.TCPClientObject1 = null;
            }
        }

        private void btn_ClearSendData1_Click(object sender, EventArgs e)
        {
            this.TB_SendData1.Text = string.Empty;
        }

        private void btn_Send1_Click(object sender, EventArgs e)
        {
            string s = string.Empty;
            if (this.CB_Listen1.Checked == true)
            {
                s = SocketClientObject.DataToPacket(CB_Customized1.SelectedItem.ToString(), this.TB_SendData1.Text);
            }
            else
            {
                s = this.TB_SendData1.Text;
            }
            this.TB_Command1.Text = s;
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            if (this.TCPClientObject1 != null)
            {
                this.TCPClientObject1.WriteSyncData(bytes);
            }
        }

        private void btn_ClearRecvData1_Click(object sender, EventArgs e)
        {
            this.TB_RecvData1.Text = string.Empty;
            this.showRecvDataLog1.Clear();
        }

        private void TB_SendData1_KeyDown(object sender, KeyEventArgs e)
        {

            int selectionStart = this.TB_SendData1.SelectionStart;
            if (e.KeyCode == Keys.Enter)
            {
                this.TB_SendData1.Text = this.TB_SendData1.Text.Insert(selectionStart, Environment.NewLine);
                this.TB_SendData1.Select(selectionStart + 2, 0);
            }
        }

        public void showReceiveData1(object sender, string recv_data)
        {
            string str = string.Format("[{0}] {1}", DateTime.Now.ToString("HH:mm:ss:fff"), recv_data);
            this.showRecvDataLog1.AppendLine(str);
            AddReceiveData(showRecvDataLog1.ToString(), TB_RecvData1);
        }

        private void CB_Listen1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.CB_Listen1.Checked == true)
            {
                this.CB_Customized1.Visible = true;
            }
            else
            {
                this.CB_Customized1.Visible = false;
            }
        }

        #endregion

        private bool checkIPAddressValid(string sIP)
        {
            IPAddress address;
            if (string.IsNullOrEmpty(sIP))
            {
                return false;
            }
            if (!IPAddress.TryParse(sIP, out address))
            {
                return false;
            }
            return true;
        }

        private void showConnectStatus1(object sender, string resp)
        {
            AddConnectStatus(resp, LB_ConnectionStatus1);
        }

        private void AddConnectStatus(string _connectstatus, Label _label)
        {
            if (this.InvokeRequired)
            {
                AddConnectDataDelegate ConnectStatus = new AddConnectDataDelegate(AddConnectStatus);
                this.Invoke(ConnectStatus, _connectstatus, _label);
            }
            else
            {
                _label.Text = _connectstatus;
            }
        }


        private void AddReceiveData(string _receivedata, TextBox _textbox)
        {
            if (this.InvokeRequired)
            {
                AddReceiveDataDelegate ReceiveData = new AddReceiveDataDelegate(AddReceiveData);
                this.Invoke(ReceiveData, _receivedata, _textbox);
            }
            else
            {
                _textbox.AppendText(_receivedata);
            }
        }

        #endregion

        #region   --XEG32夾爪控制程式--

        byte Direction = 2;//模式:絕對位置

        //模式設定:絕對位置
        void ABS_mode()
        {
            Direction = 2;
            XEG32_PosStk_text.Enabled = true;
            XEG32_Vel_text.Enabled = true;
            XEG32_CJog_text.Enabled = false;
            XEG32_Force_text.Enabled = true;
            XEG32_PushVel_text.Enabled = false;
            XEG32_PushPosStk_text.Enabled = false;
        }

        private void button34_Click(object sender, EventArgs e)
        {
            //開啟夾爪連線
            if (!XEG32.IsOpen)
                XEG32.Open();
        }

        private void button35_Click(object sender, EventArgs e)
        {
            //關閉夾爪連線
            if (XEG32.IsOpen)
                XEG32.Close();
        }

        private void XEG32_Reset_bt_Click(object sender, EventArgs e)
        {
            //夾爪重置
            byte gResetNum = 0x1f;
            SendIndexCmd(XEG32, gResetNum);
        }

        private void XEG32_Open_bt_Click(object sender, EventArgs e)
        {
            XEG32_PosStk_text.Text = "3200";//夾爪張開 20 mm
            XEG32_Vel_text.Text = "80";//速度 50 mm/s
            XEG32_Force_text.Text = "100";//力 70 %
            SendOpenClose(XEG32);
        }

        private void XEG32_Close_bt_Click(object sender, EventArgs e)
        {
            XEG32_PosStk_text.Text = "1000";//夾爪張開 0 mm
            XEG32_Vel_text.Text = "80";//速度 50 mm/s
            XEG32_Force_text.Text = "100";//力 70 %
            SendOpenClose(XEG32);
        }
        private void XEG32_Enable_bt_Click(object sender, EventArgs e)
        {
            SendOpenClose(XEG32);
        }

        //控制封包
        public string SendOpenClose(SerialPort Comm1)
        {
            try
            {
                int i_PosStk = Convert.ToInt32(XEG32_PosStk_text.Text),
                  i_Vel = Convert.ToInt32(XEG32_Vel_text.Text) * 100,
                  i_CJog = Convert.ToInt32(XEG32_CJog_text.Text),
                  i_Force = Convert.ToInt32(XEG32_Force_text.Text),
                  i_PushVel = Convert.ToInt32(XEG32_PushVel_text.Text),
                  i_PushPosStk = Convert.ToInt32(XEG32_PushPosStk_text.Text);
                long num = 0L;
                List<byte> list = new List<byte>();
                list.Add((byte)250);
                list.Add((byte)0xfb);
                list.Add((byte)0xf3);
                list.Add((byte)((i_PosStk & 0xff00) >> 8));
                list.Add((byte)(i_PosStk & 0xff));
                list.Add((byte)((i_Vel & 0xff00) >> 8));
                list.Add((byte)(i_Vel & 0xff));
                list.Add((byte)0);
                list.Add((byte)((i_CJog * 4) + Direction));
                list.Add((byte)0);
                list.Add((byte)0);
                list.Add((byte)((i_Force & 0xff00) >> 8));
                list.Add((byte)(i_Force & 0xff));
                list.Add((byte)0);
                list.Add((byte)0);
                list.Add((byte)((i_PushVel & 0xff00) >> 8));
                list.Add((byte)(i_PushVel & 0xff));
                list.Add((byte)((i_PushPosStk & 0xff00) >> 8));
                list.Add((byte)(i_PushPosStk & 0xff));
                for (int i = 3; i < (list.Count - 1); i++)
                {
                    num += Convert.ToByte(list[i]);
                }
                list.Add((byte)(num & 0xffL));
                list.Add((byte)0xfe);
                byte[] buffer = (byte[])list.ToArray();
                Comm1.Write(buffer, 0, buffer.Length);
                return BitConverter.ToString(buffer).Replace("-", " ");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return "";
            }
        }

        //命令封包
        public string SendIndexCmd(SerialPort Comm1, byte index)
        {
            try
            {
                long num = 0L;
                ArrayList list = new ArrayList();
                list.Add((byte)250);
                list.Add((byte)0xfc);
                list.Add((byte)0xe2);
                list.Add(index);
                list.Add((byte)0xef);
                for (int i = 3; i < list.Count; i++)
                {
                    num += Convert.ToByte(list[i]);
                }
                list.Add((byte)(num & 0xffL));
                list.Add((byte)0xfe);
                byte[] buffer = (byte[])list.ToArray(typeof(byte));
                Comm1.Write(buffer, 0, buffer.Length);
                return BitConverter.ToString(buffer).Replace("-", " ");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return "";
            }
        }
        #endregion

        #region --TM手臂動作--

        #region --TM_副程式--

        //TM控制副程式
        void TM_send(string _string)
        {
            string s = string.Empty;
            s = SocketClientObject.DataToPacket("$TMSCT", _string);
            //this.TB_Command.Text = s;
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            if (this.TCPClientObject != null)
            {
                this.TCPClientObject.WriteSyncData(bytes);
            }
        }

        void TM_send1(string _string)
        {
            string s = string.Empty;
            s = SocketClientObject.DataToPacket("$TMSCT", _string);
            //this.TB_Command.Text = s;
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            if (this.TCPClientObject1 != null)
            {
                this.TCPClientObject1.WriteSyncData(bytes);
            }
        }
        //手臂速度百分比
        double sp_pc = 1.0;
        private void TB_sp_pc_TextChanged(object sender, EventArgs e)
        {
            sp_pc = Convert.ToDouble(TB_sp_pc.Text);
        }

        //手臂絕對速度
        double sp_abs = 1.0;
        private void TB_sp_abs_TextChanged(object sender, EventArgs e)
        {
            try
            {
                sp_abs = Convert.ToDouble(TB_sp_abs.Text);
            }
            catch (Exception ex)
            {
                TB_Command.Text = ex.Message;
                //TB_sp_abs.Text = "1";
            }
        }

        //手臂歸位
        private void btn_TMtest_Click(object sender, EventArgs e)
        {
            int speed = 100;
            string test_string = @"1,PTP(""CPP"", 450, -122, 300 ,180,0,90," + string.Format("{0:000}", speed * sp_pc) + ",200,0,false)";
            TM_send(test_string);
        }

        #endregion

        #region --TM_test--

        //Line_絕對速度
        private void button37_Click(object sender, EventArgs e)
        {
            int speed = 1000;
            string test_string = @"1,Line(""CAP"",500,100,500,185,0,90," + string.Format("{0:000}", speed * sp_abs) + ",200,0,false)";
            TM_send(test_string);
        }

        private void exportExcel_Click(object sender, EventArgs e)
        {
            //Creating DataTable
            using (var workbook = new XLWorkbook())
            {
                var wb = workbook.Worksheets.Add("點位");
                //MessageBox.Show(PointDataGrid[6,0].Value.ToString());
                for (int x = 1; x < PointDataGrid.Rows.Count; x++)
                {
                    for (int i = 1; i < 7; i++)
                    {
                        wb.Cell(x, i).Value = PointDataGrid[i, x - 1].Value.ToString();
                    }
                }
                //Exporting to Excel
                string folderPath = @"E:\碩論\MasterCode\ControlUI\";
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                workbook.SaveAs(folderPath + "DataGridViewExport.xlsx");
            }
        }

        //Circle_絕對速度
        private void button38_Click(object sender, EventArgs e)
        {
            int speed = 2000;
            string test_string = @"1,Circle(""CPR"",540,-50,408,185,0,90,519,50,458,185,0,90," + string.Format("{0:000}", speed * sp_abs) + ",200,0,0,false)";
            TM_send(test_string);
        }
        //Line_絕對速度
        private void button39_Click(object sender, EventArgs e)
        {
            int speed = 2000;
            string test_string = @"1,Line(""CAP"",500,100,500,185,0,90," + string.Format("{0:000}", speed * sp_abs) + ",200,0,false)";
            TM_send(test_string);
        }

        #endregion

        #endregion

        private void ActionBtn_Click(object sender, EventArgs e)
        {
            var workbook = new XLWorkbook(@"E:\碩論\MasterCode\ControlUI\DataGridViewExport.xlsx");
            var ws = workbook.Worksheet(1);
            int NumberOfLastRow = ws.LastRowUsed().RowNumber();
            //List<double[]> data = new List<double[]>();
            List<string[]> data = new List<string[]>();
            var range = ws.RangeUsed();
            for (int i = 1; i < range.RowCount() + 1; i++)
            {
                string[] point = new string[6];
                for (int j = 1; j < range.ColumnCount() + 1; j++)
                {
                    point[j - 1] = (string)ws.Cell(i, j).Value.ToString();
                }
                //data.Add(Array.ConvertAll(point, double.Parse));
                data.Add(point);
            }
            for (int k = 0; k < data.Count; k++)
            {
                armMove(data[k][0], data[k][1], data[k][2], data[k][3], data[k][4], data[k][5]);
                Thread.Sleep(500);
            }
        }

        private void PointDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}