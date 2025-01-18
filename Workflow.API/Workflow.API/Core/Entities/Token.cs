using Workflow.API.Core.Enums;

namespace Workflow.API.Core.Entities
{
    public class Token
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public TokenType TokenType { get; set; }
        public string TokenString { get; set; } = string.Empty;
        public DateTime CreationDateTime { get; set; }
        public DateTime ExpiryDateTime { get; set; }
    }
}
