using System;

namespace FootballData.ExternalServices.Exceptions
{
    public class RestServiceException : ApplicationException
    {
        public RestServiceException() { }

        public RestServiceException(string message) : base(message)
        {
        }
    }
}
