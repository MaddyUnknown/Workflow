namespace Workflow.API.Core.Models.Security
{
    public class AuthToken
    {
        public string Token { get; set; } = string.Empty;
        public DateTime CreationDateTime { get; set; }
        public DateTime ExpiryDateTime { get; set; }
    }
}
