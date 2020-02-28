using Nodinite.Serilog.Models;

namespace Nodinite.Serilog.FileSink
{
    interface INodiniteSink
    {
        void LogMessage(NodiniteLogEvent logEvent);
    }
}
