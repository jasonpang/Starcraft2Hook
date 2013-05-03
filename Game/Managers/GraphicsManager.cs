using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Providers;

namespace Game.Managers
{
    public class GraphicsManager
    {
        public GraphicsProvider Provider { get; set; }

        public GraphicsManager (GraphicsProvider provider)
        {
            this.Provider = provider;
        }
    }
}
