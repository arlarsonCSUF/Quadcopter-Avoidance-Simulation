using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections;

namespace QuadcopterAvoidanceSimulation
{
    class LIDAR
    {
        public LIDAR(double x, double y, double range, double angleOffset, double RPM, Timer t, ArrayList Obstacles)
        {
            _dataArraySize = 60;
            time = t;
            _Obstacles = Obstacles;
            _xOrigin = x;
            _yOrigin = y;
            _range = range;
            _angleoffset = angleOffset;
            _angle = angleOffset;
            _xEnd = _range * Math.Sin(_angle);
            _yEnd = _range * Math.Cos(_angle);
            _lineSegment = new Equations.lineSegment(_xOrigin,_yOrigin,_xEnd,_yEnd); // line segment representing lidar beam 
            _rotationalVelocity = Equations.toRad(360 * RPM / 60); // RPM to radians/sec
            previousUpdateTime = time.micros;
            _dataPoints = new ArrayList();
            for (int i = 0; i < _dataArraySize; i++)
            {
                Equations.PolarPoint p = new Equations.PolarPoint(0, 0);
                _dataPoints.Add(p);
            }
        }

        public void updateLidar(double x, double y, double yaw)
        {
            Int64 dT = time.micros - previousUpdateTime; // in us
            
            _angleoffset += _rotationalVelocity * (dT / 1000000.0); // theta = dTheta * dt
            _angle = _angleoffset - yaw;
            _xOrigin = x;
            _yOrigin = y;
            _xEnd = _xOrigin + _range * Math.Sin(_angle);
            _yEnd = _yOrigin + _range * Math.Cos(_angle);
            _lineSegment.updateLineSegment(_xOrigin, _yOrigin, _xEnd, _yEnd); //  update line segment representing lidar beam

            double minimumDistance = _range; 
            if (_Obstacles.Count > 0){ // loop through all the obstacles look for intersection and see which intesection is closest
                for (int i = 0; i < _Obstacles.Count; i++)
                {
                    Obstacle obs = (Obstacle) _Obstacles[i];
                    Equations.lineSegment obsLineSegment = obs.lineSegment; 
                    Equations.lineIntersection LI = Equations.getLineIntersection(_lineSegment, obsLineSegment); // find intersection between the lidar beam and obstacle
                    if (LI.isIntersection == 1) // if there is an intersection
                    {
                        double distance = Equations.distanceFormula(_xOrigin, _yOrigin, LI.x, LI.y); //find distance from quad to intersection using distance formula
                        if (distance < minimumDistance) // find the minimum distance of intersection
                            minimumDistance = distance;
                    } 
                }
            }

            Equations.PolarPoint p = new Equations.PolarPoint(minimumDistance/_range,_angleoffset); //add point to polar data array
            _dataPoints.Add(p);
            previousUpdateTime = time.micros; //record time so we can determine dT next update
        }
        

        public Point originPoint { get {return new Point(_xOrigin,_yOrigin);}}
        public Point endPoint { get { return new Point(_xEnd, _yEnd);}}
        public double xOrigin { get { return _xOrigin; } set { _xOrigin = value; } }
        public double yOrigin { get { return _yOrigin; } set { _yOrigin = value; } }
        public double xEnd { get { return _xEnd; } set { _xEnd = value; } }
        public double yEnd { get { return _yEnd; } set { _yEnd = value; } }
        public double angle { get { return _angle; } }
        public ArrayList dataPoints { get { return _dataPoints;}}
        public Equations.lineSegment lineSegment { get { return _lineSegment; } }
        public int dataArraySize { get { return _dataArraySize; } }

        private Equations.lineSegment _lineSegment;
        private ArrayList _Obstacles;
        private Timer time;
        private Int64 previousUpdateTime;
        private ArrayList _dataPoints;
        private double _xOrigin, _yOrigin;
        private double _range;
        private double _xEnd, _yEnd;
        private double _angle;
        private double _angleoffset;
        private double _rotationalVelocity; // rad/sec  
        private int _dataArraySize;
    }
}
