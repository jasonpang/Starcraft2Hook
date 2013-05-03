using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Hooks.Graphics;
using Game.Transformers.Graphics.Mirrors;

namespace Game.Interpreters.Graphics
{
    public class GraphicsInterpreter : InterpreterBase <Direct3D9Mirror, Direct3D9Hook>
    {
        public GraphicsInterpreter(Direct3D9Mirror graphicsMirror)
            : base(graphicsMirror)
        {
        }

        public override void Attach()
        {
        }

        public override void Detach()
        {
        }
    }
}
