using System;
using NLog;
using NLog.Config;
using NLog.Targets;
using NLog.Targets.Wrappers;

namespace Logging
{
    public static class LoggingManager
    {
        /// <summary>
        /// Initializes the NLog logging configuration.
        /// </summary>
        /// <remarks>
        /// Using DLL injection, we can't place an Nlog.config file in our target directory, so we have to create the configuration during runtime.
        /// </remarks>
        public static void Initialize()
        {
            var config = new LoggingConfiguration();

            var target = new FileTarget
                             {
                                 FileName = "C:/Sc2Ai/Logs/Sc2AiBot.log",
                                 Layout = "${longdate} ${level} ${callsite} ${message}",
                                 DeleteOldFileOnStartup = true
                             };

            var targetWrapper = new AsyncTargetWrapper(target, Int32.MaxValue, AsyncTargetWrapperOverflowAction.Discard);
            config.AddTarget("File", targetWrapper);

            var rule = new LoggingRule("*", LogLevel.Trace, target);
            config.LoggingRules.Add(rule);

            LogManager.Configuration = config;
            //LogManager.DisableLogging();
        }
    }
}
