using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Toolbox.Eventhandler.Message
{
    public class EventMessageHeader
    {
        [JsonProperty(Required = Required.Always)]
        public EventMessageCorrelation Correlation { get; set; }

        [DataType(DataType.DateTime)]
        [JsonProperty(Required = Required.Always)]
        public DateTime TimeStamp { get; set; } = DateTime.Now;

        [MinLength(1)]
        [MaxLength(32)]
        [JsonProperty(Required = Required.Always)]
        public string Version { get; set; }

        [JsonProperty(Required = Required.Always)]
        public EventMessageSource Source { get; set; }


        public EventMessageHost Host { get; set; }



    }
}
