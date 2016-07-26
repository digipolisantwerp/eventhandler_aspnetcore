using System;
using Microsoft.Extensions.Logging;

namespace Toolbox.Eventhandler.Message
{
    public interface IEventMessageBuilder
    {


        EventMessage<T> Build<T>(string MessageType, T MessageContent, string MessageFormat = null, string ComponentID = null, string ComponentName = null);

       

    }
}
