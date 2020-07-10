using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Todoist.Net;

namespace Todoist_Automation
{
    
    public partial class AddNoteTranscribing : Form
    {

        string tdEmail = "robin.claesson@gmail.com", tdPassword = "psom@un2krok0PREA";
        

        public AddNoteTranscribing()
        {
            InitializeComponent();
        }

        private void AddNoteTranscribing_Load(object sender, EventArgs e)
        {
            LoadTodoistData();


        }

        private async void LoadTodoistData()
        {
            ITodoistTokenlessClient tokenlessClient = new TodoistTokenlessClient();
            ITodoistClient client = await tokenlessClient.LoginAsync(tdEmail, tdPassword);

            var projects = await client.Projects.GetAsync();
            foreach(var proj in projects)
            {
                
                comboBox_Project.Items.Add(proj.Name);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox_Project_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void SetControlsEnable(bool enable)
        {
            foreach (Control control in Controls)
                control.Enabled = enable;
        }
    }
}
