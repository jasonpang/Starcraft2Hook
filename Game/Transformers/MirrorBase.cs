using Game.Hooks;
using Game.Interfaces;

namespace Game.Transformers
{
    public abstract class MirrorBase<THook> : IAttachable
        where THook : HookBase
    {
        protected THook Hook { get; set; }

        protected MirrorBase(THook hook)
        {
            this.Hook = hook;
        }

        public abstract void Attach();

        public abstract void Detach();
    }
}
