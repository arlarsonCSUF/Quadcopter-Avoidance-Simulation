using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows;

namespace QuadcopterAvoidanceSimulation
{
    class Avoidance
    {
        public Avoidance(LIDAR lidar, double avoidanceThreshold,double avoidanceGain)
        {
            _LIDAR = lidar;
            data = lidar.dataPoints;
            _numberOfDataPointsPerRevolution = lidar.samplesPerRevolution;
            _avoidanceThreshold = avoidanceThreshold;
            _avoidanceGain = avoidanceGain;
            _clampAvoidanceMagnitude = 100;
        }

        public ArrayList generateVectorArray(){
            ArrayList a = new ArrayList();
            for (int i = data.Count - _numberOfDataPointsPerRevolution; i < data.Count; i++)
            {
                Equations.PolarPoint p = (Equations.PolarPoint)data[i];
                if ((p.radius * _LIDAR.range) <= _avoidanceThreshold)
                {

                    double scaledRadius = _clampAvoidanceMagnitude;
                    if (p.radius != 0) // avoid extremely unlikely case of division by zero
<<<<<<< HEAD
                        scaledRadius = -1 * _clampAvoidanceMagnitude * _LIDAR.range/_avoidanceThreshold * p.radius + _clampAvoidanceMagnitude;
                        //scaledRadius = _avoidanceGain *100 / (p.radius * _LIDAR.range); //the magnitude of the avoidance vector is inversely proportional to the object distance
=======
                        scaledRadius = -1 * _clampAvoidanceMagnitude * p.radius * _LIDAR.range/_avoidanceThreshold + _clampAvoidanceMagnitude;
                        //scaledRadius = _avoidanceGain / (p.radius * _LIDAR.range); //the magnitude of the avoidance vector is inversely proportional to the object distance
>>>>>>> 0c20b2b00c9db883c3a0580fb982633536b4d6da
                    if (scaledRadius > _clampAvoidanceMagnitude)
                        scaledRadius = _clampAvoidanceMagnitude;

                    double xComp = -1*scaledRadius * Math.Sin(p.theta);//x component of vector to polar point p
                    double yComp = -1*scaledRadius * Math.Cos(p.theta);//y component of vector to polar point p
                    Vector v = new Vector(xComp, yComp);
                    a.Add(v);
                }
            }
            return a;
        }

        public Vector findAvoidanceForceVector(){
            ArrayList a = generateVectorArray();
            Vector avoidanceForceVector = new Vector(0, 0);

            for (int i = 0; i < a.Count; i++)
            {
                Vector v = (Vector)a[i];
<<<<<<< HEAD
=======
                v = Vector.Divide(v,a.Count);
>>>>>>> 0c20b2b00c9db883c3a0580fb982633536b4d6da
                avoidanceForceVector = Vector.Add(avoidanceForceVector, v);
            }
            if(a.Count != 0)
                avoidanceForceVector = Vector.Divide(avoidanceForceVector, a.Count);

            if (avoidanceForceVector.Length > _clampAvoidanceMagnitude)
            {
                double divisionFactor = avoidanceForceVector.Length / _clampAvoidanceMagnitude;
                avoidanceForceVector =  Vector.Divide(avoidanceForceVector, divisionFactor);
            }

            Vector scaledDifference = new Vector(LPF_Beta * (smoothAvoidanceVector.X - avoidanceForceVector.X), LPF_Beta * (smoothAvoidanceVector.Y - avoidanceForceVector.Y));
            smoothAvoidanceVector = Vector.Subtract(smoothAvoidanceVector, scaledDifference);
            return smoothAvoidanceVector;

        }

        private Vector smoothAvoidanceVector;
        private double LPF_Beta = 0.5; // 0<ß<1

        private LIDAR _LIDAR;
        private ArrayList data;
        private double _avoidanceThreshold;
        private double _avoidanceGain;
        private double _clampAvoidanceMagnitude;
        private int _numberOfDataPointsPerRevolution;
        
    }
}
