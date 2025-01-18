namespace Workflow.API.Core.Exceptions
{
    public class TokenParseException : ApplicationException
    {
        public TokenParseException() : base("Failed to parse token") { }
        public TokenParseException(string message) : base(message) { }
    }
}
