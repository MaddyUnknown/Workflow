using Workflow.API.Application.Constants;
using Workflow.API.Application.Interfaces.Validators;
using Workflow.API.Application.Utils;

namespace Workflow.API.Application.Models.Requests.User
{
    public class UserRegister : IValid
    {
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public bool Validate(out IEnumerable<string> errors)
        {
            var errorList = new List<string>();


            if (string.IsNullOrWhiteSpace(Name))
            {
                errorList.Add(string.Format(ValidationConstants.NOT_EMPTY, nameof(Name)));
            }


            if (string.IsNullOrWhiteSpace(Username))
            {
                errorList.Add(string.Format(ValidationConstants.NOT_EMPTY, nameof(Username)));
            }
            else if (!ValidatorUtil.IsValidUsername(Username))
            {
                errorList.Add(string.Format(ValidationConstants.INVALID_PROPERTY, nameof(Username), Username));
            }


            if (string.IsNullOrWhiteSpace(Email))
            {
                errorList.Add(string.Format(ValidationConstants.NOT_EMPTY, nameof(Email)));
            }
            else if (!ValidatorUtil.IsValidEmail(Email))
            {
                errorList.Add(string.Format(ValidationConstants.INVALID_PROPERTY, nameof(Email), Email));
            }


            if (string.IsNullOrWhiteSpace(Password))
            {
                errorList.Add(string.Format(ValidationConstants.NOT_EMPTY, nameof(Password)));
            }
            else if (!ValidatorUtil.IsValidPassword(Password))
            {
                errorList.Add(string.Format(ValidationConstants.INVALID_PROPERTY, nameof(Password), Password));
            }


            errors = errorList;
            return errorList.Count == 0 ? true : false;

        }
    }
}