using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SlaveLoader2
{
    public partial class Form1 : Form
    {
        static internal SaveInformation ProgrammDate { get; private set; }
        NetWorker NetWorker;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Settings.LoadSettings();
            this.ApplySettings();
            Settings.SettingsChanges += this.ApplySettings;
            ProgrammDate = Settings.LoadInfo();

            UserBox.Items.AddRange(ProgrammDate.UserList.ToArray());
            NameTextBox.Text = ProgrammDate.MyInfo.Name == ProgrammDate.MyInfo.IP.ToString() ? "" : ProgrammDate.MyInfo.Name;
            PortLabel.Text += ProgrammDate.MyInfo.Port;

            NetWorker = new NetWorker(ProgrammDate.MyInfo.IPEnd, () => ProgrammDate, DowloadReqest);
        }
        private FileInfo DowloadReqest(IPAddress RemoteIP, NetWorker.DownloadRequestDate Date, out Func<bool> GetEnd, out Action<string> StateEvent)
        {
            GetEnd = null;
            StateEvent = null;
            var Remote = new IPEndPoint(RemoteIP, Date.ListenerPort);
            var RemoteString = Remote.ToString();
            var user = ProgrammDate.UserList.Find(u => u.IPEnd.ToString() == RemoteString);
            if (user == null)
            {
                user = new UserINFOItem(Remote);
                if (MessageBox.Show($"Пользователя {RemoteString} нет в Вашем списке контактов, но он отправил запрос на загрузку файла. Хотите внести {RemoteString} в список контактов?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Invoke((Action)(() =>
                    {
                        ProgrammDate.UserList.Add(user);
                        UserBox.Items.Add(user);
                        UserBox.SelectedItem = user;
                        UserBox.Text = user.ToString();
                        ButtonPanel.Visible = true;
                    }));
                }
            }
            var filedate = Date.GetFileSize();
            if (MessageBox.Show($"Пользователь {user.ToString()} отправил запрос на загрузку файла. Хотите загрузить файл {Date.FileName} {Math.Round(filedate.Count, 3).ToString()} {filedate.Size.ToString()}?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return null;
            string path = "";
            using (var file = new SaveFileDialog())
            {
                file.FileName = Date.FileName;
                if ((DialogResult)Invoke((Func<DialogResult>)file.ShowDialog) == DialogResult.Cancel) return null;
                path = file.FileName;
            }
            var consl = new FormConsole();
            Task.Run(() => Application.Run(consl));
            GetEnd = () => consl.Ending;
            StateEvent = consl.WriteLine;
            return new FileInfo(path);
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.SettingsChanges -= this.ApplySettings;

            NetWorker.Dispose();

            Settings.SaveInfo(ProgrammDate);
        }

        private void AddUserBt_Click(object sender, EventArgs e)
        {
            using (var DilogForm = new AddUserDilogForm())
            {
                var reqResult = DilogForm.ShowDialog();
                if (reqResult == DialogResult.Cancel) return;
                var temp = new UserINFOItem(DilogForm.ResultEndPoint);
                ProgrammDate.UserList.Add(temp);
                UserBox.Items.Add(temp);
                UserBox.SelectedItem = temp;
                UserBox.Text = temp.ToString();
                ButtonPanel.Visible = true;
            }
        }

        private void UserBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ButtonPanel.Visible = true;
        }

        private void PingBt_Click(object sender, EventArgs e)
        {
            var consl = new FormConsole();
            Task.Run(() => Application.Run(consl));
            var item = (UserINFOItem)UserBox.SelectedItem;
            NetWorker.Ping(item, () => consl.Ending, consl.WriteLine);
            UserBox.Text = item.ToString();
            int indx = UserBox.SelectedIndex;
            UserBox.Items.Remove(item);
            UserBox.Items.Insert(indx, item);
            UserBox.SelectedItem = item;
        }

        private void LoadBt_Click(object sender, EventArgs e)
        {
            var consl = new FormConsole();
            Task.Run(() => Application.Run(consl));
            NetWorker.Upload((UserINFOItem)UserBox.SelectedItem, () => consl.Ending, GetFile, consl.WriteLine);
            FileInfo GetFile()
            {
                var file = new OpenFileDialog();
                return file.ShowDialog() == DialogResult.Cancel ? null : new FileInfo(file.FileName);
            }
        }

        private void InfoBt_Click(object sender, EventArgs e)
        {
            var selected = UserBox.SelectedItem as UserINFOItem;
            var info = selected.GetType().GetProperties().Select(el => $"{el.Name}: {el.GetValue(selected).ToString()}{Environment.NewLine}").Aggregate((l, n) => l + n);
            var consl = new FormConsole();
            Task.Run(() => Application.Run(consl));
            consl.Write(info);
        }

        private void RemoveBt_Click(object sender, EventArgs e)
        {
            var find = UserBox.SelectedItem as UserINFOItem;
            ProgrammDate.UserList.Remove(find);
            UserBox.Items.Remove(find);
            UserBox.Text = "";
            ButtonPanel.Visible = false;
        }

        private void NameTextBox_TextChanged(object sender, EventArgs e)
        {
            ProgrammDate.MyInfo.Name = NameTextBox.Text;
        }

        private void SettingBt_Click(object sender, EventArgs e)
        {
            using(var settingForm = new SettingForm())
            {
                settingForm.ShowDialog();
            }
        }
    }
}
