namespace Todoist_Automation
{
    partial class Menu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
            this.button_Settings = new System.Windows.Forms.Button();
            this.label_loading = new System.Windows.Forms.Label();
            this.button_Notes = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button_Book = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_Settings
            // 
            this.button_Settings.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button_Settings.Location = new System.Drawing.Point(0, 425);
            this.button_Settings.Name = "button_Settings";
            this.button_Settings.Size = new System.Drawing.Size(340, 31);
            this.button_Settings.TabIndex = 0;
            this.button_Settings.Text = "Inställningar";
            this.button_Settings.UseVisualStyleBackColor = true;
            this.button_Settings.Click += new System.EventHandler(this.button_Settings_Click);
            // 
            // label_loading
            // 
            this.label_loading.AutoSize = true;
            this.label_loading.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_loading.Location = new System.Drawing.Point(7, 9);
            this.label_loading.Name = "label_loading";
            this.label_loading.Size = new System.Drawing.Size(192, 20);
            this.label_loading.TabIndex = 1;
            this.label_loading.Text = "Laddar Todoist data...";
            // 
            // button_Notes
            // 
            this.button_Notes.Location = new System.Drawing.Point(12, 75);
            this.button_Notes.Name = "button_Notes";
            this.button_Notes.Size = new System.Drawing.Size(316, 54);
            this.button_Notes.TabIndex = 2;
            this.button_Notes.Text = "Renskrivning av anteckningar";
            this.button_Notes.UseVisualStyleBackColor = true;
            this.button_Notes.Click += new System.EventHandler(this.button_Notes_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(252, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Vilken typ av uppgifter vill du lägga till?";
            // 
            // button_Book
            // 
            this.button_Book.Location = new System.Drawing.Point(15, 135);
            this.button_Book.Name = "button_Book";
            this.button_Book.Size = new System.Drawing.Size(316, 54);
            this.button_Book.TabIndex = 4;
            this.button_Book.Text = "Läsa kurslitteratur";
            this.button_Book.UseVisualStyleBackColor = true;
            this.button_Book.Click += new System.EventHandler(this.button_Book_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 456);
            this.Controls.Add(this.button_Book);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_Notes);
            this.Controls.Add(this.label_loading);
            this.Controls.Add(this.button_Settings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Menu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Todoist Hjälp";
            this.Load += new System.EventHandler(this.Menu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Settings;
        private System.Windows.Forms.Label label_loading;
        private System.Windows.Forms.Button button_Notes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_Book;
    }
}