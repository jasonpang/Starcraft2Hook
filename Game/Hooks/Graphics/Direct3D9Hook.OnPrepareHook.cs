using System;
using System.Collections.Generic;
using Game.Utilities;

namespace Game.Hooks.Graphics
{
    public partial class Direct3D9Hook
    {
        private static Dictionary<Direct3D9DeviceFunctions, IntPtr> d3d9functionAddresses;

        private void CompileDirect3D9FunctionAddresses()
        {
            d3d9functionAddresses = Direct3DUtil.GetDirect3D9VirtualTableAddresses();
        }

        // Shorthand convenience method
        public static IntPtr GetAddressOf(Direct3D9DeviceFunctions function)
        {
            return d3d9functionAddresses[function];
        }
    }
}
