namespace SlaveLoader2
{
    partial class AddUserDilogForm
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
            this.IpTextBox = new System.Windows.Forms.TextBox();
            this.PortTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.AddBt = new System.Windows.Forms.Button();
            this.CancelBt = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // IpTextBox
            // 
            this.IpTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.IpTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.IpTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.IpTextBox.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.IpTextBox.Location = new System.Drawing.Point(63, 9);
            this.IpTextBox.Name = "IpTextBox";
            this.IpTextBox.Size = new System.Drawing.Size(145, 22);
            this.IpTextBox.TabIndex = 0;
            // 
            // PortTextBox
            // 
            this.PortTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.PortTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PortTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PortTextBox.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.PortTextBox.Location = new System.Drawing.Point(263, 9);
            this.PortTextBox.Name = "PortTextBox";
            this.PortTextBox.Size = new System.Drawing.Size(57, 22);
            this.PortTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 22);
            this.label1.TabIndex = 2;
            this.label1.Text = "IPv4";
            // 
            // AddBt
            // 
            this.AddBt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.AddBt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AddBt.FlatAppearance.BorderSize = 0;
            this.AddBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddBt.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AddBt.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.AddBt.Location = new System.Drawing.Point(25, 37);
            this.AddBt.Name = "AddBt";
            this.AddBt.Size = new System.Drawing.Size(133, 23);
            this.AddBt.TabIndex = 4;
            this.AddBt.Text = "Add";
            this.AddBt.UseVisualStyleBackColor = false;
            this.AddBt.Click += new System.EventHandler(this.AddBt_Click);
            // 
            // CancelBt
            // 
            this.CancelBt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.CancelBt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CancelBt.FlatAppearance.BorderSize = 0;
            this.CancelBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelBt.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CancelBt.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.CancelBt.Location = new System.Drawing.Point(176, 37);
            this.CancelBt.Name = "CancelBt";
            this.CancelBt.Size = new System.Drawing.Size(133, 23);
            this.CancelBt.TabIndex = 5;
            this.CancelBt.Text = "Cancel";
            this.CancelBt.UseVisualStyleBackColor = false;
            this.CancelBt.Click += new System.EventHandler(this.CancelBt_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Location = new System.Drawing.Point(214, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 22);
            this.label2.TabIndex = 6;
            this.label2.Text = "Port";
            // 
            // AddUserDilogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(334, 71);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CancelBt);
            this.Controls.Add(this.AddBt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PortTextBox);
            this.Controls.Add(this.IpTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AddUserDilogForm";
            this.Text = "Add User";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox IpTextBox;
        private System.Windows.Forms.TextBox PortTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button AddBt;
        private System.Windows.Forms.Button CancelBt;
        private System.Windows.Forms.Label label2;
    }
}