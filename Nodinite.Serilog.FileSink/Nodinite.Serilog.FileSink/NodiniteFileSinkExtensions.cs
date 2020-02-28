using Nodinite.Serilog.Models;
using Serilog;
using Serilog.Configuration;
using Serilog.Events;
using System;

namespace Nodinite.Serilog.FileSink
{
    public static class NodiniteFileSinkExtensions
    {
        public static LoggerConfiguration NodiniteFileSink(
                  this LoggerSinkConfiguration loggerConfiguration,
                  string Folder,
                  NodiniteLogEventSettings Settings,
                  IFormatProvider formatProvider = null,
                  LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum)
        {
            if (loggerConfiguration == null)
                throw new ArgumentNullException("loggerConfiguration");

            return loggerConfiguration.Sink(new NodiniteFileSink(Folder, Settings, formatProvider), restrictedToMinimumLevel);
        }
    }
}
