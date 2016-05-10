using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Toolbox.Eventhandler.Message
{
    public class EventMessageMessage
    {

        public EventMessageMessage(string type, string content, string format = null)
        {
            Type = type;
            Content = content;
            Format = format;
        }



        [MinLength(1)]
        [MaxLength(256)]
        [JsonProperty(Required = Required.Always)]
        public string Type { get; set; }

        [JsonProperty(Required = Required.DisallowNull)]
        public string Content { get; set; }

        [MinLength(1)]
        [MaxLength(256)]
        [JsonProperty(Required = Required.Default)]
        public string Format { get; set; }

    }
}
