using Workflow.API.Application.Constants;
using Workflow.API.Application.Interfaces.Validators;

namespace Workflow.API.Application.Models.Requests.Project
{
    public class ProjectSearch : IValid
    {
        // 1 based indexing
        public int? PageNumber { get; set; } = null;
        public int? PageSize { get; set; } = null;

        public bool Validate(out IEnumerable<string> errors)
        {
            var errorList = new List<string>();


            if (PageNumber != null && PageNumber <= 0)
            {
                errorList.Add(string.Format(ValidationConstants.NULL_OR_GREATER_THAN, nameof(PageNumber), 0));
            }


            if (PageSize != null && PageSize <= 0)
            {
                errorList.Add(string.Format(ValidationConstants.NULL_OR_GREATER_THAN, nameof(PageSize), 0));
            }


            if (PageNumber != null && PageSize == null || PageSize != null && PageNumber == null)
            {
                var nullPropertyName = PageNumber == null ? nameof(PageNumber) : nameof(PageSize);
                var notNullPropertyName = PageNumber == null ? nameof(PageSize) : nameof(PageNumber);

                errorList.Add(string.Format("'{0}' is mandatory if '{1}' is not null", nullPropertyName, notNullPropertyName));
            }


            errors = errorList;
            return errorList.Count == 0 ? true : false;
        }
    }
}
