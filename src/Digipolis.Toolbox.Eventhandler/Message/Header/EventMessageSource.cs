using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Toolbox.Eventhandler.Message
{
    public class EventMessageSource
    {
        public EventMessageSource()
        {
           
        }

        [JsonProperty(Required = Required.Always)]
        public EventMessageApplication Application { get; set; }

        [JsonProperty(Required = Required.Always)]
        public EventMessageInstance Instance { get; set; }

        [JsonProperty(Required = Required.Default)]
        public EventMessageComponent Component { get; set; }

    }
}
