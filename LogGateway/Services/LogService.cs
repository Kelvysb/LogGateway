using LogGateway.Models;
using Serilog;

namespace LogGateway.Services
{
    public class LogService : ILogService
    {
        private const string template = "{source};{message};{detail}";
        public void Debug(LogMessage message)
        {
            Getcontext(message.Source)
                .Debug(template, message.Source, message.Description, message.Detail);
        }

        public void Error(LogMessage message)
        {
            Getcontext(message.Source)
                .Error(template, message.Source, message.Description, message.Detail);
        }

        public void Info(LogMessage message)
        {
            Getcontext(message.Source)
                .Information(template, message.Source, message.Description, message.Detail);
        }

        public void Warning(LogMessage message)
        {
            Getcontext(message.Source)
                .Warning(template, message.Source, message.Description, message.Detail);
        }

        private static Serilog.ILogger Getcontext(string source)
        {
            return Log.Logger.ForContext("source", source);
        }
    }
}
