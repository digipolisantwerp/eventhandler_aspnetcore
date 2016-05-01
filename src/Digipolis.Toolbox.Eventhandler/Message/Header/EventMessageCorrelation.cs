using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Toolbox.Eventhandler.Message
{
    public class EventMessageCorrelation
    {
        public EventMessageCorrelation()
        {           
        }

        [MinLength(1)]
        [MaxLength(1024)]
        [JsonProperty(Required = Required.Default)]
        public string CorrelationId { get; set; }

        [JsonProperty(Required = Required.Default)]
        public EventMessageApplication Application { get; set; }

        [JsonProperty(Required = Required.Default)]
        public EventMessageInstance Instance { get; set; }
        


    }
}
