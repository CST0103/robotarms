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
//使用XEG-32夾爪 請加入此參考
//
using System.Runtime.InteropServices;
using System.Net;

using System.Net.Sockets;
using ClosedXML.Excel;

using dynamixel_sdk;

public enum ImageRecogntionBais
{
    X = -68,
    Y = -7
}
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

        private delegate void AddConnectDataDelegate(string _connectstatus, Label _label);
        private delegate void AddReceiveDataDelegate(string _receivedata, TextBox _textbox);

        private AutoResetEvent autoResetEvent = new AutoResetEvent(false);
        private int GripPosition = 0;
        private int port_num;

        //Image
        ImageProcess ImageHandler = new ImageProcess();
        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Point bais = new Point();
            //Gripper: AX12A and XEG32 - initial
            try
            {

                #region AX12A 
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

                #endregion

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
            Action<String> ModifyText = SockMsg;
            Action<int, string, double, double, double, double, double, double, bool> Tolist = DataToArm;
            Action<int, string, double, double, double, double, double, double, bool> ToDataGrid = WriteDataGrid;
            Action Image = ImageProcess;
            string Declare;
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
                    int command_int = 3;
                    string reciveData = FirstListener.Recive();
                    Invoke(ModifyText, Count.ToString() + "  First Command");

                    string[] data = reciveData.Split('$');
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

                            switch (Convert.ToInt32(data[command_int - 1]))
                            {
                                case 1:
                                    if (position[2] < 72) { position[2] = 72; }
                                    break;
                                case 2:
                                    if (position[2] < 85) { position[2] = 85; }
                                    break;
                                case 3:
                                    if (position[2] < 82) { position[2] = 82; }
                                    break;
                                default:
                                    break;
                            }
                            refrencePosition = position;

                            Invoke(Tolist,
                                Convert.ToInt32(data[1]),//position_name
                                data[command_int],//command
                                position[0],
                                position[1],
                                position[2],
                                position[3],
                                position[4],
                                position[5],
                                true);
                            ArmMoving = false;
                            break;
                        case "Image":
                            if (Image_checkBox.Checked)
                            {
                                Invoke(ToDataGrid, Convert.ToInt32(data[1]), data[command_int], 0, 0, 0, 0, 0, 0, true);
                                do
                                {
                                    TM_send("1,ListenSend(90,GetString(Robot[0].CoordRobot))");
                                    Thread.Sleep(2000);
                                } while (
                                NowPosition[0] - refrencePosition[0] > 1 &&
                                NowPosition[1] - refrencePosition[1] > 1 &&
                                NowPosition[2] - refrencePosition[2] > 1
                                );
                                Image.Invoke();
                            }
                            break;
                        case "GripOpen":
                            SendOpenClose(XEG32, 3200, 80);
                            Invoke(ToDataGrid, Convert.ToInt32(data[1]), data[command_int], 0, 0, 0, 0, 0, 0, true);
                            break;
                        case "GripClose":
                            SendOpenClose(XEG32, 200, 80);
                            Invoke(ToDataGrid, Convert.ToInt32(data[1]), data[command_int], 0, 0, 0, 0, 0, 0, true);
                            break;
                    }
                    Count++;
                }
                catch (Exception ex) { };
            }
        }

        private void SecondSever()
        {
            int Count = 0;
            Action<String> ModifyText = SockMsg;
            Action<int, string, double, double, double, double, double, double, bool> Tolist = DataToArm;
            string Declare;
            try
            {
                bool _open_flag = SecondListener.Start();
                Declare = _open_flag == true ? "SecondSever Open" : "SecondSever Not Open";
                Invoke(ModifyText, Declare);

                bool _connect_flag = SecondListener.Connect();
                Declare = _connect_flag == true ? "SecondSever Connect" : "SecondSever Not Connect";
                Invoke(ModifyText, Declare);
                int command_int = 1;
                while (true)
                {
                    string reciveData = SecondListener.Recive();
                    Invoke(ModifyText, Count.ToString() + "  Second Command");
                    string[] data = reciveData.Split('$');
                    double[] position = new double[6];
                    for (int i = 0; i < 6; i++)
                    {
                        position[i] = Convert.ToDouble(data[command_int + 1 + i]);
                    }
                    position = CoordinateConversion(position, false);
                    if(position[2] <= 100) { position[2] = 100; }
                    Invoke(Tolist,
                        Convert.ToInt32(data[0]),
                        data[1],
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
        protected void SockMsg<T>(T teste)
        {
            socketmsg.Text += Environment.NewLine + teste.ToString();

            socketmsg.SelectionStart = socketmsg.TextLength;
            socketmsg.ScrollToCaret();
        }

        #endregion

        #region --座標修正--

        private double[] CoordinateConversion(double[] position,bool ArmFlag)
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
                zbias = 380;
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

            position[0] = d1;
            position[1] = d2;
            position[2] = d3;
            position[3] = d4;
            position[4] = d5;
            position[5] = d6;

            return position;

        }

        private void DataToArm(int Conit, string command, double d1, double d2, double d3, double d4, double d5, double d6, bool ArmFlag)
        {
            double[] origin = new double[6] { 450, -122, 300, 180, 0, 90 };

            Action<int, string, double, double, double, double, double, double, bool> DataToGrid = WriteDataGrid;
            Action<string, string, string, string, string, string, bool> Toarm = armMove;

            Invoke(DataToGrid, Conit, command, d1, d2, d3, d4, d5, d6, ArmFlag);
            Invoke(Toarm, d1.ToString(), d2.ToString(), d3.ToString(), d4.ToString(), d5.ToString(), d6.ToString(), ArmFlag);
        }


        #endregion

        #region --點位表格--

        private void WriteDataGrid(int count, string command, double d1, double d2, double d3, double d4, double d5, double d6, bool ArmFlag)
        {
            if (ArmFlag)
            { this.PointDataGrid.Rows.Add(count, command, d1, d2, d3, d4, d5, d6); }
            else
            { this.PointDataGrid1.Rows.Add(count, command, d1, d2, d3, d4, d5, d6); }
        }

        #endregion

        #region --移動--

        private void armMove(String d1, String d2, String d3, String d4, String d5, String d6, bool ArmFlag)
        {

            int speed = 100;
            var item = (d1: dataint[0], speed: string.Format("{0:000}", speed * sp_abs));

            //Line(CAP
            string test_string = @"1,PTP(""CPP""" + "," + d1 + "," + d2 + "," + d3 + "," + d4 + "," + d5 + "," + d6 + "," + $"{item.speed} ,200,0,false)";
            TB_SendData.Text = test_string;
            if (ArmFlag)
                TM_send(test_string);
            else
                TM_send1(test_string);

        }

        private void armReMove(object sender, EventArgs e)
        {
            bool ArmFlag = true;
            Action<string> ModifyText = SockMsg;
            Action<String, String, String, String, String, String, bool> Toarm = armMove;
            for (int i = 0; i < PointDataGrid.RowCount - 1; i++)
            {

                String rd1 = Convert.ToString(PointDataGrid[1, i].Value);
                String rd2 = Convert.ToString(PointDataGrid[2, i].Value);
                String rd3 = Convert.ToString(PointDataGrid[3, i].Value);
                String rd4 = Convert.ToString(PointDataGrid[4, i].Value);
                String rd5 = Convert.ToString(PointDataGrid[5, i].Value);
                String rd6 = Convert.ToString(PointDataGrid[6, i].Value);

                Invoke(ModifyText, "ReMove" + rd1 + "," + rd2 + "," + rd3 + "," + rd4 + "," + rd5 + "," + rd6);
                Invoke(Toarm, rd1, rd2, rd3, rd4, rd5, rd6, ArmFlag);
            }
        }
        #endregion

        private void ActionBtn_Click(object sender, EventArgs e)
        {
            var workbook = new XLWorkbook(@"E:\碩論\MasterCode\ControlUI\DataGridViewExport.xlsx");
            var ws = workbook.Worksheet(1);
            int NumberOfLastRow = ws.LastRowUsed().RowNumber();
            List<string[]> data = new List<string[]>();
            var range = ws.RangeUsed();
            for (int i = 1; i < range.RowCount() + 1; i++)
            {
                string[] point = new string[6];
                for (int j = 1; j < range.ColumnCount() + 1; j++)
                {
                    point[j - 1] = (string)ws.Cell(i, j).Value.ToString();
                }
                data.Add(point);
            }
            for (int k = 0; k < data.Count; k++)
            {
                armMove(data[k][0], data[k][1], data[k][2], data[k][3], data[k][4], data[k][5], true);
                Thread.Sleep(500);
            }
        }
        private string TM_Send_format(string cmd, int speed = 100)
        {
            int allSpeed = (int)(speed * sp_pc);
            return @"1,PTP(""CPP"", " + cmd + "," + string.Format("{0:000}", allSpeed) + ",200,0,false)";
        }
        private void ChangeName(Control control, string text)
        {
            if (control.InvokeRequired)
            {
                Action<Control, string> action = ChangeName;
                action.Invoke(control, text);
            }
            else
            {
                control.Text = text;
            }
        }
        
        private void ImageProcess()
        {
            TM_send("1,ListenSend(90,GetString(Robot[0].CoordRobot))");

            Thread.Sleep(1500);
            double[] ImageRecogntionPosition = NowPosition;

            ImageRecogntionPosition[0] = ImageRecogntionPosition[0] + (double)ImageRecogntionBais.X;
            ImageRecogntionPosition[1] = ImageRecogntionPosition[1] + (double)ImageRecogntionBais.Y;
            TM_send(TM_Send_format(double2Point(ImageRecogntionPosition)));
            Thread.Sleep(1000);
            string point = null;
            string point_format = null;
            double[] ImageCenter_bais = new double[] { };
            ImageCenter_bais = ImageHandler.ImageRecognition();

            while (ImageCenter_bais[0] >= 1 || ImageCenter_bais[0] <= -1)
            {
                ImageCenter_bais = ImageHandler.ImageRecognition();
                double bais = -1;
                if (ImageCenter_bais[0] < 0)
                { bais = 1; }
                Thread.Sleep(700);
                TM_send($"1,Move_Line(\"CPP\",{bais} , 0, 0, 0, 0, 0, 125, 200, 0, false)", false);

                //ChangeName(BaisLB, "Image_Bais: " + ImageCenter_bais[0].ToString("#0.00") + ", " + ImageCenter_bais[1].ToString("#0.00"));
            }
            while (ImageCenter_bais[1] >= 1 || ImageCenter_bais[1] <= -1)
            {
                double bais = 1;
                ImageCenter_bais = ImageHandler.ImageRecognition();
                if (ImageCenter_bais[1] < 0)
                { bais = -1; }
                Thread.Sleep(700);
                TM_send($"1,Move_Line(\"CPP\", 0, {bais}, 0, 0, 0, 0, 125, 200, 0, false)", false);

               // ChangeName(BaisLB, "Image_Bais: " + ImageCenter_bais[0].ToString("#0.00") + ", " + ImageCenter_bais[1].ToString("#0.00"));
            }

            TM_send("1,ListenSend(90,GetString(Robot[0].CoordRobot))", false);
            Thread.Sleep(1000);

            ImageRecogntionPosition = NowPosition;
            //ChangeName(BaisLB, "Image_Bais: " + ImageCenter_bais[0].ToString("#0.00") + ", " + ImageCenter_bais[1].ToString("#0.00"));
            //ChangeName(NowPositionLb, String.Format("Arm_NowPosition: {0}, {1}", NowPosition[0].ToString("#0.00"), NowPosition[1].ToString("#0.00")));

            Grip();

        }

        private void Img_Btn_Click(object sender, EventArgs e)
        {
            ImageProcess();
        }

        private void Grip()
        {
            TM_send("1,ListenSend(90,GetString(Robot[0].CoordRobot))", false);
            Thread.Sleep(1500);
            double[] ImageRecogntionPosition = NowPosition;

            ImageRecogntionPosition[0] = ImageRecogntionPosition[0] - (double)ImageRecogntionBais.X;
            ImageRecogntionPosition[1] = ImageRecogntionPosition[1] - (double)ImageRecogntionBais.Y;
            //ChangeName(img_Label, String.Format("GoalPosition: {0}, {1}", ImageRecogntionPosition[0].ToString("#0.00"), ImageRecogntionPosition[1].ToString("#0.00")));

            string point = double2Point(ImageRecogntionPosition);
            TM_send(TM_Send_format(point, 30));

            if (ImageGrip_CheckBox.Checked & GripPosition != 0)
            {
                switch (GripPosition)
                {
                    case 1:
                        ImageRecogntionPosition[2] = 65;
                        break;

                    case 2:
                        ImageRecogntionPosition[2] = 65;
                        break;
                    case 3:
                        ImageRecogntionPosition[2] = 45;
                        break;
                }

                point = double2Point(ImageRecogntionPosition);

                TM_send(TM_Send_format(point, 30));
                Thread.Sleep(10000);
                if (GripPosition == 1)
                {
                    SendOpenClose(XEG32, 200, 80);
                }
                else
                {
                    SendOpenClose(XEG32, 600, 80);
                }
                Thread.Sleep(1000);
                ImageRecogntionPosition[2] = 150;
                TM_send(TM_Send_format(double2Point(ImageRecogntionPosition)));
                Thread.Sleep(1000);

            }
        }
        private string double2Point(double[] position, int iterate = 0)
        {
            string result = null;
            for (int i = 0; i < 6; i++)
            {
                result += position[i] + ",";
            }
            return result.Remove(result.Length - 1);
        }
    }
}