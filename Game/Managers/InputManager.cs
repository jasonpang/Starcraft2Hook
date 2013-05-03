using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Providers;

namespace Game.Managers
{
    public class InputManager
    {
        public InputProvider Provider { get; set; }

        public InputManager(InputProvider provider)
        {
            this.Provider = provider;
        }
    }
}
