using System;

namespace RemotingInterface
{
    public sealed class IpcInterface : MarshalByRefObject
    {
        public bool StartPaused { get; set; }

        public override Object InitializeLifetimeService()
        {
            // Allow this object to live "forever"
            return null;
        }

        /// <summary>
        /// Does nothing, but will throw a remoting exception if the Controller is unreachable.
        /// </summary>
        public void Ping()
        {
            return;
        }
    }
}
