using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Toolbox.Eventhandler.Options
{
    public class EventhandlerOptionsReader
    {
        public static EventhandlerOptions Read(IConfiguration config)
        {
            if ( config == null ) throw new ArgumentNullException(nameof(config), $"{nameof(config)} cannot be null.");

            var options = new EventhandlerOptions()
            {
                AppId = config.Get<string>(Defaults.EventhandlerConfigKeys.AppId),
                AppName = config.Get<string>(Defaults.EventhandlerConfigKeys.AppName),
                EventEndpointUrl = config.Get<string>(Defaults.EventhandlerConfigKeys.EventEndpointUrl),
                MessageVersion = config.Get<string>(Defaults.EventhandlerConfigKeys.MessageVersion),
                Version = config.Get<string>(Defaults.EventhandlerConfigKeys.Version),
                EventEndpointNamespace = config.Get<string>(Defaults.EventhandlerConfigKeys.EventEndpointNamespace),
                EventEndpointApikey = config.Get<string>(Defaults.EventhandlerConfigKeys.EventEndpointApikey),
                EventEndpointOwnerkey = config.Get<string>(Defaults.EventhandlerConfigKeys.EventEndpointOwnerkey)

            };

            Validate(options);

            return options;
        }

        public static EventhandlerOptions Read(Action<EventhandlerOptions> setupAction)
        {
            if ( setupAction == null ) throw new ArgumentNullException(nameof(setupAction), $"{nameof(setupAction)} cannot be null.");

            var options = new EventhandlerOptions();
            setupAction.Invoke(options);

            Validate(options);

            return options;
        }

        private static void Validate(EventhandlerOptions options)
        {
            if (String.IsNullOrWhiteSpace(options.AppId)) throw new InvalidOptionException(Defaults.EventhandlerConfigKeys.AppId, options.AppId, "Eventhandler AppId is mandatory.");
            if (String.IsNullOrWhiteSpace(options.AppName)) throw new InvalidOptionException(Defaults.EventhandlerConfigKeys.AppName, options.AppName, "Eventhandler AppName is mandatory.");
            if (String.IsNullOrWhiteSpace(options.EventEndpointUrl)) throw new InvalidOptionException(Defaults.EventhandlerConfigKeys.EventEndpointUrl, options.AppId, "Eventhandler endpoint Url is mandatory.");
            if (String.IsNullOrWhiteSpace(options.EventEndpointNamespace)) throw new InvalidOptionException(Defaults.EventhandlerConfigKeys.EventEndpointNamespace, options.AppId, "Eventhandler endpoint namespace is mandatory.");
            if (String.IsNullOrWhiteSpace(options.EventEndpointApikey)) throw new InvalidOptionException(Defaults.EventhandlerConfigKeys.EventEndpointApikey, options.AppId, "Eventhandler endpoint apikey is mandatory.");
            if (String.IsNullOrWhiteSpace(options.EventEndpointOwnerkey)) throw new InvalidOptionException(Defaults.EventhandlerConfigKeys.EventEndpointOwnerkey, options.AppId, "Eventhandler endpoint ownerkey is mandatory.");
            if (String.IsNullOrWhiteSpace(options.MessageVersion)) throw new InvalidOptionException(Defaults.EventhandlerConfigKeys.MessageVersion, options.AppId, "Eventhandler Messageversion is mandatory.");
            if (String.IsNullOrWhiteSpace(options.Version)) throw new InvalidOptionException(Defaults.EventhandlerConfigKeys.Version, options.AppId, "Eventhandler version is mandatory.");


            try
            {
                var uri = new Uri(options.EventEndpointUrl);
            }
            catch (UriFormatException)
            {
                throw new InvalidOptionException(Defaults.EventhandlerConfigKeys.EventEndpointUrl, options.EventEndpointUrl, "Event endpoint Url is not a valid uri.");
            }
        }
    }
}
