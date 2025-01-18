using Workflow.API.Core.Enums;

namespace Workflow.API.Core.Models.Contexts
{
    public class UserContext
    {
        private static UserContext _anonymousInstance = new UserContext(UserLoginType.Anonymous, null, null);
        public static UserContext Anonymous { get => _anonymousInstance; }

        public UserContext(UserLoginType loginType, string? userName, long? id)
        {
            LoginType = loginType;
            UserName = userName;
            Id = id;
        }


        // TO-DO: Track ip for temporary users, user login type etc.
        public UserLoginType LoginType { get; private set; } = UserLoginType.Anonymous;
        public long? Id { get; private set; } = null;
        public string? UserName { get; private set; } = null;
        public bool IsLoggedIn { get => LoginType != UserLoginType.Anonymous; }
    }
}
