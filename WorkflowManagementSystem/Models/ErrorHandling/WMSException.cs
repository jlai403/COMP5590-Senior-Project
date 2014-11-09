using System;

namespace WorkflowManagementSystem.Models.ErrorHandling
{
    public class WMSException : Exception
    {
        public WMSException(string message) : base(message)
        {
        }

        public WMSException(string message, params object[] parameters) : this(String.Format(message, parameters))
        {
        }
    }
}