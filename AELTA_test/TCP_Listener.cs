using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlUI
{
    internal class TCP_Listener
    {
        private string _deviceIp { get; }
        private int _port { get; }
        private IPAddress ip;
        private TcpListener TCPListener;
        private TcpClient newClient;
        private delegate void updateUI(string str, Control ctl);
        private delegate void Tolist(string str, string str1, string str2, string str3, string str4, string str5);
        public TCP_Listener(string deviceIp, int port)
        {
            _deviceIp = deviceIp;
            _port = port;
            ip = IPAddress.Parse(_deviceIp);
            TCPListener = new TcpListener(ip, _port);
        }
        public bool Start()
        {
            try { TCPListener.Start(); return true; }
            catch { return false; }
        }
        public bool Connect()
        {
            try
            {
                newClient = TCPListener.AcceptTcpClient();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public string Recive()
        {
            try
            {
                NetworkStream networkStream = newClient.GetStream();
                byte[] bytesFrom = new byte[65536];
                int byteRead = networkStream.Read(bytesFrom, 0, 65536);
                string msg = Encoding.Unicode.GetString(bytesFrom, 0, byteRead);
                return msg;
            }                    
            catch 
            {
                return null;
            }
        }
        public void send(string str)
        {
            NetworkStream clientStream = newClient.GetStream();
            BinaryWriter bw = new BinaryWriter(clientStream);
            bw.Write(str);
        }

        private void Update(string srt, Control ctl)
        {
            if (ctl.InvokeRequired)
            {
                updateUI update = new updateUI(Update);
                ctl.Invoke(update, srt, ctl);
            }        
            else
            {
               ctl.Text += srt; 
            }
        }
    }

}

