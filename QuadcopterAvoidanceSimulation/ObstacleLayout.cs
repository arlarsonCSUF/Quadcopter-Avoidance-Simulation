using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;

namespace QuadcopterAvoidanceSimulation
{
    class ObstacleLayout
    {
       
        public ObstacleLayout(Panel p)
        {
            int height = p.Height;
            int widht = p.Width;
            _Obstacles = new ArrayList();
            _Obstacles.Add(new Obstacle(0,100, 100,100));
            _Obstacles.Add(new Obstacle(100,100, 100,300));
            _Obstacles.Add(new Obstacle(200, 100, 200, 300));
            _Obstacles.Add(new Obstacle(300,300,400,400));
        }
        public ArrayList Obstacles { get {return _Obstacles ;}}
        private ArrayList _Obstacles;
    }
}
