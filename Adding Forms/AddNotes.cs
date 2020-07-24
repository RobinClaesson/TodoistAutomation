using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Todoist.Net;
using Todoist.Net.Models;

namespace Todoist_Automation
{

    public partial class AddNoteTranscribing : Form
    {

        ITodoistClient client;

        public AddNoteTranscribing(ITodoistClient client)
        {
            InitializeComponent();
            this.client = client;
        }

        private void AddNoteTranscribing_Load(object sender, EventArgs e)
        {
            LoadTodoistData();
        }

        private async void LoadTodoistData()
        {
            //Loads all the users projects to a targetlist
            var projects = await client.Projects.GetAsync();
            foreach (var proj in projects)
            {
                comboBox_Project.Items.Add(proj.Name);
            }

            if (comboBox_Project.Items.Count > 0)
                comboBox_Project.SelectedIndex = 0;
        }


        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void button_Add_Click(object sender, EventArgs e)
        {
            if (comboBox_Project.Text != "")
            {
                this.DialogResult = DialogResult.OK;
            }

            else MessageBox.Show("Choose a project");
        }

        public string ProjectName { get { return comboBox_Project.Text; } }
        public DateTime LectureDate { get { return dateTimePicker.Value; } }



    }
}
