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
        static internal SaveInformation ProgramData { get; private set; }
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
            ProgramData = Settings.LoadInfo();

            UserBox.Items.AddRange(ProgramData.UserList.ToArray());
            NameTextBox.Text = ProgramData.MyInfo.Name == ProgramData.MyInfo.IP.ToString() ? "" : ProgramData.MyInfo.Name;
            PortLabel.Text += ProgramData.MyInfo.Port;

            NetWorker = new NetWorker(ProgramData.MyInfo.IPEnd, () => ProgramData, DownloadRequest);
        }

        private FileInfo DownloadRequest(IPAddress remoteIP, NetWorker.DownloadRequestData data, out Func<bool> getEnd, out Action<string> stateEvent)
        {
            getEnd = null;
            stateEvent = null;
            var remote = new IPEndPoint(remoteIP, data.ListenerPort);
            var remoteString = remote.ToString();
            var user = ProgramData.UserList.Find(u => u.IPEnd.ToString() == remoteString);

            if (user == null)
            {
                user = new UserINFOItem(remote);
                if (MessageBox.Show($"Пользователя {remoteString} нет в Вашем списке контактов, но он отправил запрос на загрузку файла. Хотите внести {remoteString} в список контактов?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Invoke((Action)(() =>
                    {
                        ProgramData.UserList.Add(user);
                        UserBox.Items.Add(user);
                        UserBox.SelectedItem = user;
                        UserBox.Text = user.ToString();
                        ButtonPanel.Visible = true;
                    }));
                }
            }
            var filedate = data.GetFileSize();
            if (MessageBox.Show($"Пользователь {user.ToString()} отправил запрос на загрузку файла. Хотите загрузить файл {data.FileName} {Math.Round(filedate.Count, 3).ToString()} {filedate.Size.ToString()}?",
                "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return null;

            string path = "";
            using (var file = new SaveFileDialog())
            {
                file.FileName = data.FileName;
                if ((DialogResult)Invoke((Func<DialogResult>)file.ShowDialog) == DialogResult.Cancel) return null;
                path = file.FileName;
            }

            var consl = new FormConsole();
            Task.Run(() => Application.Run(consl));
            getEnd = () => consl.Ending;
            stateEvent = consl.WriteLine;
            return new FileInfo(path);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.SettingsChanges -= this.ApplySettings;

            NetWorker.Dispose();

            Settings.SaveInfo(ProgramData);
        }

        private void AddUserBt_Click(object sender, EventArgs e)
        {
            using (var DilogForm = new AddUserDilogForm())
            {
                var reqResult = DilogForm.ShowDialog();
                if (reqResult == DialogResult.Cancel) return;
                var temp = new UserINFOItem(DilogForm.ResultEndPoint);
                ProgramData.UserList.Add(temp);
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
            ProgramData.UserList.Remove(find);
            UserBox.Items.Remove(find);
            UserBox.Text = "";
            ButtonPanel.Visible = false;
        }

        private void NameTextBox_TextChanged(object sender, EventArgs e)
        {
            ProgramData.MyInfo.Name = NameTextBox.Text;
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
