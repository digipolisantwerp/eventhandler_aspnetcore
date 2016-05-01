using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Toolbox.Eventhandler.Message
{
    public class EventMessageComponent
    {
        public EventMessageComponent(string componentId = null, string componentName = null)
        {
            ComponentId = componentId;
            ComponentName = componentName;
        }

        [MinLength(1)]
        [MaxLength(1024)]
        [JsonProperty(Required = Required.Default)]
        public string ComponentId { get; set; }

        [MinLength(1)]
        [MaxLength(1024)]
        [JsonProperty(Required = Required.Default)]
        public string ComponentName { get; set; }
    }
}
