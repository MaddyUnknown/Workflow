using Workflow.API.Application.Constants;
using Workflow.API.Application.Interfaces.Services;
using Workflow.API.Application.Mappers;
using Workflow.API.Application.Models.Requests.Project;
using Workflow.API.Application.Models.Responses.Project;
using Workflow.API.Application.Models.Responses.User;
using Workflow.API.Core.Enums;
using Workflow.API.Core.Exceptions;
using Workflow.API.Core.Interfaces.Collections;
using Workflow.API.Core.Interfaces.Repositories;
using Workflow.API.Core.Interfaces.Security.Accessors;
using Workflow.API.Core.Models.Options;

namespace Workflow.API.Application.Services
{
    public class ProjectService : IProjectService
    {
        private IProjectRepository _projectRepo;
        private ITaskRepository _taskRepo;
        private IUserRepository _userRepo;
        private IUserContextAccessor _userAccessor;


        public ProjectService(IProjectRepository projectRepo, ITaskRepository taskRepo, IUserRepository userRepo, IUserContextAccessor userAccessor)
        {
            _projectRepo = projectRepo;
            _taskRepo = taskRepo;
            _userRepo = userRepo;
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
            if (projectDAO == null) throw new ValidationException($"Project not found for ProjectId: '{projectId}'");

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

        public async Task<IEnumerable<ProjectMemberDetails>> AddProjectMember(ProjectMemberAdd addMember)
        {
            if (_userAccessor.User.LoginType == UserLoginType.Anonymous) throw new UserNotAuthenticatedException();
            if (!addMember.Validate(out IEnumerable<string> errors)) throw new ValidationException(errors);

            var userId = (_userAccessor.User.Id!).Value;

            // Only owner of project can add new members
            var hasAccess = await _projectRepo.HasProjectAccess(addMember.ProjectId, userId, new ProjectAccessSearchOptions { CheckIfOwner = true });
            if (!hasAccess) throw new ValidationException($"User doesn't have permission to add new members");

            var user = await _userRepo.Get(addMember.MemberUserId);
            if (user == null) new ValidationException($"User not found for user id: '{addMember.MemberUserId}'");

            var memberHasAccess = await _projectRepo.HasProjectAccess(addMember.ProjectId, addMember.MemberUserId);
            if (memberHasAccess) throw new ValidationException($"Member with user id: '{addMember.MemberUserId}' is already part to project");

            await _projectRepo.AddProjectMember(addMember.ProjectId, addMember.MemberUserId);
            var members = await _projectRepo.GetProjectMembersByProjectId(addMember.ProjectId);
            var membersDTO = members.Select(x => ProjectMemberDetailsMapper.FromProjectMember(x)).ToList();

            return membersDTO;
        }

        public async Task<IEnumerable<ProjectMemberDetails>> RemoveProjectMember(ProjectMemberRemove removeMember)
        {
            if (_userAccessor.User.LoginType == UserLoginType.Anonymous) throw new UserNotAuthenticatedException();
            if (!removeMember.Validate(out IEnumerable<string> errors)) throw new ValidationException(errors);

            var userId = (_userAccessor.User.Id!).Value;

            // Only owner of project can remove new members
            var hasAccess = await _projectRepo.HasProjectAccess(removeMember.ProjectId, userId, new ProjectAccessSearchOptions { CheckIfOwner = true });
            if (!hasAccess) throw new ValidationException($"User doesn't have permission to remove new members");
            if(userId == removeMember.MemberUserId) throw new ValidationException($"Cannot remove project owner");

            var user = await _userRepo.Get(removeMember.MemberUserId);
            if (user == null) new ValidationException($"User not found for user id: '{removeMember.MemberUserId}'");

            var memberHasAccess = await _projectRepo.HasProjectAccess(removeMember.ProjectId, removeMember.MemberUserId);
            if (!memberHasAccess) throw new ValidationException($"Member with user id: '{removeMember.MemberUserId}' is not part to project");

            await _projectRepo.RemoveProjectMember(removeMember.ProjectId, removeMember.MemberUserId);
            var members = await _projectRepo.GetProjectMembersByProjectId(removeMember.ProjectId);
            var membersDTO = members.Select(x => ProjectMemberDetailsMapper.FromProjectMember(x)).ToList();

            return membersDTO;
        }
    }
}
