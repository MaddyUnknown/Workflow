namespace Workflow.API.Infrastructure.Application.Models
{
    public class JwtTokenConfig
    {
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string SigningKey { get; set; } = string.Empty;
        public int ExpiryInMin { get; set; } = 15;

        public static readonly string DEFAULT_CONFIG_SECTION_NAME = "JwtTokenConfig";

    }
}
