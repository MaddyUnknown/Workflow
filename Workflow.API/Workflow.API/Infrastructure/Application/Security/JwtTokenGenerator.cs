using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Workflow.API.Core.Entities;
using Workflow.API.Core.Interfaces.Security;
using Workflow.API.Core.Models.Security;
using Workflow.API.Infrastructure.Application.Models;

namespace Workflow.API.Infrastructure.Application.Security
{
    public class JwtTokenGenerator : IAuthTokenGenerator
    {
        private JwtTokenConfig _tokenConfig;

        public JwtTokenGenerator(JwtTokenConfig config)
        {
            _tokenConfig = config;
        }

        public AuthToken GenerateToken(User user)
        {
            var issueDateTime = DateTime.Now;
            var expiryDateTime = issueDateTime.AddMinutes(_tokenConfig.ExpiryInMin);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfig.SigningKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var sectoken = new JwtSecurityToken(_tokenConfig.Issuer,
              _tokenConfig.Audience,
              GetUserClaims(user),
              expires: expiryDateTime,
              signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(sectoken);

            return new AuthToken
            {
                Token = token,
                CreationDateTime = issueDateTime,
                ExpiryDateTime = expiryDateTime
            };
        }

        private IEnumerable<Claim> GetUserClaims(User user)
        {
            IList<Claim> userClaims = new List<Claim>();
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Name, user.Username));
            return userClaims;
        }
    }
}
