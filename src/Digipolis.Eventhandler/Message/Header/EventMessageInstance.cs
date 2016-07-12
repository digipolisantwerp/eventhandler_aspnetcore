using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Digipolis.Eventhandler.Message.Header
{
    public class EventMessageInstance
    {
        public EventMessageInstance(string instanceId = null, string instanceName = null)
        {
            InstanceId = instanceId;
            InstanceName = instanceName;
        }

        [MinLength(1)]
        [MaxLength(1024)]
        [JsonProperty(Required = Required.Default)]
        public string InstanceId { get; set; }

        [MinLength(1)]
        [MaxLength(1024)]
        [JsonProperty(Required = Required.Default)]
        public string InstanceName { get; set; }
    }
}
