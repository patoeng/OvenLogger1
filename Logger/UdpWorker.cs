using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer= System.Threading.Timer;

namespace Logger
{
    class UdpWorker
    {
        private Timer _timer;
        private UdpClient udpClient;
        private IPEndPoint ipDestEndPoint;

        public OvenData Oven
        {
            get{return _oven;}
        }
        private OvenData _oven;
        // This method will be called when the thread is started. 
        public UdpWorker(OvenData oven)
        {
            _oven = oven;
            var port = oven.UdpPort;
            var ip = IPAddress.Parse(oven.UdpIp);
            ipDestEndPoint = new IPEndPoint(ip,port);
            udpClient = new UdpClient
            {
                Client =
                {
                    ReceiveTimeout = 300,
                    Blocking = false
                }
            };

            Random rnd = new Random();
            int r = rnd.Next(100, 500); // creates a number between 1 and 12
            _timer = new Timer(TimerCallback, null, 0, 2000+r);
        }
        public void DoWork()
        {
            while (!_shouldStop)
            {
                Thread.Sleep(1000);
                
            }
            _timer.Dispose();
        }

        private void Work()
        {
                    byte[] data = {(byte) '/', (byte) ' '};               
                    udpClient.Send(data, 2, ipDestEndPoint);
                    _oven.FailedCount++;                     
                    try
                    {
                        //data = udpClient.Receive(ref ipDestEndPoint);
                        udpClient.BeginReceive(new AsyncCallback(RecieveMessage), null);

                    }
                    catch (SocketException te)
                    {
                        
                      
                    }  
                    
        }
        
        public void RequestStop()
        {
            _shouldStop = true;
        }
        // Volatile is used as hint to the compiler that this data 
        // member will be accessed by multiple threads. 
        private volatile bool _shouldStop;

        private void RecieveMessage(IAsyncResult ar)
        {
            //  IPEndPoint remote = new IPEndPoint(IPAddress.Any, 0);
            byte[] data = udpClient.EndReceive(ar, ref ipDestEndPoint);
            var dataS = Encoding.ASCII.GetString(data);
            _oven.LastReadTemperature =Convert.ToDecimal(LoggerGetTemperature(dataS));
            _oven.LastReadTime = DateTime.Now;
            _oven.FailedCount = 0;
            _oven.Status = LoggerGetOvenStatus(dataS);
         }
      

        private string LoggerGetTemperature(string data)
        {
            data = data.Trim();
            var s = data.Split(':');
            if (s.Length != 3)
            {
                return "";
            }
            var t = Convert.ToInt32(s[2]);
            decimal y = t;
            y = y / 4;

            return y.ToString("F");
        }
        private string LoggerGetOvenStatus(string data)
        {
            var s = data.Split(':');
            string hasil = "";
            if (s.Length != 3)
            {
                return "";
            }
            var t = Convert.ToInt32(s[0]);
            if ((t & 1) == 1)
            {
                hasil = "RUN";
            }
            if ((t & 1) == 0)
            {
                hasil = "FINISH";
            }
            return hasil;
        }

        private void TimerCallback(Object o)
        {
            Work();
        }
    }
}
