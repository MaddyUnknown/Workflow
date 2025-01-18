using Workflow.API.Application.Models.Requests.Task;
using Workflow.API.Application.Models.Responses.Task;

namespace Workflow.API.Application.Interfaces.Services
{
    public interface ITaskItemService
    {
        Task<TaskDetails> CreateTaskItem(TaskCreate taskCreate);
        Task<TaskDetails> ArchiveTaskItem(ArchiveTask archiveTask);
    }
}
