using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Providers;

namespace Game.Managers
{
    public class StateManager
    {
        public StateProvider Provider { get; set; }

        public StateManager(StateProvider provider)
        {
            this.Provider = provider;
        }
    }
}
