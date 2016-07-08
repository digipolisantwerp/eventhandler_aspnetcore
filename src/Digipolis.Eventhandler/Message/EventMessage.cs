using Digipolis.Eventhandler.Message.Body;
using Digipolis.Eventhandler.Message.Header;
using Newtonsoft.Json;

namespace Digipolis.Eventhandler.Message
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
