using System.Net;

namespace QuartzApi.Exceptions
{
    public class UserException : Exception
    {
        public UserException(string message)
            : base(message)
        {
            HttpStatusCode = HttpStatusCode.BadRequest;
        }

        public UserException(string message, HttpStatusCode httpStatusCode)
            : base(message)
        {
            HttpStatusCode = httpStatusCode;
        }

        public HttpStatusCode HttpStatusCode { get; }
    }
}
