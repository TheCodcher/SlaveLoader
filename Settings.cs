using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace SlaveLoader2
{
    static class Settings
    {
        public const string SaveInformationName = "SlaveLoaderSavedInformation.json";
        public const string SaveSettingName = "SlaveLoaderSavedSettings.json";
        public static readonly string DirPath = Environment.CurrentDirectory;
        public static event Action SettingsChanges;
        public static SaveSettings MySettings { get; set; }
        public static void CallEventSettingsChanges() => SettingsChanges?.Invoke();
        public static void ApplySettings(this Control form)
        {
            var fields = form.GetType().GetRuntimeFields()
                .Where(inf => inf.GetValue(form) is Control)
                .Where(inf => inf.GetCustomAttribute(typeof(ColorAttribute), false) != null)
                .Select(inf => (inf.GetValue(form) as Control, inf.GetCustomAttribute(typeof(ActiveColor), false) != null))
                .ToList();
            fields.Add((form, form.GetType().GetCustomAttribute(typeof(ActiveColor), false) != null));
            foreach (var c in fields)
            {
                if (c.Item2)
                    c.Item1.BackColor = MySettings.ActiveWindowColor;
                else
                    c.Item1.BackColor = MySettings.BaseWindowColor;
                c.Item1.ForeColor = MySettings.FontColor;
            }
        }
        public static void LoadSettings()
        {
            var finalPath = Path.Combine(DirPath, SaveSettingName);
            var file = new FileInfo(finalPath);
            if (file.Exists)
            {
                using (var fs = file.OpenText())
                {
                    MySettings = JsonConvert.DeserializeObject<SaveSettings>(fs.ReadToEnd());
                }
            }
            if (MySettings == null) MySettings = new SaveSettings();
        }
        public static void SaveSettings() => Save(MySettings, SaveSettingName);
        public static void SaveInfo(SaveInformation source) => Save(source, SaveInformationName);
        private static void Save(object source, string name)
        {
            using (var filestream = new FileInfo(Path.Combine(DirPath, name)).Open(FileMode.Create, FileAccess.Write))
            {
                var buff = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(source));
                filestream.Write(buff, 0, buff.Length);
            }
        }
        public static SaveInformation LoadInfo()
        {
            var finalPath = Path.Combine(DirPath, SaveInformationName);
            var file = new FileInfo(finalPath);
            if (file.Exists)
            {
                SaveInformation save = null;
                using (var fs = file.OpenText())
                {
                    save = JsonConvert.DeserializeObject<SaveInformation>(fs.ReadToEnd());
                }
                if (save != null)
                {
                    if (save.MyInfo == null)
                    {
                        save.MyInfo = new UserINFOItem(new IPEndPoint(IPAddress.Any, NetWorker.CreatePort(10000)));
                    }
                    if (save.UserList == null)
                    {
                        save.UserList = new List<UserINFOItem>();
                    }
                    return save;
                }
            }
            return new SaveInformation(new List<UserINFOItem>(), new UserINFOItem(new IPEndPoint(IPAddress.Any, NetWorker.CreatePort(10000))));
        }
    }
    //Обязательно поля, а не свойства
    class SaveSettings
    {
        public Color ActiveWindowColor = Color.FromArgb(52, 52, 52);

        public Color BaseWindowColor = Color.FromArgb(38, 38, 38);

        public Color FontColor = Color.WhiteSmoke;

        public int SignCount = 1;

        public int UploadBuffer = 8192;

        public int DowloadBuffer = 8192;

        public int RequestDeley = 1000;
    }
    class SaveInformation
    {
        public List<UserINFOItem> UserList { get; set; }
        public UserINFOItem MyInfo { get; set; }
        [JsonConstructor]
        SaveInformation() { }
        public SaveInformation(IEnumerable<UserINFOItem> UserList, UserINFOItem MyInfo)
        {
            this.MyInfo = MyInfo;
            this.UserList = UserList.ToList();
        }
    }
    class ColorAttribute : Attribute { }
    class ActiveColor : ColorAttribute { }
    class BaseColor : ColorAttribute { }
    class LinkedField : Attribute
    {
        public readonly string FieldName;
        public readonly string LinkedProperty;
        public LinkedField(string Name, string Property)
        {
            FieldName = Name;
            LinkedProperty = Property;
        }
    }
    class IntegerNumberScope : Attribute
    {
        public readonly int Min;
        public readonly int Max;
        public IntegerNumberScope(int min, int max)
        {
            Min = min;
            Max = max;
        }
    }
}
