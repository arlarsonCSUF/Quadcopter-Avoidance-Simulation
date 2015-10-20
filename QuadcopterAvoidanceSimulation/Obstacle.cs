using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace QuadcopterAvoidanceSimulation
{
    class Obstacle
    {
        public Obstacle(int x1, int y1, int x2, int y2)
        {
            _x1 = x1;
            _y1 = y1;
            _x2 = x2;
            _y2 = y2;
            _lineSegment = new Equations.lineSegment(_x1, _y1, _x2, _y2);
        }

        public int x1{ get{return _x1;} set{_x1 = value;}}
        public int y1{ get{ return _y1; } set { _y1 = value;}}
        public int x2 { get { return _x2; } set {_x2 = value;}}
        public int y2 { get { return _y2; } set { _y2 = value; }}
        public Point p1 { get { return new Point(x1, y1); } }
        public Point p2 { get { return new Point(x2, y2); } }
        public Equations.lineSegment lineSegment { get { return _lineSegment; } }
        
        private
            int _x1,_y1; //cord. of top left corner
            int _x2,_y2;
            Equations.lineSegment _lineSegment;
            
    }
}
