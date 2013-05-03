using Game.Hooks;
using Game.Interfaces;
using Game.Transformers;

namespace Game.Interpreters
{
    public abstract class InterpreterBase<TMirror, THook> : IAttachable
        where TMirror : MirrorBase<THook>
        where THook : HookBase
    {
        protected TMirror Mirror { get; set; }

        protected InterpreterBase(TMirror mirror)
        {
            this.Mirror = mirror;
        }

        public abstract void Attach();

        public abstract void Detach();
    }
}
