using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Data.SQLite;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Linq.Expressions;
using System.Net;
using System.Threading;
using Microsoft.ApplicationBlocks.Data;


namespace Logger
{
    public partial class Form1 : Form
    {
        public Form1()
        {
          InitializeComponent();
           
        }

        private List<UdpWorker> _udpWorkers;
        private List<Thread> _workerThreads; 
        private string _dbConnection;
        private int _interval;
        private int _listWalker=-1;
        private bool _readyToSend = true;
        private SQLiteConnection SqlLiteConnection;
        private List<OvenData> _ovenList = new List<OvenData>();
        private UdpClient udpClient;
        private IPEndPoint ipDestEndPoint;
        public int Mode;
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.OvenId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastTemp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastReadTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UploaderTimer = new System.Windows.Forms.Timer(this.components);
            this.LoggerTimer = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(592, 162);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.richTextBox1);
            this.panel1.Location = new System.Drawing.Point(0, 427);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(775, 58);
            this.panel1.TabIndex = 2;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(775, 58);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.OvenId,
            this.Status,
            this.LastTemp,
            this.LastReadTime});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(778, 421);
            this.dataGridView1.TabIndex = 3;
            // 
            // OvenId
            // 
            this.OvenId.HeaderText = "Oven Name";
            this.OvenId.Name = "OvenId";
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            // 
            // LastTemp
            // 
            this.LastTemp.HeaderText = "Last Read Temperature (C)";
            this.LastTemp.Name = "LastTemp";
            // 
            // LastReadTime
            // 
            this.LastReadTime.HeaderText = "Last Read Time";
            this.LastReadTime.Name = "LastReadTime";
            // 
            // UploaderTimer
            // 
            this.UploaderTimer.Interval = 10000;
            this.UploaderTimer.Tick += new System.EventHandler(this.Uploader_Tick);
            // 
            // LoggerTimer
            // 
            this.LoggerTimer.Interval = 1000;
            this.LoggerTimer.Tick += new System.EventHandler(this.LoggerTimer_Tick);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(778, 490);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //
            
            //
            _dbConnection = GetDatabaseConnection();
            Mode = GetMode();
            _ovenList = GetOvenDatas(_dbConnection);

            OvenListToGridView(_ovenList);
            _interval = GetInterval();
           
            UploaderTimer.Interval = _interval;
            _listWalker = 0;
            StartWorkers();

            LoggerTimer.Enabled = true;
            UploaderTimer.Enabled = true;

        }

        private void StartWorkers()
        {
            _udpWorkers = new List<UdpWorker>();
            foreach (OvenData od in _ovenList)
            {
               _udpWorkers.Add(new UdpWorker(od));
            }

            _workerThreads = new List<Thread>();
            foreach (UdpWorker udp in _udpWorkers)
            {                
                _workerThreads.Add(new Thread(udp.DoWork));
            }

            foreach (Thread th in _workerThreads)
            {
                th.Start();
            }
        }
     
        private  void ShowMessage(string s)
        {
            richTextBox1.Text = s;
        }
        private void InsertMessage(string s)
        {
            richTextBox1.AppendText(s+"\n");
        }


        private int GetOvenIndexByIp(string ip)
        {
            for (int i = 0; i < _ovenList.Count; i++)
            {
                if (_ovenList[i].UdpIp == ip)
                {
                    return i;
                }

            }
            return -1;
        }
        private string GetDatabaseConnection()
        {
            
            SqlLiteConnection = new SQLiteConnection("Data Source=Logger.sqlite;Version=3;");
            SqlLiteConnection.Open();
            string sql = "select * from Setting";
            SQLiteCommand command = new SQLiteCommand(sql, SqlLiteConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            reader.Read();
            var temp = "Data Source=" + reader["DbServer"] + ";Initial Catalog=" + reader["DbDatabaseName"] +
                       ";Persist Security Info=True;" +
                       "User ID=" + reader["DbDatabaseUsername"] + ";Password=" + reader["DbDatabasePassword"] +
                       ";MultipleActiveResultSets=True;";
            _interval = Convert.ToInt32(reader["UploadInterval"].ToString()) * 1000; //numpang 
            SqlLiteConnection.Close();
            return temp;
        }

        private int GetInterval()
        {
            SqlLiteConnection = new SQLiteConnection("Data Source=Logger.sqlite;Version=3;");
            SqlLiteConnection.Open();
            const string sql = "select * from Setting";
            SQLiteCommand command = new SQLiteCommand(sql, SqlLiteConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            reader.Read();
            var temp = Convert.ToInt32(reader["UploadInterval"].ToString()) * 1000; //numpang 
            SqlLiteConnection.Close();
            return temp;
        }
        private int GetMode()
        {
            SqlLiteConnection = new SQLiteConnection("Data Source=Logger.sqlite;Version=3;");
            SqlLiteConnection.Open();
            const string sql = "select * from Setting";
            SQLiteCommand command = new SQLiteCommand(sql, SqlLiteConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            reader.Read();
            var temp = Convert.ToInt32(reader["Mode"].ToString()); //numpang 
            SqlLiteConnection.Close();
            return temp;
        }
        private List<OvenData> GetOvenDatas(string dbConnection)
        {
            List<OvenData> __list = new List<OvenData>();
            var ds = SqlHelper.ExecuteDataset(_dbConnection, CommandType.StoredProcedure, "usp_gpro_SelectOven");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                var j = new OvenData
                {   UdpIp = dr["AlarmIPAddr"].ToString(), 
                    OvenId = dr["OvenID"].ToString(),
                    UdpPort = Convert.ToInt32(dr["AlarmUDPPort"].ToString()),
                    Mode = this.Mode
                };
                j.StatusChanged += new ChangedEventHandler(OnStatusChanged);
                __list.Add(j);
            }
            return __list;
        }
        private void OnStatusChanged(object sender, EventArgs e)
        {
            OvenData j = (OvenData) sender;
            var i = GetOvenIndexByIp(j.UdpIp);
            UpdateGvStatus(i, _ovenList[i].Status);
            
        }
        private void OvenDataToTableRow(OvenData data)
        {
            var row = new DataGridViewRow();
            row.Cells.Add(new DataGridViewTextBoxCell{Value=data.OvenId});
            dataGridView1.Rows.Add(row);
        }
        private void OvenListToGridView(List<OvenData> list)
        {
            foreach (OvenData listRow in list)
            {
               OvenDataToTableRow(listRow);
            }
        }

        private void UpdateGv(int row, int column, string value)
        {
            dataGridView1.Rows[row].Cells[column].Value = value;
        }

        private void UpdateGvStatus(int row,string value)
        {
            UpdateGv(row,1,value);
        }
        private void UpdateGvLastReadTemp(int row,string value)
        {
            UpdateGv(row, 2, value);
        }

        private void UpdateGvlastReadTime(int row,string value)
        {
            UpdateGv(row, 3, value);
        }

       
        private void button1_Click(object sender, EventArgs e)
        {
            ShowMessage(_dbConnection);
        }

        private void Uploader_Tick(object sender, EventArgs e)
        {
            UploaderTimer.Enabled = false;
            UploadData(OvenListDataToString(_ovenList));
            UploaderTimer.Enabled = true;
        }

        private void WriteDataToGridView()
        {
            foreach (OvenData od in _ovenList)
            {
                var j = GetOvenIndexByIp(od.UdpIp);
                UpdateGvlastReadTime(j, od.LastReadTime.ToString("R"));
                UpdateGvLastReadTemp(j, od.LastReadTemperature.ToString("F"));
            }
        }
        private void LoggerTimer_Tick(object sender, EventArgs e)
        {
            LoggerTimer.Enabled = false;
            WriteDataToGridView();
            LoggerTimer.Enabled = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                foreach (UdpWorker uw in _udpWorkers)
                {
                    uw.RequestStop();
                }
                foreach (Thread th in _workerThreads)
                {
                    th.Join();
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        private String OvenListDataToString(List<OvenData> list)
        {
            StringBuilder sb = new StringBuilder();

            foreach (OvenData od in list)
            {
                sb.Append(od.OvenId+"|"+od.Status+"|"+od.LastReadTemperature+"|"+od.LastReadTime.ToString("s")+"|]");
            }
            var sbs = sb.ToString();
           
            return sbs;
        }

        private void UploadData(String data)
        {
            var par = new SqlParameter("@data", data);
            var it = SqlHelper.ExecuteNonQuery(_dbConnection, CommandType.StoredProcedure, "usp_gpro_OvenLoggerUploader", par);

        }
       
    }
}
