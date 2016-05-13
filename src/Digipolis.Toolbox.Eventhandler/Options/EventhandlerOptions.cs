using System;
using Microsoft.Extensions.Logging;
using Toolbox.Eventhandler.Options;

namespace Toolbox.Eventhandler
{
    public class EventhandlerOptions
    {

        /// <summary>
        /// The version of your message. You can use this field when parsing the message in Eventhandler.
        /// </summary>
        public string MessageVersion { get; set; } = Defaults.EventhandlerConfigKeys.MessageVersion;

        /// <summary>
        /// The application's id.
        /// </summary>
        public string AppId { get; set; } = Defaults.EventhandlerConfigKeys.AppId;
        
        /// <summary>
        /// The application's name.
        /// </summary>
        public string AppName { get; set; } = Defaults.EventhandlerConfigKeys.AppName;


        /// <summary>
        /// The instance's id.
        /// </summary>
        public string InstanceId { get; set; } = Defaults.EventhandlerConfigKeys.InstanceId;

        /// <summary>
        /// The instance's name.
        /// </summary>
        public string InstanceName { get; set; } = Defaults.EventhandlerConfigKeys.InstanceName;


        /// <summary>
        /// The url of the Eventhandler HTTP endpoint.
        /// </summary>
        public string EventEndpointUrl { get; set; } = Defaults.EventhandlerConfigKeys.EventEndpointUrl;


        /// <summary>
        /// Namespace on the Event manager endpoint
        /// </summary>
        public string EventEndpointNamespace { get; set; } = Defaults.EventhandlerConfigKeys.EventEndpointNamespace;

        /// <summary>
        /// Apikey for the event manager endpoint
        /// </summary>
        public string EventEndpointApikey { get; set; } = Defaults.EventhandlerConfigKeys.EventEndpointApikey;

        public string EventEndpointOwnerkey { get; set; } = Defaults.EventhandlerConfigKeys.EventEndpointOwnerkey;


    }
}
