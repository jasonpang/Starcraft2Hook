using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game.Providers
{
    public class InputProvider
    {
        [DllImport ("user32.dll")]
        private static extern ushort GetAsyncKeyState (int vKey);

        public static bool IsDown (Keys key)
        {
            return ( GetAsyncKeyState ((int) key) & 0x8000 ) != 0;
        }
    }
}
