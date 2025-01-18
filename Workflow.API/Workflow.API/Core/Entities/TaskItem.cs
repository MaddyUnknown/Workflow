using Workflow.API.Core.Enums;

namespace Workflow.API.Core.Entities
{
    public class TaskItem
    {
        public long Id { get; set; } = 0;
        public long ProjectId { get; set; } = 0;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateOnly? Deadline { get; set; } = null;
        public long CreatorUserId { get ; set; } = 0;
        public long? AssignedUserId { get; set; } = null;
        public TaskItemStatus Status { get; set; } = TaskItemStatus.Unknown;
    }
}