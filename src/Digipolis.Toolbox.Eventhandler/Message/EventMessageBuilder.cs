using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.Logging;
using Toolbox.Correlation;
using Toolbox.Eventhandler.Options;

namespace Toolbox.Eventhandler.Message
{
    public class EventMessageBuilder : IEventMessageBuilder
    {
        public EventMessageBuilder(IServiceProvider serviceProvider, EventhandlerOptions options, IHttpContextAccessor contextAccessor)
        {
            if (serviceProvider == null) throw new ArgumentNullException(nameof(serviceProvider), $"{nameof(serviceProvider)} cannot be null.");
            if (options == null) throw new ArgumentNullException(nameof(options), $"{nameof(options)} cannot be null.");
            if (contextAccessor == null) throw new ArgumentNullException(nameof(contextAccessor), $"{nameof(contextAccessor)} cannot be null.");
            ServiceProvider = serviceProvider;
            Options = options;
            ContextAccessor = contextAccessor;
            LocalIPAddress = GetLocalIPAddress();
            CurrentProcess = GetCurrentProcessId();
            CurrentThread = GetCurrentThreadId();
        }

        internal IServiceProvider ServiceProvider { get; private set; }
        internal EventhandlerOptions Options { get; private set; }
        internal IHttpContextAccessor ContextAccessor { get; private set; }

        internal string LocalIPAddress { get; private set; }
        internal string CurrentProcess { get; private set; }
        internal string CurrentThread { get; private set; }

        public EventMessage Build(string loggerName, LogLevel logLevel, object state, Exception exception, Func<object, Exception, string> formatter = null)
        {
            if (state == null && exception == null) return null;

           
            var eventMessage = new EventMessage();

            //HEADER
            eventMessage.Header.TimeStamp = DateTime.Now;

            eventMessage.Header.Host = new EventMessageHost(LocalIPAddress, CurrentProcess, CurrentThread);
            eventMessage.Header.Correlation = BuildCorrelation();



            string message;
            if (formatter != null)
                message = formatter(state, exception);
            else
                message = Microsoft.Extensions.Logging.LogFormatter.Formatter(state, exception);

            if (!string.IsNullOrEmpty(message))
            {
                
                eventMessage.Header.Source = new EventMessageSource(Options.AppId, loggerName);
                eventMessage.Header.VersionNumber = Defaults.Message.HeaderVersion;

                //logMessage.Body.User = new EventMessageUser() { UserId = Thread.CurrentPrincipal?.Identity?.Name, IPAddress = LocalIPAddress };       // ToDo (SVB) : where does user's ip address come from?
                eventMessage.Body.User = new EventMessageUser() { UserId = "ss", IPAddress = LocalIPAddress };
                eventMessage.Body.VersionNumber = Options.MessageVersion;
                eventMessage.Body.Content = message;
                //logMessage.Body.Content = Serialize(state);     // ??

            }

            return eventMessage;
        }



        private EventMessageCorrelation BuildCorrelation()
        {
            var correlationContext = ServiceProvider.GetService(typeof(ICorrelationContext)) as ICorrelationContext;
            if (correlationContext != null)
                return new EventMessageCorrelation(correlationContext.CorrelationSource, correlationContext.CorrelationId);
            else
                return new EventMessageCorrelation(Options.AppId, 
                                                   Options.AppName,
                                                   Options.InstanceId,
                                                   Options.InstanceName,
                                                   Guid.NewGuid().ToString());
                      
        }


        private string GetLocalIPAddress()
        {
 ContextAccessor.HttpContext.Request.Host...Current.Request.UserHostAddress;

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

        private string GetHostIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.get.GetHostName());
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
