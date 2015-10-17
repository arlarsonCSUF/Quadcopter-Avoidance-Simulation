using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Management;
using System.Drawing.Imaging;
using System.Collections;
using Microsoft.DirectX.DirectInput;

namespace QuadcopterAvoidanceSimulation
{
    public partial class Form1 : Form
    {
        ObstacleLayout walls;
        Timer time;
        Quad mainQuad;
        private Joystick joystick;
        private bool[] joystickButtons;
        private Thread joystickUpdateThread;
        

        public Form1()
        {
            InitializeComponent();

            joystick = new Joystick(this.Handle);
            connectToJoystick(joystick);
            joystickUpdateThread = new Thread(new ThreadStart(joystickUpdate));
            joystickUpdateThread.Start();

            time = new Timer(DateTime.Now.Ticks);
            walls = new ObstacleLayout();   
            mainQuad = new Quad(100, 100,time);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void mainViewPort_Paint(object sender, PaintEventArgs e)
        {
            Pen blackPen = new Pen(Color.Black, 3);
            SolidBrush blackBrush = new SolidBrush(Color.Black);
            Graphics g = e.Graphics;
            for (int i = 0; i < walls.Obstacles.Count; i++)
            {
                Obstacle o = (Obstacle)walls.Obstacles[i];
                //g.FillRectangle(blackBrush,o.xPosition,o.yPosition,o.width,o.height);
            }
            int x = Convert.ToInt32(mainQuad.xPosition);
            int y = Convert.ToInt32(mainQuad.yPosition);

            g.FillEllipse(blackBrush,x,y,5,5);
            //g.DrawString(Convert.ToString(mainQuad.xPosition) + "\t\t\t" + Convert.ToString(mainQuad.velocityX), DefaultFont, blackBrush, new PointF(300, 300));
                
            g.Dispose();
        }

        private void viewPortUpdate_Tick(object sender, EventArgs e)
        {
            joystickUpdate();
            mainQuad.updateState();
            mainViewPort.Invalidate();
        }

        private void enableTimer()
        {
            if (this.InvokeRequired)
            {
                BeginInvoke(new ThreadStart(delegate()
                {
                    //joystickTimer.Enabled = true;
                }));
            }
            //else
                //joystickTimer.Enabled = true;
        }

        private void connectToJoystick(Joystick joystick)
        {
            while (true)
            {
                string sticks = joystick.FindJoysticks();
                if (sticks != null)
                {
                    if (joystick.AcquireJoystick(sticks))
                    {
                        enableTimer();
                        break;
                    }
                }
            }
        }

        private void joystickUpdate()
        {
            try
            {
                joystick.UpdateStatus();
                joystickButtons = joystick.buttons;

                mainQuad.roll = Equations.toRad(joystick.Xaxis / 2.0);
                mainQuad.pitch = Equations.toRad(-1*joystick.Yaxis / 2.0);
                //mainQuad.yawRate = Equations.toRad(joystick.Zaxis / 2.0);
               //Console.WriteLine(Equations.toDeg(mainQuad.yaw));
                for (int i = 0; i < joystickButtons.Length; i++)
                {
                    if (joystickButtons[i] == true)
                        Console.Write("Button " + i + " Pressed\n");
                }
            }
            catch
            {
                //joystickTimer.Enabled = false;
                connectToJoystick(joystick);
            }
        }

    }

    
}
