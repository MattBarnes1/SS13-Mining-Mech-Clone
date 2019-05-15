namespace ClientUpdater
{
    partial class GuestPass
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
            this.label1 = new System.Windows.Forms.Label();
            this.UsernameTextBox = new System.Windows.Forms.TextBox();
            this.PasswordText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.LoginButton = new System.Windows.Forms.Button();
            this.InstallDirectory = new System.Windows.Forms.FolderBrowserDialog();
            this.DownloadProgress = new System.Windows.Forms.ProgressBar();
            this.Status = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.OnlineOfflineStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // UsernameTextBox
            // 
            this.UsernameTextBox.Location = new System.Drawing.Point(74, 23);
            this.UsernameTextBox.Name = "UsernameTextBox";
            this.UsernameTextBox.Size = new System.Drawing.Size(148, 20);
            this.UsernameTextBox.TabIndex = 1;
            // 
            // PasswordText
            // 
            this.PasswordText.Location = new System.Drawing.Point(74, 60);
            this.PasswordText.Name = "PasswordText";
            this.PasswordText.PasswordChar = '*';
            this.PasswordText.Size = new System.Drawing.Size(148, 20);
            this.PasswordText.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password";
            // 
            // LoginButton
            // 
            this.LoginButton.Location = new System.Drawing.Point(74, 86);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(148, 23);
            this.LoginButton.TabIndex = 4;
            this.LoginButton.Text = "Login and Download";
            this.LoginButton.UseVisualStyleBackColor = true;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // InstallDirectory
            // 
            this.InstallDirectory.HelpRequest += new System.EventHandler(this.folderBrowserDialog1_HelpRequest);
            // 
            // DownloadProgress
            // 
            this.DownloadProgress.Location = new System.Drawing.Point(13, 178);
            this.DownloadProgress.Name = "DownloadProgress";
            this.DownloadProgress.Size = new System.Drawing.Size(209, 23);
            this.DownloadProgress.TabIndex = 5;
            // 
            // Status
            // 
            this.Status.AutoSize = true;
            this.Status.Location = new System.Drawing.Point(88, 162);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(0, 13);
            this.Status.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Server Status:";
            // 
            // OnlineOfflineStatus
            // 
            this.OnlineOfflineStatus.AutoSize = true;
            this.OnlineOfflineStatus.ForeColor = System.Drawing.Color.Red;
            this.OnlineOfflineStatus.Location = new System.Drawing.Point(88, 125);
            this.OnlineOfflineStatus.Name = "OnlineOfflineStatus";
            this.OnlineOfflineStatus.Size = new System.Drawing.Size(37, 13);
            this.OnlineOfflineStatus.TabIndex = 8;
            this.OnlineOfflineStatus.Text = "Offline";
            // 
            // GuestPass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(250, 230);
            this.Controls.Add(this.OnlineOfflineStatus);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Status);
            this.Controls.Add(this.DownloadProgress);
            this.Controls.Add(this.LoginButton);
            this.Controls.Add(this.PasswordText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.UsernameTextBox);
            this.Controls.Add(this.label1);
            this.Name = "GuestPass";
            this.Text = "Client Update";
            this.Load += new System.EventHandler(this.GuestPass_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox UsernameTextBox;
        private System.Windows.Forms.TextBox PasswordText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.FolderBrowserDialog InstallDirectory;
        private System.Windows.Forms.ProgressBar DownloadProgress;
        private System.Windows.Forms.Label Status;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label OnlineOfflineStatus;
    }
}

