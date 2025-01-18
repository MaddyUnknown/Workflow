namespace Workflow.API.Core.Exceptions
{
    public class ValidationException: ApplicationException
    {
        private IEnumerable<string> _validationErrors;

        public IEnumerable<string> Errors
        {
            get { return _validationErrors; }
        }

        public ValidationException(string validationError, string? message = null) : base(message == null ? "Validation exception occured" : message)
        {
            this._validationErrors = new List<string> { validationError };
        }

        public ValidationException(IEnumerable<string> validationErrors, string? message = null) : base(message == null ? "Validation exception occured" : message)
        {
            this._validationErrors = validationErrors;
        }
    }
}
