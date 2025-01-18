using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workflow.API.Application.Interfaces.Services;
using Workflow.API.Application.Models.Requests.Task;
using Workflow.API.Application.Models.Responses.Task;

namespace Workflow.API.Infrastructure.Web.Controllers
{
    [Authorize]
    [Route("api/task/")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private ITaskItemService _taskItemService;


        public TaskController(ITaskItemService taskItemService)
        {
            _taskItemService = taskItemService;
        }


        [HttpPost("create")]
        public async Task<ActionResult<TaskDetails>> CreateProject(TaskCreate task)
        {
            return Ok(await _taskItemService.CreateTaskItem(task));
        }

        [HttpPost("archive")]
        public async Task<ActionResult<TaskDetails>> ArchiveProject([FromBody] ArchiveTask task)
        {
            return Ok(await _taskItemService.ArchiveTaskItem(task));
        }
    }
}
