using System.Windows.Forms;
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
            this.viewPortUpdate = new System.Windows.Forms.Timer(this.components);
            this.labelPitch = new System.Windows.Forms.Label();
            this.labelRoll = new System.Windows.Forms.Label();
            this.labelYaw = new System.Windows.Forms.Label();
            this.lidarSensorViewPort = new QuadcopterAvoidanceSimulation.BufferedPanel();
            this.mainViewPort = new QuadcopterAvoidanceSimulation.BufferedPanel();
            this.SuspendLayout();
            // 
            // viewPortUpdate
            // 
            this.viewPortUpdate.Enabled = true;
            this.viewPortUpdate.Interval = 60;
            this.viewPortUpdate.Tick += new System.EventHandler(this.viewPortUpdate_Tick);
            // 
            // labelPitch
            // 
            this.labelPitch.AutoSize = true;
            this.labelPitch.Location = new System.Drawing.Point(682, 12);
            this.labelPitch.Name = "labelPitch";
            this.labelPitch.Size = new System.Drawing.Size(35, 13);
            this.labelPitch.TabIndex = 0;
            this.labelPitch.Text = "label1";
            // 
            // labelRoll
            // 
            this.labelRoll.AutoSize = true;
            this.labelRoll.Location = new System.Drawing.Point(682, 39);
            this.labelRoll.Name = "labelRoll";
            this.labelRoll.Size = new System.Drawing.Size(35, 13);
            this.labelRoll.TabIndex = 0;
            this.labelRoll.Text = "label1";
            // 
            // labelYaw
            // 
            this.labelYaw.AutoSize = true;
            this.labelYaw.Location = new System.Drawing.Point(682, 66);
            this.labelYaw.Name = "labelYaw";
            this.labelYaw.Size = new System.Drawing.Size(35, 13);
            this.labelYaw.TabIndex = 0;
            this.labelYaw.Text = "label1";
            // 
            // lidarSensorViewPort
            // 
            this.lidarSensorViewPort.Location = new System.Drawing.Point(685, 136);
            this.lidarSensorViewPort.Name = "lidarSensorViewPort";
            this.lidarSensorViewPort.Size = new System.Drawing.Size(350, 350);
            this.lidarSensorViewPort.TabIndex = 3;
            this.lidarSensorViewPort.Paint += new System.Windows.Forms.PaintEventHandler(this.lidarSensorViewPort_Paint_1);
            // 
            // mainViewPort
            // 
            this.mainViewPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mainViewPort.Location = new System.Drawing.Point(12, 12);
            this.mainViewPort.Name = "mainViewPort";
            this.mainViewPort.Size = new System.Drawing.Size(660, 474);
            this.mainViewPort.TabIndex = 2;
            this.mainViewPort.Paint += new System.Windows.Forms.PaintEventHandler(this.mainViewPort_Paint_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1046, 506);
            this.Controls.Add(this.lidarSensorViewPort);
            this.Controls.Add(this.mainViewPort);
            this.Controls.Add(this.labelYaw);
            this.Controls.Add(this.labelRoll);
            this.Controls.Add(this.labelPitch);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer viewPortUpdate;
        private System.Windows.Forms.Label labelPitch;
        private System.Windows.Forms.Label labelRoll;
        private System.Windows.Forms.Label labelYaw;
        private BufferedPanel mainViewPort;
        private BufferedPanel lidarSensorViewPort;
    }
}

