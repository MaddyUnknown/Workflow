using Workflow.API.Core.Entities;
using Workflow.API.Core.Enums;

namespace Workflow.API.Core.Interfaces.Repositories
{
    public interface ITokenRepository
    {
        public Task<Token?> GetByUserIdAndTokenType(long userId, TokenType tokenType);
        public Task<Token?> GetByTokenAndTokenType(string token, TokenType tokenType);
        public Task<Token> AddOrUpdate(Token token);
        public Task<Token?> DeleteIfExists(long tokenId);
    }
}
