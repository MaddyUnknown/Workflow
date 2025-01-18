using Workflow.API.Application.Models.Responses.User;
using Workflow.API.Core.Enums;

namespace Workflow.API.Application.Models.Responses.Task
{
    public class TaskDetails
    {
        public long Id { get; set; } = 0;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateOnly? Deadline { get; set; } = DateOnly.MinValue;
        public TaskUserDetails? CreatorUser { get; set; } = null;
        public TaskUserDetails? AssignedUser { get; set; } = null;
        public TaskItemStatus Status { get; set; } = TaskItemStatus.Unknown;
    }
}
