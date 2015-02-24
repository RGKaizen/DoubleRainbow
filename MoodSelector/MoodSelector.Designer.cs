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
            this.components = new System.ComponentModel.Container();
            this.colorChsBtn = new System.Windows.Forms.Button();
            this.addClrBtn = new System.Windows.Forms.Button();
            this.animateBtn = new System.Windows.Forms.Button();
            this.moodsCmbBox = new System.Windows.Forms.ComboBox();
            this.addMoodBtn = new System.Windows.Forms.Button();
            this.removeMoodBtn = new System.Windows.Forms.Button();
            this.moodsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.moodNameLbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.moodsBindingSource)).BeginInit();
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
            // moodsCmbBox
            // 
            this.moodsCmbBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.moodsCmbBox.Location = new System.Drawing.Point(12, 129);
            this.moodsCmbBox.Name = "moodsCmbBox";
            this.moodsCmbBox.Size = new System.Drawing.Size(128, 21);
            this.moodsCmbBox.TabIndex = 5;
            this.moodsCmbBox.SelectedIndexChanged += new System.EventHandler(this.moodList_SelectedIndexChanged);
            // 
            // addMoodBtn
            // 
            this.addMoodBtn.Location = new System.Drawing.Point(146, 129);
            this.addMoodBtn.Name = "addMoodBtn";
            this.addMoodBtn.Size = new System.Drawing.Size(45, 23);
            this.addMoodBtn.TabIndex = 6;
            this.addMoodBtn.Text = "Add";
            this.addMoodBtn.UseVisualStyleBackColor = true;
            this.addMoodBtn.Click += new System.EventHandler(this.addMoodbtn_Click);
            // 
            // removeMoodBtn
            // 
            this.removeMoodBtn.Location = new System.Drawing.Point(197, 129);
            this.removeMoodBtn.Name = "removeMoodBtn";
            this.removeMoodBtn.Size = new System.Drawing.Size(60, 23);
            this.removeMoodBtn.TabIndex = 7;
            this.removeMoodBtn.Text = "Remove";
            this.removeMoodBtn.UseVisualStyleBackColor = true;
            this.removeMoodBtn.Click += new System.EventHandler(this.removeMoodBtn_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(169, 100);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "New";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.newMood_Click);
            // 
            // moodNameLbl
            // 
            this.moodNameLbl.AutoSize = true;
            this.moodNameLbl.Location = new System.Drawing.Point(12, 105);
            this.moodNameLbl.Name = "moodNameLbl";
            this.moodNameLbl.Size = new System.Drawing.Size(68, 13);
            this.moodNameLbl.TabIndex = 9;
            this.moodNameLbl.Text = "Mood Name:";
            // 
            // MoodSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(295, 164);
            this.Controls.Add(this.moodNameLbl);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.removeMoodBtn);
            this.Controls.Add(this.addMoodBtn);
            this.Controls.Add(this.moodsCmbBox);
            this.Controls.Add(this.animateBtn);
            this.Controls.Add(this.addClrBtn);
            this.Controls.Add(this.colorChsBtn);
            this.Name = "MoodSelector";
            this.Text = "Mood Selector";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MoodSelector_Closing);
            ((System.ComponentModel.ISupportInitialize)(this.moodsBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button colorChsBtn;
        private System.Windows.Forms.Button addClrBtn;

        private System.Windows.Forms.Button animateBtn;
        private System.Windows.Forms.ComboBox moodsCmbBox;
        private System.Windows.Forms.Button addMoodBtn;
        private System.Windows.Forms.Button removeMoodBtn;
        private System.Windows.Forms.BindingSource moodsBindingSource;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label moodNameLbl;
    }
}