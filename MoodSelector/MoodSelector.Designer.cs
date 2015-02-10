namespace DoubleRainbow
{
    partial class MoodSelector
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
            this.colorChsBtn = new System.Windows.Forms.Button();
            this.addClrBtn = new System.Windows.Forms.Button();
            this.animateBtn = new System.Windows.Forms.Button();
            this.moodList = new System.Windows.Forms.ComboBox();
            this.addMoodBtn = new System.Windows.Forms.Button();
            this.removeMoodBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // colorChsBtn
            // 
            this.colorChsBtn.Location = new System.Drawing.Point(12, 12);
            this.colorChsBtn.Name = "colorChsBtn";
            this.colorChsBtn.Size = new System.Drawing.Size(91, 23);
            this.colorChsBtn.TabIndex = 0;
            this.colorChsBtn.Text = "Color Chooser";
            this.colorChsBtn.UseVisualStyleBackColor = true;
            this.colorChsBtn.Click += new System.EventHandler(this.colorChsBtn_Click);
            // 
            // addClrBtn
            // 
            this.addClrBtn.Location = new System.Drawing.Point(109, 12);
            this.addClrBtn.Name = "addClrBtn";
            this.addClrBtn.Size = new System.Drawing.Size(75, 23);
            this.addClrBtn.TabIndex = 1;
            this.addClrBtn.Text = "AddColor";
            this.addClrBtn.UseVisualStyleBackColor = true;
            this.addClrBtn.Click += new System.EventHandler(this.addClrBtn_Click);
            // 
            // animateBtn
            // 
            this.animateBtn.Location = new System.Drawing.Point(197, 12);
            this.animateBtn.Name = "animateBtn";
            this.animateBtn.Size = new System.Drawing.Size(75, 23);
            this.animateBtn.TabIndex = 3;
            this.animateBtn.Text = "Play";
            this.animateBtn.UseVisualStyleBackColor = true;
            this.animateBtn.Click += new System.EventHandler(this.PlayPause);
            // 
            // moodList
            // 
            this.moodList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.moodList.FormattingEnabled = true;
            this.moodList.Location = new System.Drawing.Point(12, 92);
            this.moodList.Name = "moodList";
            this.moodList.Size = new System.Drawing.Size(121, 21);
            this.moodList.TabIndex = 5;
            this.moodList.SelectedIndexChanged += new System.EventHandler(this.moodList_SelectedIndexChanged);
            this.moodList.FormattingEnabled = false;
            // 
            // addMoodBtn
            // 
            this.addMoodBtn.Location = new System.Drawing.Point(139, 92);
            this.addMoodBtn.Name = "addMoodBtn";
            this.addMoodBtn.Size = new System.Drawing.Size(45, 23);
            this.addMoodBtn.TabIndex = 6;
            this.addMoodBtn.Text = "Add";
            this.addMoodBtn.UseVisualStyleBackColor = true;
            this.addMoodBtn.Click += new System.EventHandler(this.addMoodbtn_Click);
            // 
            // removeMoodBtn
            // 
            this.removeMoodBtn.Location = new System.Drawing.Point(209, 92);
            this.removeMoodBtn.Name = "removeMoodBtn";
            this.removeMoodBtn.Size = new System.Drawing.Size(60, 23);
            this.removeMoodBtn.TabIndex = 7;
            this.removeMoodBtn.Text = "Remove";
            this.removeMoodBtn.UseVisualStyleBackColor = true;
            this.removeMoodBtn.Click += new System.EventHandler(this.removeMoodBtn_Click);
            // 
            // MoodSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(281, 137);
            this.Controls.Add(this.removeMoodBtn);
            this.Controls.Add(this.addMoodBtn);
            this.Controls.Add(this.moodList);
            this.Controls.Add(this.animateBtn);
            this.Controls.Add(this.addClrBtn);
            this.Controls.Add(this.colorChsBtn);
            this.Name = "MoodSelector";
            this.Text = "Mood Selector";
            this.ResumeLayout(false);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MoodSelector_Closing);

        }

        #endregion

        private System.Windows.Forms.Button colorChsBtn;
        private System.Windows.Forms.Button addClrBtn;

        private System.Windows.Forms.Button animateBtn;
        private System.Windows.Forms.ComboBox moodList;
        private System.Windows.Forms.Button addMoodBtn;
        private System.Windows.Forms.Button removeMoodBtn;
    }
}