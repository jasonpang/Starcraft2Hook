using Game.Interfaces;

namespace Game.Hooks
{
    public abstract class HookBase : IInstallable
    {
        public abstract void Install();

        public abstract void Uninstall();
    }
}
