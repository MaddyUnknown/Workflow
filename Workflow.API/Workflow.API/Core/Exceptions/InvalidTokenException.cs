namespace Workflow.API.Core.Exceptions
{
    public class InvalidTokenException : ApplicationException
    {
        public InvalidTokenException() : base("Invalid token") { }
        public InvalidTokenException(string message) : base(message) { }
    }
}
