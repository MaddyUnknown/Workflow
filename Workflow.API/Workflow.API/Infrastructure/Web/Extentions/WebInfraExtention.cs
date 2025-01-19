using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json.Serialization;
using Workflow.API.Core.Interfaces.Security.Accessors;
using Workflow.API.Infrastructure.Application.Models;
using Workflow.API.Infrastructure.Web.Accessors;
using Workflow.API.Infrastructure.Web.Middleware;
using Workflow.API.Infrastructure.Web.Serdes;

namespace Workflow.API.Infrastructure.Web.Extentions
{
    public static class WebInfraExtention
    {
        public static void AddWebInfrastructureService(this IServiceCollection services, IConfiguration config)
        {
            // Register controllers
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

            services.AddSingleton<IUserContextAccessor, UserContextAccessor>();

            //Authentication
            var accessTokenConfig = config.GetSection(JwtTokenConfig.DEFAULT_CONFIG_SECTION_NAME).Get<JwtTokenConfig>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 options.MapInboundClaims = false; // Stops 'sub' claim type being converted to 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'

                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     NameClaimType = JwtRegisteredClaimNames.Name, // Sets the identityName for the user
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = accessTokenConfig.Issuer,
                     ValidAudience = accessTokenConfig.Audience,
                     ValidAlgorithms = new[] { SecurityAlgorithms.HmacSha256 },
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(accessTokenConfig.SigningKey))
                 };
             });
        }

        public static void ConfigureHttpRequestPipeline(this WebApplication app)
        {
            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseMiddleware<GlobalExceptionMiddleware>();

            app.UseMiddleware<UserContextMiddleware>();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
