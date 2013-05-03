namespace Injector.Configuration
{
    /// <summary>
    /// Centralized application configuration methods.
    /// </summary>
    public static class Config
    {
        /// <summary>
        /// The filename of our INI configuration file.
        /// </summary>
        public const string Filename = "ControllerConfig.ini";

        public static WindowConfiguration Window { get; private set; }
        public static InjectionConfiguration Injection { get; private set; }

        static Config()
        {
            Window = new WindowConfiguration();
            Injection = new InjectionConfiguration();
        }
    }
}
