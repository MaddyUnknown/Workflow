using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Workflow.API.Core.Enums;
using Workflow.API.Core.Exceptions;
using Workflow.API.Core.Interfaces.Security.Accessors;
using Workflow.API.Core.Models.Contexts;

namespace Workflow.API.Infrastructure.Web.Middleware
{
    public class UserContextMiddleware
    {
        private RequestDelegate _next;
        private IUserContextAccessor _accessor;


        public UserContextMiddleware(RequestDelegate next, IUserContextAccessor accessor)
        {
            _next = next;
            _accessor = accessor;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            // If user is not authenticated then user login is Anonymous
            if (!(context.User.Identity?.IsAuthenticated ?? false))
            {
                _accessor.User = UserContext.Anonymous;
                await _next(context);
                return;
            }

            // Else fetch jwt fields and populate User Accessor context
            var userIdStr = context.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
            var userName = context.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Name)?.Value;

            if (userIdStr != null && long.TryParse(userIdStr, out long userId))
            {
                _accessor.User = new UserContext(UserLoginType.UsernameAndPassword, userName, userId);
            }
            else
            {
                // Throw exception as user is supposed to have these claims if authenticated
                throw new TokenParseException();
            }

            await _next(context);
        }
    }
}
