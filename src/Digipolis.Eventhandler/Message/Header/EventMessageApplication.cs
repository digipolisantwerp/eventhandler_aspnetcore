using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Digipolis.Eventhandler.Message.Header
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
