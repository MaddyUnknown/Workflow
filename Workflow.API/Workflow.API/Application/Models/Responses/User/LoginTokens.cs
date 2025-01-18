namespace Workflow.API.Application.Models.Responses.User
{
    public class LoginTokens
    {
        public string AccessToken { get; set; } = string.Empty;
        public DateTime AccessTokenExpiryTimestamp { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime RefreshTokenExpiryTimestamp { get; set; }
    }
}
