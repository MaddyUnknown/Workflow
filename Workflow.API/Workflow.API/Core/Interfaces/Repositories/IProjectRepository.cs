using Workflow.API.Core.Entities;
using Workflow.API.Core.Interfaces.Collections;
using Workflow.API.Core.Models.Options;

namespace Workflow.API.Core.Interfaces.Repositories
{
    public interface IProjectRepository
    {
        Task<IPaginatedList<Project>> GetByUserId(long userId, ProjectSearchOptions? options);
        Task<Project?> GetByUserIdAndProjectId(long userId, long projectId);
        Task<bool> HasProjectAccess(long projectId, long userId);
        Task<Project> AddWithOwnerUserId(long ownerUserId, Project proj);
    }
}
