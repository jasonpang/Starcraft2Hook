using System;

namespace Game.Extensions
{
    namespace RemotingInterface.Extensions
    {
        public static class EventHandlerExtensions
        {
            /// <summary>
            /// Safely fires an event by checking for subscribers first.
            /// </summary>
            public static void Fire (this EventHandler self, object sender, EventArgs args)
            {
                if (self != null)
                    self (sender, args);
            }

            /// <summary>
            /// Safely fires an event by checking for subscribers first.
            /// </summary>
            public static void Fire <T> (this EventHandler <T> self, object sender, T args)
                where T : EventArgs
            {
                if (self != null)
                    self (sender, args);
            }
        }
    }
}
