using Workflow.API.Core.Interfaces.Security.Accessors;
using Workflow.API.Core.Models.Contexts;

namespace Workflow.API.Infrastructure.Web.Accessors
{
    public class UserContextAccessor : IUserContextAccessor
    {
        private AsyncLocal<UserContextHolder> _userContextHolder = new AsyncLocal<UserContextHolder>();

        public UserContextAccessor()
        {
            _userContextHolder.Value = new UserContextHolder();
        }

        public UserContext User
        {
            get
            {
                return _userContextHolder.Value!.Context;
            }
            set
            {
                _userContextHolder.Value = new UserContextHolder { Context = value };
            }
        }


        private sealed class UserContextHolder
        {
            public UserContext Context = UserContext.Anonymous;
        }
    }
}
