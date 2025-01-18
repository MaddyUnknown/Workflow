using Workflow.API.Core.Entities;

namespace Workflow.API.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User?> Get(long id);
        Task<User?> GetByEmail(string email);
        Task<User?> GetByUsername(string username);
        public Task<User> Add(User entity);
    }
}
