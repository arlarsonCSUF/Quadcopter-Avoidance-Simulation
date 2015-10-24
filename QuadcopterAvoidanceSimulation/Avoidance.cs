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
            _avoidanceThreshold = avoidanceThreshold;
            _avoidanceGain = avoidanceGain;
            _clampAvoidanceMagnitude = 100;
        }

        public ArrayList generateVectorArray(){
            ArrayList a = new ArrayList();
            for (int i = 0; i < data.Count; i++)
            {
                Equations.PolarPoint p = (Equations.PolarPoint)data[i];
                if ((p.radius * _LIDAR.range) <= _avoidanceThreshold)
                {

                    double scaledRadius = _clampAvoidanceMagnitude;
                    if (p.radius != 0) // avoid extremely unlikely case of division by zero
                        scaledRadius = -1 * _clampAvoidanceMagnitude * p.radius * _LIDAR.range/_avoidanceThreshold + _clampAvoidanceMagnitude;
                        //scaledRadius = _avoidanceGain / (p.radius * _LIDAR.range); //the magnitude of the avoidance vector is inversely proportional to the object distance
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
                v = Vector.Divide(v,a.Count);
                avoidanceForceVector = Vector.Add(avoidanceForceVector, v);
            }
            if (avoidanceForceVector.Length > _clampAvoidanceMagnitude)
            {
                double divisionFactor = avoidanceForceVector.Length / _clampAvoidanceMagnitude;
                avoidanceForceVector =  Vector.Divide(avoidanceForceVector, divisionFactor);
            }

            return avoidanceForceVector;

        }
        private LIDAR _LIDAR;
        private ArrayList data;
        private double _avoidanceThreshold;
        private double _avoidanceGain;
        private double _clampAvoidanceMagnitude;
        
    }
}
