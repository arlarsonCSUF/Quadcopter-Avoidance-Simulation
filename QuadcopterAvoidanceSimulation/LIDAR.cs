using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuadcopterAvoidanceSimulation
{
    class LIDAR
    {
        public LIDAR(double x, double y, double range, double angleOffset, double RPM, Timer t)
        {
            time = t;
            _xOrigin = x;
            _yOrigin = y;
            _range = range;
            _angleoffset = angleOffset;
            _angle = angleOffset;
            _xEnd = _range * Math.Sin(_angle);
            _yEnd = _range * Math.Cos(_angle);
            _lineSegment.x1 = _xOrigin;
            _lineSegment.y1 = _yOrigin;
            _lineSegment.x2 = _xEnd;
            _lineSegment.y2 = _yEnd;
            _rotationalVelocity = Equations.toRad(360 * RPM / 60);
            previousUpdateTime = time.micros;
        }

        public void updateLidar(double x, double y, double yaw)
        {
            Int64 dT = time.micros - previousUpdateTime; // in us
            
            _angleoffset += _rotationalVelocity * (dT / 1000000.0);
            _angle = _angleoffset - yaw;
            _xOrigin = x;
            _yOrigin = y;
            _xEnd = _xOrigin + _range * Math.Sin(_angle);
            _yEnd = _yOrigin + _range * Math.Cos(_angle);
            
            previousUpdateTime = time.micros;
        }

        public Point originPoint { get {return new Point(_xOrigin,_yOrigin);}}
        public Point endPoint { get { return new Point(_xEnd, _yEnd);}}
        public double xOrigin { get { return _xOrigin; } set { _xOrigin = value; } }
        public double yOrigin { get { return _yOrigin; } set { _yOrigin = value; } }
        public double xEnd { get { return _xEnd; } set { _xEnd = value; } }
        public double yEnd { get { return _yEnd; } set { _yEnd = value; } }

        private Equations.lineSegment _lineSegment;
        private Timer time;
        private Int64 previousUpdateTime;
        private double _xOrigin, _yOrigin;
        private double _range;
        private double _xEnd, _yEnd;
        private double _angle;
        private double _angleoffset;
        private double _rotationalVelocity; // rad/sec

        
    }
}
