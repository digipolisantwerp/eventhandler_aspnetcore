using Newtonsoft.Json;

namespace Digipolis.Eventhandler.Message.Header
{
    public class EventMessageSource
    {
        public EventMessageSource(string applicationId = null, string applicationName = null, string instanceId = null, string instanceName = null, string componentId = null, string componentName = null)
        {
            Application = new EventMessageApplication(applicationId, applicationName);
            Instance = new EventMessageInstance(instanceId, instanceName);
            Component = new EventMessageComponent(componentId, componentName);
                    
        }

  

        [JsonProperty(Required = Required.Always)]
        public EventMessageApplication Application { get; set; }

        [JsonProperty(Required = Required.Always)]
        public EventMessageInstance Instance { get; set; }

        [JsonProperty(Required = Required.Default)]
        public EventMessageComponent Component { get; set; }

    }
}
