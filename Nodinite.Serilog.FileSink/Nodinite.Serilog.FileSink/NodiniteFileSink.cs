
using Newtonsoft.Json;
using Nodinite.Serilog.Models;
using Serilog.Core;
using Serilog.Events;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Nodinite.Serilog.FileSink
{
    public class NodiniteFileSink : ILogEventSink, INodiniteSink
    {
        private readonly IFormatProvider _formatProvider;
        private readonly string _folder;
        private readonly NodiniteLogEventSettings _settings;
        private readonly Guid _localInterchangeId;

        public NodiniteFileSink(string folder, NodiniteLogEventSettings settings, IFormatProvider formatProvider)
        {
            _folder = folder;
            _settings = settings;
            _formatProvider = formatProvider;
            _localInterchangeId = Guid.NewGuid();

            // validate settings
            if (!_settings.LogAgentValueId.HasValue)
                throw new ArgumentNullException("LogAgentValueId must not be null");

            if (!Directory.Exists(_folder))
                throw new ArgumentException($"The specified folder '{_folder}' does not exists.");
        }

        public void Emit(LogEvent logEvent)
        {
            var message = logEvent.RenderMessage(_formatProvider);

            var nEvent = new NodiniteLogEvent(message, logEvent, _settings);

            LogMessage(nEvent);
        }

        public void LogMessage(NodiniteLogEvent logEvent)
        {
            logEvent.LocalInterchangeId = _localInterchangeId;
            logEvent.ServiceInstanceActivityId = Guid.NewGuid();

            var message = JsonConvert.SerializeObject(logEvent);

            File.WriteAllText(Path.Combine(_folder, Guid.NewGuid().ToString() + ".txt"), message);
        }
    }
}
