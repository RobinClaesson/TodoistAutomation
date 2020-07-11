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
    public partial class AddTextbookChapters : Form
    {
        ITodoistClient client;
        public AddTextbookChapters(ITodoistClient client)
        {
            InitializeComponent();

            this.client = client;
        }

        private async void LoadTodoistData()
        {

            //Hämtar alla projectnamn och lägger till dom som alternativ
            var projects = await client.Projects.GetAsync();
            foreach (var proj in projects)
            {
                comboBox_Project.Items.Add(proj.Name);
            }

        }

        private void button_Add_Click(object sender, EventArgs e)
        {
            if (comboBox_Project.Text != "")
            {
                AddTask();
                this.DialogResult = DialogResult.OK;
            }

            else MessageBox.Show("Välj ett projekt");
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
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
                    var quickAddItem = new QuickAddItem("Läs " + textBox_Book.Text + " @Läsning #" + comboBox_Project.Text);
                    var task = await client.Items.QuickAddAsync(quickAddItem);

                    for (int i = 1; i <= numericUpDown_Chapters.Value; i++)
                    {
                        var chapterTaskId = await transaction.Items.AddAsync(new Item("Kapitel " + i, proj.Id));
                        await transaction.Items.MoveAsync(ItemMoveArgument.CreateMoveToParent(chapterTaskId, task.Id));

                        var readTaskID = await transaction.Items.AddAsync(new Item("Läsa", proj.Id));
                        await transaction.Items.MoveAsync(ItemMoveArgument.CreateMoveToParent(readTaskID, chapterTaskId));

                        var summaryTaskID = await transaction.Items.AddAsync(new Item("Sammanfatta", proj.Id));
                        await transaction.Items.MoveAsync(ItemMoveArgument.CreateMoveToParent(summaryTaskID, chapterTaskId));
                    }

                    //Skickar infon till servern
                    await transaction.CommitAsync();

                    MessageBox.Show("Lagt till \"Läs " + textBox_Book.Text + "\"");

                }
            }
        }
        private void comboBox_Project_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AddTextbookChapters_Load(object sender, EventArgs e)
        {
            LoadTodoistData();
        }




    }
}
