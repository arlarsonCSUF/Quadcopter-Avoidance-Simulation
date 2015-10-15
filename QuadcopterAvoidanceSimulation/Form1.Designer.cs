namespace QuadcopterAvoidanceSimulation
{
    partial class Form1
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
            this.mainViewPort = new System.Windows.Forms.Panel();
            this.viewPortUpdate = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // mainViewPort
            // 
            this.mainViewPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mainViewPort.Location = new System.Drawing.Point(12, 12);
            this.mainViewPort.Name = "mainViewPort";
            this.mainViewPort.Size = new System.Drawing.Size(664, 478);
            this.mainViewPort.TabIndex = 0;
            this.mainViewPort.Paint += new System.Windows.Forms.PaintEventHandler(this.mainViewPort_Paint);
            // 
            // viewPortUpdate
            // 
            this.viewPortUpdate.Enabled = true;
            this.viewPortUpdate.Interval = 60;
            this.viewPortUpdate.Tick += new System.EventHandler(this.viewPortUpdate_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(876, 581);
            this.Controls.Add(this.mainViewPort);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mainViewPort;
        private System.Windows.Forms.Timer viewPortUpdate;
    }
}

