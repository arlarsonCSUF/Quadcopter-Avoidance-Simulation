﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace QuadcopterAvoidanceSimulation
{
    public static class Drawing
    {
        public static void drawPolarGraph(Graphics g,Panel p){
            Pen largePen = new Pen(Color.Black,2);
            Pen smallPen = new Pen(Color.Gray, 1);
            Font mainFont = SystemFonts.DefaultFont;
            SolidBrush blackBrush = new SolidBrush(Color.Black);

            int width = p.Width;
            int height = p.Height;
           
            Point center = new Point(width/2,height/2);
            Console.WriteLine(center.X);

            int radius = 0;
            if (width <= height)
                radius = width / 2 - 30;
            else
                radius = height / 2 - 30;


            g.DrawEllipse(largePen, center.X - radius, center.Y-radius, radius * 2, radius * 2);
            g.DrawEllipse(largePen, center.X - 2, center.Y - 2, 4, 4);

            for (int i = 0; i < 360; i += 360 / 12) // create radial pattern every 360/12 = 30 degrees 
            {
                int x2 = Convert.ToInt32(center.X + radius * Math.Sin(Equations.toRad(i)));
                int y2 = Convert.ToInt32(center.Y + radius * Math.Cos(Equations.toRad(i-180)));
                Point endPoint = new Point(x2,y2);
                g.DrawLine(smallPen, center, endPoint);
                PointF textLocation;
                int xOffset = 0;
                if (i == 0 || i == 180)
                    xOffset = 0;
                else if (i > 0 && i < 180)
                    xOffset = 5;
                else
                    xOffset = -25;

                int yOffset = 0;
                if (i == 90 || i == 270)
                    yOffset = -10;
                else if (i > 90 && i < 270)
                    yOffset = 10;
                else
                    yOffset = -20;
                
                textLocation = new PointF(x2+xOffset,y2+yOffset +2);

                g.DrawString(Convert.ToString(i), mainFont, blackBrush, textLocation);
            }
        }
    } 
}