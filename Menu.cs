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
using IniParser;
using IniParser.Model;
using Todoist.Net;
using Todoist.Net.Models;

namespace Todoist_Automation
{
    public partial class Menu : Form
    {
        IniData settings;
        ITodoistClient client;

        public Menu()
        {
            InitializeComponent();

            var parser = new FileIniDataParser();
            //Loads the settings
            if (!File.Exists("settings.ini"))
                CreateSettings();

            settings = parser.ReadFile("settings.ini");


        }


        private static void CreateSettings()
        {
            StreamWriter writer = new StreamWriter("settings.ini");
            writer.WriteLine("[Todoist]");
            writer.WriteLine("token=\"\"");


            writer.Close();
        }

        private async void LoginToTodoist()
        {
            SetButtonEnabled(false);
            label_loading.Text = "Laddar Todoist data...";

            try
            {
                client = new TodoistClient(settings["Todoist"]["token"]);

                var resources = await client.GetResourcesAsync();
                label_loading.Text = "Inloggad som " + resources.UserInfo.FullName;
                SetButtonEnabled(true);

            }

            catch
            {
                MessageBox.Show("Todoist login misslyckat. Kontrollera todoist api token");
                label_loading.Text = "Todoist login misslyckat";

            }
        }

        private void Menu_Load(object sender, EventArgs e)
        {


            LoginToTodoist();
        }

        private void SetButtonEnabled(bool enabled)
        {
            foreach (Control control in Controls)
            {
                if (control is Button && !control.Name.Contains("Setting"))
                    control.Visible = enabled;
            }
        }

        private void button_Notes_Click(object sender, EventArgs e)
        {
            AddNoteTranscribing addNotes = new AddNoteTranscribing(client);

            addNotes.ShowDialog();
        }

        private void button_Settings_Click(object sender, EventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow(settings);
            settingsWindow.ShowDialog();

            if (settingsWindow.DialogResult == DialogResult.OK)
            {
                LoginToTodoist();
            }


        }

        private void button_Book_Click(object sender, EventArgs e)
        {
            AddTextbookChapters addBook = new AddTextbookChapters(client);
            addBook.ShowDialog();
        }
    }
}
