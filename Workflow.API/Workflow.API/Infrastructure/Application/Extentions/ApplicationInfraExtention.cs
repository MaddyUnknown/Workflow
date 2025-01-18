using Workflow.API.Core.Enums;
using Workflow.API.Core.Interfaces.Security;
using Workflow.API.Infrastructure.Application.Models;
using Workflow.API.Infrastructure.Application.Security;

namespace Workflow.API.Infrastructure.Application.Extentions
{
    public static class ApplicationInfraExtention
    {
        public static void AddApplicationInfrastructureServices(this IServiceCollection services)
        {
            services.AddSingleton<JwtTokenGenerator>(provider =>
            {
                var config = provider.GetRequiredService<IConfiguration>();
                var tokenConfig = config.GetSection(JwtTokenConfig.DEFAULT_CONFIG_SECTION_NAME).Get<JwtTokenConfig>();

                return new JwtTokenGenerator(tokenConfig);
            });

            services.AddSingleton<OpaqueTokenGenerator>(provider =>
            {
                var config = provider.GetRequiredService<IConfiguration>();
                var tokenConfig = config.GetSection(OpaqueTokenConfig.DEFAULT_CONFIG_SECTION_NAME).Get<OpaqueTokenConfig>();

                return new OpaqueTokenGenerator(tokenConfig);
            });

            services.AddSingleton<Func<AuthTokenType, IAuthTokenGenerator>>(provider => key =>
            {
                return key switch
                {
                    AuthTokenType.Access => provider.GetRequiredService<JwtTokenGenerator>(),
                    AuthTokenType.Refresh => provider.GetRequiredService<OpaqueTokenGenerator>(),
                    _ => throw new ArgumentException($"IAuthTokenGenerator service not found for key : '{key.ToString()}'")
                };
            });


            services.AddSingleton<IPasswordHasher, BCryptPasswordHasher>(provider =>
            {
                return new BCryptPasswordHasher(13);
            });
        }
    }
}
