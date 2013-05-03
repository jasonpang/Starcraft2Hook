using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Game.Hooks.Graphics;
using SharpDX.Direct3D9;

namespace Game.Utilities
{
    public enum Direct3DVersion
    {
        #region Enum Code

        /// <summary>
        /// Direct3D 9
        /// </summary>
        Nine,

        /// <summary>
        /// Direct3D 10
        /// </summary>
        Ten,

        /// <summary>
        /// Direct3D 10.1
        /// </summary>
        TenOne,

        /// <summary>
        /// Direct3D 11
        /// </summary>
        Eleven,

        /// <summary>
        /// Direct3D 11.1
        /// </summary>
        ElevenOne,

        /// <summary>
        /// Unknown Direct3D version.
        /// </summary>
        Unknown

        #endregion
    }

    public static class Direct3DUtil
    {
        /// <summary>
        /// Returns the currently loaded module version of Direct3D in the executing assembly.
        /// </summary>
        public static Direct3DVersion GetLoadedVersion()
        {

            if (Windows.GetModuleHandle("d3d9.dll") != IntPtr.Zero)
                return Direct3DVersion.Nine;

            if (Windows.GetModuleHandle("d3d10.dll") != IntPtr.Zero)
                return Direct3DVersion.Ten;

            if (Windows.GetModuleHandle("d3d10_1.dll") != IntPtr.Zero)
                return Direct3DVersion.TenOne;

            if (Windows.GetModuleHandle("d3d11.dll") != IntPtr.Zero)
                return Direct3DVersion.Eleven;

            if (Windows.GetModuleHandle("d3d11_1.dll") != IntPtr.Zero)
                return Direct3DVersion.ElevenOne;

            return Direct3DVersion.Unknown;
        }

        /// <summary>
        /// Returns a dictionary of Direct3D 9 function names to their in-memory function addresses.
        /// </summary>
        public static Dictionary<Direct3D9DeviceFunctions, IntPtr> GetDirect3D9VirtualTableAddresses()
        {
            var functionAddressDictionary = new Dictionary<Direct3D9DeviceFunctions, IntPtr>();

            using (var direct3D = new Direct3D())
            {
                using (
                    var device = new Device(direct3D, 0, DeviceType.NullReference, IntPtr.Zero, SharpDX.Direct3D9.CreateFlags.HardwareVertexProcessing,
                        new PresentParameters() { BackBufferWidth = 1, BackBufferHeight = 1 }))
                {
                    var virtualTablePointer = Marshal.ReadIntPtr(device.NativePointer);
                    var numFunctions = Enum.GetNames(typeof(Direct3D9DeviceFunctions)).Length;

                    for (int index = 0; index < numFunctions; index++)
                    {
                        functionAddressDictionary.Add((Direct3D9DeviceFunctions)index,
                                                      Marshal.ReadIntPtr(virtualTablePointer, index * IntPtr.Size));
                    }
                }
            }

            return functionAddressDictionary;
        }
    }
}
