namespace SlaveLoader2
{
    [BaseColor]
    partial class FormConsole
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
            this.TextPrinter = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // TextPrinter
            // 
            this.TextPrinter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextPrinter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.TextPrinter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextPrinter.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TextPrinter.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.TextPrinter.Location = new System.Drawing.Point(12, 12);
            this.TextPrinter.Multiline = true;
            this.TextPrinter.Name = "TextPrinter";
            this.TextPrinter.ReadOnly = true;
            this.TextPrinter.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TextPrinter.Size = new System.Drawing.Size(475, 426);
            this.TextPrinter.TabIndex = 0;
            // 
            // FormConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(499, 450);
            this.Controls.Add(this.TextPrinter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FormConsole";
            this.Text = "Console Window";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormConsole_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        [ActiveColor]
        private System.Windows.Forms.TextBox TextPrinter;
    }
}