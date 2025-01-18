using Dapper;
using System.Data.SqlClient;
using Workflow.API.Core.Entities;
using Workflow.API.Core.Enums;
using Workflow.API.Core.Interfaces.Repositories;
using Workflow.API.Infrastructure.DataAccess.Resources;

namespace Workflow.API.Infrastructure.DataAccess.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private string _connectionStr;

        public TokenRepository(IConfiguration config)
        {
            _connectionStr = config.GetConnectionString("default");
        }

        public async Task<Token?> GetByTokenAndTokenType(string token, TokenType tokenType)
        {
            using(var conn = new SqlConnection(_connectionStr))
            {
                var result = await conn.QueryFirstOrDefaultAsync<Token>(TokenQueries.GET_BY_TOKEN_STR_TOKEN_TYPE, new { TokenStr = token, TokenType = tokenType });
                return result;
            }
        }

        public async Task<Token?> GetByUserIdAndTokenType(long userId, TokenType tokenType)
        {
            using (var conn = new SqlConnection(_connectionStr))
            {
                var result = await conn.QueryFirstOrDefaultAsync<Token>(TokenQueries.GET_BY_USER_ID_TOKEN_TYPE, new { UserId = userId, TokenType = tokenType });
                return result;
            }
        }

        public async Task<Token> AddOrUpdate(Token token)
        {
            using (var conn = new SqlConnection(_connectionStr))
            {
                var result = await conn.QueryFirstAsync<Token>(TokenQueries.ADD_OR_UPDATE_TOKEN, token);
                return result;
            }
        }

        public async Task<Token?> DeleteIfExists(long tokenId)
        {
            using (var conn = new SqlConnection(_connectionStr))
            {
                var result = await conn.QueryFirstOrDefaultAsync<Token>(TokenQueries.DELETE_TOKEN, new { TokenId = tokenId });
                return result;
            }
        }
    }
}
