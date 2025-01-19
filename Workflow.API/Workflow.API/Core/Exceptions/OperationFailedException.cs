using Workflow.API.Application.Extentions;

namespace Workflow.API.Core.Exceptions
{
    public class OperationFailedException : ApplicationException
    {
        public OperationFailedException() : base("Requested operation failed!") { }
        public OperationFailedException(string message) : base(message) { }
    }
}
