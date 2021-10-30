using LogGateway.Models;

namespace LogGateway.Services
{
    public interface ILogService
    {
        void Error(LogMessage message);

        void Info(LogMessage message);

        void Warning(LogMessage message);

        void Debug(LogMessage message);
    }
}
