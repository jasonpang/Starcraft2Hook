using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Interpreters.Graphics;

namespace Game.State
{
    public class GraphicsState
    {
        public GraphicsInterpreter GraphicsInterpreter { get; set; }

        public GraphicsState (GraphicsInterpreter graphicsInterpreter)
        {
            this.GraphicsInterpreter = graphicsInterpreter;
        }
    }
}
