namespace Digipolis.Toolbox.Eventhandler
{
    public interface IEventHandler
    {
        void Publish<T>(string eventType, T eventContent, string userName, string userIP, string componentId, string componentName, string eventFormat = null);
        void PublishJson(string eventType, string eventContent, string userName, string userIP, string componentId, string componentName, string eventFormat = null);
        void PublishString(string eventType, string eventContent, string userName, string userIP, string componentId, string componentName, string eventFormat = null);
    }
}