using System.Windows.Forms;
namespace DoubleRainbow
{
    partial class KeyMonitor
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
            this.debugmenu = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // debugmenu
            // 
            this.debugmenu.Location = new System.Drawing.Point(12, 53);
            this.debugmenu.Multiline = true;
            this.debugmenu.Name = "debugmenu";
            this.debugmenu.Size = new System.Drawing.Size(481, 356);
            this.debugmenu.TabIndex = 0;
            // 
            // KeyMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 421);
            this.Controls.Add(this.debugmenu);
            this.KeyPreview = true;
            this.Name = "KeyMonitor";
            this.Text = "KeyMonitor";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox debugmenu;
    }
}