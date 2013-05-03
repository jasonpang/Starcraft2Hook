using Cinchoo.Core.Configuration;

namespace Injector.Configuration
{
    [ChoIniConfigurationSection("Injection", "Injection Configuration", Config.Filename)]
    public class InjectionConfiguration : ChoConfigurableObject
    {
        /// <summary>
        /// Set to true to pause the process with a MessageBox before installing hooks.
        /// </summary>
        [ChoPropertyInfo("StartProcessPaused", DefaultValue = false)]
        public bool StartProcessPaused { get; set; }

        /// <summary>
        /// Automatically inject when the process is found.
        /// </summary>
        [ChoPropertyInfo("DoAutoInject", DefaultValue = false)]
        public bool AutoInject { get; set; }
    }
}
