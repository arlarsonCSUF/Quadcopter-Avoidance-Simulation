using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuadcopterAvoidanceSimulation
{
    public static class Equations
    {
        //kinematics1 Vf = Vi + A*T                 Velocity as a function of time
        //kinematics2 Xf = Xi + 1/2(Vi + Vf)*T      Postion as a function of velocity and time
        //kinematics3 Xf = Xi + Vi*T + 1/2*A*T^2    Postion as a function of time
        //kinematics4 Vf = sqrt(Vi^2 + 2A(Xf - Xi)  Velocity as a function of positon

        /// <summary>
        /// Velocity as a function of time.
        /// </summary>
        /// <param name="Velocity Initial"></param>
        /// <param name="Acceleration"></param>
        /// <param name="Time"></param>
        /// <returns></returns>
        public static double kinematics1(double Vi, double A, double T)
        {
            return (Vi + A * T);
        }

        /// <summary>
        /// Postion as a function of time.
        /// </summary>
        /// <param name="Initial Position"></param>
        /// <param name="Velocity Initial"></param>
        /// <param name="Acceleration"></param>
        /// <param name="Time"></param>
        /// <returns></returns>
        public static double kinematics3(double Xi, double Vi, double A, double T)
        {
            return (Xi + Vi * T + 1 / 2 * A * T * T);
        }

        /*public static Vector findAccelerationVector(double pitch, double roll, Vector quadHeading)
        {
            Vector Acceleration;
            Vector jHat;
            jHat = new Vector(0,1);
            Acceleration = new Vector(
                
                Vector.AngleBetween(jHat,quadHeading)
            return Acceleration;
        }*/

        public static Vector polarToCartesian(double theta, double magnitude)
        {
            Vector v = new Vector(magnitude * Math.Sin(theta), magnitude * Math.Cos(theta));
            return v;
        }

        public static double xComponentOfVector(double theta, double magnitude)
        {
            return magnitude * Math.Cos(theta);
        }

        public static double yComponentOfVector(double theta, double magnitude)
        {
            return magnitude * Math.Sin(theta);
        }
        public static double dotProduct(Vector a, Vector b)
        {
            return a.X * b.X + a.Y * b.Y;
        }

        /// <summary>
        /// Calculates the projection of vector a onto vector b, returns a scalar.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double vectorProjection(Vector a, Vector b)
        {
            return dotProduct(a, b) / b.Length;
        }

        public static Vector orthagonalVector(Vector a)
        {
            Vector V = new Vector(a.Y, -1 * a.X);
            return V;
        }

        public static double toDeg(double rad)
        {
            return rad * 57.2957795;
        }

        public static double toRad(double deg)
        {
            return deg * 0.01745329251;
        }

        public static double toRad(int deg)
        {
            return deg * 0.01745329251;
        }

        public static double map(double input, double fromLow, double fromHigh, double toLow, double toHigh)
        {
            return (input - fromLow) * (toHigh - toLow) / (fromHigh - fromLow) + toLow;
        }

        public static Vector dragVector(double cDrag, Vector velocity)
        {
            double magnitudeOfDrag = -1 * cDrag * velocity.LengthSquared / 2;
            Vector V = new Vector(magnitudeOfDrag * velocity.X, magnitudeOfDrag * velocity.Y);
            return V;
        }

        public struct intersection
        {
            public bool isIntersection;
            public Point p;
        }

        public class PolarPoint
        {
            public PolarPoint(double r, double t)
            {
                _radius = r;
                _theta = t;
            }
            public double radius { get { return _radius; } set { _radius = value; } }
            public double theta { get { return _theta; } set { _theta = value; } }
            double _radius;
            double _theta;
        }

        public struct lineIntersection
        {
            public int isIntersection;
            public double x, y;
        }

        public class lineSegment
        {
            public lineSegment(double X0, double Y0, double X1, double Y1)
            {
                _x0 = X0;
                _y0 = Y0;
                _x1 = X1;
                _y1 = Y1;
            }

            public void updateLineSegment(double X0, double Y0, double X1, double Y1)
            {
                _x0 = X0;
                _y0 = Y0;
                _x1 = X1;
                _y1 = Y1;
            }

            public double x0 { get { return _x0; } set { _x0 = value; } }
            public double x1 { get { return _x1; } set { _x1 = value; } }
            public double y0 { get { return _y0; } set { _y0 = value; } }
            public double y1 { get { return _y1; } set { _y1 = value; } }

            private double _x0, _x1, _y0, _y1;
        }

        public static lineIntersection getLineIntersection(lineSegment L1, lineSegment L2)
        {
            lineIntersection LI;
            LI.isIntersection = 0;
            LI.x = 0;
            LI.y = 0;

            double p0_x = L1.x0;
            double p0_y = L1.y0;
            double p1_x = L1.x1;
            double p1_y = L1.y1;

            double p2_x = L2.x0;
            double p2_y = L2.y0;
            double p3_x = L2.x1;
            double p3_y = L2.y1;

            double s02_x, s02_y, s10_x, s10_y, s32_x, s32_y, s_numer, t_numer, denom, t;
            s10_x = p1_x - p0_x;
            s10_y = p1_y - p0_y;
            s32_x = p3_x - p2_x;
            s32_y = p3_y - p2_y;

            denom = s10_x * s32_y - s32_x * s10_y;
            if (denom == 0)
                return LI; // Collinear
            bool denomPositive = denom > 0;

            s02_x = p0_x - p2_x;
            s02_y = p0_y - p2_y;
            s_numer = s10_x * s02_y - s10_y * s02_x;
            if ((s_numer < 0) == denomPositive)
                return LI; // No collision

            t_numer = s32_x * s02_y - s32_y * s02_x;
            if ((t_numer < 0) == denomPositive)
                return LI; // No collision

            if (((s_numer > denom) == denomPositive) || ((t_numer > denom) == denomPositive))
                return LI; // No collision
            // Collision detected
            t = t_numer / denom;
            
                LI.x = p0_x + (t * s10_x);
            
                LI.y = p0_y + (t * s10_y);

                LI.isIntersection = 1; // there is intersection
            return LI;
        }

        public static double distanceFormula(double x0, double y0, double x1, double y1)
        {
            return Math.Sqrt(Math.Pow(x1 - x0, 2) + Math.Pow(y1 - y0, 2));
        }
    }
}
