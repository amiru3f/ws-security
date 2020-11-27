using System.Diagnostics;

namespace WcfService.Logging
{
    public class NLogTraceListener : TraceListener
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public override void Write(string message)
        {
            logger?.Trace(message);
        }

        public override void WriteLine(string message)
        {
            logger?.Trace(message);
        }
    }
}