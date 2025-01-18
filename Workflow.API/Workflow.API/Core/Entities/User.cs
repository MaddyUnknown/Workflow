namespace Workflow.API.Core.Entities
{
    public class User
    {
        public long Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string HashedPassword { get; set; } = string.Empty;
    }
}
