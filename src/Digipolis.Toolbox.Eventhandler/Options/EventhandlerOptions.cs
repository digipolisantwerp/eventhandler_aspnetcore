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
        public string MessageVersion = Defaults.EventhandlerConfigKeys.MessageVersion;

        /// <summary>
        /// The application's id.
        /// </summary>
        public string AppId = Defaults.EventhandlerConfigKeys.AppId;
        
        /// <summary>
        /// The application's name.
        /// </summary>
        public string AppName = Defaults.EventhandlerConfigKeys.AppName;


        /// <summary>
        /// The instance's id.
        /// </summary>
        public string InstanceId = Defaults.EventhandlerConfigKeys.InstanceId;

        /// <summary>
        /// The instance's name.
        /// </summary>
        public string InstanceName = Defaults.EventhandlerConfigKeys.InstanceName;


        /// <summary>
        /// The url of the Eventhandler HTTP endpoint.
        /// </summary>
        public string EventEndpointUrl = Defaults.EventhandlerConfigKeys.EventEndpointUrl;


        
    }
}
