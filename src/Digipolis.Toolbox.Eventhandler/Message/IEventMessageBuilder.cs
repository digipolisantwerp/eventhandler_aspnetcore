using System;
using Microsoft.Extensions.Logging;

namespace Toolbox.Eventhandler.Message
{
    public interface IEventMessageBuilder
    {


        EventMessage Build(string MessageType, String MessageContent, string MessageFormat = null, string ComponentID = null, string ComponentName = null);

       

    }
}
