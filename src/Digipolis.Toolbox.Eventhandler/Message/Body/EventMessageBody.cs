using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Toolbox.Eventhandler.Options;

namespace Toolbox.Eventhandler.Message
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
