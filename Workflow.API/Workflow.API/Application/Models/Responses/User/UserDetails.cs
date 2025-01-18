namespace Workflow.API.Application.Models.Responses.User
{
    public class UserDetails
    {
        public long Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
