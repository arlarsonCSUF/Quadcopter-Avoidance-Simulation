using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuadcopterAvoidanceSimulation
{
    public class Timer
    {
        public Timer(Int64 initTicks)
        {
            initialTicks = initTicks;
        }

        public Int64 millis{
            get { return (DateTime.Now.Ticks - initialTicks)/10000; }            
        }

        public Int64 micros
        {
            get { return ((DateTime.Now.Ticks - initialTicks) / 10); }
        }

        private Int64 initialTicks;
        
    }
}
