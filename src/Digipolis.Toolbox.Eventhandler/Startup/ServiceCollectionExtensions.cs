using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Toolbox.Eventhandler.Options;

namespace Toolbox.Eventhandler
{
    public static class ServiceCollectionExtentions
    {
        public static IServiceCollection AddEventHandler(this IServiceCollection services, Action<EventhandlerOptions> setupAction)
        {
            if ( setupAction == null ) throw new ArgumentNullException(nameof(setupAction), $"{nameof(setupAction)} cannot be null.");

            var options = new EventhandlerOptions();
            setupAction.Invoke(options);

            ConfigureEventhandlerOptions(services, options);

            return services;
        }

        public static IServiceCollection AddEventHandler(this IServiceCollection services, Action<EventhandlerJsonFile> setupAction)
        {
            if ( setupAction == null ) throw new ArgumentNullException(nameof(setupAction), $"{nameof(setupAction)} cannot be null.");

            var options = new EventhandlerJsonFile();
            setupAction.Invoke(options);
            
            ConfigureEventhandlerOptions(services, options);        

            return services;
        }


        private static void ConfigureEventhandlerOptions(IServiceCollection services, EventhandlerOptions options)
        {
            ValidateMandatoryField(options.AppId, nameof(options.AppId));
            ValidateMandatoryField(options.AppName, nameof(options.AppName));
            ValidateMandatoryField(options.EventEndpointUrl, nameof(options.EventEndpointUrl));
            ValidateMandatoryField(options.MessageVersion, nameof(options.MessageVersion));

            services.Configure<EventhandlerOptions>(opt =>
            {
                opt.AppId = options.AppId;
                opt.AppName = options.AppName;
                opt.EventEndpointUrl = options.EventEndpointUrl;
                opt.MessageVersion = options.MessageVersion;
            });
        }

        private static void ConfigureEventhandlerOptions(IServiceCollection services, EventhandlerJsonFile options)
        {
            ValidateMandatoryField(options.FileName, nameof(options.FileName));
            ValidateMandatoryField(options.Section, nameof(options.Section));

            var builder = new ConfigurationBuilder().AddJsonFile(options.FileName);
            var config = builder.Build();
            var section = config.GetSection(options.Section);
            services.Configure<EventhandlerOptions>(section);
        }

      
        private static void ValidateMandatoryField(string field, string fieldName)
        {
            if ( field == null ) throw new ArgumentNullException(fieldName, $"{fieldName} cannot be null.");
            if ( field.Trim() == String.Empty ) throw new ArgumentException($"{fieldName} cannot be empty.", fieldName);
        }

        private static void ValidateMandatoryField(object field, string fieldName)
        {
            if ( field == null ) throw new ArgumentNullException(fieldName, $"{fieldName} cannot be null.");
        }
    }
}
