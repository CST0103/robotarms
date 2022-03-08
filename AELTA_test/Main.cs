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
    X=-69,
    Y=-7
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
            TM_send("1,ListenSend(90,GetString(Robot[0].CoordRobot))");
        }


        #region --通訊--
        private void FirstSever()
        {
            int Count = 0;
            Action<String> ModifyText = SockMsg;
            Action<int, double, double, double, double, double, double, bool> Tolist = CoordinateConversion;
            try
            {
                string Declare;
                bool _open_flag = FirstListener.Start();
                Declare = _open_flag == true ? "FirstSever Open" : "FirstSever Not Open";
                Invoke(ModifyText, Declare);

                bool _connect_flag = FirstListener.Connect();
                Declare = _connect_flag == true ? "FirstSever Connect" : "FirstSever Not Connect";
                Invoke(ModifyText, Declare);

                while (true)
                {
                    int position_int = 2;
                    string reciveData = FirstListener.Recive();
                    Invoke(ModifyText, Count.ToString() + "  First Command");
                    string[] data = reciveData.Split('$');
                    switch(data[position_int].ToString())
                    {
                        case "position":
                            Invoke(Tolist,
                                Convert.ToInt32(data[1]),
                                Convert.ToDouble(data[position_int + 1]),
                                Convert.ToDouble(data[position_int + 2]),
                                Convert.ToDouble(data[position_int + 3]),
                                Convert.ToDouble(data[position_int + 4]),
                                Convert.ToDouble(data[position_int + 5]),
                                Convert.ToDouble(data[position_int + 6]),
                                true);
                            break;
                        case "Image":
                            Img_Btn.PerformClick();
                            break;
                    }
                }
            }
            catch (Exception e) { }
        }

        private void SecondSever()
        {
            int Count = 0;
            Action<String> ModifyText = SockMsg;
            Action<int, double, double, double, double, double, double, bool> Tolist = CoordinateConversion;
            string Declare;
            try
            {
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
                    string[] data = reciveData.Split('$');
                    Invoke(Tolist,
                        Convert.ToInt32(data[0]),
                        Convert.ToDouble(data[1]),
                        Convert.ToDouble(data[2]),
                        Convert.ToDouble(data[3]),
                        Convert.ToDouble(data[4]),
                        Convert.ToDouble(data[5]),
                        Convert.ToDouble(data[6]),
                        false);
                    Count++;
                    GripPosition = Convert.ToInt32(data[0]);
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

        private void CoordinateConversion(int Conit, double d1, double d2, double d3, double d4, double d5, double d6, bool ArmFlag)
        {
            double[] origin = new double[6] { 450, -122, 300, 180, 0, 90 };

            Action<int, double, double, double, double, double, double, bool> DataToGrid = WriteDataGrid;
            Action<string, string, string, string, string, string, bool> Toarm = armMove;



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

            Invoke(DataToGrid, Conit, d1, d2, d3, d4, d5, d6, ArmFlag);
            Invoke(Toarm, d1.ToString(), d2.ToString(), d3.ToString(), d4.ToString(), d5.ToString(), d6.ToString(), ArmFlag);
        }


        #endregion

        #region --點位表格--

        private void WriteDataGrid(int count, double d1, double d2, double d3, double d4, double d5, double d6, bool ArmFlag)
        {
            if (ArmFlag)
            { this.PointDataGrid.Rows.Add(count, d1, d2, d3, d4, d5, d6); }
            else
            { this.PointDataGrid1.Rows.Add(count, d1, d2, d3, d4, d5, d6); }
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



        #region --TM手臂動作--

        #region --TM_副程式--

        //TM控制副程式

        //手臂速度百分比
        double sp_pc = 1.0;
        private void TB_sp_pc_TextChanged(object sender, EventArgs e)
        {
            try
            { sp_pc = Convert.ToDouble(TB_sp_pc.Text); }
            catch (Exception ex) { };
            
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
                armMove(data[k][0], data[k][1], data[k][2], data[k][3], data[k][4], data[k][5], true);
                Thread.Sleep(500);
            }
        }
        //可修改為Excel使用
        private void AssembleBtn_Click(object sender, EventArgs e)
        {
            string point;
            //original position
            point = "450, -122, 300 ,180,0,90";
            TM_send(TM_Send_format(point, 100));
            Thread.Sleep(2000);
            XEG32_Open_bt.PerformClick();
            Thread.Sleep(1000);

            //聯軸器上方
            point = "300, -287, 150, 180, 0 , 90";
            TM_send(TM_Send_format(point, 100));
            Thread.Sleep(5000);

            //聯軸器夾取位置
            point = "300, -287, 65, 180, 0 , 90";
            TM_send(TM_Send_format(point, 100));
            Thread.Sleep(3000);

            //聯軸器夾取
            XEG32_Close_Other.PerformClick();
            Thread.Sleep(1000);

            //聯軸器夾起
            point = "300, -287, 150, 180, 0 , 90";
            TM_send(TM_Send_format(point, 100));
            Thread.Sleep(2000);

            //固定座上方
            point = "535,-291,150,180,0,90";
            TM_send(TM_Send_format(point, 100));
            Thread.Sleep(3000);

            //放入聯軸器(速度30慢放)
            point = "535,-291, 75,180,0,90";
            TM_send(TM_Send_format(point, 30));
            Thread.Sleep(6000);

            //開夾爪
            XEG32_Open_bt.PerformClick();
            Thread.Sleep(1000);

            //抬起手臂
            point = "535,-291,150,180,0,90";
            TM_send(TM_Send_format(point, 100));
            Thread.Sleep(3000);

            //旋轉手臂
            point = "535,-291,150,180,0,0";
            TM_send(TM_Send_format(point, 400));
            Thread.Sleep(5500);

            //墊高板上方
            point = "365, -298, 150,180,0,0";
            TM_send(TM_Send_format(point, 100));
            Thread.Sleep(3000);

            //墊高版夾取位置
            point = "365, -298, 75,180,0,0";
            TM_send(TM_Send_format(point, 100));
            Thread.Sleep(4000);

            //墊高版夾取
            XEG32_Close_bt.PerformClick();
            Thread.Sleep(1000);

            //墊高版夾起
            point = "365, -298, 150, 180,0,0";
            TM_send(TM_Send_format(point, 100));
            Thread.Sleep(3000);

            //固定座上方
            point = "535, -291, 150, 180,0,90";
            TM_send(TM_Send_format(point, 100));
            Thread.Sleep(5000);

            //放入墊高版(速度30慢放)
            point = "535, -291, 105, 180,0,90";
            TM_send(TM_Send_format(point, 30));
            Thread.Sleep(5500);
            XEG32_Open_bt.PerformClick();
            Thread.Sleep(1000);

            //手臂抬起
            point = "535, -291, 150, 180,0,90";
            TM_send(TM_Send_format(point, 100));
            Thread.Sleep(3000);

            //萬象軸上方
            point = "438, -320, 150, 180,0,90";
            TM_send(TM_Send_format(point, 100));
            Thread.Sleep(3000);

            //萬象軸夾取位置
            point = "438, -320, 45, 180,0,90";
            TM_send(TM_Send_format(point, 100));
            Thread.Sleep(3000);

            //萬象軸夾取
            XEG32_Close_bt.PerformClick();
            Thread.Sleep(1000);

            //手臂抬起
            point = "438, -320, 150, 180,0,90";
            TM_send(TM_Send_format(point, 100));
            Thread.Sleep(3000);

            //固定座上方
            point = "535, -291, 150, 180,0,90";
            TM_send(TM_Send_format(point, 100));
            Thread.Sleep(3000);

            //放入萬象軸(速度30慢放)
            point = "535, -291, 85, 180,0,90";
            TM_send(TM_Send_format(point, 30));
            Thread.Sleep(5000);

            //開啟夾爪
            XEG32_Open_bt.PerformClick();
            Thread.Sleep(1000);

            //抬起手臂
            point = "535, -291, 150, 180,0,90";
            TM_send(TM_Send_format(point, 100));
            Thread.Sleep(3000);
        }
        private string TM_Send_format(string cmd, int speed = 100)
        {
            int allSpeed = (int)(speed * sp_pc);
            return @"1,PTP(""CPP"", " + cmd + "," + string.Format("{0:000}", allSpeed) + ",200,0,false)";
        }
        private void Img_Btn_Click(object sender, EventArgs e)
        {
            TM_send("1,ListenSend(90,GetString(Robot[0].CoordRobot))");
            string point = null;
            string point_format = null;
            Thread.Sleep(500);

            double[] ImageCenter_bais = ImageHandler.ImageRecognition();
            BaisLB.Text = "Image_Bais: " + ImageCenter_bais[0].ToString("#0.00") + ", " + ImageCenter_bais[1].ToString("#0.00");

            NowPositionLb.Text = String.Format("Arm_NowPosition: {0}, {1}", NowPosition[0].ToString("#0.00"), NowPosition[1].ToString("#0.00"));

            Grip(ImageCenter_bais);




        }
        private void Grip(double[] ImageBais)
        {
            double[] ImageRecogntionPosition = NowPosition;

            ImageRecogntionPosition[0] = ImageRecogntionPosition[0] - (double)ImageRecogntionBais.X - ImageBais[0];
            ImageRecogntionPosition[1] = ImageRecogntionPosition[1] - (double)ImageRecogntionBais.Y + ImageBais[1];
            img_Label.Text = String.Format("GoalPosition: {0}, {1}", ImageRecogntionPosition[0].ToString("#0.00"), ImageRecogntionPosition[1].ToString("#0.00"));

            string point = double2Point(ImageRecogntionPosition);
            TM_send(TM_Send_format(point, 30));

            if (ArmMoving_CheckBox.Checked & GripPosition != 0) {
                switch(GripPosition)
                {
                    case 1:
                        ImageRecogntionPosition[2] = 65;
                        break;

                    case 2:
                        ImageRecogntionPosition[2] = 45;
                        break;
                    case 3:
                        ImageRecogntionPosition[2] = 50;
                        break;
                }

                point = double2Point(ImageRecogntionPosition);

                TM_send(TM_Send_format(point,30));
                Thread.Sleep(5000);
                XEG32_Close_Other.PerformClick();

            }
        }
        private string double2Point(double[] position, int iterate = 0)
        {
            string result = null;
            for(int i = 0; i < 6; i++)
            {
               result += position[i]+",";
            }
            return result.Remove(result.Length - 1);
        }

        private void 聯軸器_Click(object sender, EventArgs e)
        {
            if(img_Position.Checked)
                TM_send(TM_Send_format("231, -294, 150, 180, 0, 90"));
            else
                TM_send(TM_Send_format("300, -287, 150, 180, 0, 90"));
        }

        private void 萬象軸_Click(object sender, EventArgs e)
        {
            if(img_Position.Checked)
                TM_send(TM_Send_format("369, -327, 150, 180, 0, 90"));
            else
                TM_send(TM_Send_format("438, -320, 150, 180, 0, 90"));
        }

        private void 固定座_Click(object sender, EventArgs e)
        {
            if(img_Position.Checked)
                TM_send(TM_Send_format("462, -301, 150, 180, 0, 90"));
            else
                TM_send(TM_Send_format("535, -291, 150, 180, 0, 90"));
        }

    }
}