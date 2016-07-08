using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace Api.Shared.Extensions
{
    public static class LoggerFactoryExtensions
    {
        public static void AddSeriLog(this ILoggerFactory loggerFactory, IConfiguration config)
        {
            var minimumLevel = config.GetValue("MinimumLevel", LogLevel.Information);
            var seriLevel = ConvertLogLevel(minimumLevel);

            var rollingConfig = config.GetSection("RollingLogFile");
            var path = rollingConfig.GetValue<string>("Path");
            var template = rollingConfig.GetValue<string>("OutputTemplate");
            var rollingLevel = rollingConfig.GetValue<LogLevel>("MinimumLevel");

            Log.Logger = new LoggerConfiguration()
                .WriteTo.RollingFile(path, outputTemplate: template).MinimumLevel.Is(ConvertLogLevel(rollingLevel))
                .MinimumLevel.Is(seriLevel)
                .CreateLogger();
        }

        private static LogEventLevel ConvertLogLevel(LogLevel msLevel)
        {
            var seriLevel = LogEventLevel.Information;

            switch ( msLevel )
            {
                case LogLevel.Debug:
                    seriLevel = LogEventLevel.Verbose;
                    break;
                case LogLevel.Trace:
                    seriLevel = LogEventLevel.Debug;
                    break;
                case LogLevel.Information:
                    seriLevel = LogEventLevel.Information;
                    break;
                case LogLevel.Warning:
                    seriLevel = LogEventLevel.Warning;
                    break;
                case LogLevel.Error:
                    seriLevel = LogEventLevel.Error;
                    break;
                case LogLevel.Critical:
                    seriLevel = LogEventLevel.Fatal;
                    break;
                case LogLevel.None:
                    seriLevel = LogEventLevel.Fatal;
                    break;
            }

            return seriLevel;
        }
    }
}
