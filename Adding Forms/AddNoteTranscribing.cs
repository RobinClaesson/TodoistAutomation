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
                AddTask();
                this.DialogResult = DialogResult.OK;
            }

            else MessageBox.Show("Choose a project");
        }


        private async void AddTask()
        {
            //Loads all projects and searches for the selected project
            var projects = await client.Projects.GetAsync();
            foreach (var proj in projects)
            {
                if (proj.Name == comboBox_Project.Text)
                {
                    //Create a transaction to lower the times we request and send data to the server
                    var transaction = client.CreateTransaction();

                    //Adding the main task
                     var quickAddItem = new QuickAddItem("Renskrivning föreläsning " + comboBox_Project.Text + " " + dateTimePicker.Value.Day + " / " + dateTimePicker.Value.Month + " @Renskrivning @KTH #" + comboBox_Project.Text);
                    var task = await client.Items.QuickAddAsync(quickAddItem);

                    //Adds subsaks and moves them under the main task
                    var sub1ID = await transaction.Items.AddAsync(new Item("Scanna orginal", proj.Id));
                    await transaction.Items.MoveAsync(ItemMoveArgument.CreateMoveToParent(sub1ID, task.Id));

                    var sub2ID = await transaction.Items.AddAsync(new Item("Renskriv", proj.Id));
                    await transaction.Items.MoveAsync(ItemMoveArgument.CreateMoveToParent(sub2ID, task.Id));

                    var sub3ID = await transaction.Items.AddAsync(new Item("Samanfatta", proj.Id));
                    await transaction.Items.MoveAsync(ItemMoveArgument.CreateMoveToParent(sub3ID, task.Id));

                    //Sends the rest of the data to server
                    await transaction.CommitAsync();

                    MessageBox.Show("Added \"Renskrivning föreläsning " + comboBox_Project.Text + " " + dateTimePicker.Value.Day + "/" + dateTimePicker.Value.Month + "\"");
                }
            }

        }
    }
}
