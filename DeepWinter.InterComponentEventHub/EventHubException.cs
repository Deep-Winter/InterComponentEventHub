using System;
using System.Collections.Generic;
using System.Text;

namespace DeepWinter.InterComponentEventHub
{
    public class EventHubException : Exception
    {

        public EventHubException(string message) : base(message)
        {
        }

        public EventHubException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
