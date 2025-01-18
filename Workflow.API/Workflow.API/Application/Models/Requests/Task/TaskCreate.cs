using Workflow.API.Application.Constants;
using Workflow.API.Application.Interfaces.Validators;
using Workflow.API.Core.Enums;

namespace Workflow.API.Application.Models.Requests.Task
{
    public class TaskCreate : IValid
    {
        public long ProjectId { get; set; } = 0;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateOnly? Deadline { get; set; } = null;
        public long? AssignedUserId { get; set; } = null;
        public TaskItemStatus Status { get; set; } = TaskItemStatus.Unknown;

        public bool Validate(out IEnumerable<string> errors)
        {
            var errorList = new List<string>();


            if (ProjectId <= 0)
            {
                errorList.Add(string.Format(ValidationConstants.GREATER_THAN, nameof(ProjectId), 0));
            }


            if (string.IsNullOrWhiteSpace(Title))
            {
                errorList.Add(string.Format(ValidationConstants.NOT_EMPTY, nameof(Title)));
            }


            if (string.IsNullOrWhiteSpace(Description))
            {
                errorList.Add(string.Format(ValidationConstants.NOT_EMPTY, nameof(Description)));
            }


            if (AssignedUserId != null && AssignedUserId <= 0)
            {
                errorList.Add(string.Format(ValidationConstants.NULL_OR_GREATER_THAN, nameof(AssignedUserId), 0));
            }

            if (Status == TaskItemStatus.Unknown)
            {
                errorList.Add(string.Format(ValidationConstants.NOT_EQUAL_TO, nameof(Status), TaskItemStatus.Unknown));
            }


            errors = errorList;
            return errorList.Count == 0 ? true : false;
        }
    }
}
