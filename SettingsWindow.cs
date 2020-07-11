using IniParser;
using IniParser.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Todoist_Automation.Properties;

namespace Todoist_Automation
{
    public partial class SettingsWindow : Form
    {
        IniData settings;
        public SettingsWindow(IniData iniData)
        {
            InitializeComponent();
            this.settings = iniData;
        }

        private void SettingsWindow_Load(object sender, EventArgs e)
        {
            textBox_Token.Text = settings["Todoist"]["token"];
        }

        private void button_Ok_Click(object sender, EventArgs e)
        {
            settings["Todoist"]["token"] = textBox_Token.Text;

            var parser = new FileIniDataParser();
            parser.WriteFile("settings.ini", settings);

            this.DialogResult = DialogResult.OK;
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        public IniData GetSettings
        {
            get 
            {
                return settings;
            }
        }
    }
}
