using System.Drawing;
using Cinchoo.Core.Configuration;

namespace Injector.Configuration
{
    [ChoIniConfigurationSection("Window", "Window Configuration", Config.Filename)]
    public class WindowConfiguration : ChoConfigurableObject
    {
        /// <summary>
        /// Set to true to save the window position on exit.
        /// </summary>
        [ChoPropertyInfo("SaveWindowPositionOnExit", DefaultValue = true)]
        public bool SaveWindowPositionOnExit { get; set; }

        /// <summary>
        /// Set to true to save the window position on exit.
        /// </summary>
        [ChoPropertyInfo("LastWindowPosition", DefaultValue = "(0, 0)")]
        public Point LastWindowPosition { get; set; }
    }
}
