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

                //XEG32
                GripCnt_Btn.PerformClick();
            } catch (Exception ex) { }

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
            Action<int, string, double, double, double, double, double, double, bool> Tolist = DataToArm;
            Action<int, string, double, double, double, double, double, double, bool> ToDataGrid = WriteDataGrid;
            Action Image = ImageProcess;

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
                                    if (position[2] < 75) { position[2] = 75; }
                                    break;
                                case 2:
                                    if (position[2] < 85) { position[2] = 85; }
                                    break;
                                case 3:
                                    if (position[2] < 82) { position[2] = 82; }
                                    break;

                                case 10:
                                    if (position[2] < 82) { position[2] = 82; }
                                    break;

                                case 11:
                                    if (position[2] < 82) { position[2] = 82; }
                                    break;

                                case 5:
                                    if (position[2] <72) { position[2] = 72; }
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
                            if(GripPosition == 10)
                            {
                                GripRotation(Convert.ToInt32(data[2]));
                                break;
                            }
                            if (Image_checkBox.Checked)
                            {
                                Invoke(ToDataGrid, Convert.ToInt32(data[1]), data[command_int], 0, 0, 0, 0, 0, 0, true);
                                do
                                {
                                    TM_send("1,ListenSend(90,GetString(Robot[0].CoordRobot))");
                                    Thread.Sleep(1000);
                                } while (
                                NowPosition[0] - refrencePosition[0] > 1 &&
                                NowPosition[1] - refrencePosition[1] > 1 &&
                                NowPosition[2] - refrencePosition[2] > 1
                                );
                                Image.Invoke();
                            }
                            break;
                        case "GripOpen":
                            while (!ArmMoving) { }
                                SendOpenClose(XEG32, 3200, 80);
                                Invoke(ToDataGrid, Convert.ToInt32(data[1]), data[command_int], 0, 0, 0, 0, 0, 0, true);
                            break;
                        case "GripClose":
                            SendOpenClose(XEG32, 200, 80);
                            Invoke(ToDataGrid,Convert.ToInt32(data[1]), data[command_int], 0, 0, 0, 0, 0, 0, true);
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
                    if (position[1] < -110) { position[1] = -110; }
                    position = CoordinateConversion(position, false);
                    if (position[2] <= 100) { position[2] = 100; }
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

            double x, y, z;

            x = Convert.ToDouble(d1) + xbias;
            y = Convert.ToDouble(d2) + ybias;
            z = Convert.ToDouble(d3) + zbias;

            x = Math.Round(x,2);
            y = Math.Round(y,2);
            z = Math.Round(z,2);

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

        private void DataToArm(int Conit, string command, double d1, double d2, double d3, double d4, double d5, double d6, bool ArmFlag)
        {
            Action<int, string, double, double, double, double, double, double, bool> DataToGrid = WriteDataGrid;
            Action<string, string, string, string, string, string, bool> Toarm = armMove;

            DataToGrid.Invoke(Conit, command, d1, d2, d3, d4, d5, d6, ArmFlag);
            Toarm.Invoke(d1.ToString(), d2.ToString(), d3.ToString(), d4.ToString(), d5.ToString(), d6.ToString(), ArmFlag);
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
            int j = 1;

            Action<string> ModifyText = SockMsg;
            Action<String, String, String, String, String, String, bool> Toarm = armMove;

            for (int i = 0; i < PointDataGrid.RowCount - 1; i++)
            {

                String rd1 = Convert.ToString(PointDataGrid[j + 1, i].Value);
                String rd2 = Convert.ToString(PointDataGrid[j + 2, i].Value);
                String rd3 = Convert.ToString(PointDataGrid[j + 3, i].Value);
                String rd4 = Convert.ToString(PointDataGrid[j + 4, i].Value);
                String rd5 = Convert.ToString(PointDataGrid[j + 5, i].Value);
                String rd6 = Convert.ToString(PointDataGrid[j + 6, i].Value);

                Invoke(ModifyText, "ReMove" + rd1 + "," + rd2 + "," + rd3 + "," + rd4 + "," + rd5 + "," + rd6);
                Invoke(Toarm, rd1, rd2, rd3, rd4, rd5, rd6, ArmFlag);
            }
        }
        #endregion

        #region Excel

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

        #endregion

        private string TM_Send_format(string cmd, int speed = 100)
        {
            int allSpeed = (int)(speed * sp_pc);
            return @"1,PTP(""CPP"", " + cmd + "," + string.Format("{0:000}", allSpeed) + ",200,0,false)";
        }

        private void Img_Btn_Click(object sender, EventArgs e)
        {
            ImageProcess();
        }
        
        private void ImageProcess()
        {
            TM_send("1,ListenSend(90,GetString(Robot[0].CoordRobot))");

            Thread.Sleep(1000);
            double[] ImageRecogntionPosition = NowPosition;

            ImageRecogntionPosition[0] = ImageRecogntionPosition[0] + (double)ImageRecogntionBais.X;
            ImageRecogntionPosition[1] = ImageRecogntionPosition[1] + (double)ImageRecogntionBais.Y;

            TM_send(TM_Send_format(double2Point(ImageRecogntionPosition)));
            Thread.Sleep(1000);

            double[] ImageCenter_bais = ImageHandler.ImageRecognition();
            while (Math.Abs(ImageCenter_bais[0]) >= 7 || Math.Abs(ImageCenter_bais[1]) >= 7)
            {
                while (Math.Abs(ImageCenter_bais[0]) >= 5)
                {
                    ImageCenter_bais = ImageHandler.ImageRecognition();
                    double bais = -1;
                    if (ImageCenter_bais[0] < 0) { bais = 1; }
                    Thread.Sleep(600);
                    TM_send($"1,Move_Line(\"CPP\",{bais} , 0, 0, 0, 0, 0, 125, 200, 0, false)", false);

                    //ChangeName(BaisLB, "Image_Bais: " + ImageCenter_bais[0].ToString("#0.00") + ", " + ImageCenter_bais[1].ToString("#0.00"));
                }

                while (Math.Abs(ImageCenter_bais[1]) >= 5)
                {
                    double bais = 1;
                    ImageCenter_bais = ImageHandler.ImageRecognition();
                    if (ImageCenter_bais[1] < 0) { bais = -1; }
                    Thread.Sleep(600);
                    TM_send($"1,Move_Line(\"CPP\", 0, {bais}, 0, 0, 0, 0, 125, 200, 0, false)", false);

                    // ChangeName(BaisLB, "Image_Bais: " + ImageCenter_bais[0].ToString("#0.00") + ", " + ImageCenter_bais[1].ToString("#0.00"));
                }
                ImageCenter_bais = ImageHandler.ImageRecognition();

            }
            TM_send("1,ListenSend(90,GetString(Robot[0].CoordRobot))", false);
            Thread.Sleep(1000);

            //ChangeName(BaisLB, "Image_Bais: " + ImageCenter_bais[0].ToString("#0.00") + ", " + ImageCenter_bais[1].ToString("#0.00"));
            //ChangeName(NowPositionLb, String.Format("Arm_NowPosition: {0}, {1}", NowPosition[0].ToString("#0.00"), NowPosition[1].ToString("#0.00")));

            Grip();

        }
        private void Grip()
        {
            double[] ImageRecogntionPosition = NowPosition;

            ImageRecogntionPosition[0] = ImageRecogntionPosition[0] - (double)ImageRecogntionBais.X;
            ImageRecogntionPosition[1] = ImageRecogntionPosition[1] - (double)ImageRecogntionBais.Y;
            //ChangeName(img_Label, String.Format("GoalPosition: {0}, {1}", ImageRecogntionPosition[0].ToString("#0.00"), ImageRecogntionPosition[1].ToString("#0.00")));

            string point = double2Point(ImageRecogntionPosition);
            TM_send(TM_Send_format(point));

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
                        ImageRecogntionPosition[2] = 42;
                        break;

                    case 5:
                        ImageRecogntionPosition[2] = 65;
                        break;

                }

                point = double2Point(ImageRecogntionPosition);

                TM_send(TM_Send_format(point));
                Thread.Sleep(4000);

                if (GripPosition == 1 ||GripPosition == 5)
                {
                    SendOpenClose(XEG32, 200, 80);
                }
                else if(GripPosition == 2)
                {
                    SendOpenClose(XEG32, 0, 80);
                }
                else
                {
                    SendOpenClose(XEG32, 500, 80);
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
        private void GripRotation(int NowStatic)
        {
            string point = "540, -190, 150, 180, 0, 90";
            TM_send(TM_Send_format(point));
            Thread.Sleep(1000);
            point = "540, -190, 50, 180, 0, 90";
            TM_send(TM_Send_format(point));
            Thread.Sleep(3000);
            if (NowStatic == 10)
                SendOpenClose(XEG32, 1300, 80);
            else if (NowStatic == 11)
                SendOpenClose(XEG32, 3200, 80);
            point = "540, -190, 150, 180, 0, 90";
            Thread.Sleep(1000);
            TM_send(TM_Send_format(point));
        }

    }
}