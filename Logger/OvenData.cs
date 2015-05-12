using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public delegate void ChangedEventHandler(object sender, EventArgs e);
    class OvenData
    {
        private int _failedCount;
        private string _status;
        public string OvenId;
        public string UdpIp;
        public int UdpPort;
        public decimal LastReadTemperature = 0;
        public DateTime LastReadTime= DateTime.Now;
        public int FailedLimit = 2;
        public string Status { 
            get { return _status; }
            set
            {
                if (Mode != 1)
                {
                    _status = value;
                    OnStatusChanged(EventArgs.Empty);
                }
            } 
        }
        public int Mode;
        public int FailedCount
        {
            get { return _failedCount;}
            set
            {
                _failedCount = value;
                if ((_failedCount >= FailedLimit) && (Mode == 1))
                {
                    if (_status != "FINISH")
                    {
                        _status = "FINISH";
                        OnStatusChanged(EventArgs.Empty);
                    }
                }
                if ((value==0) && (Mode == 1))
                {
                    if (_status != "RUN")
                    {
                        _status = "RUN";
                        OnStatusChanged(EventArgs.Empty);
                    }
                }
            }
        }

        //
        public event ChangedEventHandler StatusChanged;
        protected virtual void OnStatusChanged(EventArgs e)
        {
            if (StatusChanged != null)
                StatusChanged(this, e);
        }
    }
}
