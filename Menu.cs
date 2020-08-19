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

            if (addNotes.DialogResult == DialogResult.OK)
            {
                AddNotesTask(addNotes.ProjectName, addNotes.LectureDate);
            }
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
            AddReading addReading = new AddReading(client);
            addReading.ShowDialog();

            if (addReading.DialogResult == DialogResult.OK)
            {
                AddReadingTask(addReading.ProjectName, addReading.BookName, addReading.StartDate, addReading.DueDate, addReading.LastChapter, addReading.FirstChapter);
            }
        }


        public async void AddNotesTask(string projectName, DateTime lectureDate)
        {
            //Loads all projects and searches for the selected project
            var projects = await client.Projects.GetAsync();
            foreach (var proj in projects)
            {
                if (proj.Name == projectName)
                {
                    //Create a transaction to lower the times we request and send data to the server
                    var transaction = client.CreateTransaction();

                    //Adding the main task
                    var quickAddItem = new QuickAddItem("Renskrivning föreläsning " + projectName + " " + lectureDate.Day + " / " + lectureDate.Month
                                                     + " @Renskrivning #" + projectName);


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

                    MessageBox.Show("Added \"Renskrivning föreläsning " + projectName + " " + lectureDate.Day + "/" + lectureDate.Month + "\"");
                }
            }

        }

        public async void AddReadingTask(string projectName, string bookName, DateTime startDate, DateTime dueDate, int lastChapter, int firstChapter)
        {
            //Gets all projects and finds the choosen
            var projects = await client.Projects.GetAsync();
            foreach (var proj in projects)
            {

                if (proj.Name == projectName)
                {
                    //Creates a transaction to cut down on times we contact server
                    var transaction = client.CreateTransaction();

                    //Adds main task
                    var quickAddItem = new QuickAddItem("Läs \"" + bookName + "\" @Läsning #" + projectName);
                    var task = await client.Items.QuickAddAsync(quickAddItem);
                    task.DueDate = new DueDate(dueDate.Day + "/" + dueDate.Month);
                    await client.Items.UpdateAsync(task);

                    //Calculates how many days we have per chapter
                    int chaptersToRead = lastChapter - (firstChapter-1);
                    int daysToDeadline = BusinessDaysUntil(startDate.Date, dueDate.Date);
                    int daysPerChapter = daysToDeadline / chaptersToRead;

                    var nextDueDate = startDate;

                    for (int i = firstChapter; i <= lastChapter; i++)
                    {
                        //Moves the duedate forward
                        nextDueDate = nextDueDate.AddDays(daysPerChapter + (i % 2) * (daysToDeadline % lastChapter));

                        //Ignores saturdays and sundays
                        if (nextDueDate.DayOfWeek == System.DayOfWeek.Saturday)
                            nextDueDate = nextDueDate.AddDays(2);
                        else if (nextDueDate.DayOfWeek == System.DayOfWeek.Sunday)
                            nextDueDate = nextDueDate.AddDays(1);


                        //Adds the chapter as a subtask with suptasks of its own
                        var quickAddSub = new QuickAddItem("Kapitel " + i);
                        var subtask = await client.Items.QuickAddAsync(quickAddSub);
                        subtask.DueDate = new DueDate(nextDueDate.Day + "/" + nextDueDate.Month);
                        await client.Items.UpdateAsync(subtask);

                        await transaction.Items.MoveAsync(ItemMoveArgument.CreateMoveToParent(subtask.Id, task.Id));

                        var readTaskID = await transaction.Items.AddAsync(new Item("Läsa", proj.Id));
                        await transaction.Items.MoveAsync(ItemMoveArgument.CreateMoveToParent(readTaskID, subtask.Id));

                        var summaryTaskID = await transaction.Items.AddAsync(new Item("Sammanfatta", proj.Id));
                        await transaction.Items.MoveAsync(ItemMoveArgument.CreateMoveToParent(summaryTaskID, subtask.Id));
                    }

                    //Sends the unsynced changes to the server
                    await transaction.CommitAsync();

                    MessageBox.Show("Added \"Läs " + bookName + "\"");

                }
            }
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


    }
}
