using Microsoft.AspNetCore.Mvc;
using Workflow.API.Application.Interfaces.Services;
using Workflow.API.Application.Models.Requests.User;
using Workflow.API.Application.Models.Responses.User;

namespace Workflow.API.Infrastructure.Web.Controllers
{
    [Route("api/auth/")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginTokens>> Login([FromBody] UserLogin loginRequest)
        {
            var tokenDetails = await _authService.AuthenticateUser(loginRequest);
            return Ok(tokenDetails);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<LoginTokens>> RefreshTokens([FromBody] RefreshUserToken refreshUserToken)
        {
            var tokenDetails = await _authService.RefreshUserTokens(refreshUserToken);
            return Ok(tokenDetails);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDetails>> RegisterUser([FromBody] UserRegister userRegistration)
        {
            var user = await _authService.RegisterUser(userRegistration);
            return Ok(user);
        }

        // TO-DO change password and forgot password
    }
}
