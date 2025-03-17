using HRLeaveManagement.Application.CustomLogging;
using Microsoft.Extensions.Logging;

namespace HRLeaveManagement.Infrastructure.CustomLogging
{
    public class LoggerAdapter<T>(ILoggerFactory loggerFactory) : IAppLogger<T>
    {
        private readonly ILogger<T> _logger = loggerFactory.CreateLogger<T>();

        public void LogInformation(string message, params object[] args)
        {
            _logger.LogInformation(message, args);
        }

        public void LogWarning(string message, params object[] args)
        {
            _logger.LogWarning(message, args);
        }
    }
}
