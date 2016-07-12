using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Digipolis.Eventhandler.Message.Body
{
    public class EventMessageBody
    {
        public EventMessageBody()
        {
           
        }

        [JsonProperty(Required = Required.DisallowNull)]
        public EventMessageUser User { get; set; }

               
        [JsonProperty(Required = Required.Always)]
        public EventMessageMessage Message { get; set; }

        [MinLength(1)]
        [MaxLength(32)]
        [JsonProperty(Required = Required.Always)]
        public string MessageVersion { get; set; }
    }
}
