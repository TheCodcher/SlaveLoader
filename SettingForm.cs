using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace SlaveLoader2
{
    public partial class SettingForm : Form
    {
        public SettingForm()
        {
            InitializeComponent();
            this.ApplySettings();
            button3.BackColor = Settings.MySettings.FontColor;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            var values = GetType().GetRuntimeFields()
                .Where(it => it.GetCustomAttribute(typeof(LinkedField)) != null)
                .Select(value => (typeof(SaveSettings).GetRuntimeField(((LinkedField)value.GetCustomAttribute(typeof(LinkedField))).FieldName), value.GetValue(this).GetType().GetRuntimeProperty(((LinkedField)value.GetCustomAttribute(typeof(LinkedField))).LinkedProperty), value.GetValue(this), value.GetCustomAttribute(typeof(IntegerNumberScope)) as IntegerNumberScope));
            foreach(var val in values)
            {
                if (val.Item2.PropertyType == typeof(string))
                    try
                    {
                        int temp = int.Parse((string)val.Item2.GetValue(val.Item3));
                        if (temp < val.Item4.Min || temp > val.Item4.Max)
                        {
                            MessageBox.Show($"{temp} должен находится в пределах от {val.Item4.Min} до {val.Item4.Max}", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            continue;
                        }
                        val.Item1.SetValue(Settings.MySettings, temp);
                    }
                    catch
                    {
                        MessageBox.Show("Некорректное значение, значения должны быть целочисленными", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        continue;
                    }
                else
                    val.Item1.SetValue(Settings.MySettings, val.Item2.GetValue(val.Item3));
            }

            this.ApplySettings();
            button3.BackColor = Settings.MySettings.FontColor;

            Settings.CallEventSettingsChanges();
        }
        private void Color_Click(object sender, EventArgs e)
        {
            colorDialog.Color = ((Button)sender).BackColor;
            if (colorDialog.ShowDialog() != DialogResult.Cancel)
                ((Button)sender).BackColor = colorDialog.Color;
        }

        private void SettingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.SaveSettings();
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {
            var textboxsvalues = GetType().GetRuntimeFields()
                .Where(it => it.GetCustomAttribute(typeof(LinkedField)) != null)
                .Where(it => typeof(SaveSettings).GetRuntimeField((it.GetCustomAttribute(typeof(LinkedField)) as LinkedField).FieldName).FieldType == typeof(int))
                .Select(it => (it.GetValue(this) as TextBox, (int)typeof(SaveSettings).GetRuntimeField((it.GetCustomAttribute(typeof(LinkedField)) as LinkedField).FieldName).GetValue(Settings.MySettings)));
            foreach (var t in textboxsvalues)
                t.Item1.Text = t.Item2.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Settings.MySettings = new SaveSettings();
            this.ApplySettings();
            button3.BackColor = Settings.MySettings.FontColor;

            Settings.CallEventSettingsChanges();
        }
    }
}
