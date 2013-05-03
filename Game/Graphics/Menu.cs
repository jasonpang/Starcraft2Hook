using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Game.Extensions;
using Game.Input;
using Game.Interfaces;
using NLog;
using SharpDX;
using SharpDX.Direct3D9;
using Color = System.Drawing.Color;
using Font = System.Drawing.Font;

namespace Game.Graphics
{
    public class Menu : IDrawable
    {
        private readonly Logger Log = LogManager.GetCurrentClassLogger();

        public List <MenuItem> Items { get; set; }
        public Point Location { get; set; }
        public Orientation Orientation { get; set; }
        public int SelectedIndex { get; set; }
        public Color ForeColor { get; set; }
        public Color SelectedForeColor { get; set; }
        public Font Font { get; set; }
        public int ItemPadding { get; set; }

        private SharpDX.Direct3D9.Font DrawingFont;
        private Device Device { get; set; }

        private Key IncrementMenuKey, DecrementMenuKey, IncrementValueKey, DecrementValueKey, ResetToZeroKey;

        public Menu (Device device, Point location, Orientation orientation, params MenuItem[] items)
        {
            Log.Trace("Menu()");
            this.Device = device;
            Items = new List <MenuItem>(items);
            Location = location;
            Orientation = orientation;
            ForeColor = Color.White;
            SelectedForeColor = Color.Red;
            Font = new Font ("Arial", 12, FontStyle.Bold);
            ItemPadding = 15;

            DrawingFont = new SharpDX.Direct3D9.Font (device, Font);

            IncrementMenuKey = new Key (Keys.OemCloseBrackets);
            DecrementMenuKey = new Key (Keys.OemOpenBrackets);
            IncrementValueKey = new Key (Keys.PageUp);
            DecrementValueKey = new Key (Keys.PageDown);
            ResetToZeroKey = new Key (Keys.End);

            IncrementMenuKey.OnJustPressed += (sender, args) => { SelectedIndex = (SelectedIndex + 1).Clamp(SelectedIndex, Items.Count - 1); };
            DecrementMenuKey.OnJustPressed += (sender, args) => { SelectedIndex = (SelectedIndex - 1).Clamp(0, SelectedIndex); };
            IncrementValueKey.OnHold += (sender, args) => Items[SelectedIndex].IncrementValue(2);
            DecrementValueKey.OnHold += (sender, args) => Items[SelectedIndex].DecrementValue(2);
            ResetToZeroKey.OnJustPressed += (sender, args) => { Items[SelectedIndex].Value = 0; };
        }
        
        public void Update()
        {
            Log.Trace("Update()");

            IncrementMenuKey.Update();
            DecrementMenuKey.Update();
            IncrementValueKey.Update();
            DecrementValueKey.Update();
            ResetToZeroKey.Update();
        }

        public void Draw()
        {
            Log.Trace("Draw()");
            for (int i = 0; i < Items.Count; i++)
            {
                DrawText (
                          Orientation == Orientation.Vertical
                              ? new Point (Location.X, Location.Y + ( ItemPadding*i ))
                              : new Point (Location.X + ( ItemPadding*i ), Location.Y),
                          String.Format ("{0}: {1}", Items [i].Name, Items [i].Value),
                          i == SelectedIndex ? SelectedForeColor : ForeColor
                    );
            }
        }

        private void DrawText(Point location, string text, Color color)
        {
            DrawingFont.DrawText(null, text, location.X, location.Y, new ColorBGRA(color.R, color.G, color.B, color.A));
        }

        [DllImport("user32.dll")]
        static extern ushort GetAsyncKeyState(int vKey);

        private bool IsDown(System.Windows.Forms.Keys vKey)
        {
            return 0 != (GetAsyncKeyState((int)vKey) & 0x8000);
        }
    }
}
