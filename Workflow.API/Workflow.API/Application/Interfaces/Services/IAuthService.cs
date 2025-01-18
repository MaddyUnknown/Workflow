using Workflow.API.Application.Models.Requests.User;
using Workflow.API.Application.Models.Responses.User;

namespace Workflow.API.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<LoginTokens> AuthenticateUser(UserLogin loginCreds);
        Task<LoginTokens> RefreshUserTokens(RefreshUserToken refreshUserToken);
        Task<UserDetails> RegisterUser(UserRegister user);
    }
}
