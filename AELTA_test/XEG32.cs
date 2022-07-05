using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ControlUI
{
    partial class GripPosition_
    {
        
        #region   --XEG32夾爪控制程式--

        byte Direction = 2;//模式:絕對位置
        int XEG32_CJog = 0,
            XEG32_Force = 100,
            XEG32_PushVel = 0,
            XEG32_PushPosStk = 0;
        //模式設定:絕對位置

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
            SendOpenClose(XEG32);
        }

        private void XEG32_Close_bt_Click(object sender, EventArgs e)
        {
            XEG32_PosStk_text.Text = "600";//夾爪張開 0 mm
            XEG32_Vel_text.Text = "80";//速度 50 mm/s
            SendOpenClose(XEG32);
        }
        private void XEG32_Close_Other_Click(object sender, EventArgs e)
        {
            XEG32_PosStk_text.Text = "200";//夾爪張開 0 mm
            XEG32_Vel_text.Text = "80";//速度 50 mm/s
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
                  i_CJog = XEG32_CJog,
                  i_Force = XEG32_Force,
                  i_PushVel = XEG32_PushVel,
                  i_PushPosStk = XEG32_PushPosStk;
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

        public string SendOpenClose(SerialPort Comm1, int PosStk, int Vel)
        {
            try
            {
                    int i_PosStk = PosStk,
                  i_Vel = Vel * 100,
                  i_CJog = XEG32_CJog,
                  i_Force = XEG32_Force,
                  i_PushVel = XEG32_PushVel,
                  i_PushPosStk = XEG32_PushPosStk;
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

    }
}
