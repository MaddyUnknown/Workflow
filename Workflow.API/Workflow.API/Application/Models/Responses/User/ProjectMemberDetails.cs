namespace Workflow.API.Application.Models.Responses.User
{
    public class ProjectMemberDetails
    {
        public long Id { get; set; } = 0;
        public string Username { get; set; } = string.Empty;
        public bool? IsOwner { get; set; } = null;
    }
}
