using System;
using Microsoft.Extensions.Logging;

namespace Toolbox.Eventhandler.Message
{
    public interface IEventMessageBuilder
    {


        EventMessage Build(string MessageVersion, string MessageContent, string MessageFormat = null);

       

    }
}
