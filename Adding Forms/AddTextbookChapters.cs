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
            if (comboBox_Project.Text != "" && textBox_Book.Text != "")
            {
                AddTask();
                this.DialogResult = DialogResult.OK;
            }

            else MessageBox.Show("Välj ett projekt och namge boken");
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private int BusinessDaysUntil(DateTime firstDay, DateTime lastDay)
        {
            //https://stackoverflow.com/questions/1617049/calculate-the-number-of-business-days-between-two-dates

            firstDay = firstDay.Date;
            lastDay = lastDay.Date;
            if (firstDay > lastDay)
                throw new ArgumentException("Incorrect last day " + lastDay);

            TimeSpan span = lastDay - firstDay;
            int businessDays = span.Days + 1;
            int fullWeekCount = businessDays / 7;
            // find out if there are weekends during the time exceedng the full weeks
            if (businessDays > fullWeekCount * 7)
            {
                // we are here to find out if there is a 1-day or 2-days weekend
                // in the time interval remaining after subtracting the complete weeks
                int firstDayOfWeek = (int)firstDay.DayOfWeek;
                int lastDayOfWeek = (int)lastDay.DayOfWeek;
                if (lastDayOfWeek < firstDayOfWeek)
                    lastDayOfWeek += 7;
                if (firstDayOfWeek <= 6)
                {
                    if (lastDayOfWeek >= 7)// Both Saturday and Sunday are in the remaining time interval
                        businessDays -= 2;
                    else if (lastDayOfWeek >= 6)// Only Saturday is in the remaining time interval
                        businessDays -= 1;
                }
                else if (firstDayOfWeek <= 7 && lastDayOfWeek >= 7)// Only Sunday is in the remaining time interval
                    businessDays -= 1;
            }

            // subtract the weekends during the full weeks in the interval
            businessDays -= fullWeekCount + fullWeekCount;


            return businessDays;
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
                    //Skapar en transaktion för att minimera info till och från servern 
                    var transaction = client.CreateTransaction();

                    //Lägger till uppgiften
                    //var taskID = await transaction.Items.AddAsync(new Item("Renskrivning föreläsning " + comboBox_Project.Text + " " + dateTimePicker.Value.Day + "/" + dateTimePicker.Value.Month, proj.Id));
                    var quickAddItem = new QuickAddItem("Läs \"" + textBox_Book.Text + "\" @Läsning #" + comboBox_Project.Text);
                    var task = await client.Items.QuickAddAsync(quickAddItem);
                    task.DueDate = new DueDate(dateTimePicker_DueDate.Value.Day + "/" + dateTimePicker_DueDate.Value.Month);
                    await client.Items.UpdateAsync(task);

                    //Räknar ut hur många arbetsdagar per kapitel jag har
                    int daysToDeadline = BusinessDaysUntil(DateTime.Now.Date, dateTimePicker_DueDate.Value.Date);
                    int daysPerChapter = daysToDeadline / (int)numericUpDown_Chapters.Value;

                    var dueDate = DateTime.Now.Date;

                    for (int i = 1; i <= numericUpDown_Chapters.Value; i++)
                    {
                        //Flyttar fram deadline för kapitlet
                        dueDate = dueDate.AddDays(daysPerChapter);

                        //Ser till att deadline inte är på en lördag eller en söndag
                        if (dueDate.DayOfWeek == System.DayOfWeek.Saturday)
                            dueDate = dueDate.AddDays(2);
                        else if (dueDate.DayOfWeek == System.DayOfWeek.Sunday)
                            dueDate = dueDate.AddDays(1);

                        //Lägger till kapitlet med underuppgifter

                        var quickAddSub = new QuickAddItem("Kapitel " + i + " #" + comboBox_Project.Text);
                        var subtask = await client.Items.QuickAddAsync(quickAddSub);
                        subtask.DueDate = new DueDate(dueDate.Day + "/" + dueDate.Month);
                        await client.Items.UpdateAsync(subtask);

                        await transaction.Items.MoveAsync(ItemMoveArgument.CreateMoveToParent(subtask.Id, task.Id));

                        var readTaskID = await transaction.Items.AddAsync(new Item("Läsa", proj.Id));
                        await transaction.Items.MoveAsync(ItemMoveArgument.CreateMoveToParent(readTaskID, subtask.Id));

                        var summaryTaskID = await transaction.Items.AddAsync(new Item("Sammanfatta", proj.Id));
                        await transaction.Items.MoveAsync(ItemMoveArgument.CreateMoveToParent(summaryTaskID, subtask.Id));
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
