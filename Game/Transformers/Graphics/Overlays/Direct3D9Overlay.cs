using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Hooks.Graphics;
using Game.Providers;
using SharpDX.Direct3D9;

namespace Game.Transformers.Graphics.Overlays
{
    public abstract class Direct3D9Overlay : OverlayBase
    {
        protected Direct3D9Hook Hook
        {
            get { return Context.GraphicsHook; }
        }

        protected Device Device
        {
            get { return Context.GraphicsHook.Device; }
            set { Context.GraphicsHook.Device = value; }
        }

        protected GraphicsProvider GraphicsProvider
        {
            get { return Context.GraphicsProvider; }
        }

        private bool areDefaultsSetup = false;

        protected Direct3D9Overlay(GameContext context)
            : base(context)
        {
        }

        protected Device GetOrCreateDevice(IntPtr devicePointer)
        {
            return this.Device ?? ( this.Device = Device.FromPointer <Device> (devicePointer) );
        }

        public override void Attach()
        {
            Hook.OnEndScene += (ref IntPtr devicePointer) =>
                                   {
                                       GetOrCreateDevice(devicePointer);

                                       if (!areDefaultsSetup)
                                       {
                                           Initialize();
                                           areDefaultsSetup = true;
                                       }

                                       Update();
                                       Draw();
                                   };
        }

        public override void Detach()
        {
            // Whatever
        }

        protected abstract void Initialize();

        protected abstract void Update();

        protected abstract void Draw();
    }
}
