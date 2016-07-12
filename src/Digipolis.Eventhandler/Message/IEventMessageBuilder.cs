using System;

namespace Digipolis.Eventhandler.Message
{
    public interface IEventMessageBuilder
    {
        EventMessage Build(string messageType, String messageContent, string messageFormat = null, string componentId = null, string componentName = null);
    }
}
