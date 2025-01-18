namespace Workflow.API.Infrastructure.Application.Models
{
    public class OpaqueTokenConfig
    {
        public int TokenSize { get; set; } = 64;
        public int ExpiryInMin { get; set; } = 15;

        public static readonly string DEFAULT_CONFIG_SECTION_NAME = "OpaqueTokenConfig";
    }
}
