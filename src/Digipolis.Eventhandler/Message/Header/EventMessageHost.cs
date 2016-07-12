using System.ComponentModel.DataAnnotations;

namespace Digipolis.Eventhandler.Message.Header
{
    public class EventMessageHost
    {
        public EventMessageHost(string ipAddress = null, string processId = null, string threadId = null)
        {
            IpAddress = ipAddress;
            ProcessId = processId;
            ThreadId = threadId;
        }

        [RegularExpression(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b")]
        public string IpAddress { get; set; }

        [RegularExpression(@"^\d$")]
        public string ProcessId { get; set; }

        [RegularExpression(@"^\d$")]
        public string ThreadId { get; set; }


    }
}
