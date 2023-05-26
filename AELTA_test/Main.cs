using System;
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
using System.Timers;
//使用XEG-32夾爪 請加入此參考
//
using System.Runtime.InteropServices;
using System.Net;

using System.Net.Sockets;
using ClosedXML.Excel;

using dynamixel_sdk;
using System.Diagnostics;
using System.ComponentModel.Composition.Primitives;
using System.Text.RegularExpressions;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Reg;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Intel.RealSense;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace ControlUI
{
    public partial class GripPosition_ : Form
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
        TCP_Listener SecondListener = new TCP_Listener(ipAd.ToString(), PortNumber + 1);

        private delegate void AddConnectDataDelegate(string _connectstatus, Label _label);
        private delegate void AddReceiveDataDelegate(string _receivedata, TextBox _textbox);

        private int GripPosition = 0;
        private int port_num;

        private bool StopFlag = false;
        private bool PutDownToFixedSeat = false;

        ThreadStart action = null;
        //Image
        ImageProcess ImageHandler = new ImageProcess();
        #endregion

        public GripPosition_()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Point bais = new Point();
            //Gripper: AX12A and XEG32 - initial
            try
            {

                int port_num = dynamixel.portHandler(DEVICENAME);

                dynamixel.packetHandler();
                dynamixel.setBaudRate(port_num, BAUDRATE);
                dynamixel.write1ByteTxRx(port_num, PROTOCOL_VERSION, DXL_ID, ADDR_MX_TORQUE_ENABLE, TORQUE_ENABLE);
                int dxl_comm_result = COMM_TX_FAIL;                                   // Communication result

                dynamixel.write1ByteTxRx(port_num, PROTOCOL_VERSION, DXL_ID, ADDR_MX_TORQUE_ENABLE, TORQUE_ENABLE);
                if ((dxl_comm_result = dynamixel.getLastTxRxResult(port_num, PROTOCOL_VERSION)) != COMM_SUCCESS)
                {
                    AX12A_Status.Text = (Marshal.PtrToStringAnsi(dynamixel.getTxRxResult(PROTOCOL_VERSION, dxl_comm_result)));
                }
                else if ((dxl_error = dynamixel.getLastRxPacketError(port_num, PROTOCOL_VERSION)) != 0)
                {
                    AX12A_Status.Text = (Marshal.PtrToStringAnsi(dynamixel.getRxPacketError(PROTOCOL_VERSION, dxl_error)));
                }
                else
                {
                    AX12A_Status.Text = ("Dynamixel has been successfully connected");
                }

                //XEG32
                GripCnt_Btn.PerformClick();
            }
            catch (Exception ex) { }

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
            int command_int = 3;
            string Declare;

            Action<String> ModifyText = SockMsg;
            Action<int, int, string, double, double, double, double, double, double, bool> Tolist = DataToArm;
            Action<int, int, string, double, double, double, double, double, double, bool> ToDataGrid = WriteDataGrid;
            Action<int> Image = ImageProcess;

            Action<int> CameraVision = VaildPresentPosition;

            while (true)
            {
                bool _open_flag = FirstListener.Start();
                Declare = _open_flag == true ? "FirstSever Open" : "FirstSever Not Open";

                Invoke(ModifyText, Declare);
                bool _connect_flag = FirstListener.Connect();
                Declare = _connect_flag == true ? "FirstSever Connect" : "FirstSever Not Connect";

                Invoke(ModifyText, Declare);

                double[] refrencePosition = new double[] { };
                while (_connect_flag)
                {
                    try
                    {

                        string reciveData = FirstListener.Recive();
                        Invoke(ModifyText, Count.ToString() + "  First Command");

                        string[] data = reciveData.Split('$');
                        if (data == null || data.Length < 3)
                        { break; }
                        GripPosition = Convert.ToInt32(data[1]);
                        double[] position = new double[6];

                        switch (data[command_int].ToString())
                        {
                            case "position":
                                for (int i = 0; i < 6; i++)
                                {
                                    position[i] = Convert.ToDouble(data[command_int + 1 + i]);
                                }
                                position = CoordinateConversion(position, true);
                                if (position[1] > -300)
                                { Invoke(CameraVision, 640); }
                                else
                                { Invoke(CameraVision, 384); }

                                switch (Convert.ToInt32(data[command_int - 1]))
                                {
                                    //case (int)EGripPostion.Coupling:
                                    //    if (position[2] < 75) { position[2] = 75; }
                                    //    break;
                                    //case (int)EGripPostion.raiseBoard:
                                    //    if (position[2] < 92) { position[2] = 92; }
                                    //    break;
                                    //case (int)EGripPostion.Vientiane:
                                    //    if (position[2] < 82) { position[2] = 82; }
                                    //    break;
                                    //case 11:
                                    //    if (position[2] < 82) { position[2] = 82; }
                                    //    break;

                                    //case (int)EGripPostion.RotationBoard: 
                                    //    if (position[2] < 82) { position[2] = 82; }
                                    //    break;

                                    //case (int)EGripPostion.Coupling_back:
                                    //    if (position[2] < 170) { position[2] = 170; }
                                    //    break;

                                    //case (int)EGripPostion.chair_legs:
                                    //    if(position[2] < 82) { position[2] = 82; }
                                    //    break;

                                    case (int)Eshape.Rectangle:
                                        DoGrip((int)Eshape.Rectangle);
                                        break;
                                    case (int)Eshape.Circle:
                                        DoGrip((int)Eshape.Circle);
                                        break;
                                    case (int)Eshape.Square:
                                        DoGrip((int)Eshape.Square);
                                        break;

                                }


                                switch (Convert.ToInt32(data[1]))
                                {
                                    case 11:
                                        if (position[2] < 160) { position[2] = 160; }
                                        break;
                                    case 13:
                                        if (position[2] < 90) { position[2] = 87; }
                                        break;

                                    case 14:
                                        if (position[2] < 260) { position[2] = 258; }
                                        break;
                                }

                                refrencePosition = position;

                                Invoke(Tolist,
                                    Convert.ToInt32(data[0]),//Count
                                    Convert.ToInt32(data[1]),//GripObject
                                    data[command_int],//command
                                    position[0],
                                    position[1],
                                    position[2],
                                    position[3],
                                    position[4],
                                    position[5],
                                    true);
                                break;

                            case "Image":
                                if (GripPosition == 10 && Convert.ToInt32(data[2]) != 11)
                                {
                                    Invoke(ToDataGrid, Count, GripPosition, "GripRotation", 0, 0, 0, 0, 0, 0, true);
                                }
                                else if (GripPosition == 10 && Convert.ToInt32(data[2]) == 11)
                                {
                                    Invoke(ToDataGrid, Count, GripPosition, "DownRotation", 0, 0, 0, 0, 0, 0, true);
                                }

                                else if (GripPosition > 20)
                                {
                                    Invoke(ToDataGrid, Count, GripPosition, "Image", 0, 0, 0, 0, 0, 0, true);
                                }
                                else { Invoke(ToDataGrid, Count, Convert.ToInt32(data[1]), data[command_int], 0, 0, 0, 0, 0, 0, true); }

                                if (Image_checkBox.Checked)
                                {
                                    if (GripPosition == 10)
                                    {
                                        GripRotation(Convert.ToInt32(data[2]));
                                        break;
                                    }
                                    else if (GripPosition == 11)
                                    {
                                        Invoke(ToDataGrid, Count, GripPosition, "DownRotation", 0, 0, 0, 0, 0, 0, true);
                                        break;
                                    }
                                    //                                    else if (GripPosition > 20)
                                    //                                    {
                                    //                                        int legs = GripPosition % 10;
                                    //                                        GripChairLegs(legs);
                                    //                                        break;
                                    //                                    }

                                    try
                                    {
                                        do
                                        {
                                            TM_send("1,ListenSend(90,GetString(Robot[0].CoordRobot))", false);
                                            Thread.Sleep(700);
                                        } while (
                                        Math.Abs(NowPosition[0] - refrencePosition[0]) >= 1 ||
                                        Math.Abs(NowPosition[1] - refrencePosition[1]) >= 1 ||
                                        Math.Abs(NowPosition[2] - refrencePosition[2]) >= 1
                                        );
                                        //Image.Invoke();
                                        switch (Convert.ToInt32(data[command_int - 1]))
                                        {
                                            case (int)Eshape.Rectangle:
                                                DoGrip((int)Eshape.Rectangle);
                                                break;
                                            case (int)Eshape.Circle:
                                                DoGrip((int)Eshape.Circle);
                                                break;
                                            case (int)Eshape.Square:
                                                DoGrip((int)Eshape.Square);
                                                break;

                                        }
                                    }
                                    catch (Exception ex)
                                    { }
                                }
                                break;
                            case "GripOpen":
                                SendOpenClose(XEG32, 3200, 80);
                                Invoke(ToDataGrid, Count, Convert.ToInt32(data[1]), data[command_int], 0, 0, 0, 0, 0, 0, true);
                                break;
                            case "GripClose":
                                SendOpenClose(XEG32, 200, 80);
                                Invoke(ToDataGrid, Count, Convert.ToInt32(data[1]), data[command_int], 0, 0, 0, 0, 0, 0, true);
                                break;
                        }
                        Count++;
                    }
                    catch (Exception ex) { };
                }
            }
        }

        private void SecondSever()
        {
            int Count = 0;
            Action<String> ModifyText = SockMsg;
            Action<int, int, string, double, double, double, double, double, double, bool> Tolist = DataToArm;

            string Declare;
            while (true)
            {
                try
                {
                    bool _open_flag = SecondListener.Start();
                    Declare = _open_flag == true ? "SecondSever Open" : "SecondSever Not Open";
                    Invoke(ModifyText, Declare);

                    bool _connect_flag = SecondListener.Connect();
                    Declare = _connect_flag == true ? "SecondSever Connect" : "SecondSever Not Connect";
                    Invoke(ModifyText, Declare);
                    int command_int = 2;
                    while (true)
                    {
                        string reciveData = SecondListener.Recive();
                        Invoke(ModifyText, Count.ToString() + "  Second Command");
                        string[] data = reciveData.Split('$');

                        if (data == null || data.Length == 0)
                        { break; }

                        double[] position = new double[6];
                        for (int i = 0; i < 6; i++)
                        {
                            position[i] = Convert.ToDouble(data[command_int + 1 + i]);
                        }
                        position = CoordinateConversion(position, false);
                        switch (Convert.ToInt32(data[command_int - 1]))
                        {
                            case 0:
                                if (position[5] == 0 && position[1] < -190)
                                {
                                    position[1] = -190;
                                }

                                if (position[2] <= 100)
                                {
                                    position[2] = 100;
                                }
                                break;

                            case 1:
                                if (position[0] > 250 && position[1] < 198 && position[5] == 0)
                                {
                                    position[1] = 198;
                                }

                                if (position[0] > 200 && position[1] > -197 && position[5] == 180)
                                {
                                    position[1] = -197;

                                }
                                else if (position[0] < 200 && position[0] > 100 && position[1] > -206)
                                {
                                    position[1] = -206;
                                }
                                break;
                        }
                        Invoke(Tolist,
                            Convert.ToInt32(data[0]),
                            Convert.ToInt32(data[1]),
                            data[2],
                            position[0],
                            position[1],
                            position[2],
                            position[3],
                            position[4],
                            position[5],
                            false);
                        Count++;
                    }
                }
                catch (Exception ex)
                {

                }
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

        private double[] CoordinateConversion(double[] position, bool ArmFlag)
        {
            double d1 = position[0],
                 d2 = position[1],
                 d3 = position[2],
                 d4 = position[3],
                 d5 = position[4],
                 d6 = position[5];

            int xmax, xmin, ymax, ymin, zmax, zmin, xbias, ybias, zbias;

            if (ArmFlag)
            {
                xmax = 750;
                xmin = 70;
                ymax = 550;
                ymin = -750;
                zmax = 400;
                zmin = -45;

                xbias = 450;
                ybias = -122;
                zbias = 300;
            }
            else
            {
                xmax = 750;
                xmin = 40;
                ymax = 550;
                ymin = -800;
                zmax = 600;
                zmin = -45;

                xbias = 450;
                ybias = 20;
                zbias = 380;
            }

            double RotateBuffer_x = 180, RotateBuffer_y = 0, RotateBuffer_z = 90;

            double x, y, z;

            x = Convert.ToDouble(d1) + xbias;
            y = Convert.ToDouble(d2) + ybias;
            z = Convert.ToDouble(d3) + zbias;

            x = Math.Round(x, 2);
            y = Math.Round(y, 2);
            z = Math.Round(z, 2);

            d1 = x;
            d2 = y;
            d3 = z;

            RotateBuffer_x = 180 - d4;
            RotateBuffer_y = d5;
            RotateBuffer_z = 90 - d6;

            d4 = RotateBuffer_x;
            d5 = RotateBuffer_y;
            d6 = RotateBuffer_z;


            if (d1 > xmax) { d1 = xmax; }
            if (d1 < xmin) { d1 = xmin; }

            if (d2 > ymax) { d2 = ymax; }
            if (d2 < ymin) { d2 = ymin; }

            if (d3 > zmax) { d3 = zmax; }
            if (d3 < zmin) { d3 = zmin; }

            position[0] = d1; position[1] = d2; position[2] = d3; position[3] = d4; position[4] = d5; position[5] = d6;

            return position;

        }

        private void DataToArm(int Conit, int GripObetct, string command, double d1, double d2, double d3, double d4, double d5, double d6, bool ArmFlag)
        {
            Action<int, int, string, double, double, double, double, double, double, bool> DataToGrid = WriteDataGrid;
            Action<string, string, string, string, string, string, bool> Toarm = armMove;

            DataToGrid.Invoke(Conit, GripObetct, command, d1, d2, d3, d4, d5, d6, ArmFlag);
            Toarm.Invoke(d1.ToString(), d2.ToString(), d3.ToString(), d4.ToString(), d5.ToString(), d6.ToString(), ArmFlag);
        }


        #endregion

        #region --點位表格--

        private void WriteDataGrid(int count, int GripObject, string command, double d1, double d2, double d3, double d4, double d5, double d6, bool ArmFlag)
        {
            if (ArmFlag)
            { this.PointDataGrid.Rows.Add("left", count, GripObject, command, d1, d2, d3, d4, d5, d6); }
            else
            { this.PointDataGrid.Rows.Add("right", count, GripObject, command, d1, d2, d3, d4, d5, d6); }
        }

        #endregion

        #region --移動--

        private void armMove(String d1, String d2, String d3, String d4, String d5, String d6, bool ArmFlag)
        {

            int speed = 100;
            var item = (d1: dataint[0], speed: string.Format("{0:000}", speed * sp_abs));

            //Line(CAP
            string test_string = @"1,Line(""CPP""" + "," + d1 + "," + d2 + "," + d3 + "," + d4 + "," + d5 + "," + d6 + "," + $"{item.speed} ,200,0,false)";
            if (ArmFlag)
            {
                TM_send(test_string);
                TB_SendData.Text = test_string;
            }
            else
            {
                TM_send1(test_string);
                TB_SendData1.Text = test_string;
            }

        }

        private void armReMove(object sender, EventArgs e)
        {
            bool ArmFlag = true;
            int j = 1;
            int RowCount = PointDataGrid.RowCount;
            Action<string> ModifyText = SockMsg;
            Action<String, String, String, String, String, String, bool> Toarm = armMove;

            for (int i = 0; i < RowCount - 1; i++)
            {

                String rd1 = Convert.ToString(PointDataGrid[RowCount - 5, i].Value);
                String rd2 = Convert.ToString(PointDataGrid[RowCount - 4, i].Value);
                String rd3 = Convert.ToString(PointDataGrid[RowCount - 3, i].Value);
                String rd4 = Convert.ToString(PointDataGrid[RowCount - 2, i].Value);
                String rd5 = Convert.ToString(PointDataGrid[RowCount - 1, i].Value);
                String rd6 = Convert.ToString(PointDataGrid[RowCount, i].Value);

                Invoke(ModifyText, "ReMove" + rd1 + "," + rd2 + "," + rd3 + "," + rd4 + "," + rd5 + "," + rd6);
                Invoke(Toarm, rd1, rd2, rd3, rd4, rd5, rd6, ArmFlag);
            }
        }
        #endregion

        #region Excel

        private void ActionBtn_Click(object sender, EventArgs e)
        {
            ThreadStart action = null;
            PutDownToFixedSeat = false;
            if (Excel.Checked)
            {
                ActionMoveFromExcel();
            }

            else
            {
                try
                {
                    this.TCPClientObject.IsMoveOver = false;
                    this.TCPClientObject1.IsMoveOver = false;

                    if (action == null)
                    {
                        action = delegate
                        {
                            this.ActionMoveFromGrid();
                        };
                        new Thread(action) { IsBackground = true }.Start();
                    }
                }
                catch
                {
                    MessageBox.Show("請先確認雙手臂連線");
                }

            }

        }

        private void StopMove_Click(object sender, EventArgs e)
        {
            StopFlag = true;
        }
        private void ActionMoveFromExcel()
        {
            bool arm_Select = false;
            Action<int> Image = ImageProcess;

            var ws = workbook.Worksheet(1);
            var range = ws.RangeUsed();

            int ColumnCount = range.ColumnCount();
            int RowCount = range.RowCount();
            if (Excel.Checked)
            {
                for (int k = 0; k < Pointdata.Count; k++)
                {
                    if (Pointdata[k][0] == "left") { arm_Select = true; }
                    else { arm_Select = false; }
                    switch (Pointdata[k][3])
                    {
                        case "position":
                            armMove(Pointdata[k][ColumnCount - 6], Pointdata[k][ColumnCount - 5], Pointdata[k][ColumnCount - 4], Pointdata[k][ColumnCount - 3], Pointdata[k][ColumnCount - 2], Pointdata[k][ColumnCount - 1], arm_Select);
                            TM_send("1,ListenSend(90,GetString(Robot[0].CoordRobot))", false);
                            if (arm_Select)
                            {
                                while (!this.TCPClientObject.IsMoveOver) { };
                            }
                            else
                            { }
                            break;
                        case "Image":
                            this.GripPosition = Convert.ToInt32(Pointdata[k][2]);
                            //Image();
                            break;
                        case "GripOpen":
                            XEG32_Open_bt.PerformClick();
                            break;
                        case "GripClose":
                            XEG32_Close_Other.PerformClick();
                            break;
                        case "GripRotation":
                            GripRotation(Convert.ToInt32(Pointdata[k][2]));
                            break;
                        case "DownRotation":
                            GripRotation(Convert.ToInt32(Pointdata[k][2]));
                            break;
                    }
                    this.TCPClientObject.IsMoveOver = false;
                    Thread.Sleep(700);
                }
            }
        }

        private void ActionMoveFromGrid()
        {

            if (PointDataGrid.RowCount <= 1)
            {
                MessageBox.Show("請先點擊 Export 按鈕");
            }

            else
            {
                Action<String, String, String, String, String, String, bool> Toarm = armMove;
                Action<string, bool> SendCommand = TM_send;

                Action<int> Image = ImageProcess;
                Action<Button> btn_click = (sender) =>
                {
                    ((Button)sender).PerformClick();
                };

                Action<int> GripRotate = GripRotation;
                Action<int, bool> GridHight = HighLight;
                Action<int> SelectRow_ = SeletRow;

                bool arm_Select = false;
                int ColumnCount = PointDataGrid.Columns.Count;
                int NowSelected = 0;
                if (PointDataGrid.CurrentCell != null)
                {
                    NowSelected = PointDataGrid.CurrentCell.RowIndex;
                }

                for (int i = NowSelected; i < PointDataGrid.Rows.Count - 1; i++)
                {
                    if (StopFlag)
                    {
                        StopFlag = false;
                        break;
                    }

                    Invoke(GridHight, i, true);

                    if (PointDataGrid[0, i].Value.ToString() == "left")
                    {
                        arm_Select = true;
                    }

                    else
                    {
                        arm_Select = false;
                    }

                    this.TCPClientObject.IsMoveOver = false;
                    Invoke(SelectRow_, i);

                    switch (PointDataGrid[3, i].Value.ToString())
                    {
                        case "position":
                            Invoke(Toarm,
                                PointDataGrid[ColumnCount - 6, i].Value.ToString(),
                                PointDataGrid[ColumnCount - 5, i].Value.ToString(),
                                PointDataGrid[ColumnCount - 4, i].Value.ToString(),
                                PointDataGrid[ColumnCount - 3, i].Value.ToString(),
                                PointDataGrid[ColumnCount - 2, i].Value.ToString(),
                                PointDataGrid[ColumnCount - 1, i].Value.ToString(),
                                arm_Select);
                            Invoke(SendCommand, "1,ListenSend(90,GetString(Robot[0].CoordRobot))", false);
                            this.TCPClientObject.IsMoveOver = false;
                            this.TCPClientObject1.IsMoveOver = false;
                            if (arm_Select)
                            {
                                if (TCPClientObject.IsConnected)
                                {
                                    while (!this.TCPClientObject.IsMoveOver) { };
                                }
                            }
                            else
                            {
                                if (TCPClientObject1.IsConnected)
                                {
                                    Stopwatch t = new Stopwatch();
                                    while (!this.TCPClientObject1.IsMoveOver)
                                    {
                                        t.Start();
                                        if (t.ElapsedMilliseconds > 10000)
                                        {
                                            break;
                                        }
                                    };
                                }
                            }
                            break;
                        case "Image":
                            this.GripPosition = Convert.ToInt32(PointDataGrid[2, i].Value);
                            //Image.Invoke();
                            break;
                        case "GripOpen":
                            Invoke(btn_click, XEG32_Open_bt);
                            break;
                        case "GripClose":
                            Invoke(btn_click, XEG32_Close_Other);
                            break;
                        case "GripRotation":
                            Invoke(GripRotate, 10);
                            break;
                        case "DownRotation":
                            Invoke(GripRotate, 11);
                            break;
                    }
                    Thread.Sleep(350);
                    this.TCPClientObject.IsMoveOver = false;
                    this.TCPClientObject1.IsMoveOver = false;
                    Invoke(GridHight, i, false);
                }
            }
        }
        private void SeletRow(int i)
        {
            //每頁15筆資料
            if (i > 8)
            {
                if (!((i - 8) < 0))
                { PointDataGrid.FirstDisplayedScrollingRowIndex = i - 8; }
            }
        }
        private void HighLight(int Index, bool HighOrNot)
        {
            if (HighOrNot)
            {
                PointDataGrid.Rows[Index].DefaultCellStyle.BackColor = Color.Yellow;
            }
            else
            {
                PointDataGrid.Rows[Index].DefaultCellStyle.BackColor = Color.White;
            }
        }
        private void Clr_btn_Click(object sender, EventArgs e)
        {
            PointDataGrid.Rows.Clear();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in PointDataGrid.SelectedRows)
            {
                PointDataGrid.Rows.RemoveAt(row.Index);
            }
        }
        #endregion

        #region Image
        private string TM_Send_format(string cmd, int speed = 100)
        {
            int allSpeed = (int)(speed * sp_pc);
            return @"1,Line(""CPP"", " + cmd + "," + string.Format("{0:000}", allSpeed) + ",200,0,false)";
        }

        private void Img_Btn_Click(object sender, EventArgs e)
        {
            if (Image_checkBox.Checked)
            {
                //ImageProcess();
            }
            else
            {
                //double[] vs = ImageHandler.ImageRecognition();
            }
        }

        private void ImageProcess(int shape)
        {
            Action<Label, string> ChangeName = UpdateLocation;

            /* 移動手臂，把手臂向後移動影像座的距離，開始影像辨識 */
            TM_send($"1,Move_Line(\"CPP\",{(int)ImageRecogntionBais.X } , {(int)ImageRecogntionBais.Y}, 0, 0, 0, 0, 125, 200, 0, false)");

            /* 直到手臂到達定位 */
            this.TCPClientObject.IsMoveOver = false;
            while (!this.TCPClientObject.IsMoveOver) { }
            this.TCPClientObject.IsMoveOver = false;
            Thread.Sleep(500);

            /* Get Image Info */
            double[] ImageCenter_bais = ImageHandler.ImageRecognition(shape);

            /* until the object into the image center */
            while (Math.Abs(ImageCenter_bais[0]) > 1 || Math.Abs(ImageCenter_bais[1]) > 1)
            {
                double bais_X = -0.4, bais_Y = 0.4;
                if (Math.Abs(ImageCenter_bais[0]) <= 3) { bais_X = -0.125; }
                if (Math.Abs(ImageCenter_bais[1]) <= 3) { bais_Y = 0.125; }


                if (ImageCenter_bais[1] < 0) { bais_Y = -bais_Y; }
                if (ImageCenter_bais[0] < 0) { bais_X = -bais_X; }

                if (Math.Abs(ImageCenter_bais[0]) <= 0) { bais_X = 0; }
                if (Math.Abs(ImageCenter_bais[1]) <= 0) { bais_Y = 0; }

                /* 吋動手臂 */
                TM_send($"1,Move_Line(\"CPP\",{bais_X} , {bais_Y}, 0, 0, 0, 0, 125, 200, 0, false)", false);

                Thread.Sleep(550);

                /* Get Image Info Again */
                ImageCenter_bais = ImageHandler.ImageRecognition(shape);
                //ChangeName(BaisLB, "Image_Bais: " + ImageCenter_bais[0].ToString("#0.00") + ", " + ImageCenter_bais[1].ToString("#0.00"));

            }

            TM_send("1,ListenSend(90,GetString(Robot[0].CoordRobot))", false);
            Thread.Sleep(1000);

            //TM_send($"1,Move_Line(\"CPP\",{-(int)ImageRecogntionBais.X } , {-(int)ImageRecogntionBais.Y}, 0, 0, 0, 0, 125, 200, 0, false)", false);
            //ChangeName(BaisLB, "Image_Bais: " + ImageCenter_bais[0].ToString("#0.00") + ", " + ImageCenter_bais[1].ToString("#0.00"));
            ChangeName(NowPositionLb, String.Format("Arm_NowPosition: {0}, {1} {2}", NowPosition[0].ToString("#0.00"), NowPosition[1].ToString("#0.00"), NowPosition[2].ToString("#0.00")));

            //Grip();

        }
        private void UpdateLocation(Label lb, string imfo)
        {
            if (this.InvokeRequired)
            {
                Action<Label, string> act = delegate { UpdateLocation(lb, imfo); };
                lb.Invoke(act, lb, imfo);
            }
            else
            {
                lb.Text = imfo.ToString();
            }
        }
        private void Grip()
        {
            Action<Label, string> ChangeName = UpdateLocation;
            double[] ImageRecogntionPosition = NowPosition;
            double baisX_ = 0, baisY_ = 0;
            //GripPosition = Convert.ToInt32(Grip_Position.Text);
            if (GripPosition == 4)
            {
                if (!PutDownToFixedSeat)
                {
                    baisX_ = (0.0593 * (NowPosition[2] - 150) + 4.0134) * -1 + 0.75;
                    baisY_ = (-0.0601 * (NowPosition[2] - 150) - 2.0824) * -1 - 1;
                }
                else
                {
                    baisX_ = -(0.0521 * (NowPosition[2] - 150) - 1.8327);
                    baisY_ = -(-0.0638 * (NowPosition[2] - 150) + 1.0707) + .15;
                }
            }
            else if (GripPosition > 100)
            {
                //baisX_ = 0.0538 * (NowPosition[2] - 150) + 1.6693;
                baisX_ = -(0.0424 * (NowPosition[2] - 170) - 6.2634);
                baisY_ = -(-0.0588 * (NowPosition[2] - 170) + 3.0847);
            }
            ChangeName(BaisLB, "x: " + baisX_.ToString() + ",y: " + baisY_.ToString());
            ImageRecogntionPosition[0] = ImageRecogntionPosition[0] - (double)ImageRecogntionBais.X + baisX_;
            ImageRecogntionPosition[1] = ImageRecogntionPosition[1] - (double)ImageRecogntionBais.Y + baisY_;
            ChangeName(img_Label, String.Format("GoalPosition: {0}, {1}", ImageRecogntionPosition[0].ToString("#0.00"), ImageRecogntionPosition[1].ToString("#0.00")));

            string point = double2Point(ImageRecogntionPosition);
            TM_send(TM_Send_format(point), false);

            if (ImageGrip_CheckBox.Checked & GripPosition != 0)
            {
                switch (GripPosition)
                {
                    case (int)EGripPostion.Coupling:
                    case (int)EGripPostion.raiseBoard:
                        ImageRecogntionPosition[2] = 65;
                        break;

                    case (int)EGripPostion.Vientiane:
                        ImageRecogntionPosition[2] = 42;
                        break;

                    case (int)EGripPostion.FixedSeat:
                        ImageRecogntionPosition[2] = 105;
                        point = double2Point(ImageRecogntionPosition);

                        TM_send(TM_Send_format(point), false);
                        Thread.Sleep(1000);
                        if (!PutDownToFixedSeat)
                        {
                            PutDownToFixedSeat = true;
                            ImageRecogntionPosition[2] = 75;
                        }
                        else
                        {
                            ImageRecogntionPosition[2] = 87;
                        }
                        break;

                    case 5:
                        ImageRecogntionPosition[2] = 86;
                        break;

                    case 11:
                        ImageRecogntionPosition[2] = 160;
                        break;
                }
                if (GripPosition > 100)
                {
                    ImageRecogntionPosition[2] = 95;
                }
                point = double2Point(ImageRecogntionPosition);

                TM_send(TM_Send_format(point), false);
                Thread.Sleep(3000);

                if (GripPosition == 1 || GripPosition == 5)
                {
                    SendOpenClose(XEG32, 200, 80);
                }
                else if (GripPosition == 2)
                {
                    SendOpenClose(XEG32, 0, 80);
                }
                else if (GripPosition == 11)
                {
                    SendOpenClose(XEG32, 1400, 80);
                }
                else if (GripPosition == 4)
                {
                    SendOpenClose(XEG32, 3200, 80);
                }
                else if ((GripPosition) > 10)
                {
                    Thread.Sleep(500);
                    SendOpenClose(XEG32, 400, 80);
                }
                else if (GripPosition == 3)
                {
                    Thread.Sleep(500);
                    SendOpenClose(XEG32, 400, 80);
                }
                else
                {
                    SendOpenClose(XEG32, 400, 80);
                }
                Thread.Sleep(750);

                if (GripPosition <= 10)
                {
                    ImageRecogntionPosition[2] = 150;
                }
                else
                {
                    ImageRecogntionPosition[2] = 200;
                }

                TM_send(TM_Send_format(double2Point(ImageRecogntionPosition)), false);
                Thread.Sleep(1000);


            }
        }
        #endregion

        private string double2Point(double[] position, int iterate = 0)
        {
            string result = null;
            for (int i = 0; i < 6; i++)
            {
                result += position[i] + ",";
            }
            return result.Remove(result.Length - 1);
        }
        private void GripRotation(int NowStatic)
        {
            string point = "540, -190, 150, 180, 0, 90";
            TM_send(TM_Send_format(point));
            while (!waitPoint(point)) { }
            point = "540, -190, 50, 180, 0, 90";
            TM_send(TM_Send_format(point));
            while (!waitPoint(point)) { }
            if (NowStatic == 10)
                SendOpenClose(XEG32, 1100, 80);
            else if (NowStatic == 11)
                SendOpenClose(XEG32, 3200, 80);
            Thread.Sleep(500);
            point = "540, -190, 150, 180, 0, 90";
            Thread.Sleep(700);
            TM_send(TM_Send_format(point));
        }
        private void GripChairLegs(int legs)
        {
            string[] Point_array = new string[] { "424.5", "113, ", "200, ", "180, ", "0, ", "90" };
            double chair_legs = (legs * 32.5);

            Point_array[0] = (Convert.ToDouble(Point_array[0]) - chair_legs).ToString() + ", ";
            string point = String.Concat(Point_array);
            TM_send(TM_Send_format(point), false);
            while (!waitPoint(point)) { }

            Point_array[2] = "95, ";
            point = String.Concat(Point_array);
            TM_send(TM_Send_format(point));

            while (!waitPoint(point)) { }
            Thread.Sleep(700);
            SendOpenClose(XEG32, 600, 80);

            Thread.Sleep(500);

            Point_array[2] = "150, ";
            point = String.Concat(Point_array);
            TM_send(TM_Send_format(point));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GripRotation(10);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            GripRotation(11);
        }

        private void button3_Click(object sender, EventArgs e)
        {

            Action<Label, string> ChangeName = UpdateLocation;
            TM_send("1,ListenSend(90,GetString(Robot[0].CoordRobot))", false);
            Thread.Sleep(1000);

            ChangeName(NowPositionLb, String.Format("Arm_NowPosition: {0}, {1} {2}", NowPosition[0].ToString("#0.00"), NowPosition[1].ToString("#0.00"), NowPosition[2].ToString("#0.00")));
        }

        private void Do_Btn_Click(object sender, EventArgs e)
        {
            string command = null;
            bool arm;
            int Count = PointDataGrid.CurrentCell.RowIndex;
            if (Count > 0)
            {
                for (int i = PointDataGrid.Columns.Count - 6; i < PointDataGrid.Columns.Count; i++)
                {
                    command += PointDataGrid[i, Count].Value.ToString() + " ,";
                }
            }
            if (PointDataGrid[0, Count].Value.ToString() == "left")
            {
                arm = true;
            }
            else
            {
                arm = false;
            }
            switch (PointDataGrid[3, Count].Value.ToString())
            {
                case "position":
                    if (arm)
                    {
                        TM_send(TM_Send_format(command.Remove(command.Length - 1)));
                    }
                    else
                    {
                        TM_send1(TM_Send_format(command.Remove(command.Length - 1)));
                    }
                    break;

                case "Image":
                    GripPosition = Convert.ToInt32(PointDataGrid[2, Count].Value);
                    //ImageProcess();
                    break;

                case "GripOpen":
                    XEG32_Close_Other.PerformClick();
                    break;

                case "GripClose":
                    XEG32_Close_bt.PerformClick();
                    break;
            }
        }
        void DoGrip(int shape)
        {
            // string point 是給手臂的座標位置
            string point = "272.5, -268, 259, 180, 0, 90";
            TM_send(TM_Send_format(point));


            // 使用 switch 分辨形狀
            switch (shape)
            {

                case (int)Eshape.Circle:
                    //讓手臂移動第一次
                    //修改 point 的位置
                    // 使用 TM_Send_format 將 point 給 TM_Send 讓手臂移動
                    // delay 1s 等待手臂移動完畢
                    //讓手臂移動影像辨識點
                    point = "170, -268, 465, 180, 0, 90";
                    TM_send(TM_Send_format(point));
                    Thread.Sleep(10000);

                    //讓手臂移動圓上
                    point = "276, -269.5, 125, 180, 0, 90";
                    TM_send(TM_Send_format(point));
                    Thread.Sleep(1000);
                    point = "276, -269.5, 83, 180, 0, 90";
                    TM_send(TM_Send_format(point));
                    Thread.Sleep(1000);
                    this.TCPClientObject.IsMoveOver = false;
                    while (!this.TCPClientObject.IsMoveOver) { }
                    Thread.Sleep(1000);

                    //夾起圓柱
                    point = "276, -269.5, 300, 180, 0, 90";
                    TM_send(TM_Send_format(point));
                    Thread.Sleep(10000);
                    
                    //移動至放下區
                    point = "350, 33.5, 380, 180, 0, 90";
                    TM_send(TM_Send_format(point));
                    Thread.Sleep(10000);
                    //到達圓柱正上方
                    point = "431, 34.5, 175, 180, 0, 90";
                    TM_send(TM_Send_format(point));
                    Thread.Sleep(2000);
                    point = "431, 34.5, 170, 180, 0, 90";
                    TM_send(TM_Send_format(point));
                    Thread.Sleep(2000);
                    //慢慢放下
                    point = "431, 34.5, 165, 180, 0, 90";
                    TM_send(TM_Send_format(point));
                    Thread.Sleep(2000);

                    point = "431, 34.5, 160, 180, 0, 90";
                    TM_send(TM_Send_format(point));
                    Thread.Sleep(2000);

                    point = "431, 34.5, 155, 180, 0, 90";
                    TM_send(TM_Send_format(point));
                    Thread.Sleep(2000);

                    SendOpenClose(XEG32, 3200, 80);
                    Thread.Sleep(2000);

                    
                    break;


                case (int)Eshape.Rectangle:
                    point = "170, -268, 465, 180, 0, 90";
                    TM_send(TM_Send_format(point));
                    Thread.Sleep(10000);

                    //讓手臂移動長方上
                    point = "340, -268, 125, 180, 0, 90";
                    TM_send(TM_Send_format(point));
                    Thread.Sleep(1000);
                    point = "340, -268, 83, 180, 0, 90";
                    TM_send(TM_Send_format(point));
                    Thread.Sleep(1000);
                    this.TCPClientObject.IsMoveOver = false;
                    while (!this.TCPClientObject.IsMoveOver) { }
                    Thread.Sleep(1000);

                    SendOpenClose(XEG32, 2600, 80);
                    Thread.Sleep(2000);

                    //夾起長方
                    point = "340, -268, 300, 180, 0, 90";
                    TM_send(TM_Send_format(point));
                    Thread.Sleep(10000);

                    //移動至放下區
                    point = "350, 33.5, 380, 180, 0, 90";
                    TM_send(TM_Send_format(point));
                    Thread.Sleep(10000);

                    //到達長方正上方
                    point = "370.5, 35.5, 175, 180, 0, 90";
                    TM_send(TM_Send_format(point));
                    Thread.Sleep(2000);
                    point = "370.5, 35.5, 170, 180, 0, 90";
                    TM_send(TM_Send_format(point));
                    Thread.Sleep(2000);
                    point = "370.5, 35.5, 165, 180, 0, 90";
                    TM_send(TM_Send_format(point));
                    Thread.Sleep(2000);
                    //慢慢放下
                    point = "370.5, 35.5, 160, 180, 0, 90";
                    TM_send(TM_Send_format(point));
                    Thread.Sleep(2000);

                    point = "370.5, 35.5, 155, 180, 0, 90";
                    TM_send(TM_Send_format(point));
                    Thread.Sleep(2000);

                    SendOpenClose(XEG32, 3200, 80);
                    Thread.Sleep(2000);
                    
                    break;


                case (int)Eshape.Square:
                    point = "170, -268, 465, 180, 0, 90";
                    TM_send(TM_Send_format(point));
                    Thread.Sleep(10000);

                    //讓手臂移動正方上
                    point = "209, -269.5, 125, 180, 0, 90";
                    TM_send(TM_Send_format(point));
                    Thread.Sleep(1000);
                    point = "209, -269.5, 83, 180, 0, 90";
                    TM_send(TM_Send_format(point));
                    Thread.Sleep(1000);
                    this.TCPClientObject.IsMoveOver = false;
                    while (!this.TCPClientObject.IsMoveOver) { }
                    Thread.Sleep(1000);

                    SendOpenClose(XEG32, 700, 80);
                    Thread.Sleep(2000);

                    point = "209, -269.5, 125, 180, 0, 90";
                    TM_send(TM_Send_format(point));
                    Thread.Sleep(1000);
                    

                    //夾起長方
                    point= "276, -269.5, 300, 180, 0, 90";
                    TM_send(TM_Send_format(point));
                    Thread.Sleep(10000);

                    //移動至放下區
                    point = "350, 33.5, 380, 180, 0, 90";
                    TM_send(TM_Send_format(point));
                    Thread.Sleep(10000);
                    //到達正方正上方
                    point = "498, 35.5, 175, 180, 0, 90";
                    TM_send(TM_Send_format(point));
                    Thread.Sleep(2000);
                    point = "498, 35.5, 170, 180, 0, 90";
                    TM_send(TM_Send_format(point));
                    Thread.Sleep(2000);
                    point = "498, 35.5, 165, 180, 0, 90";
                    TM_send(TM_Send_format(point));
                    Thread.Sleep(2000);
                    //慢慢放下
                    point = "498, 35.5, 160, 180, 0, 90";
                    TM_send(TM_Send_format(point));
                    Thread.Sleep(2000);

                    point = "498, 35.5, 155, 180, 0, 90";
                    TM_send(TM_Send_format(point));
                    Thread.Sleep(2000);

                    SendOpenClose(XEG32, 3200, 80);
                    Thread.Sleep(2000);
                    break;
            }
        }

    }
}