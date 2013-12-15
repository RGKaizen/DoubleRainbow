namespace DoubleRainbow
{
    partial class Spiral
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnDrawCyl = new System.Windows.Forms.Button();
            this.btnDrawSpiral = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.DrawWorker = new System.ComponentModel.BackgroundWorker();
            this.tabPage1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnDrawCyl);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(548, 97);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Square";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnDrawCyl
            // 
            this.btnDrawCyl.Location = new System.Drawing.Point(8, 54);
            this.btnDrawCyl.Name = "btnDrawCyl";
            this.btnDrawCyl.Size = new System.Drawing.Size(75, 23);
            this.btnDrawCyl.TabIndex = 2;
            this.btnDrawCyl.Text = "Draw";
            this.btnDrawCyl.UseVisualStyleBackColor = true;
            this.btnDrawCyl.Click += new System.EventHandler(this.DrawCyl_Click);
            // 
            // btnDrawSpiral
            // 
            this.btnDrawSpiral.Location = new System.Drawing.Point(8, 15);
            this.btnDrawSpiral.Name = "btnDrawSpiral";
            this.btnDrawSpiral.Size = new System.Drawing.Size(75, 23);
            this.btnDrawSpiral.TabIndex = 0;
            this.btnDrawSpiral.Text = "Draw";
            this.btnDrawSpiral.UseVisualStyleBackColor = true;
            this.btnDrawSpiral.Click += new System.EventHandler(this.Draw_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabControl1.Location = new System.Drawing.Point(0, 667);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(556, 123);
            this.tabControl1.TabIndex = 3;
            // 
            // DrawWorker
            // 
            this.DrawWorker.WorkerSupportsCancellation = true;
            this.DrawWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.DrawWorker_DoWork);
            // 
            // Spiral
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tabControl1);
            this.Size = new System.Drawing.Size(556, 790);
            this.Text = "Spiral";
            this.tabPage1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnDrawSpiral;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button btnDrawCyl;
        private System.ComponentModel.BackgroundWorker DrawWorker;
    }
}

