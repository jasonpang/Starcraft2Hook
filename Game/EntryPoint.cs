using Game.Hooks.Graphics;
using Game.Interpreters.Graphics;
using Game.Managers;
using Game.Providers;
using Game.State;
using Game.Transformers.Graphics;
using Game.Transformers.Graphics.Overlays.Fps;
using Game.Transformers.Graphics.Overlays.StrideLogger;
using NLog;

namespace Game
{
    /// <summary>
    /// Called by Interceptor.EntryPoint.
    /// </summary>
    public static class EntryPoint
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public static GameContext GameContext { get; set; }

        public static void InstallGameHooks()
        {
            Log.Trace ("Entering EntryPoint InstallGameHooks().");

            GameContext = new GameContext();

            Log.Trace ("Installing hooks...");
            InstallHooks (GameContext);
            Log.Trace ("Attaching transformers...");
            AttachTransformers (GameContext);
            Log.Trace ("Attaching interpreters...");
            AttachInterpreters (GameContext);
            Log.Trace ("Creating states...");
            CreateStates (GameContext);
            Log.Trace ("Creating providers...");
            CreateProviders (GameContext);
            Log.Trace ("Creating managers...");
            CreateManagers (GameContext);

            Log.Trace ("Exiting EntryPoint InstallGameHooks().");
        }

        public static void UninstallGameHooks()
        {
            Log.Trace ("Entering EntryPoint UninstallGameHooks().");

            UninstallHooks (GameContext);

            Log.Trace ("Exiting EntryPoint UninstallGameHooks().");
        }

        private static void InstallHooks (GameContext context)
        {
            context.GraphicsHook = new Direct3D9Hook();
            //context.GraphicsHook.Install();
            /*
            context.GraphicsHook.InstallOnly (Direct3D9DeviceFunctions.CreateQuery);
            context.GraphicsHook.InstallOnly (Direct3D9DeviceFunctions.CreateTexture,
                                              Direct3D9DeviceFunctions.ColorFill);
            context.GraphicsHook.InstallOnly (Direct3D9DeviceFunctions.DrawIndexedPrimitive,
                                              Direct3D9DeviceFunctions.SetStreamSource,
                                              Direct3D9DeviceFunctions.EndScene);*/
            context.GraphicsHook.InstallOnly(Direct3D9DeviceFunctions.CreateQuery,
                                              Direct3D9DeviceFunctions.CreateTexture,
                                              Direct3D9DeviceFunctions.SetTexture,
                                              Direct3D9DeviceFunctions.ColorFill,
                                              Direct3D9DeviceFunctions.DrawIndexedPrimitive,
                                              Direct3D9DeviceFunctions.SetStreamSource,
                                              Direct3D9DeviceFunctions.EndScene);
        }

        private static void UninstallHooks (GameContext context)
        {
            context.GraphicsHook.Uninstall();
        }

        private static void AttachTransformers (GameContext context)
        {
            context.GraphicsTransformer = new Direct3D9Transformer(context.GraphicsHook);//, new StrideLoggerOverlay (context));
            //context.GraphicsTransformer.GraphicsOverlays.Add(new FpsOverlay(context));
            context.GraphicsTransformer.GraphicsOverlays.Add(new StrideLoggerOverlay(context));
            context.GraphicsTransformer.Attach();
        }

        private static void AttachInterpreters (GameContext context)
        {
            context.GraphicsInterpreter = new GraphicsInterpreter (context.GraphicsTransformer.Mirror);
        }

        private static void CreateStates (GameContext context)
        {
            context.GraphicsState = new GraphicsState (context.GraphicsInterpreter);
            context.InputState = new InputState();
        }

        private static void CreateProviders (GameContext context)
        {
            context.GraphicsProvider = new GraphicsProvider();
            context.InputProvider = new InputProvider();
            context.StateProvider = new StateProvider (context.GraphicsState, context.InputState);
        }

        private static void CreateManagers (GameContext context)
        {
            context.GraphicsState = new GraphicsState (context.GraphicsInterpreter);
            context.InputManager = new InputManager (context.InputProvider);
            context.StateManager = new StateManager (context.StateProvider);
        }
    }
}
