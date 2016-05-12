using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Toolbox.Eventhandler;
using Toolbox.Eventhandler.Message;

namespace Digipolis.Toolbox.Eventhandler
{
    public class EventHandler: IEventHandler
    {
        public EventHandler(IEventMessageBuilder eventMessageBuilder, EventhandlerOptions options)
        {
            if (eventMessageBuilder == null) throw new ArgumentNullException(nameof(eventMessageBuilder), $"{nameof(eventMessageBuilder)} cannot be null.");
            EventMessageBuilder = eventMessageBuilder;
            EventhandlerOptions = options;
        }

        internal IEventMessageBuilder EventMessageBuilder { get; private set; }
        internal EventhandlerOptions EventhandlerOptions { get; private set; }


        /// <summary>
        /// Publishes an EventMessage after serialising its content to Json
        /// </summary>
        /// <typeparam name="T">The type of the EventMessage content</typeparam>
        /// <param name="eventType">The type of the event eg. invoice:created</param>
        /// <param name="eventContent">Content of the event as generic type</param>
        /// <param name="userName"></param>
        /// <param name="userIP"></param>
        /// <param name="componentId">Unique identifier of the component in the application that publishes the event.</param>
        /// <param name="componentName">Display-name of the component in the application that publishes the event.</param>
        /// <param name="eventFormat">Format of the content (can be used by parsers).</param>

        public void Publish<T>(string eventType, T eventContent, string userName, string userIP, string componentId, string componentName, string eventFormat = null)
        {
            //Publish<DataType>(eventtype, datatype, [principal], [component])
            //De toolbox zal het datatype serializen naar json en deze als content doorsturen.
            var json = JsonConvert.SerializeObject(eventContent);  //TODO Correcte serialisatie via anonymous type???
            PublishJson(eventType, json, userName, userIP, componentId, componentName, eventFormat);

        }

        /// <summary>
        /// Publishes an EventMessage
        /// </summary>
        /// <param name="eventType">The type of the event eg. invoice:created</param>
        /// <param name="eventContent">String content of the event</param>
        /// <param name="userName"></param>
        /// <param name="userIP"></param>
        /// <param name="componentId">Unique identifier of the component in the application that publishes the event.</param>
        /// <param name="componentName">Display-name of the component in the application that publishes the event.</param>
        /// <param name="eventFormat">Format of the content (can be used by parsers).</param>

        public void PublishString(string eventType, string eventContent, string userName, string userIP, string componentId, string componentName, string eventFormat = null)
        {
            var json = JsonConvert.SerializeObject(new { content = eventContent });  //TODO Correcte serialisatie via anonymous type???
            PublishJson(eventType, json, userName, userIP, componentId, componentName, eventFormat);
        }

        /// <summary>
        /// Publishes an EventMessage
        /// </summary>
        /// <param name="eventType">The type of the event eg. invoice:created</param>
        /// <param name="eventContent">JSon content of the event</param>
        /// <param name="userName"></param>
        /// <param name="userIP"></param>
        /// <param name="componentId">Unique identifier of the component in the application that publishes the event.</param>
        /// <param name="componentName">Display-name of the component in the application that publishes the event.</param>
        /// <param name="eventFormat">Format of the content (can be used by parsers).</param>
        public void PublishJson(string eventType, string eventContent, string userName, string userIP, string componentId, string componentName, string eventFormat = null)
        {            
            var eventMessage = EventMessageBuilder.Build(eventType, eventContent, eventFormat, componentId, componentName); //TODO USER ???

            PublishToEndpoint(eventMessage);
        }




        private void PublishToEndpoint(EventMessage eventmessage)
        {
            //TODO publish to URL
            //EventhandlerOptions.EventEndpointUrl

        }



    }
}
