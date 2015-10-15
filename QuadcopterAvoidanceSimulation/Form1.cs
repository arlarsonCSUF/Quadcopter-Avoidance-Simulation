using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuadcopterAvoidanceSimulation
{
    public partial class Form1 : Form
    {
        ObstacleLayout walls;
        Timer time;
        Quad mainQuad;

        public Form1()
        {
            InitializeComponent();
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
                g.FillRectangle(blackBrush,o.xPosition,o.yPosition,o.width,o.height);
            }
            g.DrawString(Convert.ToString(mainQuad.xPosition) + "\t\t\t" + Convert.ToString(mainQuad.velocityX), DefaultFont, blackBrush, new PointF(300, 300));
                
            g.Dispose();
        }

        private void viewPortUpdate_Tick(object sender, EventArgs e)
        {
            mainViewPort.Invalidate();
            mainQuad.updateState();
        }
    }

    
}
