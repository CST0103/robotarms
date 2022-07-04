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
        #region --TM1--
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
                                    TCPClientObject.ReceiveData += new SocketClientObject.TCPReceiveData(this.GetNowPosition);
                                    //TCPClientObject.ReceiveData += new SocketClientObject.TCPReceiveData(this.showReceiveData);
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

        //手臂歸位
        private void btn_TMtest_Click(object sender, EventArgs e)
        {
            int speed = 100;
            string test_string = @"1,PTP(""CPP"", 450, -122, 300 ,180,0,90," + string.Format("{0:000}", speed * sp_pc) + ",200,0,false)";
            TM_send(test_string);
        }

        private void btn_ClearSendData_Click(object sender, EventArgs e)
        {
            this.TB_SendData.Text = string.Empty;
        }

        private void btn_ClearRecvData_Click(object sender, EventArgs e)
        {
            this.TB_RecvData.Text = string.Empty;
            this.showRecvDataLog.Clear();
        }

        private void btn_Disconnect_Click(object sender, EventArgs e)
        {
            if ((this.TCPClientObject != null) && this.TCPClientObject.Disconnect())
            {
                this.TCPClientObject.ReceiveData -= new SocketClientObject.TCPReceiveData(this.GetNowPosition);
                this.TCPClientObject.ReceiveData -= new SocketClientObject.TCPReceiveData(this.showReceiveData);
                this.TCPClientObject = null;
            }
        }

        private void btn_Send_Click(object sender, EventArgs e)
        {
            string cmd = TB_SendData.Text;
            TM_send(cmd);
//            string s = string.Empty;
//            if (this.CB_Listen.Checked == true)
//            {
//                s = SocketClientObject.DataToPacket(CB_Customized.SelectedItem.ToString(), this.TB_SendData.Text);
//            }
//            else
//            {
//                s = this.TB_SendData.Text;
//            }
//            this.TB_Command.Text = s;
//            byte[] bytes = Encoding.UTF8.GetBytes(s);
//            if (this.TCPClientObject != null)
//            {
//                this.TCPClientObject.WriteSyncData(bytes);
//            }
        }
        private void showConnectStatus(object sender, string resp)
        {
            AddConnectStatus(resp, LB_ConnectionStatus);
        }

        public void showReceiveData(object sender, string recv_data)
        {
            string str = string.Format("[{0}] {1}", DateTime.Now.ToString("HH:mm:ss:fff"), recv_data);
            this.showRecvDataLog.AppendLine(str);
            AddReceiveData(showRecvDataLog.ToString(), this.TB_RecvData);
        }
        public void GetNowPosition(object sender, string recv_data)
        {
            try
            { GetNowPosition(recv_data); }
            catch (Exception ex) { }
            ReceiveDataHandler(recv_data.ToString());
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
        public void TM_send(string _string, bool flag = true)
        {
            string s = string.Empty;
            if (flag) { _string += "\r\nQueueTag(1)"; }
            s = SocketClientObject.DataToPacket("$TMSCT", _string);
            //this.TB_Command.Text = s;
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            if (this.TCPClientObject != null)
            {
                this.TCPClientObject.WriteSyncData(bytes);
            }
        }
        #endregion
        private void 聯軸器_Click(object sender, EventArgs e)
        {
            if (img_Position.Checked)
                TM_send(TM_Send_format("235, -294, 150, 180, 0, 90"));
            else
            {
                TM_send(TM_Send_format("300, -287, 150, 180, 0, 90"));
            }
        }

        private void 萬象軸_Click(object sender, EventArgs e)
        {
            if (img_Position.Checked)
                TM_send(TM_Send_format("327, -327, 150, 180, 0, 90"));
            else
                TM_send(TM_Send_format("395, -320, 150, 180, 0, 90"));
        }

        private void 固定座_Click(object sender, EventArgs e)
        {
            if (img_Position.Checked)
                TM_send(TM_Send_format("462, -301, 150, 180, 0, 90"));
            else
                TM_send(TM_Send_format("535, -291, 150, 180, 0, 90"));
        }
        private void 墊高板_Click(object sender, EventArgs e)
        {
            if (img_Position.Checked)
                TM_send(TM_Send_format("465, -88, 150, 180, 0, 90"));
            else
                TM_send(TM_Send_format("530, -95, 150, 180, 0, 90"));
        }
    }
}
