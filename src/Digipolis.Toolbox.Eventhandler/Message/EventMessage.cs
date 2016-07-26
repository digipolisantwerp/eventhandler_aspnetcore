using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Toolbox.Eventhandler.Options;

namespace Toolbox.Eventhandler.Message
{
    public class EventMessage<T>
    {
        public EventMessage()
        {
            Header = new EventMessageHeader();
            Body = new EventMessageBody<T>();
        }

        [JsonProperty(Required = Required.Always)]
        public EventMessageHeader Header { get; set; }

        [JsonProperty(Required = Required.Always)]
        public EventMessageBody<T> Body { get; set; }
    }
}
