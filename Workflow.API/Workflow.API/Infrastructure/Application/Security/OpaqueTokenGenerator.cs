using System.Security.Cryptography;
using Workflow.API.Core.Entities;
using Workflow.API.Core.Interfaces.Security;
using Workflow.API.Core.Models.Security;
using Workflow.API.Infrastructure.Application.Models;

namespace Workflow.API.Infrastructure.Application.Security
{
    public class OpaqueTokenGenerator : IAuthTokenGenerator
    {
        private OpaqueTokenConfig _tokenConfig;

        public OpaqueTokenGenerator(OpaqueTokenConfig config)
        {
            _tokenConfig = config;
        }

        public AuthToken GenerateToken(User user)
        {
            var issueDateTime = DateTime.Now;
            var expiryDateTime = issueDateTime.AddMinutes(_tokenConfig.ExpiryInMin);
            byte[] tokenBytes = new byte[_tokenConfig.TokenSize];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(tokenBytes);
            }

            var token = Convert.ToBase64String(tokenBytes).TrimEnd('=').Replace('+', '-').Replace('/', '_');

            return new AuthToken
            {
                Token = token,
                CreationDateTime = issueDateTime,
                ExpiryDateTime = expiryDateTime,
            };
        }
    }
}
