using System;
using SharpDX.Direct3D9;

namespace Game.Hooks.Graphics
{
    public partial class Direct3D9Hook
    {
        public Device Device { get; set; }

        private Device GetOrCreateDevice(IntPtr devicePointer)
        {
            if (this.Device == null)
                this.Device = Device.FromPointer<Device>(devicePointer);

            return this.Device;
        }
    }
}