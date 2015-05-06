using System;

namespace Game.Hooks.Graphics
{
    public partial class Direct3D9Hook
    {
        private static readonly Int32[] acl = new Int32[1];

        private void ActivateHooks()
        {            
            foreach (var hook in this.Hooks)
            {
                this.Log.Trace ("Activating hook: " + hook.Key);
                hook.Value.ThreadACL.SetExclusiveACL(acl);
            }
        }
    }
}
