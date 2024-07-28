using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace SlaveLoader2
{
    public partial class AddUserDilogForm : Form
    {
        public AddUserDilogForm()
        {
            InitializeComponent();
            this.ApplySettings();
        }
        /// <summary>
        /// Не безопасное свойство
        /// </summary>
        public IPEndPoint ResultEndPoint { get => new IPEndPoint(IPAddress.Parse(IpTextBox.Text), int.Parse(PortTextBox.Text)); }
        private void AddBt_Click(object sender, EventArgs e)
        {
            try
            {
                IPAddress.Parse(IpTextBox.Text);
            }
            catch
            {
                MessageBox.Show("IPv4 адресс введен некоректно", "Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                int.Parse(PortTextBox.Text);
            }
            catch
            {
                MessageBox.Show("Port введен некоректно", "Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelBt_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
