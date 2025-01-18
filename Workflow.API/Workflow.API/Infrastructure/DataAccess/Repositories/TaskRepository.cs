using Dapper;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Workflow.API.Core.Entities;
using Workflow.API.Core.Entities.Aggregates;
using Workflow.API.Core.Interfaces.Repositories;
using Workflow.API.Infrastructure.DataAccess.Resources;

namespace Workflow.API.Infrastructure.DataAccess.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private string _connectionStr;

        public TaskRepository(IConfiguration config)
        {
            _connectionStr = config.GetConnectionString("default");
        }

        public async Task<TaskItem?> Get(long taskId)
        {
            using(var conn = new SqlConnection(_connectionStr))
            {
                var task = await conn.QueryFirstOrDefaultAsync<TaskItem>(TaskItemQueries.GET_BY_ID, new { TaskId = taskId });
                return task;
            }
        }

        public async Task<IEnumerable<TaskItemWithUser>> GetByProjectId(long projectId, bool includeArchiveTask = false)
        {
            using (var conn = new SqlConnection(_connectionStr))
            {
                var tasks = await conn.QueryAsync<TaskItemWithUser, User?, User?, TaskItemWithUser>(TaskItemQueries.GET_WITH_TASK_BY_PROJECT_ID, 
                    (task, creator, assignee) =>
                    {
                        if(creator != null)
                        {
                            task.CreatorUserId = creator.Id;
                            task.CreatorUser = creator;
                        }

                        if(assignee != null)
                        {
                            task.AssignedUserId = assignee.Id;
                            task.AssignedUser = assignee;
                        }

                        return task;
                    },
                    new { ProjectId = projectId, IncludeArchivedTask = includeArchiveTask },
                    splitOn: "Id,Id");
                return tasks;
            }
        }


        public async Task<TaskItem> Add(TaskItem task)
        {
            using (var conn = new SqlConnection(_connectionStr))
            {
                long id = await conn.QueryFirstAsync<long>(TaskItemQueries.ADD_TASK, task);
                task.Id = id;
                return task;
            }
        }

        public async Task<TaskItem> Update(TaskItem task)
        {
            using (var conn = new SqlConnection(_connectionStr))
            {
                var updatedTask = await conn.QueryFirstAsync<TaskItem>(TaskItemQueries.UPDATE_TASK, task);
                return updatedTask;
            }
        }

        public async Task<TaskItem?> DeleteIfExists(long taskId)
        {
            using (var conn = new SqlConnection(_connectionStr))
            {
                var deletedTask = await conn.QueryFirstOrDefaultAsync<TaskItem>(TaskItemQueries.DELETE_TASK, new { TaskId = taskId });
                return deletedTask;
            }
        }
    }
}
