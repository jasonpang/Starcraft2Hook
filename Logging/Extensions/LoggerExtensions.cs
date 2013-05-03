using System;
using System.Diagnostics;
using System.Text;
using NLog;

namespace Logging.Extensions
{
    public static class LoggerExtensions
    {
        public static void LogMethodSignatureTypesAndValues(this Logger logger, params object[] values)
        {
            var method = new StackTrace().GetFrame(1).GetMethod();
            var parameters = method.GetParameters();

            var stringBuilder = new StringBuilder();

            for (int i = 0; i < parameters.Length; i++)
            {
                stringBuilder.Append(String.Format("{0} {1}: {2}, ", parameters[i].ParameterType.Name, parameters[i].Name, values[i] ?? "Null"));
            }

            stringBuilder.Length = stringBuilder.Length - 2; // Remove the last ", "

            logger.Trace("{0}({1})", 
                method.Name,
                stringBuilder.ToString()
                );

        }

        public static void LogMethodSignatureAndValues(this Logger logger, params string[] values)
        {
            var method = new StackTrace().GetFrame(1).GetMethod();
            var parameters = method.GetParameters();

            var stringBuilder = new StringBuilder();

            for (int i = 0; i < parameters.Length; i++)
            {
                stringBuilder.Append(String.Format("{0}: {1}", parameters[i].Name, values[i] ?? "Null"));
            }

            logger.Trace("{0}({1})",
                method.Name,
                stringBuilder.ToString()
                );

        }

        public static void LogExceptionLineNumber(this Logger logger, Exception ex)
        {
            var stackFrame = new StackTrace().GetFrame(1);
            int columnNumber = stackFrame.GetFileColumnNumber();

            logger.Fatal (String.Format("{0} occured at line {1}.", ex.GetType().Name, columnNumber));
        }
    }
}
