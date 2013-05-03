using System;
using System.Drawing;
using Game.Extensions;
using NLog;
using SharpDX;

namespace Game.Transformers.Graphics.Overlays.Fps
{
    public enum DisplayMode
    {
        /// <summary>
        /// Display the number of frames per second.
        /// </summary>
        Fps,

        /// <summary>
        /// Display the number of seconds per frame.
        /// </summary>
        Spf,
    }

    public partial class FpsOverlay
    {
        /// <summary>
        /// The font family and size to draw the FPS with.
        /// </summary>
        public Font Font { get; set; }

        /// <summary>
        /// The absolute location to draw the FPS.
        /// </summary>
        public Point Location { get; set; }

        /// <summary>
        /// The last known FPS.
        /// </summary>
        public double Fps { get; private set; }

        /// <summary>
        /// The last known Spf.
        /// </summary>
        public double Spf { get; private set; }

        /// <summary>
        /// Whether to display FPS or SPF.
        /// </summary>
        public DisplayMode DisplayMode { get; set; }

        /// <summary>
        /// The number of fractional digits to display in the FPS or SPF.
        /// </summary>
        public int NumFractionalDigits { get; set; }

        /// <summary>
        /// The display color of the FPS or SPF.
        /// </summary>
        public System.Drawing.Color Color { get; set; }

        /// <summary>
        /// The smoothing algorithm applied to the FPS.
        /// </summary>
        public double FpsWeightRatio { get; set; }

        private readonly Logger Log = LogManager.GetCurrentClassLogger();
        private SharpDX.Direct3D9.Font drawingFont;
        private DateTime lastDrawTime;
        private DateTime lastRecordedFpsDateTime;

        protected override void Initialize()
        {
            try
            {
                this.Font = new Font ("Impact", 18.0f);
                this.drawingFont = new SharpDX.Direct3D9.Font (this.Device, this.Font);
                this.Color = System.Drawing.Color.Yellow;

                this.lastDrawTime = DateTime.Now;
                this.NumFractionalDigits = 1;
                this.Fps = 1;
                this.DisplayMode = DisplayMode.Spf;
                this.FpsWeightRatio = 0.1;
                this.lastRecordedFpsDateTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                this.Log.Warn (ex);
            }
        }

        protected override void Update()
        {
            try
            {
                CalculateFor (DisplayMode);
            }
            catch (Exception ex)
            {
                this.Log.Warn (ex);
            }
        }

        protected override void Draw()
        {
            try
            {
                DrawFor (DisplayMode);
            }
            catch (Exception ex)
            {
                this.Log.Warn (ex);
            }
        }

        private void CalculateFor (DisplayMode displayMode)
        {
            if (displayMode == DisplayMode.Spf)
            {
                var timeElapsedSinceLastFrame = DateTime.Now.Subtract (this.lastDrawTime);
                lastDrawTime = DateTime.Now;
                Spf = timeElapsedSinceLastFrame.TotalSeconds;
            }
            else if (displayMode == DisplayMode.Fps)
            {
                var timeElapsedSinceLastFrame = DateTime.Now.Subtract(this.lastDrawTime);
                lastDrawTime = DateTime.Now;

                Fps = Fps * (1.0 - FpsWeightRatio) + timeElapsedSinceLastFrame.TotalSeconds * FpsWeightRatio;
            }
        }

        private void DrawFor (DisplayMode displayMode)
        {
            double value = default( double );

            if (displayMode == DisplayMode.Spf)
            {
                value = Spf;
            }
            else if (displayMode == DisplayMode.Fps)
            {
                value = Fps;
            }

            drawingFont.DrawText(null, String.Format("{0:N1}", value), Location.X, Location.Y, new ColorBGRA(Color.R, Color.G, Color.B, Color.A));
        }
    }
}
