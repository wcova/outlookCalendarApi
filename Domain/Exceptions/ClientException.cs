using System;
using System.Net;

namespace outlookCalendarApi.Domain.Exceptions
{
    [Serializable]
    public class ClientException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; }
        public ClientException() { }

        public ClientException(string message, HttpStatusCode httpStatusCode) : base(message)
        {
            HttpStatusCode = httpStatusCode;
        }
    }
}
