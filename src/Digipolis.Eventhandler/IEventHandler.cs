namespace Digipolis.Eventhandler
{
    public interface IEventHandler
    {
        void Publish<T>(string messagetopic, string eventType, T eventContent, string componentId, string componentName, string eventFormat = null);
        void PublishJson(string messagetopic, string eventType, string eventContent, string componentId, string componentName, string eventFormat = null);
        void PublishString(string messagetopic, string eventType, string eventContent, string componentId, string componentName, string eventFormat = null);
    }
}