using System;
using System.Reflection;

namespace Toolbox.Eventhandler.Options
{
    public class Defaults
    {
      
        public static class EventhandlerConfigKeys
        {
            public const string MessageVersion = "1";
            public const string Version = "1";
            public static string AppId = "AppId";
            public static string AppName = "AppName";
            public static string InstanceId = "InstanceId";
            public static string InstanceName = "InstanceName";
            public static string EventEndpointUrl = "Url";
            public static string EventEndpointNamespace = "myNamespace";
            public static string EventEndpointApikey = "666-666-666-666-666";
            public static string EventEndpointOwnerkey = "666-666-666-666-666";
            public static bool HideEventHandlerErrors = true;            

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
