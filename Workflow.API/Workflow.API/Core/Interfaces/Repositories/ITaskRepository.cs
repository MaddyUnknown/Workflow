using Workflow.API.Core.Entities;
using Workflow.API.Core.Entities.Aggregates;

namespace Workflow.API.Core.Interfaces.Repositories
{
    public interface ITaskRepository
    {
        Task<TaskItem?> Get(long taskId);
        Task<IEnumerable<TaskItemWithUser>> GetByProjectId(long projectId, bool includeArchiveTask = false);
        Task<TaskItem> Add(TaskItem task);
        Task<TaskItem> Update(TaskItem task);
        Task<TaskItem?> DeleteIfExists(long taskId);
    }
}
