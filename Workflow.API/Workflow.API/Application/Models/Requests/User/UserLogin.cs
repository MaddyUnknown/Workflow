using Workflow.API.Application.Constants;
using Workflow.API.Application.Interfaces.Validators;
using Workflow.API.Application.Utils;

namespace Workflow.API.Application.Models.Requests.User
{
    public class UserLogin : IValid
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public bool Validate(out IEnumerable<string> errors)
        {
            var errorList = new List<string>();


            if (string.IsNullOrWhiteSpace(Username))
            {
                errorList.Add(string.Format(ValidationConstants.NOT_EMPTY, nameof(Username)));
            }
            else if (!ValidatorUtil.IsValidUsername(Username))
            {
                errorList.Add(string.Format(ValidationConstants.INVALID_PROPERTY, nameof(Username), Username));
            }


            if (string.IsNullOrWhiteSpace(Password))
            {
                errorList.Add(string.Format(ValidationConstants.NOT_EMPTY, nameof(Password)));
            }


            errors = errorList;
            return errorList.Count == 0 ? true : false;
        }
    }
}
