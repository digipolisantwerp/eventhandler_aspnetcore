using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Digipolis.Eventhandler.Message.Body;
using Digipolis.Eventhandler.Message.Header;
using Digipolis.Eventhandler.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Digipolis.Eventhandler.Message
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
            LocalIpAddress = GetLocalIpAddress();
            HostIpAddress = GetHostIpAddress();
            CurrentProcess = GetCurrentProcessId();
            CurrentThread = GetCurrentThreadId();
        }

        internal IServiceProvider ServiceProvider { get; private set; }
        internal EventhandlerOptions Options { get; private set; }
        internal IHttpContextAccessor ContextAccessor { get; private set; }

        internal string LocalIpAddress { get; private set; }
        internal string HostIpAddress { get; private set; }
        internal string CurrentProcess { get; private set; }
        internal string CurrentThread { get; private set; }


        public EventMessage Build(string messageType, String messageContent, string messageFormat = null, string componentId = null, string componentName = null)
        {
           
           
            var eventMessage = new EventMessage();

            //HEADER
            eventMessage.Header.TimeStamp = DateTime.Now;

            eventMessage.Header.Host = new EventMessageHost(HostIpAddress, CurrentProcess, CurrentThread);
            eventMessage.Header.Correlation = BuildCorrelation();

            eventMessage.Header.Source = new EventMessageSource(Options.AppId,
                                                   Options.AppName,
                                                   Options.InstanceId,
                                                   Options.InstanceName,
                                                   componentId ?? Options.AppId,
                                                   componentName?? Options.AppName);

            eventMessage.Header.Version = Options.Version;

            //BODY

            eventMessage.Body.MessageVersion = Options.MessageVersion;

            eventMessage.Body.User = new EventMessageUser { UserName = ContextAccessor.HttpContext?.User?.Identity?.Name, IpAddress = LocalIpAddress };

            eventMessage.Body.Message = new EventMessageMessage(messageType, messageContent, messageFormat);


            return eventMessage;
        }

                


        private EventMessageCorrelation BuildCorrelation()
        {
            return null;
            //TODO: (.Net Core 1.0.0 Upgrade) Enable lines below when correlation toolbox is upgraded.
            //var correlationContext = ServiceProvider.GetService(typeof(ICorrelationContext)) as ICorrelationContext;
            //if (correlationContext != null)
            //    return new EventMessageCorrelation(correlationContext.SourceId,
            //                                       correlationContext.SourceName,
            //                                       correlationContext.InstanceId,
            //                                       correlationContext.InstanceName,
            //                                       correlationContext.Id);
            //else
            //    return new EventMessageCorrelation(Options.AppId, 
            //                                       Options.AppName,
            //                                       Options.InstanceId,
            //                                       Options.InstanceName,
            //                                       Guid.NewGuid().ToString());
        }


        private string GetLocalIpAddress()
        {
            return GetRequestIp();
        }


        public string GetRequestIp(bool tryUseXForwardHeader = true)
        {
            string ip = null;

            // todo support new "Forwarded" header (2014) https://en.wikipedia.org/wiki/X-Forwarded-For

            if (tryUseXForwardHeader)
                ip = Helpers.SplitCsv(Helpers.GetHeaderValueAs<string>("X-Forwarded-For", ContextAccessor)).FirstOrDefault();

            // RemoteIpAddress is always null in DNX RC1 Update1 (bug).
            if (String.IsNullOrWhiteSpace(ip) && ContextAccessor.HttpContext?.Connection?.RemoteIpAddress != null)
                ip = ContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

            if (String.IsNullOrWhiteSpace(ip))
                ip = Helpers.GetHeaderValueAs<string>("REMOTE_ADDR", ContextAccessor);

            // _httpContextAccessor.HttpContext?.Request?.Host this is the local host.

            //if (String.IsNullOrWhiteSpace(ip))
            //    throw new Exception("Unable to determine caller's IP.");

            return ip;
        }

        private string GetHostIpAddress()
        {
            var host = Task.Run(()=> Dns.GetHostEntryAsync(Dns.GetHostName())).Result;
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
