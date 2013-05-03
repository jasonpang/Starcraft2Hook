using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.State;

namespace Game.Providers
{
    public class StateProvider
    {
        public GraphicsState GraphicsState { get; set; }
        public InputState InputState { get; set; }

        public StateProvider (GraphicsState graphicsState, InputState inputState)
        {
            this.GraphicsState = graphicsState;
            this.InputState = inputState;
        }
    }
}
