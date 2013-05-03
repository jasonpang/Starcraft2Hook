using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Game.Extensions.RemotingInterface.Extensions;
using Game.Providers;

namespace Game.Input
{
    public class Key
    {
        public Keys Binding { get; set; }
        private bool lastKeyState = false;

        public Key (Keys binding)
        {
            this.Binding = binding;
        }

        public void Update()
        {
            var newState_KeyDown = InputProvider.IsDown (Binding);

            if (newState_KeyDown && !lastKeyState)
            {
                OnJustPressed.Fire (this, null);
            }
            else if (newState_KeyDown && lastKeyState)
            {
                OnHold.Fire (this, null);
            }
            else if (!newState_KeyDown && lastKeyState)
            {
                OnJustReleased.Fire (this, null);
            }

            lastKeyState = newState_KeyDown;
        }

        /// <summary>
        /// Occurs when the key was just pressed (but not held).
        /// </summary>
        public event EventHandler OnJustPressed;
        /// <summary>
        /// Occurs when the key is held down (occurs repeatedly if key is held down).
        /// </summary>
        public event EventHandler OnHold;
        /// <summary>
        /// Occurs when the key was just released (the last key event).
        /// </summary>
        public event EventHandler OnJustReleased;
    }
}
