using Workflow.API.Application.Constants;
using Workflow.API.Application.Interfaces.Services;
using Workflow.API.Application.Mappers;
using Workflow.API.Application.Models.Requests.Task;
using Workflow.API.Application.Models.Responses.Task;
using Workflow.API.Core.Entities;
using Workflow.API.Core.Enums;
using Workflow.API.Core.Exceptions;
using Workflow.API.Core.Interfaces.Repositories;
using Workflow.API.Core.Interfaces.Security.Accessors;

namespace Workflow.API.Application.Services
{
    public class TaskItemService : ITaskItemService
    {
        private ITaskRepository _taskRepo;
        private IProjectRepository _projectRepo;
        private IUserRepository _userRepo;
        private IUserContextAccessor _userAccessor;


        public TaskItemService(ITaskRepository taskRepo, IProjectRepository projectRepository, IUserRepository userRepo, IUserContextAccessor userAccessor)
        {
            _taskRepo = taskRepo;
            _projectRepo = projectRepository;
            _userRepo = userRepo;
            _userAccessor = userAccessor;
        }


        public async Task<TaskDetails> CreateTaskItem(TaskCreate taskCreate)
        {
            if (_userAccessor.User.LoginType == UserLoginType.Anonymous) throw new UserNotAuthenticatedException();
            if (!taskCreate.Validate(out IEnumerable<string> errors)) throw new ValidationException(errors);

            var userId = (_userAccessor.User.Id!).Value;

            //Check if project exists
            var project = await _projectRepo.GetByUserIdAndProjectId(userId, taskCreate.ProjectId);
            if (project == null) throw new ValidationException($"Project not found for project id: {taskCreate.ProjectId}");


            // Check if assigned user are part of the project
            if (taskCreate.AssignedUserId != null && !await _projectRepo.HasProjectAccess(taskCreate.ProjectId, taskCreate.AssignedUserId.Value)) 
                throw new ValidationException($"User '{taskCreate.AssignedUserId}' doesn't have access to project id: {taskCreate.ProjectId}");


            var creatorUser = await _userRepo.Get(userId);
            var assignedUser = (taskCreate.AssignedUserId == null) ? null : await _userRepo.Get(taskCreate.AssignedUserId.Value);

            var taskDAO = TaskItemMapper.FromTaskCreate(taskCreate, userId);
            var taskAdded = await _taskRepo.Add(taskDAO);

            var taskDetails = TaskDetailsMapper.FromTaskAndUsers(taskAdded, creatorUser, assignedUser);

            return taskDetails;
        }

        public async Task<TaskDetails> ArchiveTaskItem(ArchiveTask archiveTask)
        {
            if (_userAccessor.User.LoginType == UserLoginType.Anonymous) throw new UserNotAuthenticatedException();

            var userId = (_userAccessor.User.Id!).Value;

            TaskItem? task = await _taskRepo.Get(archiveTask.TaskId);
            if (task == null || ! await _projectRepo.HasProjectAccess(task.ProjectId, userId)) throw new ValidationException($"Task item not found for task id: {archiveTask.TaskId}");

            task.Status = TaskItemStatus.Archive;
            TaskItem updatedTask = await _taskRepo.Update(task);

            var creatorUser = await _userRepo.Get(task.CreatorUserId);
            var assignedUser = (task.AssignedUserId == null) ? null : await _userRepo.Get(task.AssignedUserId.Value);

            var taskDetails = TaskDetailsMapper.FromTaskAndUsers(updatedTask, creatorUser, assignedUser);
            return taskDetails;
        }
    }
}
