using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Toolbox.Eventhandler.Message
{
    public class EventMessageCorrelation
    {
        
        public EventMessageCorrelation(string applicationId = null, string applicationName = null, string instanceId = null, string instanceName = null, string correlationId = null)
        {
            Application = new EventMessageApplication(applicationId, applicationName);
            Instance = new EventMessageInstance(instanceId, instanceName);
            CorrelationId = correlationId;
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
