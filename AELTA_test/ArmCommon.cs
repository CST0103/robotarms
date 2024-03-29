﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlUI
{
    partial class GripPosition_
    {

        double[] NowPosition = null;
        bool ArmMoving= false;

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

        #endregion


        #endregion
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
                _textbox.Invoke(ReceiveData, _receivedata, _textbox);
            }
            else
            {
                _textbox.Text = _receivedata;
            }
        }

        private void ReceiveDataHandler(string recvData)
        {
            recvData = recvData.Trim();

            try
            {
                int index = recvData.IndexOf("$TMSTA");

                recvData = recvData.Substring(index, recvData.Length - index);
                string target = recvData.Split(',')[4];
                if (target == "true")
                {
                    ArmMoving = true;
                }
            }
            catch (Exception ex)
            { }
        }
        private bool waitPoint(string point)
        {
            string[] _point = point.Split(',');
            double[] _Nowposition = new double[] { };
            TM_send("1,ListenSend(90,GetString(Robot[0].CoordRobot))", false);
            Thread.Sleep(500);
            if (NowPosition == null)
            { return false; }
            do
            {
                TM_send("1,ListenSend(90,GetString(Robot[0].CoordRobot))", false);
                Thread.Sleep(300);
                _Nowposition = NowPosition;
            } while (
            Math.Abs(NowPosition[0] - Convert.ToDouble(_point[0])) >= 1 ||
            Math.Abs(NowPosition[1] - Convert.ToDouble(_point[1])) >= 1 ||
            Math.Abs(NowPosition[2] - Convert.ToDouble(_point[2])) >= 1
            );
            return true;
        }
        public void GetNowPosition(string recvData)
        {

            double[] _lastPosition = null;
            recvData = recvData.Trim();
            if (recvData.IndexOf("$TMSTA") == 0 && recvData.Split(',')[2] == "90")
            {
                var head = recvData.IndexOf('{');
                var end = recvData.IndexOf('}');
                var pos = recvData.Substring(head + 1, (end - head) - 1).Split(',');

                if (pos.Length == 6)
                {
                    NowPosition = Array.ConvertAll<string,double>(pos,value =>(Convert.ToDouble(value)));
                }
            }
        }
    }
}
