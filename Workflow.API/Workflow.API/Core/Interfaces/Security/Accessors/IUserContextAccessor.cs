using Workflow.API.Core.Models.Contexts;

namespace Workflow.API.Core.Interfaces.Security.Accessors
{
    public interface IUserContextAccessor
    {
        public UserContext User { get; set; }
    }
}
