using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Hooks.Graphics;
using NLog;

namespace Game.Transformers.Graphics.Mirrors
{
    public partial class Direct3D9Mirror : MirrorBase <Direct3D9Hook>
    {
        private readonly Logger Log = LogManager.GetCurrentClassLogger();

        public Direct3D9Mirror(Direct3D9Hook hook) 
            : base(hook)
        {
        }

        public override void Attach()
        {/*
            Hook.OnCreateTexture += Hook_OnCreateTexture;
            Hook.OnSetTexture += Hook_OnSetTexture;*/
        }

        public override void Detach()
        {
        }
    }
}
