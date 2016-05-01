//using System;
//using Microsoft.Extensions.Logging;

//namespace Toolbox.Eventhandler.Message
//{
//    public interface IEventMessageBuilder
//    {
        

//        EventMessage Build(string loggerName, LogLevel level, object state, Exception exception, Func<object, Exception, string> formatter = null);

//        Publish<DataType>(eventtype, datatype, [principal], [component])
//	De toolbox zal het datatype serializen naar json en deze als content doorsturen.
//PublishString(eventtype, string, [principal], [component])
//	De toolbox zal de string omzetten naar een json string met 1 veld.
//PublishJson(eventtype, json-string, [principal], [component])
//	De toolbox geeft de json-string letterlijk door als content.



//        var mijnVierkant = new Vierkant(....)

//IEventHandler.Publish<Vierkant>("vierkant:updated", mijnVierkant, user, component, ...)

//IEventHandler.PublishString(""vierkant:updated", "de vierkant is aangepast", user, component, ...)

//IEventHandler.PublishJson(""vierkant:updated", "{ de vierkant is aangepast
//    } ", user, component, ...)



//    }
//}
