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
            try
            { GetNowPosition(recv_data); }
            catch (Exception ex) { }
            string str = string.Format("[{0}] {1}", DateTime.Now.ToString("HH:mm:ss:fff"), recv_data);
            this.showRecvDataLog.AppendLine(str);
            ReceiveDataHandler(recv_data.ToString());
            AddReceiveData(showRecvDataLog.ToString(), this.TB_RecvData);
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
        public void TM_send(string _string)
        {
            string s = string.Empty;
            _string += "\r\nQueueTag(1)";
            s = SocketClientObject.DataToPacket("$TMSCT", _string);
            //this.TB_Command.Text = s;
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            if (this.TCPClientObject != null)
            {
                this.TCPClientObject.WriteSyncData(bytes);
            }
        }
        #endregion
    }
}
