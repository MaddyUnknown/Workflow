using Workflow.API.Core.Entities;
using Workflow.API.Core.Entities.Aggregates;
using Workflow.API.Core.Interfaces.Collections;
using Workflow.API.Core.Models.Options;

namespace Workflow.API.Core.Interfaces.Repositories
{
    public interface IProjectRepository
    {
        Task<IPaginatedList<Project>> GetByUserId(long userId, ProjectSearchOptions? options);
        Task<Project?> GetByUserIdAndProjectId(long userId, long projectId);
        Task<IEnumerable<ProjectMember>> GetProjectMembersByProjectId(long projectId);
        Task<bool> HasProjectAccess(long projectId, long userId, ProjectAccessSearchOptions? options = null);
        Task<Project> AddWithOwnerUserId(long ownerUserId, Project proj);
        Task AddProjectMember(long projectId, long userId);
        Task RemoveProjectMember(long projectId, long userId);

    }
}
