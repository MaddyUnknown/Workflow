using Workflow.API.Core.Interfaces.Collections;
using Workflow.API.Application.Models.Requests.Project;
using Workflow.API.Application.Models.Responses.Project;
using Workflow.API.Application.Models.Responses.User;

namespace Workflow.API.Application.Interfaces.Services
{
    public interface IProjectService
    {
        Task<IPaginatedList<ProjectDetails>> GetProjects(ProjectSearch search);
        Task<ProjectDetailsAggregate> GetProjectDetails(long projectId);
        Task<ProjectDetails> CreateProject(ProjectCreate project);
    }
}
