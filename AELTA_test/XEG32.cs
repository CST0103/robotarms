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
    partial class Form1
    {
        
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

    }
}
