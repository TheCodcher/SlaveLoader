namespace SlaveLoader2
{
    [BaseColor]
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ButtonPanel = new System.Windows.Forms.Panel();
            this.RemoveBt = new System.Windows.Forms.Button();
            this.InfoBt = new System.Windows.Forms.Button();
            this.LoadBt = new System.Windows.Forms.Button();
            this.PingBt = new System.Windows.Forms.Button();
            this.UserBox = new System.Windows.Forms.ComboBox();
            this.NameLabel = new System.Windows.Forms.Label();
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.PortLabel = new System.Windows.Forms.Label();
            this.AddUserBt = new System.Windows.Forms.Button();
            this.SettingBt = new System.Windows.Forms.Button();
            this.ButtonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ButtonPanel
            // 
            this.ButtonPanel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.ButtonPanel.Controls.Add(this.RemoveBt);
            this.ButtonPanel.Controls.Add(this.InfoBt);
            this.ButtonPanel.Controls.Add(this.LoadBt);
            this.ButtonPanel.Controls.Add(this.PingBt);
            this.ButtonPanel.Location = new System.Drawing.Point(16, 72);
            this.ButtonPanel.Name = "ButtonPanel";
            this.ButtonPanel.Size = new System.Drawing.Size(322, 26);
            this.ButtonPanel.TabIndex = 1;
            this.ButtonPanel.Visible = false;
            // 
            // RemoveBt
            // 
            this.RemoveBt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.RemoveBt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RemoveBt.FlatAppearance.BorderSize = 0;
            this.RemoveBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RemoveBt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RemoveBt.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.RemoveBt.Location = new System.Drawing.Point(235, 0);
            this.RemoveBt.Margin = new System.Windows.Forms.Padding(0);
            this.RemoveBt.Name = "RemoveBt";
            this.RemoveBt.Size = new System.Drawing.Size(74, 22);
            this.RemoveBt.TabIndex = 10;
            this.RemoveBt.Text = "Remove";
            this.RemoveBt.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.RemoveBt.UseVisualStyleBackColor = false;
            this.RemoveBt.Click += new System.EventHandler(this.RemoveBt_Click);
            // 
            // InfoBt
            // 
            this.InfoBt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.InfoBt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.InfoBt.FlatAppearance.BorderSize = 0;
            this.InfoBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.InfoBt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.InfoBt.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.InfoBt.Location = new System.Drawing.Point(162, 0);
            this.InfoBt.Margin = new System.Windows.Forms.Padding(0);
            this.InfoBt.Name = "InfoBt";
            this.InfoBt.Size = new System.Drawing.Size(63, 22);
            this.InfoBt.TabIndex = 9;
            this.InfoBt.Text = "Info";
            this.InfoBt.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.InfoBt.UseVisualStyleBackColor = false;
            this.InfoBt.Click += new System.EventHandler(this.InfoBt_Click);
            // 
            // LoadBt
            // 
            this.LoadBt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.LoadBt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LoadBt.FlatAppearance.BorderSize = 0;
            this.LoadBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LoadBt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LoadBt.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.LoadBt.Location = new System.Drawing.Point(85, 0);
            this.LoadBt.Margin = new System.Windows.Forms.Padding(0);
            this.LoadBt.Name = "LoadBt";
            this.LoadBt.Size = new System.Drawing.Size(67, 22);
            this.LoadBt.TabIndex = 8;
            this.LoadBt.Text = "Upload";
            this.LoadBt.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.LoadBt.UseVisualStyleBackColor = false;
            this.LoadBt.Click += new System.EventHandler(this.LoadBt_Click);
            // 
            // PingBt
            // 
            this.PingBt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.PingBt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PingBt.FlatAppearance.BorderSize = 0;
            this.PingBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PingBt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PingBt.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.PingBt.Location = new System.Drawing.Point(0, 0);
            this.PingBt.Margin = new System.Windows.Forms.Padding(0);
            this.PingBt.Name = "PingBt";
            this.PingBt.Size = new System.Drawing.Size(75, 22);
            this.PingBt.TabIndex = 7;
            this.PingBt.Text = "Ping";
            this.PingBt.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.PingBt.UseVisualStyleBackColor = false;
            this.PingBt.Click += new System.EventHandler(this.PingBt_Click);
            // 
            // UserBox
            // 
            this.UserBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UserBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.UserBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UserBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UserBox.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.UserBox.FormattingEnabled = true;
            this.UserBox.Location = new System.Drawing.Point(16, 36);
            this.UserBox.Name = "UserBox";
            this.UserBox.Size = new System.Drawing.Size(364, 28);
            this.UserBox.TabIndex = 2;
            this.UserBox.SelectionChangeCommitted += new System.EventHandler(this.UserBox_SelectionChangeCommitted);
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NameLabel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.NameLabel.Location = new System.Drawing.Point(12, 8);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(57, 22);
            this.NameLabel.TabIndex = 3;
            this.NameLabel.Text = "Name";
            // 
            // NameTextBox
            // 
            this.NameTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.NameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.NameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NameTextBox.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.NameTextBox.Location = new System.Drawing.Point(75, 8);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.Size = new System.Drawing.Size(100, 22);
            this.NameTextBox.TabIndex = 4;
            this.NameTextBox.TextChanged += new System.EventHandler(this.NameTextBox_TextChanged);
            // 
            // PortLabel
            // 
            this.PortLabel.AutoSize = true;
            this.PortLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PortLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PortLabel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.PortLabel.Location = new System.Drawing.Point(181, 8);
            this.PortLabel.Name = "PortLabel";
            this.PortLabel.Size = new System.Drawing.Size(53, 22);
            this.PortLabel.TabIndex = 5;
            this.PortLabel.Text = "Port: ";
            // 
            // AddUserBt
            // 
            this.AddUserBt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddUserBt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.AddUserBt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AddUserBt.FlatAppearance.BorderSize = 0;
            this.AddUserBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddUserBt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AddUserBt.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.AddUserBt.Location = new System.Drawing.Point(287, 6);
            this.AddUserBt.Margin = new System.Windows.Forms.Padding(0);
            this.AddUserBt.Name = "AddUserBt";
            this.AddUserBt.Size = new System.Drawing.Size(93, 22);
            this.AddUserBt.TabIndex = 6;
            this.AddUserBt.Text = "Add User";
            this.AddUserBt.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.AddUserBt.UseVisualStyleBackColor = false;
            this.AddUserBt.Click += new System.EventHandler(this.AddUserBt_Click);
            // 
            // SettingBt
            // 
            this.SettingBt.BackColor = System.Drawing.Color.Transparent;
            this.SettingBt.BackgroundImage = global::SlaveLoader2.Properties.Resources.lro_staffing_technology_icon;
            this.SettingBt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.SettingBt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SettingBt.FlatAppearance.BorderSize = 0;
            this.SettingBt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.SettingBt.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.SettingBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SettingBt.ForeColor = System.Drawing.Color.Transparent;
            this.SettingBt.Location = new System.Drawing.Point(344, 70);
            this.SettingBt.Name = "SettingBt";
            this.SettingBt.Size = new System.Drawing.Size(36, 28);
            this.SettingBt.TabIndex = 7;
            this.SettingBt.UseVisualStyleBackColor = false;
            this.SettingBt.Click += new System.EventHandler(this.SettingBt_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(397, 105);
            this.Controls.Add(this.SettingBt);
            this.Controls.Add(this.AddUserBt);
            this.Controls.Add(this.PortLabel);
            this.Controls.Add(this.NameTextBox);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.UserBox);
            this.Controls.Add(this.ButtonPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "SlaveLoader2 v1.1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ButtonPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        [BaseColor]
        private System.Windows.Forms.Panel ButtonPanel;
        [ActiveColor]
        private System.Windows.Forms.ComboBox UserBox;
        [BaseColor]
        private System.Windows.Forms.Label NameLabel;
        [ActiveColor]
        private System.Windows.Forms.TextBox NameTextBox;
        [BaseColor]
        private System.Windows.Forms.Label PortLabel;
        [ActiveColor]
        private System.Windows.Forms.Button AddUserBt;
        [ActiveColor]
        private System.Windows.Forms.Button RemoveBt;
        [ActiveColor]
        private System.Windows.Forms.Button InfoBt;
        [ActiveColor]
        private System.Windows.Forms.Button LoadBt;
        [ActiveColor]
        private System.Windows.Forms.Button PingBt;
        private System.Windows.Forms.Button SettingBt;
    }
}

