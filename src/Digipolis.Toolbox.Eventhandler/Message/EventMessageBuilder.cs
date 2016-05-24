using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.OptionsModel;
using Toolbox.Correlation;
using Toolbox.Eventhandler.Options;

namespace Toolbox.Eventhandler.Message
{
    public class EventMessageBuilder : IEventMessageBuilder
    {
        public EventMessageBuilder(IServiceProvider serviceProvider, IOptions<EventhandlerOptions> options, IHttpContextAccessor contextAccessor)
        {
            if (serviceProvider == null) throw new ArgumentNullException(nameof(serviceProvider), $"{nameof(serviceProvider)} cannot be null.");
            if (options == null) throw new ArgumentNullException(nameof(options), $"{nameof(options)} cannot be null.");
            if (contextAccessor == null) throw new ArgumentNullException(nameof(contextAccessor), $"{nameof(contextAccessor)} cannot be null.");
            ServiceProvider = serviceProvider;
            Options = options.Value;
            ContextAccessor = contextAccessor;
            LocalIPAddress = GetLocalIPAddress();
            HostIPAddress = GetHostIPAddress();
            CurrentProcess = GetCurrentProcessId();
            CurrentThread = GetCurrentThreadId();
        }

        internal IServiceProvider ServiceProvider { get; private set; }
        internal EventhandlerOptions Options { get; private set; }
        internal IHttpContextAccessor ContextAccessor { get; private set; }

        internal string LocalIPAddress { get; private set; }
        internal string HostIPAddress { get; private set; }
        internal string CurrentProcess { get; private set; }
        internal string CurrentThread { get; private set; }


        public EventMessage Build(string MessageType, String MessageContent, string MessageFormat = null, string ComponentID = null, string ComponentName = null)
        {
           
           
            var eventMessage = new EventMessage();

            //HEADER
            eventMessage.Header.TimeStamp = DateTime.Now;

            eventMessage.Header.Host = new EventMessageHost(HostIPAddress, CurrentProcess, CurrentThread);
            eventMessage.Header.Correlation = BuildCorrelation();

            eventMessage.Header.Source = new EventMessageSource(Options.AppId,
                                                   Options.AppName,
                                                   Options.InstanceId,
                                                   Options.InstanceName,
                                                   ComponentID ?? Options.AppId,
                                                   ComponentName?? Options.AppName);

            eventMessage.Header.Version = Options.Version;

            //BODY

            eventMessage.Body.MessageVersion = Options.MessageVersion;

            eventMessage.Body.User = new EventMessageUser { UserName = ContextAccessor.HttpContext?.User?.Identity?.Name, IPAddress = LocalIPAddress };

            eventMessage.Body.Message = new EventMessageMessage(MessageType, MessageContent, MessageFormat);


            return eventMessage;
        }

                


        private EventMessageCorrelation BuildCorrelation()
        {
            var correlationContext = ServiceProvider.GetService(typeof(ICorrelationContext)) as ICorrelationContext;
            if (correlationContext != null)
                return new EventMessageCorrelation(correlationContext.SourceId,
                                                   correlationContext.SourceName,
                                                   correlationContext.InstanceId,
                                                   correlationContext.InstanceName,
                                                   correlationContext.Id);
            else
                return new EventMessageCorrelation(Options.AppId, 
                                                   Options.AppName,
                                                   Options.InstanceId,
                                                   Options.InstanceName,
                                                   Guid.NewGuid().ToString());
                      
        }


        //TODO
        private string GetLocalIPAddress()
        {
            return ContextAccessor.HttpContext?.Request?.HttpContext?.Connection?.RemoteIpAddress?.ToString();
        }


        //TODO

        private string GetHostIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return "unable to determine local IP Address.";
        }


        private string GetCurrentProcessId()
        {
            return Process.GetCurrentProcess().Id.ToString();            
        }

        private string GetCurrentThreadId()
        {
            return Thread.CurrentThread.ManagedThreadId.ToString();
        }



    }
}
