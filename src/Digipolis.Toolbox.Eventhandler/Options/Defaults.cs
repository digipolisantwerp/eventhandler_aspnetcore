using System;
using System.Reflection;

namespace Toolbox.Eventhandler.Options
{
    public class Defaults
    {
      
        public static class EventhandlerConfigKeys
        {
            public const string MessageVersion = "1";
            public static string AppId = "AppId";
            public static string AppName = "AppName";
            public static string EventEndpointUrl = "Url";
        }

        //public static class HttpLogger
        //{
        //    public static string Name = Guid.NewGuid().ToString();
        //}


        public class EventHandlerJsonFile
        {
            public const string FileName = "eventhandlerconfig.json";
            public const string Section = "EventHandler";
        }


        public static class Exceptions
        {
            public static class InvalidOptionException
            {
                public static string Message = "Invalid option.";
            }
        }

    }
}
