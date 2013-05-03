using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Hooks.Graphics;
using Game.Transformers.Graphics.Mirrors;
using Game.Transformers.Graphics.Overlays;
using Game.Transformers.Graphics.Overlays.Console;
using Game.Transformers.Graphics.Overlays.Fps;
using Game.Transformers.Graphics.Overlays.StrideLogger;

namespace Game.Transformers.Graphics
{
    public class Direct3D9Transformer : TransformerBase <Direct3D9Hook, Direct3D9Mirror>
    {
        public List <Direct3D9Overlay> GraphicsOverlays { get; set; }

        public Direct3D9Transformer(Direct3D9Hook hook, params Direct3D9Overlay[] overlays)
            : base(hook, func => new Direct3D9Mirror (hook))
        {
            GraphicsOverlays = new List<Direct3D9Overlay>(overlays);
        }

        public override void Attach()
        {
            base.Attach();
            GraphicsOverlays.ForEach ((overlay) => overlay.Attach());
        }

        public override void Detach()
        {
            base.Detach();
            GraphicsOverlays.ForEach((overlay) => overlay.Detach());
        }

        // Add 119 Direct3D9 methods here
    }
}
