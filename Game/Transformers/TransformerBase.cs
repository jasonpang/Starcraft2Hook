using System;
using Game.Hooks;
using Game.Interfaces;

namespace Game.Transformers
{
    public abstract class TransformerBase <THook, TMirror> : IAttachable
        where THook : HookBase 
        where TMirror : MirrorBase<THook>
    {
        protected THook Hook { get; set; }
        public virtual TMirror Mirror { get; set; }

        protected TransformerBase(THook hook, Func<HookBase, TMirror> mirror)
        {
            this.Hook = hook;
            this.Mirror = mirror(this.Hook);
        }

        public virtual void Attach()
        {
            this.Mirror.Attach();
        }

        public virtual void Detach()
        {
            this.Mirror.Detach();
        }
    }
}
