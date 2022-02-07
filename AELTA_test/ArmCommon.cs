using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlUI
{
    partial class Form1
    {
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
    }
}
