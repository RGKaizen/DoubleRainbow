using System;
namespace DoubleRainbow
{
    partial class MainWindow
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
            this.posBox = new System.Windows.Forms.TextBox();
            this.tryButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.Slider1 = new System.Windows.Forms.HScrollBar();
            this.Slider1Lbl = new System.Windows.Forms.Label();
            this.Slider1ValueLbl = new System.Windows.Forms.Label();
            this.RefreshBar = new System.Windows.Forms.HScrollBar();
            this.Slider2ValueLbl = new System.Windows.Forms.Label();
            this.Slider2Lbl = new System.Windows.Forms.Label();
            this.Slider2 = new System.Windows.Forms.HScrollBar();
            this.Slider3ValueLbl = new System.Windows.Forms.Label();
            this.Slider3Lbl = new System.Windows.Forms.Label();
            this.Slider3 = new System.Windows.Forms.HScrollBar();
            this.button3 = new System.Windows.Forms.Button();
            this.spiral = new DoubleRainbow.Spiral();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // posBox
            // 
            this.posBox.Location = new System.Drawing.Point(12, 12);
            this.posBox.Name = "posBox";
            this.posBox.Size = new System.Drawing.Size(54, 20);
            this.posBox.TabIndex = 0;
            this.posBox.Text = "3";
            // 
            // tryButton
            // 
            this.tryButton.Location = new System.Drawing.Point(105, 38);
            this.tryButton.Name = "tryButton";
            this.tryButton.Size = new System.Drawing.Size(75, 23);
            this.tryButton.TabIndex = 4;
            this.tryButton.Text = "Test";
            this.tryButton.UseVisualStyleBackColor = true;
            this.tryButton.Click += new System.EventHandler(this.testButton);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 38);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Clear";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.ClearButton);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(207, 38);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "PlayPause";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.PlayPause);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // Slider1
            // 
            this.Slider1.Location = new System.Drawing.Point(66, 108);
            this.Slider1.Maximum = 1000;
            this.Slider1.Minimum = 1;
            this.Slider1.Name = "Slider1";
            this.Slider1.Size = new System.Drawing.Size(216, 18);
            this.Slider1.SmallChange = 5;
            this.Slider1.TabIndex = 10;
            this.Slider1.Value = 60;
            this.Slider1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.Slider1_Scroll);
            // 
            // Slider1Lbl
            // 
            this.Slider1Lbl.AutoSize = true;
            this.Slider1Lbl.Location = new System.Drawing.Point(21, 108);
            this.Slider1Lbl.Name = "Slider1Lbl";
            this.Slider1Lbl.Size = new System.Drawing.Size(42, 13);
            this.Slider1Lbl.TabIndex = 11;
            this.Slider1Lbl.Text = "Slider 1";
            // 
            // Slider1ValueLbl
            // 
            this.Slider1ValueLbl.AutoSize = true;
            this.Slider1ValueLbl.Location = new System.Drawing.Point(296, 114);
            this.Slider1ValueLbl.Name = "Slider1ValueLbl";
            this.Slider1ValueLbl.Size = new System.Drawing.Size(28, 13);
            this.Slider1ValueLbl.TabIndex = 12;
            this.Slider1ValueLbl.Text = "Val: ";
            // 
            // RefreshBar
            // 
            this.RefreshBar.Location = new System.Drawing.Point(66, 75);
            this.RefreshBar.Name = "RefreshBar";
            this.RefreshBar.Size = new System.Drawing.Size(216, 18);
            this.RefreshBar.TabIndex = 13;
            this.RefreshBar.Value = 50;
            this.RefreshBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.RefreshBar_Scroll);
            // 
            // Slider2ValueLbl
            // 
            this.Slider2ValueLbl.AutoSize = true;
            this.Slider2ValueLbl.Location = new System.Drawing.Point(296, 144);
            this.Slider2ValueLbl.Name = "Slider2ValueLbl";
            this.Slider2ValueLbl.Size = new System.Drawing.Size(28, 13);
            this.Slider2ValueLbl.TabIndex = 16;
            this.Slider2ValueLbl.Text = "Val: ";
            // 
            // Slider2Lbl
            // 
            this.Slider2Lbl.AutoSize = true;
            this.Slider2Lbl.Location = new System.Drawing.Point(21, 138);
            this.Slider2Lbl.Name = "Slider2Lbl";
            this.Slider2Lbl.Size = new System.Drawing.Size(42, 13);
            this.Slider2Lbl.TabIndex = 15;
            this.Slider2Lbl.Text = "Slider 2";
            // 
            // Slider2
            // 
            this.Slider2.Location = new System.Drawing.Point(66, 138);
            this.Slider2.Maximum = 1000;
            this.Slider2.Minimum = 1;
            this.Slider2.Name = "Slider2";
            this.Slider2.Size = new System.Drawing.Size(216, 18);
            this.Slider2.SmallChange = 5;
            this.Slider2.TabIndex = 14;
            this.Slider2.Value = 95;
            this.Slider2.Scroll += new System.Windows.Forms.ScrollEventHandler(this.Slider2_Scroll);
            // 
            // Slider3ValueLbl
            // 
            this.Slider3ValueLbl.AutoSize = true;
            this.Slider3ValueLbl.Location = new System.Drawing.Point(296, 172);
            this.Slider3ValueLbl.Name = "Slider3ValueLbl";
            this.Slider3ValueLbl.Size = new System.Drawing.Size(28, 13);
            this.Slider3ValueLbl.TabIndex = 19;
            this.Slider3ValueLbl.Text = "Val: ";
            // 
            // Slider3Lbl
            // 
            this.Slider3Lbl.AutoSize = true;
            this.Slider3Lbl.Location = new System.Drawing.Point(21, 166);
            this.Slider3Lbl.Name = "Slider3Lbl";
            this.Slider3Lbl.Size = new System.Drawing.Size(42, 13);
            this.Slider3Lbl.TabIndex = 18;
            this.Slider3Lbl.Text = "Slider 3";
            // 
            // Slider3
            // 
            this.Slider3.Location = new System.Drawing.Point(66, 166);
            this.Slider3.Maximum = 1000;
            this.Slider3.Minimum = 1;
            this.Slider3.Name = "Slider3";
            this.Slider3.Size = new System.Drawing.Size(216, 18);
            this.Slider3.SmallChange = 5;
            this.Slider3.TabIndex = 17;
            this.Slider3.Value = 125;
            this.Slider3.Scroll += new System.Windows.Forms.ScrollEventHandler(this.Slider3_Scroll);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(174, 316);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 21;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // spiral
            // 
            this.spiral.BackColor = System.Drawing.Color.White;
            this.spiral.Location = new System.Drawing.Point(377, 4);
            this.spiral.Name = "spiral";
            this.spiral.Size = new System.Drawing.Size(700, 700);
            this.spiral.TabIndex = 20;
            this.spiral.Text = "Spiral";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1091, 734);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.spiral);
            this.Controls.Add(this.Slider3ValueLbl);
            this.Controls.Add(this.Slider3Lbl);
            this.Controls.Add(this.Slider3);
            this.Controls.Add(this.Slider2ValueLbl);
            this.Controls.Add(this.Slider2Lbl);
            this.Controls.Add(this.Slider2);
            this.Controls.Add(this.RefreshBar);
            this.Controls.Add(this.Slider1ValueLbl);
            this.Controls.Add(this.Slider1Lbl);
            this.Controls.Add(this.Slider1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tryButton);
            this.Controls.Add(this.posBox);
            this.Name = "MainWindow";
            this.Text = "Main Window";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_Closing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox posBox;
        private System.Windows.Forms.Button tryButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.HScrollBar Slider1;
        private System.Windows.Forms.Label Slider1Lbl;
        private System.Windows.Forms.Label Slider1ValueLbl;
        private System.Windows.Forms.HScrollBar RefreshBar;
        private System.Windows.Forms.Label Slider2ValueLbl;
        private System.Windows.Forms.Label Slider2Lbl;
        private System.Windows.Forms.HScrollBar Slider2;
        private System.Windows.Forms.Label Slider3ValueLbl;
        private System.Windows.Forms.Label Slider3Lbl;
        private System.Windows.Forms.HScrollBar Slider3;
        private Spiral spiral;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

