namespace Workflow.API.Core.Exceptions
{
    // Base class for all custom exception class in this project
    public class ApplicationException : Exception
    {
        public ApplicationException() : base() { }
        public ApplicationException(string message) : base(message) { }
        public ApplicationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
