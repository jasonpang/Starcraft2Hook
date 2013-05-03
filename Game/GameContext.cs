using Game.Hooks.Graphics;
using Game.Interpreters.Graphics;
using Game.Managers;
using Game.Providers;
using Game.State;
using Game.Transformers.Graphics;

namespace Game
{
    public class GameContext
    {
        #region Hooks

        #region Graphics Hook

        internal Direct3D9Hook GraphicsHook { get; set; }

        #endregion

        #region Input Hook

        #endregion

        #endregion

        #region Transformers

        #region Graphics Transformer

        internal Direct3D9Transformer GraphicsTransformer { get; set; }

        #endregion

        #region Input Transformer

        #endregion

        #endregion

        #region Interpreters

        #region Graphics Interpreter

        internal GraphicsInterpreter GraphicsInterpreter { get; set; }

        #endregion

        #region Input Interpreter

        #endregion

        #endregion

        #region States

        #region Graphics State

        internal GraphicsState GraphicsState { get; set; }

        #endregion

        #region Input State

        internal InputState InputState { get; set; }

        #endregion

        #endregion

        #region Providers

        #region Graphics Provider

        internal GraphicsProvider GraphicsProvider { get; set; }

        #endregion

        #region Input Provider

        internal InputProvider InputProvider { get; set; }

        #endregion

        #region State Provider

        internal StateProvider StateProvider { get; set; }

        #endregion

        #endregion

        #region Managers

        #region Graphics Manager

        public GraphicsManager GraphicsManager { get; set; }

        #endregion

        #region Input Manager

        public InputManager InputManager { get; set; }

        #endregion

        #region State Manager

        public StateManager StateManager { get; set; }

        #endregion

        #endregion
    }
}
