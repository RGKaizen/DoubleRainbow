namespace DoubleRainbow
{
    partial class BPMCalc
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
            this.BPMBox = new System.Windows.Forms.TextBox();
            this.BPMLabel = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.ClrBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BPMBox
            // 
            this.BPMBox.Location = new System.Drawing.Point(93, 49);
            this.BPMBox.Name = "BPMBox";
            this.BPMBox.ReadOnly = true;
            this.BPMBox.Size = new System.Drawing.Size(48, 20);
            this.BPMBox.TabIndex = 0;
            // 
            // BPMLabel
            // 
            this.BPMLabel.AutoSize = true;
            this.BPMLabel.Location = new System.Drawing.Point(51, 52);
            this.BPMLabel.Name = "BPMLabel";
            this.BPMLabel.Size = new System.Drawing.Size(36, 13);
            this.BPMLabel.TabIndex = 1;
            this.BPMLabel.Text = "BPM: ";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ClrBtn
            // 
            this.ClrBtn.Location = new System.Drawing.Point(54, 12);
            this.ClrBtn.Name = "ClrBtn";
            this.ClrBtn.Size = new System.Drawing.Size(75, 23);
            this.ClrBtn.TabIndex = 2;
            this.ClrBtn.Text = "Clear";
            this.ClrBtn.UseVisualStyleBackColor = true;
            this.ClrBtn.Click += new System.EventHandler(this.ClrBtn_Click);
            // 
            // BPMCalc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(205, 94);
            this.Controls.Add(this.ClrBtn);
            this.Controls.Add(this.BPMLabel);
            this.Controls.Add(this.BPMBox);
            this.Name = "BPMCalc";
            this.Text = "BPMCalc";
            this.Load += new System.EventHandler(this.BPMCalc_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox BPMBox;
        private System.Windows.Forms.Label BPMLabel;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button ClrBtn;
    }
}