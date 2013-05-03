using Game.Interfaces;

namespace Game.Transformers.Graphics.Overlays
{
    /// <summary>
    /// A graphic overlay, capable of intercepting and modifying graphic input.
    /// </summary>
    public abstract class OverlayBase : IAttachable
    {
        protected GameContext Context { get; set; }

        protected OverlayBase (GameContext context)
        {
            this.Context = context;
        }

        public abstract void Attach();

        public abstract void Detach();
    }
}
