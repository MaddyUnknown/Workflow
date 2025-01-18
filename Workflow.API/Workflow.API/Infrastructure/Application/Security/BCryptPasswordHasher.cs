using Workflow.API.Core.Interfaces.Security;
using BC = BCrypt.Net.BCrypt;

namespace Workflow.API.Infrastructure.Application.Security
{
    public class BCryptPasswordHasher : IPasswordHasher
    {
        private int _workFactor;
        public BCryptPasswordHasher(int workFactor)
        {
            _workFactor = workFactor;
        }

        public string GenerateHashedPassword(string password)
        {
            return BC.EnhancedHashPassword(password, this._workFactor);
        }

        public bool ValidateHashedPassword(string password, string hashedPassword)
        {
            return BC.EnhancedVerify(password, hashedPassword);
        }
    }
}
