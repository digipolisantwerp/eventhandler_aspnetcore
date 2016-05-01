using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Toolbox.Eventhandler.Options;

namespace Toolbox.Eventhandler.Message
{
    public class EventMessage
    {
        public EventMessage()
        {
            Header = new EventMessageHeader();
            Body = new EventMessageBody();
        }

        [JsonProperty(Required = Required.Always)]
        public EventMessageHeader Header { get; set; }

        [JsonProperty(Required = Required.Always)]
        public EventMessageBody Body { get; set; }
    }
}
