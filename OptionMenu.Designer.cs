namespace DoubleRainbow
{
    partial class OptionMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionMenu));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.strip_comboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(32, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(78, 45);
            this.button1.TabIndex = 0;
            this.button1.Text = "Color Wheel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(138, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(78, 45);
            this.button2.TabIndex = 1;
            this.button2.Text = "Rainbow Gen";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(249, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(78, 45);
            this.button3.TabIndex = 2;
            this.button3.Text = "Mood Lighting";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // strip_comboBox
            // 
            this.strip_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.strip_comboBox.FormattingEnabled = true;
            this.strip_comboBox.Items.AddRange(new object[] {
            "Kai",
            "Zen",
            "Both"});
            this.strip_comboBox.Location = new System.Drawing.Point(32, 68);
            this.strip_comboBox.Name = "strip_comboBox";
            this.strip_comboBox.Size = new System.Drawing.Size(121, 21);
            this.strip_comboBox.TabIndex = 3;
            this.strip_comboBox.SelectedIndexChanged += new System.EventHandler(this.strip_comboBox_SelectedIndexChanged);
            // 
            // OptionMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 101);
            this.Controls.Add(this.strip_comboBox);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OptionMenu";
            this.Text = "Double Rainbow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OptionMenu_Closing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ComboBox strip_comboBox;
    }
}