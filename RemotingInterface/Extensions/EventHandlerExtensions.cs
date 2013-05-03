using System;
using System.Collections.Generic;
using System.Text;

namespace RemotingInterface.Extensions
{
    public static class EventHandlerExtensions
    {
        /// <summary>
        /// Safely fires an event by checking for subscribers first.
        /// </summary>
        public static void Fire<T>(this EventHandler<T> self, object sender, T args)
            where T : EventArgs
        {
            if (self != null)
                self(sender, args);
        }
    }
}
