using Workflow.API.Application.Interfaces.Services;
using Workflow.API.Application.Services;
using Workflow.API.Core.Enums;
using Workflow.API.Core.Interfaces.Repositories;
using Workflow.API.Core.Interfaces.Security;

namespace Workflow.API.Application.Extentions
{
    public static class ApplicationExtentions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>(provider =>
            {
                var authTokenGenFactory = provider.GetRequiredService<Func<AuthTokenType, IAuthTokenGenerator>>();
                var accessTokenGen = authTokenGenFactory(AuthTokenType.Access);
                var refreshTokenGen = authTokenGenFactory(AuthTokenType.Refresh);

                var passwordHasher = provider.GetRequiredService<IPasswordHasher>();
                var userRepo = provider.GetRequiredService<IUserRepository>();
                var tokenRepo = provider.GetRequiredService<ITokenRepository>();

                return new AuthService(userRepo, tokenRepo, accessTokenGen, refreshTokenGen, passwordHasher);
            });

            services.AddTransient<IProjectService, ProjectService>();

            services.AddTransient<ITaskItemService, TaskItemService>();
        }

    }
}
