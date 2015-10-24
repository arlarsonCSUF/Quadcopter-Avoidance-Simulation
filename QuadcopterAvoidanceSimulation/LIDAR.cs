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
            _dataArraySize = 1024;
            _numberOfSamplePerRevolution = 60;
            _sampleAngle = Equations.toRad(360/(double)_numberOfSamplePerRevolution);
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

            _dataPointsForDisplay = new ArrayList(); // this array is used for holding data points to display to graph
            _dataPoints = new ArrayList(); // this holds data from the last lidar rotation only this is what we do our calculations with

            for (int i = 0; i < _dataArraySize; i++)
            {
                Equations.PolarPoint p = new Equations.PolarPoint(1,i*Equations.toRad(360)/_dataArraySize);
                _dataPointsForDisplay.Add(p);
            }

            for (int i = 0; i < _numberOfSamplePerRevolution; i++)
            {
                Equations.PolarPoint p = new Equations.PolarPoint(1, i * Equations.toRad(360) / _dataArraySize);
                _dataPoints.Add(p);
            }
        }

        public void updateLidar(double x, double y, double yaw)
        {
            Int64 dT = time.micros - previousUpdateTime; // in us
            
            double dTheta =  _rotationalVelocity * (dT / 1000000.0); // dTheta = w * dt
            
            int start = Convert.ToInt32(Math.Ceiling(_angleoffset /_sampleAngle));
            int max = Convert.ToInt32(Math.Floor((_angleoffset + dTheta)/_sampleAngle));

            _xOrigin = x;
            _yOrigin = y;

            for (int i = start; i <= max; i++)
            {
                _angle = _angleoffset - yaw + _sampleAngle*i;
                _xEnd = _xOrigin + _range * Math.Sin(_angle);
                _yEnd = _yOrigin + _range * Math.Cos(_angle);
                _lineSegment.updateLineSegment(_xOrigin, _yOrigin, _xEnd, _yEnd); //  update line segment representing lidar beam

                double minimumDistance = _range;
                if (_Obstacles.Count > 0)
                { // loop through all the obstacles look for intersection and see which intesection is closest
                    for (int j = 0; j < _Obstacles.Count; j++)
                    {
                        Obstacle obs = (Obstacle)_Obstacles[j];
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

                Equations.PolarPoint p = new Equations.PolarPoint(minimumDistance / _range, _angleoffset + _sampleAngle * i); //add point to polar data array
                _dataPointsForDisplay.Add(p);
                _dataPoints.Add(p);
                if (_dataPoints.Count > _numberOfSamplePerRevolution)
                    _dataPoints.RemoveAt(0);
                
            }
            _angleoffset += dTheta;
            
            
            previousUpdateTime = time.micros; //record time so we can determine dT next update
        }
        

        public Point originPoint { get {return new Point(_xOrigin,_yOrigin);}}
        public Point endPoint { get { return new Point(_xEnd, _yEnd);}}
        public double xOrigin { get { return _xOrigin; } set { _xOrigin = value; } }
        public double yOrigin { get { return _yOrigin; } set { _yOrigin = value; } }
        public double xEnd { get { return _xEnd; } set { _xEnd = value; } }
        public double yEnd { get { return _yEnd; } set { _yEnd = value; } }
        public double angle { get { return _angle; } }
        public double range { get { return _range; } set { _range = value; } }
        public ArrayList dataPoints { get { return _dataPoints;}}
        public ArrayList dataPointsForDisplay{get{ return _dataPointsForDisplay;}}
        public Equations.lineSegment lineSegment { get { return _lineSegment; } }
        public int dataArraySize { get { return _dataArraySize; } }

        private Equations.lineSegment _lineSegment;
        private ArrayList _Obstacles;
        private Timer time;
        private Int64 previousUpdateTime;
        private ArrayList _dataPoints;
        private ArrayList _dataPointsForDisplay;
        private double _xOrigin, _yOrigin;
        private double _range;
        private double _xEnd, _yEnd;
        private double _angle;
        private double _angleoffset;
        private double _rotationalVelocity; // rad/sec  
        private int _dataArraySize;
        private int _numberOfSamplePerRevolution;
        private double _sampleAngle;
    }
}
