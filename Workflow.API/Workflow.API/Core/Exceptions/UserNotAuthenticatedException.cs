namespace Workflow.API.Core.Exceptions
{
    public class UserNotAuthenticatedException : ApplicationException
    {
        public UserNotAuthenticatedException() : base("User not authenticated") { }
        public UserNotAuthenticatedException(string message) : base(message) { }
    }
}
