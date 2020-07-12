﻿namespace Todoist_Automation
{
    partial class AddTextbookChapters
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_Add = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_Project = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_Book = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown_Chapters = new System.Windows.Forms.NumericUpDown();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dateTimePicker_Due = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dateTimePicker_Start = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Chapters)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_Cancel
            // 
            this.button_Cancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.button_Cancel.Location = new System.Drawing.Point(202, 0);
            this.button_Cancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(187, 100);
            this.button_Cancel.TabIndex = 6;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // button_Add
            // 
            this.button_Add.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_Add.Location = new System.Drawing.Point(0, 0);
            this.button_Add.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_Add.Name = "button_Add";
            this.button_Add.Size = new System.Drawing.Size(187, 100);
            this.button_Add.TabIndex = 7;
            this.button_Add.Text = "Add";
            this.button_Add.UseVisualStyleBackColor = true;
            this.button_Add.Click += new System.EventHandler(this.button_Add_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "Project";
            // 
            // comboBox_Project
            // 
            this.comboBox_Project.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Project.FormattingEnabled = true;
            this.comboBox_Project.Location = new System.Drawing.Point(12, 28);
            this.comboBox_Project.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox_Project.MaxDropDownItems = 100;
            this.comboBox_Project.Name = "comboBox_Project";
            this.comboBox_Project.Size = new System.Drawing.Size(348, 24);
            this.comboBox_Project.TabIndex = 9;
            this.comboBox_Project.SelectedIndexChanged += new System.EventHandler(this.comboBox_Project_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 17);
            this.label2.TabIndex = 10;
            this.label2.Text = "Textbook name";
            // 
            // textBox_Book
            // 
            this.textBox_Book.Location = new System.Drawing.Point(12, 74);
            this.textBox_Book.Name = "textBox_Book";
            this.textBox_Book.Size = new System.Drawing.Size(348, 22);
            this.textBox_Book.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 17);
            this.label3.TabIndex = 12;
            this.label3.Text = "Number of chapters";
            // 
            // numericUpDown_Chapters
            // 
            this.numericUpDown_Chapters.Location = new System.Drawing.Point(12, 119);
            this.numericUpDown_Chapters.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_Chapters.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_Chapters.Name = "numericUpDown_Chapters";
            this.numericUpDown_Chapters.Size = new System.Drawing.Size(348, 22);
            this.numericUpDown_Chapters.TabIndex = 13;
            this.numericUpDown_Chapters.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button_Add);
            this.panel1.Controls.Add(this.button_Cancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 301);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(389, 100);
            this.panel1.TabIndex = 14;
            // 
            // dateTimePicker_Due
            // 
            this.dateTimePicker_Due.Location = new System.Drawing.Point(11, 208);
            this.dateTimePicker_Due.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dateTimePicker_Due.Name = "dateTimePicker_Due";
            this.dateTimePicker_Due.Size = new System.Drawing.Size(348, 22);
            this.dateTimePicker_Due.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 189);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 17);
            this.label4.TabIndex = 16;
            this.label4.Text = "Due date";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 144);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 17);
            this.label5.TabIndex = 17;
            this.label5.Text = "Start date";
            // 
            // dateTimePicker_Start
            // 
            this.dateTimePicker_Start.Location = new System.Drawing.Point(12, 164);
            this.dateTimePicker_Start.Name = "dateTimePicker_Start";
            this.dateTimePicker_Start.Size = new System.Drawing.Size(348, 22);
            this.dateTimePicker_Start.TabIndex = 18;
            // 
            // AddTextbookChapters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 401);
            this.ControlBox = false;
            this.Controls.Add(this.dateTimePicker_Start);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dateTimePicker_Due);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.numericUpDown_Chapters);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_Book);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox_Project);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AddTextbookChapters";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add textbook to read";
            this.Load += new System.EventHandler(this.AddTextbookChapters_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Chapters)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_Add;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_Project;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_Book;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDown_Chapters;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dateTimePicker_Due;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dateTimePicker_Start;
    }
}