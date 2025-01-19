using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workflow.API.Application.Interfaces.Services;
using Workflow.API.Application.Models.Requests.Project;
using Workflow.API.Application.Models.Responses.Project;
using Workflow.API.Application.Models.Responses.User;
using Workflow.API.Core.Interfaces.Collections;

namespace Workflow.API.Infrastructure.Web.Controllers
{
    [Authorize]
    [Route("api/project/")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private IProjectService _projectService;


        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }


        [HttpGet("all")]
        public async Task<ActionResult<IPaginatedList<ProjectDetails>>> GetPaginatedProjects(int pageSize, int pageNumber)
        {
            ProjectSearch search = new ProjectSearch { PageNumber = pageNumber, PageSize = pageSize };
            return Ok(await _projectService.GetProjects(search));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDetailsAggregate>> GetProjectDetails(long id)
        {
            return Ok(await _projectService.GetProjectDetails(id));
        }

        [HttpPost("create")]
        public async Task<ActionResult<ProjectDetails>> CreateProject(ProjectCreate project)
        {
            return Ok(await _projectService.CreateProject(project));
        }

        [HttpPost("{id}/add-member")]
        public async Task<ActionResult<IEnumerable<ProjectMemberDetails>>> AddProjectMember(long id, ProjectMemberAdd addProjectUser)
        {
            addProjectUser.ProjectId = id;
            return Ok(await _projectService.AddProjectMember(addProjectUser));
        }

        [HttpPost("{id}/remove-member")]
        public async Task<ActionResult<IEnumerable<ProjectMemberDetails>>> RemoveProjectMember(long id, ProjectMemberRemove removeProjectUser)
        {
            removeProjectUser.ProjectId = id;
            return Ok(await _projectService.RemoveProjectMember(removeProjectUser));
        }
    }
}
