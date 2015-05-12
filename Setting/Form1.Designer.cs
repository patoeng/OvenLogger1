﻿namespace Setting
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textDBServer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textDBName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textUserName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textDBPassword = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textUploadInterval = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.textMode = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textDBServer
            // 
            this.textDBServer.Location = new System.Drawing.Point(122, 12);
            this.textDBServer.Name = "textDBServer";
            this.textDBServer.Size = new System.Drawing.Size(216, 20);
            this.textDBServer.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Database Server";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Database Name";
            // 
            // textDBName
            // 
            this.textDBName.Location = new System.Drawing.Point(122, 38);
            this.textDBName.Name = "textDBName";
            this.textDBName.Size = new System.Drawing.Size(216, 20);
            this.textDBName.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Database User Name";
            // 
            // textUserName
            // 
            this.textUserName.Location = new System.Drawing.Point(122, 64);
            this.textUserName.Name = "textUserName";
            this.textUserName.Size = new System.Drawing.Size(216, 20);
            this.textUserName.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Database Password";
            // 
            // textDBPassword
            // 
            this.textDBPassword.Location = new System.Drawing.Point(122, 90);
            this.textDBPassword.Name = "textDBPassword";
            this.textDBPassword.PasswordChar = '*';
            this.textDBPassword.Size = new System.Drawing.Size(216, 20);
            this.textDBPassword.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Upload Interval (s)";
            // 
            // textUploadInterval
            // 
            this.textUploadInterval.Location = new System.Drawing.Point(122, 116);
            this.textUploadInterval.Name = "textUploadInterval";
            this.textUploadInterval.Size = new System.Drawing.Size(216, 20);
            this.textUploadInterval.TabIndex = 8;
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button2.Location = new System.Drawing.Point(0, 177);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(368, 42);
            this.button2.TabIndex = 11;
            this.button2.Text = "Update";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 145);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Mode";
            // 
            // textMode
            // 
            this.textMode.Location = new System.Drawing.Point(122, 142);
            this.textMode.Name = "textMode";
            this.textMode.Size = new System.Drawing.Size(216, 20);
            this.textMode.TabIndex = 12;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 219);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textMode);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textUploadInterval);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textDBPassword);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textUserName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textDBName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textDBServer);
            this.Name = "Form1";
            this.Text = "Logger Setting";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textDBServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textDBName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textUserName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textDBPassword;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textUploadInterval;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textMode;
    }
}

