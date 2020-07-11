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


        //Acces the Todoist servers using the users private API-Token
        //No usernames or passwords needed  
        private async void LoginToTodoist()
        {
            //Hides the buttons until connection with server
            SetButtonVisability(false);
            label_loading.Text = "Loading Todoist Data";

            try
            {
                //Connects to server
                client = new TodoistClient(settings["Todoist"]["token"]);

                //Test the connection by fetching the username and displaying it
                var resources = await client.GetResourcesAsync();
                label_loading.Text = "Loged in as " + resources.UserInfo.FullName;
                SetButtonVisability(true);

            }

            catch
            {
                //Failed connection
                MessageBox.Show("Failed to load Todoist data, check API-Token");
                label_loading.Text = "Load failed";

            }
        }

        private void Menu_Load(object sender, EventArgs e)
        {


            LoginToTodoist();
        }

        //Hides every button except for the settingsbutton      
        private void SetButtonVisability(bool visable)
        {
            foreach (Control control in Controls)
            {
                if (control is Button && !control.Name.Contains("Setting"))
                    control.Visible = visable;
            }

            label_ask.Visible = visable;
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

            //Resets the connection to Todoist server if the settings are changed
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
