using Workflow.API.Application.Interfaces.Services;
using Workflow.API.Application.Mappers;
using Workflow.API.Application.Models.Requests.User;
using Workflow.API.Application.Models.Responses.User;
using Workflow.API.Core.Entities;
using Workflow.API.Core.Enums;
using Workflow.API.Core.Exceptions;
using Workflow.API.Core.Interfaces.Repositories;
using Workflow.API.Core.Interfaces.Security;

namespace Workflow.API.Application.Services
{
    public class AuthService : IAuthService
    {
        private IUserRepository _userRepo;
        private ITokenRepository _tokenRepository;
        private IAuthTokenGenerator _accessTokenGenerator, _refreshTokenGenerator;
        private IPasswordHasher _passwordHasher;


        public AuthService(IUserRepository userRepository,
                                    ITokenRepository tokenRepository,
                                    IAuthTokenGenerator accessTokenGenerator,
                                    IAuthTokenGenerator refreshTokenGenerator,
                                    IPasswordHasher passwordHasher)
        {
            _userRepo = userRepository;
            _tokenRepository = tokenRepository;
            _accessTokenGenerator = accessTokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
            _passwordHasher = passwordHasher;
        }


        public async Task<LoginTokens> AuthenticateUser(UserLogin loginCreds)
        {
            //Validate request
            if (!loginCreds.Validate(out IEnumerable<string> errors)) throw new ValidationException(errors);

            // Validate credentials
            User? user = await _userRepo.GetByUsername(loginCreds.Username);
            if (user == null || !IsValidCredential(user, loginCreds.Password)) throw new ValidationException("Invalid username and password");

            //Generate access and refresh tokens
            var accessToken = _accessTokenGenerator.GenerateToken(user);
            var refreshToken = _refreshTokenGenerator.GenerateToken(user);

            //Save refresh token for user
            var token = new Token
            {
                UserId = user.Id,
                TokenType = TokenType.Refresh,
                TokenString = refreshToken.Token,
                CreationDateTime = refreshToken.CreationDateTime,
                ExpiryDateTime = refreshToken.ExpiryDateTime
            };

            await _tokenRepository.AddOrUpdate(token);


            return new LoginTokens
            {
                AccessToken = accessToken.Token,
                AccessTokenExpiryTimestamp = accessToken.ExpiryDateTime.ToUniversalTime(),

                RefreshToken = refreshToken.Token,
                RefreshTokenExpiryTimestamp = refreshToken.ExpiryDateTime.ToUniversalTime()
            };
        }

        public async Task<LoginTokens> RefreshUserTokens(RefreshUserToken refreshUserToken)
        {
            var existingToken = await _tokenRepository.GetByTokenAndTokenType(refreshUserToken.RefreshToken, TokenType.Refresh);
            if (existingToken == null) throw new InvalidTokenException("Invalid refresh token");

            //expired token
            if (existingToken.ExpiryDateTime <= DateTime.Now)
            {
                await _tokenRepository.DeleteIfExists(existingToken.Id);
                throw new InvalidTokenException("Expired refresh token");
            }

            var user = await _userRepo.Get(existingToken.UserId);
            if (user == null) throw new ValidationException("User not found");

            //Generate access and refresh tokens
            var accessToken = _accessTokenGenerator.GenerateToken(user);
            var refreshToken = _refreshTokenGenerator.GenerateToken(user);

            //Save refresh token for user
            var token = new Token
            {
                UserId = user.Id,
                TokenType = TokenType.Refresh,
                TokenString = refreshToken.Token,
                CreationDateTime = refreshToken.CreationDateTime,
                ExpiryDateTime = refreshToken.ExpiryDateTime
            };

            await _tokenRepository.AddOrUpdate(token);


            return new LoginTokens
            {
                AccessToken = accessToken.Token,
                AccessTokenExpiryTimestamp = accessToken.ExpiryDateTime.ToUniversalTime(),

                RefreshToken = refreshToken.Token,
                RefreshTokenExpiryTimestamp = refreshToken.ExpiryDateTime.ToUniversalTime()
            };
        }

        public async Task<UserDetails> RegisterUser(UserRegister user)
        {
            if (!user.Validate(out IEnumerable<string> errors)) throw new ValidationException(errors);

            if (await _userRepo.GetByUsername(user.Username) != null) throw new ValidationException($"Username '{user.Username}' already exists");
            if (await _userRepo.GetByEmail(user.Email) != null) throw new ValidationException($"User already registered for email : '{user.Email}'");


            string hashedPassword = GenerateHashedPassword(user.Password);
            Console.WriteLine(hashedPassword);
            User userDAO = UserMapper.FromUserRegister(user, hashedPassword);
            User addedUser = await _userRepo.Add(userDAO);

            UserDetails addedUserDetails = UserDetailsMapper.FromUser(addedUser);

            return addedUserDetails;
        }


        private string GenerateHashedPassword(string password) => _passwordHasher.GenerateHashedPassword(password);

        private bool IsValidCredential(User user, string password) => _passwordHasher.ValidateHashedPassword(password, user.HashedPassword);
    
    }
}
