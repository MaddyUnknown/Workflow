using Workflow.API.Core.Entities;
using Workflow.API.Core.Models.Security;

namespace Workflow.API.Core.Interfaces.Security
{
    public interface IAuthTokenGenerator
    {
        public AuthToken GenerateToken(User user);
    }
}
