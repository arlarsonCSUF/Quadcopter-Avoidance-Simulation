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
        public Obstacle(int x, int y, int w, int h)
        {
            _xPosition = x;
            _yPosition = y;
            _width = w;
            _height = h;
        }

        public int xPosition{ get{return _xPosition;} set{_xPosition = value;}}
        public int yPosition{ get{ return _yPosition; } set { _yPosition = value;}}
        public int height { get { return _height; } set {_height = value;}}
        public int width { get { return _width; } set { _width = value; }} 
        
        private
            int _xPosition,_yPosition; //cord. of top left corner
            int _width,_height;
            
    }
}
