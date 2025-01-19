namespace Workflow.API.Core.Entities.Aggregates
{
    public class ProjectMember : User
    {
        public bool IsOwner { get; set; } = false;
    }
}
