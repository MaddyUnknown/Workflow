using Workflow.API.Application.Constants;
using Workflow.API.Application.Interfaces.Services;
using Workflow.API.Application.Mappers;
using Workflow.API.Application.Models.Requests.Project;
using Workflow.API.Application.Models.Responses.Project;
using Workflow.API.Core.Enums;
using Workflow.API.Core.Exceptions;
using Workflow.API.Core.Interfaces.Collections;
using Workflow.API.Core.Interfaces.Repositories;
using Workflow.API.Core.Interfaces.Security.Accessors;

namespace Workflow.API.Application.Services
{
    public class ProjectService : IProjectService
    {
        private IProjectRepository _projectRepo;
        private ITaskRepository _taskRepo;
        private IUserContextAccessor _userAccessor;


        public ProjectService(IProjectRepository projectRepo, ITaskRepository taskRepo, IUserContextAccessor userAccessor)
        {
            _projectRepo = projectRepo;
            _taskRepo = taskRepo;
            _userAccessor = userAccessor;
        }

        public async Task<IPaginatedList<ProjectDetails>> GetProjects(ProjectSearch? search = null)
        {
            if (_userAccessor.User.LoginType == UserLoginType.Anonymous) throw new UserNotAuthenticatedException();
            if (search != null && !search.Validate(out IEnumerable<string> errors)) throw new ValidationException(errors);


            var userId = (_userAccessor.User.Id!).Value;
            var searchOptions = ProjectSearchOptionsMapper.FromProjectSearch(search);

            var projects = await _projectRepo.GetByUserId(userId, searchOptions);
            var projectDetailsList = ProjectDetailsMapper.FromProjectPaginatedList(projects);

            return projectDetailsList;
        }

        public async Task<ProjectDetailsAggregate> GetProjectDetails(long projectId)
        {
            if (_userAccessor.User.LoginType == UserLoginType.Anonymous) throw new UserNotAuthenticatedException();
            if (projectId <= 0) throw new ValidationException(string.Format(ValidationConstants.GREATER_THAN, nameof(projectId), 0));


            var userId = (_userAccessor.User.Id!).Value;
            var projectDAO = await _projectRepo.GetByUserIdAndProjectId(userId, projectId);
            if (projectDAO == null) new ValidationException($"Project not found for ProjectId: '{projectId}'");

            var taskListDAO = await _taskRepo.GetByProjectId(projectDAO!.Id);

            return ProjectDetailsWithTaskMapper.FromProjectAndTasks(projectDAO, taskListDAO);
        }

        public async Task<ProjectDetails> CreateProject(ProjectCreate project)
        {
            if (_userAccessor.User.LoginType == UserLoginType.Anonymous) throw new UserNotAuthenticatedException();
            if (!project.Validate(out IEnumerable<string> errors)) throw new ValidationException(errors);


            var userId = (_userAccessor.User.Id!).Value;
            var projectDAO = ProjectMapper.FromProjectCreate(project);
            var addedProject = await _projectRepo.AddWithOwnerUserId(userId, projectDAO);
            var addedProjectDetails = ProjectDetailsMapper.FromProject(addedProject);

            return addedProjectDetails;
        }
    }
}
