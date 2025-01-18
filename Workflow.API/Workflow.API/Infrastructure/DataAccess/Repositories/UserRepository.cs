using Dapper;
using System.Data.SqlClient;
using Workflow.API.Core.Entities;
using Workflow.API.Core.Interfaces.Repositories;
using Workflow.API.Infrastructure.DataAccess.Resources;

namespace Workflow.API.Infrastructure.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private string _connectionStr;

        public UserRepository(IConfiguration config)
        {
            _connectionStr = config.GetConnectionString("default");
        }

        public async Task<User?> Get(long id)
        {
            using (var conn = new SqlConnection(_connectionStr))
            {
                User? user = await conn.QueryFirstOrDefaultAsync<User>(UserQueries.GET_BY_USER_ID, new { Id = id });

                return user;
            }
        }

        public async Task<User?> GetByEmail(string email)
        {
            using (var conn = new SqlConnection(_connectionStr))
            {
                User? user = await conn.QueryFirstOrDefaultAsync<User>(UserQueries.GET_BY_EMAIL, new { Email = email });

                return user;
            }
        }

        public async Task<User?> GetByUsername(string username)
        {
            using (var conn = new SqlConnection(_connectionStr))
            {
                User? user = await conn.QueryFirstOrDefaultAsync<User>(UserQueries.GET_BY_USERNAME, new { Username = username });

                return user;
            }
        }

        public async Task<User> Add(User entity)
        {
            using (var conn = new SqlConnection(_connectionStr))
            {
                long userId = await conn.QuerySingleAsync<long>(UserQueries.INSERT_USER, entity);
                entity.Id = userId;

                return entity;
            }
        }
    }
}