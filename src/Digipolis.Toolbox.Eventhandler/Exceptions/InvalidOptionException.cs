using System;
using Toolbox.Eventhandler.Options;

namespace Toolbox.Eventhandler
{
    public class InvalidOptionException : Exception
    {
        public InvalidOptionException() : base(Defaults.Exceptions.InvalidOptionException.Message)
        { }

        public InvalidOptionException(string key, string value, string message = null) : base(message == null ? Defaults.Exceptions.InvalidOptionException.Message : message)
        {
            OptionKey = key ?? "(null)";
            OptionValue = value ?? "(null)";
        }

        public string OptionKey { get; private set; }
        public string OptionValue { get; private set; }
    }
}
