using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
namespace ControlUI
{
    partial class Form1
    {

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

                                    TCPClientObject1.ReceiveData += new SocketClientObject.TCPReceiveData(this.GetNowPosition);
                                    //TCPClientObject1.ReceiveData += new SocketClientObject.TCPReceiveData(this.showReceiveData1);
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

        private void btn_TMtest_Click1(object sender, EventArgs e)
        {
            int speed = 100;
            string test_string = @"1,PTP(""CPP"", 450, 60, 380 ,180,0,90," + string.Format("{0:000}", speed * sp_pc) + ",200,0,false)";
            TM_send1(test_string);
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
            string cmd = TB_SendData1.Text;
            TM_send1(cmd);
//            if (this.CB_Listen1.Checked == true)
//            {
//                s = SocketClientObject.DataToPacket(CB_Customized1.SelectedItem.ToString(), this.TB_SendData1.Text);
//            }
//            else
//            {
//                s = this.TB_SendData1.Text;
//            }
//            this.TB_Command1.Text = s;
//            byte[] bytes = Encoding.UTF8.GetBytes(s);
//            if (this.TCPClientObject1 != null)
//            {
//                this.TCPClientObject1.WriteSyncData(bytes);
//            }
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
        void TM_send1(string _string,bool flag = true)
        {
            string s = string.Empty;
            if (flag) { _string += "\r\nQueueTag(1)"; }
            s = SocketClientObject.DataToPacket("$TMSCT", _string);
            //this.TB_Command.Text = s;
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            if (this.TCPClientObject1 != null)
            {
                this.TCPClientObject1.WriteSyncData(bytes);
            }
        }
    }
}
