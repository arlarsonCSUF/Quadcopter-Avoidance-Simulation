using System;
using System.Windows.Forms;

namespace QuadcopterAvoidanceSimulation
{
    class BufferedPanel : Panel
    {
        public BufferedPanel()
        {
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;
        }
    }
}
