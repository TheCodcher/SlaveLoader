using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SlaveLoader2
{
    public partial class FormConsole : Form
    {
        public bool Ending { get; private set; } = false;
        public FormConsole()
        {
            InitializeComponent();
            this.ApplySettings();
            Settings.SettingsChanges += this.ApplySettings;
        }

        public void Clear()
        {
            try
            {
                Invoke((Action)TextPrinter.Clear);
            }
            catch (InvalidOperationException ep) { }
        }

        public void Write(string message)
        {
            try
            {
                Invoke((Action<string>)TextPrinter.AppendText, message);
            }
            catch (InvalidOperationException ep) 
            {
                try
                {
                    TextPrinter.Text += message;
                }
                catch { }
            }
        }

        public void WriteLine(string message = "")
        {
            try
            {
                Invoke((Action<string>)TextPrinter.AppendText, message + Environment.NewLine);
            }
            catch (InvalidOperationException ep)
            {
                try
                {
                    TextPrinter.Text += message + Environment.NewLine;
                }
                catch { }
            }
        }

        private void FormConsole_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.SettingsChanges -= this.ApplySettings;
            Ending = true;
        }
    }
}
