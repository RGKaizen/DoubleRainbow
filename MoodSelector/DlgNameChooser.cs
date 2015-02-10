using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DoubleRainbow
{
    public partial class DlgNameChooser : Form
    {
        private TextBox nameTextBox;
        private Label label1;
        private Button Okaybtn;

        public String name = "";

        public DlgNameChooser()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private void InitializeComponent()
        {
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Okaybtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(15, 32);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(100, 20);
            this.nameTextBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Enter name for MoodSeq";
            // 
            // Okaybtn
            // 
            this.Okaybtn.Location = new System.Drawing.Point(15, 62);
            this.Okaybtn.Name = "Okaybtn";
            this.Okaybtn.Size = new System.Drawing.Size(75, 23);
            this.Okaybtn.TabIndex = 2;
            this.Okaybtn.Text = "Okay";
            this.Okaybtn.UseVisualStyleBackColor = true;
            this.Okaybtn.Click += new System.EventHandler(this.Okaybtn_Click);
            // 
            // DlgNameChooser
            // 
            this.ClientSize = new System.Drawing.Size(143, 97);
            this.Controls.Add(this.Okaybtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nameTextBox);
            this.Name = "DlgNameChooser";
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        private void Okaybtn_Click(object sender, EventArgs e)
        {
            name = nameTextBox.Text.Trim();
            this.DialogResult = DialogResult.OK;
        }
    }
}
