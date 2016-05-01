using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Toolbox.Eventhandler.Message
{
    public class EventMessageHost
    {
        public EventMessageHost()
        {
           
        }

        [RegularExpression(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b")]
        public string IPAddress { get; set; }

        [RegularExpression(@"^\d$")]
        public string ProcessId { get; set; }

        [RegularExpression(@"^\d$")]
        public string ThreadId { get; set; }


    }
}
