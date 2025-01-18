using Workflow.API.Application.Models.Requests.Task;
using Workflow.API.Application.Models.Responses.Task;
using Workflow.API.Core.Entities;
using Workflow.API.Core.Entities.Aggregates;

namespace Workflow.API.Application.Mappers
{
    public static class TaskItemMapper
    {
        public static TaskItem FromTaskCreate(TaskCreate task, long creatorUserId)
        {
            return new TaskItem
            {
                Title = task.Title,
                Description = task.Description,
                Deadline = task.Deadline,
                CreatorUserId = creatorUserId,
                AssignedUserId = task.AssignedUserId,
                Status = task.Status,
                ProjectId = task.ProjectId
            };
        }
    }

    public static class TaskDetailsMapper
    {
        public static TaskDetails FromTaskWithUsers(TaskItemWithUser task)
        {
            return new TaskDetails
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Deadline = task.Deadline,
                CreatorUser = task.CreatorUser == null ? null : TaskUserDetailsMapper.FromUser(task.CreatorUser),
                AssignedUser = task.AssignedUser == null ? null : TaskUserDetailsMapper.FromUser(task.AssignedUser),
                Status = task.Status
            };
        }

        public static TaskDetails FromTaskAndUsers(TaskItem task, User? creatorUser, User? assignedUser)
        {
            return new TaskDetails
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Deadline = task.Deadline,
                CreatorUser = creatorUser == null ? null : TaskUserDetailsMapper.FromUser(creatorUser),
                AssignedUser = assignedUser == null ? null : TaskUserDetailsMapper.FromUser(assignedUser),
                Status = task.Status
            };
        }
    }
}
