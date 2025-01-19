using Workflow.API.Application.Models.Requests.User;
using Workflow.API.Application.Models.Responses.User;
using Workflow.API.Core.Entities;
using Workflow.API.Core.Entities.Aggregates;

namespace Workflow.API.Application.Mappers
{
    public static class UserMapper
    {
        public static User FromUserRegister(UserRegister user, string hashedPassword)
        {
            return new User
            {
                Name = user.Name,
                Email = user.Email,
                Username = user.Username,
                HashedPassword = hashedPassword
            };
        }
    }

    public static class UserDetailsMapper
    {
        public static UserDetails FromUser(User user)
        {
            return new UserDetails
            {
                Id = user.Id,
                Name = user.Name,
                Username = user.Username,
                Email = user.Email
            };
        }
    }

    public static class TaskUserDetailsMapper
    {
        public static TaskUserDetails FromUser(User user)
        {
            return new TaskUserDetails { Id = user.Id, Username = user.Username };
        }
    }

    public static class ProjectMemberDetailsMapper
    {
        public static ProjectMemberDetails FromProjectMember(ProjectMember user)
        {
            return new ProjectMemberDetails { Id = user.Id, Username = user.Username, IsOwner = (user.IsOwner == false) ? null : true };
        }
    }
}
