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

    public partial class AddNoteTranscribing : Form
    {

        string tdEmail = "robin.claesson@gmail.com", tdPassword = "psom@un2krok0PREA";
        ITodoistTokenlessClient tokenlessClient;
        ITodoistClient client;

        long labelID = 0;

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

            //Hämtar LabelID för renskrivning
            var labels = await client.Labels.GetAsync();
            foreach (var label in labels)
            {
                if (label.Name == "Renskrivning")
                    labelID = label.ItemOrder;
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

                    ////Lägger till uppgiften
                    //var mainTaskID = await client.Items.AddAsync(new Item("Renskrivning föreläsning " + comboBox_Project.Text + " " + dateTimePicker.Value.Day + "/" + dateTimePicker.Value.Month, proj.Id));

                    ////Hämtar uppgiften för att komma åt dess position i listan
                    //var mainTaskChildOrder = 0;
                    //var tasks = await client.Items.GetAsync();
                    //foreach (Item task in tasks)
                    //    if (task.Id == mainTaskID)
                    //        mainTaskChildOrder = (int)task.ChildOrder;

                    ////Lägger till underuppgifter 
                    //List<ComplexId> subtaskIDs = new List<ComplexId>();
                    //subtaskIDs.Add(await client.Items.AddAsync(new Item("Scanna orginal", proj.Id)));
                    ////subtaskIDs.Add(await client.Items.AddAsync(new Item("Renskriva", proj.Id)));
                    ////subtaskIDs.Add(await client.Items.AddAsync(new Item("Sammanfatta", proj.Id)));

                    ////Hämtar hem alla uppgifterna igen och loopar igenom efter underuppgifterna och uppdaterar dom som en underuppgift
                    //tasks = await client.Items.GetAsync();
                    //foreach (Item task in tasks)
                    //{
                    //    if(subtaskIDs.Contains(task.Id))
                    //    {
                    //        task.ParentId = mainTaskChildOrder;
                    //        await client.Items.UpdateAsync()
                    //    }
                    //}


                    //tasks = await client.Items.GetAsync();


                    var transaction = client.CreateTransaction();


                    var taskId = await transaction.Items.AddAsync(new Item("Renskrivning föreläsning " + comboBox_Project.Text + " " + 
                        dateTimePicker.Value.Day + "/" + dateTimePicker.Value.Month, proj.Id));

                    transaction.Items.

                    await transaction.CommitAsync();

                }
            }

        }
    }
}
