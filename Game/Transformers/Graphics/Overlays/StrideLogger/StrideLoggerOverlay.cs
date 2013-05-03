using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Game.Hooks.Graphics;
using Logging.Extensions;
using NLog;
using SharpDX;
using SharpDX.Direct3D9;
using Color = SharpDX.Color;
using Menu = Game.Graphics.Menu;
using MenuItem = Game.Graphics.MenuItem;

namespace Game.Transformers.Graphics.Overlays.StrideLogger
{
    public class StrideLoggerOverlay : Direct3D9Overlay
    {
        private readonly Logger Log = LogManager.GetCurrentClassLogger();

        public Menu Menu { get; set; }

        private bool areDefaultsSetup = false;

        public int Stride
        {
            get { return Menu.Items[0].Value; }
            set { Menu.Items[0].Value = value; }
        }

        public int NumVertices
        {
            get { return Menu.Items[1].Value; }
            set { Menu.Items[1].Value = value; }
        }

        public int PrimitiveCount
        {
            get { return Menu.Items[2].Value; }
            set { Menu.Items[2].Value = value; }
        }

        public int StartIndex
        {
            get { return Menu.Items[3].Value; }
            set { Menu.Items[3].Value = value; }
        }

        public bool Active { get; set; }

        public StrideLoggerOverlay(GameContext context)
            : base(context)
        {
        }

        public override void Attach()
        {
            Log.Trace ("Attach()");
            Hook.OnDrawIndexedPrimitve +=
                (ref IntPtr pointer, ref PrimitiveType type, ref int index, ref int vertexIndex,
                 ref int vertices, ref int startIndex, ref int count) =>
                    {
                        GetOrCreateDevice (pointer);

                        if (!areDefaultsSetup)
                        {
                            Initialize();
                            areDefaultsSetup = true;
                        }

                        Hook_OnDrawIndexedPrimitve (ref pointer, ref type, ref index, ref vertexIndex, ref vertices,
                                                    ref startIndex, ref count);
                    };

            Hook.OnSetStreamSource +=
                (ref IntPtr pointer, ref int number, ref IntPtr data, ref int bytes, ref int stride) =>
                    {
                        GetOrCreateDevice (pointer);

                        if (!areDefaultsSetup)
                        {
                            Initialize();
                            areDefaultsSetup = true;
                        }

                        Hook_OnSetStreamSource (ref pointer, ref number, ref data, ref bytes, ref stride);
                    };

            Hook.OnEndScene += (ref IntPtr devicePointer) =>
                                   {
                                       GetOrCreateDevice (devicePointer);

                                       if (!areDefaultsSetup)
                                       {
                                           Initialize();
                                           areDefaultsSetup = true;
                                       }

                                       Update();
                                       Draw();
                                   };
        }


        private Texture TextureOrange, TextureRed;
        private bool AutoSet { get; set; }

        private void Hook_OnDrawIndexedPrimitve (ref IntPtr devicePointer,
                                                 ref SharpDX.Direct3D9.PrimitiveType primitiveType,
                                                 ref int baseVertexIndex, ref int minVertexIndex, ref int numVertices,
                                                 ref int startIndex, ref int primitiveCount)
        {
            Log.Trace("Hook_OnDrawIndexedPrimitve()");

            Active = Control.IsKeyLocked(Keys.NumLock);

            AutoSet = Control.IsKeyLocked(Keys.Scroll);

            if (!Active)
                return;

            if (AutoSet)
            {
                NumVertices = numVertices;
                PrimitiveCount = primitiveCount;
                StartIndex = startIndex;
            }

            if (Stride == Stride &&
                NumVertices == numVertices &&
                PrimitiveCount == primitiveCount)
                //StartIndex == startIndex)
            {
                Device.SetRenderState (RenderState.ZEnable, false);
                Device.SetRenderState (RenderState.FillMode, FillMode.Solid);
                Device.SetTexture(0, TextureOrange);

                Device.DrawIndexedPrimitive (primitiveType, baseVertexIndex, minVertexIndex, numVertices, startIndex, primitiveCount);

                Device.SetRenderState(RenderState.ZEnable, true);
                Device.SetRenderState(RenderState.FillMode, FillMode.Solid);
                Device.SetTexture(0, TextureRed);
            }
        }

        void Hook_OnSetStreamSource(ref IntPtr devicePointer, ref int streamNumber, ref IntPtr streamVertexBufferData, ref int offsetInBytes, ref int stride)
        {
            Active = Control.IsKeyLocked(Keys.NumLock);

            AutoSet = Control.IsKeyLocked(Keys.Scroll);

            if (!Active)
                return;

            Log.Trace("Hook_OnSetStreamSource()");
            if (streamNumber == 0)
                Stride = stride;
        }

        protected override void Initialize()
        {
            Log.Trace("Initialize()");
            
            Active = true;
            TextureOrange = Texture.FromFile(Hook.Device, @"C:\Sc2Ai\Temp\TextureOrange.png");
            TextureRed = Texture.FromFile(Hook.Device, @"C:\Sc2Ai\Temp\TextureRed.png");

            Menu = new Menu (Device, new Point(5, 5), Orientation.Vertical, 
                new MenuItem ("Stride", 0, 0, Int32.MaxValue),
                new MenuItem ("NumVertices", 0, 0, Int32.MaxValue),
                new MenuItem ("PrimitiveCount", 0, 0, Int32.MaxValue),
                new MenuItem ("StartIndex", 0, 0, Int32.MaxValue)
                );
        }

        protected override void Update()
        {
            Active = Control.IsKeyLocked(Keys.NumLock);

            AutoSet = Control.IsKeyLocked(Keys.Scroll);

            if (!Active)
                return;

            Log.Trace ("Update()");
            Menu.Update();

            if (IsDown (Keys.End))
            {
                try
                {
                    File.AppendAllText (@"C:\Sc2Ai\Logs\StrideLogger.txt",
                                        String.Format (
                                                       "Stride: {0}, NumVertices: {1}, PrimitiveCount: {2}, StartIndex: {3} \n", Stride, NumVertices, PrimitiveCount, StartIndex));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        protected override void Draw()
        {
            if (!Active)
                return;

            Log.Trace("Draw()");
            Menu.Draw();
        }

        [DllImport("user32.dll")]
        static extern ushort GetAsyncKeyState(int vKey);

        private bool IsDown(System.Windows.Forms.Keys vKey)
        {
            return 0 != (GetAsyncKeyState((int)vKey) & 0x8000);
        }
    }
}
