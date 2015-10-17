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
            Vector v = new Vector(magnitude * Math.Cos(theta), magnitude * Math.Sin(theta));
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

        public static double map(double input, double fromLow, double fromHigh, double toLow, double toHigh){
            double fRange = fromHigh - fromLow;
            double tRange = toHigh - toLow;
            double scale = tRange/fRange;
            return input * scale - toHigh;
        }

        public static Vector dragVector(double cDrag,Vector velocity)
        {
            double magnitudeOfDrag = -1 * cDrag * velocity.LengthSquared/2;
            Console.WriteLine(magnitudeOfDrag);
            Vector V = new Vector(magnitudeOfDrag * velocity.X, magnitudeOfDrag * velocity.Y);
            return V;
        }

    }
}
