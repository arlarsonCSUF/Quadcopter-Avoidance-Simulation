using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace QuadcopterAvoidanceSimulation
{
    class ObstacleLayout
    {
        public ArrayList Obstacles;
        public ObstacleLayout()
        {
            Obstacles = new ArrayList();
            Obstacles.Add(new Obstacle(0, 0, 100, 10));
            Obstacles.Add(new Obstacle(100, 10, 10, 100));
        }
    }
}
