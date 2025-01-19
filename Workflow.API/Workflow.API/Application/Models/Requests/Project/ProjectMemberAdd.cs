using Workflow.API.Application.Constants;
using Workflow.API.Application.Interfaces.Validators;

namespace Workflow.API.Application.Models.Requests.Project
{
    public class ProjectMemberAdd : IValid
    {
        public long ProjectId { get; set; } = 0;
        public long MemberUserId { get; set; } = 0;

        public bool Validate(out IEnumerable<string> errors)
        {
            var errorList = new List<string>();

            if (ProjectId <= 0)
            {
                errorList.Add(string.Format(ValidationConstants.GREATER_THAN, nameof(ProjectId), 0));
            }

            if (MemberUserId <= 0)
            {
                errorList.Add(string.Format(ValidationConstants.GREATER_THAN, nameof(MemberUserId), 0));
            }

            errors = errorList;
            return errorList.Count == 0 ? true : false;
        }
    }
}
