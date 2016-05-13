namespace Digipolis.Toolbox.Eventhandler
{
    public interface IEventHandler
    {
        void Publish<T>(string messagetopic, string eventType, T eventContent, string userName, string userIP, string componentId, string componentName, string eventFormat = null);
        void PublishJson(string messagetopic, string eventType, string eventContent, string userName, string userIP, string componentId, string componentName, string eventFormat = null);
        void PublishString(string messagetopic, string eventType, string eventContent, string userName, string userIP, string componentId, string componentName, string eventFormat = null);
    }
}