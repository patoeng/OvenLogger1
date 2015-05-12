using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;
using Setting.Data;


namespace Setting
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Data.Data SettingData;
        private void Form1_Load(object sender, EventArgs e)
        {
            if (!IsDataBaseExist(DataBaseName))           
            {
                CreateDataBase(DataBaseName);
            }

            SettingData = GetSetting(DataBaseName);
            DataShowToTb(SettingData);

        }

        private const string DataBaseName = "Logger.sqlite";
        private Data.Data GetSetting(string dbname)
        {
            Data.Data data=new Data.Data();
            var connectionString = "Data Source=" + dbname + ";Version=3;";
            const string sql = "select * from Setting";
                     
            using (SQLiteConnection c = new SQLiteConnection(connectionString))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, c))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            data.DataBaseName = reader["DbDatabaseName"].ToString();
                            data.Server = reader["DbServer"].ToString();
                            data.UserName = reader["DbDatabaseUserName"].ToString();
                            data.Password = reader["DbDatabasePassword"].ToString();
                            data.Interval = reader["UploadInterval"].ToString();
                            data.Mode = reader["Mode"].ToString();
                        }
                    }
                }
            }
            return data;
        }

        private bool IsDataBaseExist(string dbname)
        {
            return File.Exists(dbname);
        }

        private bool CreateDataBase(string dbname)
        {
          
                SQLiteConnection.CreateFile(dbname);
                var connectionString = "Data Source=" + dbname + ";Version=3;";


                string sql =
                    @"CREATE TABLE ""Setting"" (""DbServer"" varchar,""DbDatabaseName"" varchar,""DbDatabasePassword"" varchar,""UploadInterval"" INTEGER DEFAULT (null) , ""DbDatabaseUsername"" VARCHAR, ""Mode"" INTEGER DEFAULT 0)";
                using (SQLiteConnection c = new SQLiteConnection(connectionString))
                {
                    c.Open();
                    using (SQLiteCommand command = new SQLiteCommand(sql, c))
                    {                       
                        command.ExecuteNonQuery();
                    }
                }
                
                sql =
                    @"INSERT INTO Setting (DbServer,DbDatabaseName,DbDatabasePassword,UploadInterval,DbDatabaseUsername,Mode) VALUES (@server,@db,@passw,@interval,@user,@mode)";

                using (SQLiteConnection c = new SQLiteConnection(connectionString))
                {
                    c.Open();
                    using (SQLiteCommand command = new SQLiteCommand(sql, c))
                    {
                        command.Parameters.AddWithValue("@server", "127.0.0.1");
                        command.Parameters.AddWithValue("@db", "MYOVEN");
                        command.Parameters.AddWithValue("@passw", "passwordsa");
                        command.Parameters.AddWithValue("@interval", 20);
                        command.Parameters.AddWithValue("@user", "sa");
                        command.Parameters.AddWithValue("@mode", "0");
                        command.ExecuteNonQuery();
                    }
                }
            return true;
        }

        private bool UpdateDatabase(Data.Data data,string dbname)
        {

            var connectionString = "Data Source=" + dbname + ";Version=3;";

            var sql = @"UPDATE Setting SET DbServer= @server, DbDatabaseName=@db, DbDatabasePassword=@passw, UploadInterval=@interval,
                        DbDatabaseUsername=@user, Mode=@mode";

             using (SQLiteConnection c = new SQLiteConnection(connectionString))
                {
                    c.Open();
                    using (SQLiteCommand command = new SQLiteCommand(sql, c))
                    {
                        command.Parameters.AddWithValue("@server",  data.Server);
                        command.Parameters.AddWithValue("@db", data.DataBaseName);
                        command.Parameters.AddWithValue("@passw", data.Password);
                        command.Parameters.AddWithValue("@interval", data.Interval);
                        command.Parameters.AddWithValue("@user", data.UserName);
                        command.Parameters.AddWithValue("@mode", data.Mode);
                        command.ExecuteNonQuery();
                    }
                }
            return true;
        }

        private bool DataShowToTb(Data.Data data)
        {

            textDBServer.Text = data.Server;
            textDBPassword.Text = data.Password;
            textDBName.Text = data.DataBaseName;
            textUserName.Text = data.UserName;
            textUploadInterval.Text = data.Interval;
            textMode.Text = data.Mode;    

            return true;           
        }

        private bool LoadFromTb(Data.Data data)
        {
            data.Server = textDBServer.Text;
            data.Password = textDBPassword.Text;
            data.DataBaseName = textDBName.Text;
            data.UserName = textUserName.Text;
            data.Interval = textUploadInterval.Text;
            data.Mode = textMode.Text;
           
            return true;           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadFromTb(SettingData);
            UpdateDatabase(SettingData,DataBaseName);
        }
    }
}
