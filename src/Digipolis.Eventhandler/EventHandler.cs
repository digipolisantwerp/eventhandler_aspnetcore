using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Digipolis.Eventhandler.Message;
using Digipolis.Eventhandler.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Digipolis.Eventhandler
{
    public class EventHandler : IEventHandler
    {
        public EventHandler(IEventMessageBuilder eventMessageBuilder, IOptions<EventhandlerOptions> options)
        {
            if (eventMessageBuilder == null) throw new ArgumentNullException(nameof(eventMessageBuilder), $"{nameof(eventMessageBuilder)} cannot be null.");
            EventMessageBuilder = eventMessageBuilder;
            EventhandlerOptions = options.Value;
        }

        internal IEventMessageBuilder EventMessageBuilder { get; private set; }

        internal EventhandlerOptions EventhandlerOptions { get; private set; }


        /// <summary>
        /// Publishes an EventMessage after serialising its content to Json
        /// </summary>
        /// <param name="eventType">The type of the event eg. invoice:created</param>
        /// <param name="eventContent">Content of the event as generic type</param>
        /// <param name="componentId">Unique identifier of the component in the application that publishes the event.</param>
        /// <param name="componentName">Display-name of the component in the application that publishes the event.</param>
        /// <param name="eventFormat">Format of the content (can be used by parsers).</param>
        /// <param name="messagetopic">todo: describe messagetopic parameter on Publish</param>
        /// <typeparam name="T">The type of the EventMessage content</typeparam>

        public void Publish<T>(string messagetopic, string eventType, T eventContent, string componentId, string componentName, string eventFormat = null)
        {
            //Publish<DataType>(eventtype, datatype, [principal], [component])
            //De toolbox zal het datatype serializen naar json en deze als content doorsturen.
            var json = JsonConvert.SerializeObject(eventContent);  //TODO Correcte serialisatie via anonymous type???
            PublishJson(messagetopic, eventType, json, componentId, componentName, eventFormat);

        }

        /// <summary>
        /// Publishes an EventMessage
        /// </summary>
        /// <param name="eventType">The type of the event eg. invoice:created</param>
        /// <param name="eventContent">String content of the event</param>
        /// <param name="componentId">Unique identifier of the component in the application that publishes the event.</param>
        /// <param name="componentName">Display-name of the component in the application that publishes the event.</param>
        /// <param name="eventFormat">Format of the content (can be used by parsers).</param>
        /// <param name="messagetopic">todo: describe messagetopic parameter on PublishString</param>

        public void PublishString(string messagetopic, string eventType, string eventContent, string componentId, string componentName, string eventFormat = null)
        {
            var json = JsonConvert.SerializeObject(new { content = eventContent });  //TODO Correcte serialisatie via anonymous type???
            PublishJson(messagetopic, eventType, json, componentId, componentName, eventFormat);
        }

        /// <summary>
        /// Publishes an EventMessage
        /// </summary>
        /// <param name="eventType">The type of the event eg. invoice:created</param>
        /// <param name="eventContent">JSon content of the event</param>
        /// <param name="componentId">Unique identifier of the component in the application that publishes the event.</param>
        /// <param name="componentName">Display-name of the component in the application that publishes the event.</param>
        /// <param name="eventFormat">Format of the content (can be used by parsers).</param>
        /// <param name="messagetopic">todo: describe messagetopic parameter on PublishJson</param>
        public void PublishJson(string messagetopic, string eventType, string eventContent, string componentId, string componentName, string eventFormat = null)
        {
            try
            {
                var eventMessage = EventMessageBuilder.Build(eventType, eventContent, eventFormat, componentId, componentName);

                PublishToEndpoint(messagetopic, eventMessage);

            }
            catch (Exception)
            {
                if (!EventhandlerOptions.HideEventHandlerErrors)
                {
                    throw;
                }
            }

        }


        private void PublishToEndpoint(string messagetopic, EventMessage eventmessage)
        {
            //TODO: logic to create topics???
            try
            {
                var eventmessageJson = JsonConvert.SerializeObject(eventmessage);
                using (var client = new HttpClient())
                {
                    var uri = string.Concat(EventhandlerOptions.EventEndpointUrl, EventhandlerOptions.EventEndpointNamespace,"/", messagetopic, "/publish");
                    client.DefaultRequestHeaders.Add("apikey", EventhandlerOptions.EventEndpointApikey);
                    client.DefaultRequestHeaders.Add("owner-key", EventhandlerOptions.EventEndpointOwnerkey);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json; charset=utf-8"));
                    var response = client.PutAsync(uri, new StringContent(eventmessageJson,Encoding.UTF8, "application/json"));
                }
                //var client = new RestClient(EventhandlerOptions.EventEndpointUrl + EventhandlerOptions.EventEndpointNamespace + "/" + messagetopic + "/publish");
                //var request = new RestRequest(Method.PUT);
                //request.AddHeader("apikey", EventhandlerOptions.EventEndpointApikey);
                //request.AddHeader("owner-key", EventhandlerOptions.EventEndpointOwnerkey);
                //request.JsonSerializer.ContentType = "application/json; charset=utf-8";
                //request.AddParameter("application/json", eventmessageJson, ParameterType.RequestBody);
                //IRestResponse response = client.Execute(request);


            }
            catch (Exception)
            {
                if (!EventhandlerOptions.HideEventHandlerErrors)
                {
                    throw;
                }
            }

        }
    }
}