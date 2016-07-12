namespace Digipolis.Eventhandler.Options
{
    public class EventhandlerJsonFile
    {
        public EventhandlerJsonFile() : this(Defaults.EventHandlerJsonFile.FileName, Defaults.EventHandlerJsonFile.Section)
        { }

        public EventhandlerJsonFile(string fileName, string section)
        {
            FileName = fileName;
            Section = section;
        }

        public string FileName { get; set; }
        public string Section { get; set; }

        public EventhandlerOptions EventhandlerOptions { get; set; }
    }
}
