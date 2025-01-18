namespace Workflow.API.Core.Entities.Aggregates
{
    public class TaskItemWithUser : TaskItem
    {
        public User? AssignedUser { get; set; }
        public User? CreatorUser { get; set; }
    }
}
