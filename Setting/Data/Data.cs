using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setting.Data
{
    class Data
    {
       
        public string Server { get; set; }
        public string DataBaseName { get; set; }
        public string UserName { get; set; }
        public string Password{ get; set; }
        public string  Interval { get; set; }
        public string Mode { get; set; }

        public Data(string server, string dbname, string username, string password, string interval,string mode )
        {
            this.Server = server;
            this.DataBaseName = dbname;
            this.UserName = username;
            this.Password = password;
            this.Interval = interval;
            this.Mode = mode;
        }

        public Data()
        {
            // TODO: Complete member initialization
        }
    }
}
