using Workflow.API.Application.Constants;
using Workflow.API.Application.Interfaces.Validators;
using Workflow.API.Application.Utils;

namespace Workflow.API.Application.Models.Requests.Project
{
    public class ProjectCreate : IValid
    {
        public string Title { get; set; } = string.Empty;

        public bool Validate(out IEnumerable<string> errors)
        {
            var errorList = new List<string>();

            if (string.IsNullOrWhiteSpace(Title))
            {
                errorList.Add(string.Format(ValidationConstants.NOT_EMPTY, nameof(Title)));
            }

            errors = errorList;
            return errorList.Count == 0 ? true : false;
        }
    }
}