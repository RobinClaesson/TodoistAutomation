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
using Todoist.Net.Models;

namespace Todoist_Automation
{
    public partial class AddReading : Form
    {
        ITodoistClient client;
        public AddReading(ITodoistClient client)
        {
            InitializeComponent();

            this.client = client;
        }

        private async void LoadTodoistData()
        {

            //Gets all projectnames
            var projects = await client.Projects.GetAsync();
            foreach (var proj in projects)
            {
                comboBox_Project.Items.Add(proj.Name);
            }

        }

        private void button_Add_Click(object sender, EventArgs e)
        {
            if (comboBox_Project.Text != "" && textBox_Book.Text != "")
            {
                this.DialogResult = DialogResult.OK;
            }

            else MessageBox.Show("Choose a project and bookname");
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void comboBox_Project_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AddTextbookChapters_Load(object sender, EventArgs e)
        {
            LoadTodoistData();
        }


        public string ProjectName { get { return comboBox_Project.Text; } }
        public string BookName { get { return textBox_Book.Text; } }

        public int FirstChapter { get { return (int)numericUpDown_FirstChapter.Value; } }
        public int LastChapter { get { return (int)numericUpDown_LastChapters.Value; } }
        public DateTime StartDate { get { return dateTimePicker_Start.Value; } }
        public DateTime DueDate { get { return dateTimePicker_Due.Value; } }

        private void numericUpDown_LastChapters_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown_LastChapters.Value < numericUpDown_FirstChapter.Value)
                numericUpDown_LastChapters.Value = numericUpDown_FirstChapter.Value;
        }

        private void numericUpDown_FirstChapter_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown_LastChapters.Value < numericUpDown_FirstChapter.Value)
                numericUpDown_LastChapters.Value = numericUpDown_FirstChapter.Value;
        }
    }
}
