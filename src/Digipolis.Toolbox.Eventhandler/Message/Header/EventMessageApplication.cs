using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Toolbox.Eventhandler.Message
{
    public class EventMessageApplication
    {
        public EventMessageApplication(string applicationId = null, string applicationName = null)
        {
            ApplicationId = applicationId;
            ApplicationName = applicationName;
        }

        [MinLength(1)]
        [MaxLength(1024)]
        [JsonProperty(Required = Required.Default)]
        public string ApplicationId { get; set; }

        [MinLength(1)]
        [MaxLength(1024)]
        [JsonProperty(Required = Required.Default)]
        public string ApplicationName { get; set; }
    }
}
