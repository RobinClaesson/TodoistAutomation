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

        string tdEmail = "robin.claesson@gmail.com", tdPassword = "psom@un2krok0PREA";
        ITodoistTokenlessClient tokenlessClient;
        ITodoistClient client;


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
            //Loggar in på Todoist
            //TODO: Logga in med token istället för email
            tokenlessClient = new TodoistTokenlessClient();
            client = await tokenlessClient.LoginAsync(tdEmail, tdPassword);

            //Hämtar alla projectnamn och lägger till dom som alternativ
            var projects = await client.Projects.GetAsync();
            foreach (var proj in projects)
            {
                comboBox_Project.Items.Add(proj.Name);
            }

        }


        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button_Add_Click(object sender, EventArgs e)
        {
            AddTask();
            this.DialogResult = DialogResult.OK;
        }


        private async void AddTask()
        {
            //Hämtar alla projekt
            var projects = await client.Projects.GetAsync();
            foreach (var proj in projects)
            {
                //Hittar projektet som stämmer överens med valet
                if (proj.Name == comboBox_Project.Text)
                {
                    //Skapar en transaktion för att bara behöva skicka infon till serven en gång
                    var transaction = client.CreateTransaction();

                    //Lägger till uppgiften
                    //var taskID = await transaction.Items.AddAsync(new Item("Renskrivning föreläsning " + comboBox_Project.Text + " " + dateTimePicker.Value.Day + "/" + dateTimePicker.Value.Month, proj.Id));
                    var quickAddItem = new QuickAddItem("Renskrivning föreläsning " + comboBox_Project.Text + " " + dateTimePicker.Value.Day + " / " + dateTimePicker.Value.Month + " @Renskrivning #" + comboBox_Project.Text);
                    var task = await client.Items.QuickAddAsync(quickAddItem);

                    //Lägger till underuppgifter och sätter dom under uppgiften
                    var sub1ID = await transaction.Items.AddAsync(new Item("Scanna orginal", proj.Id));
                    await transaction.Items.MoveAsync(ItemMoveArgument.CreateMoveToParent(sub1ID, task.Id));

                    var sub2ID = await transaction.Items.AddAsync(new Item("Renskriv", proj.Id));
                    await transaction.Items.MoveAsync(ItemMoveArgument.CreateMoveToParent(sub2ID, task.Id));

                    var sub3ID = await transaction.Items.AddAsync(new Item("Samanfatta", proj.Id));
                    await transaction.Items.MoveAsync(ItemMoveArgument.CreateMoveToParent(sub3ID, task.Id));

                    //Skickar infon till servern
                    await transaction.CommitAsync();

                    MessageBox.Show("Lagt till \"Renskrivning föreläsning " + comboBox_Project.Text + " " + dateTimePicker.Value.Day + "/" + dateTimePicker.Value.Month + "\"");
                }
            }

        }
    }
}
