using Dapper;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Workflow.API.Core.Collections;
using Workflow.API.Core.Entities;
using Workflow.API.Core.Entities.Aggregates;
using Workflow.API.Core.Interfaces.Collections;
using Workflow.API.Core.Interfaces.Repositories;
using Workflow.API.Core.Models.Options;
using Workflow.API.Infrastructure.DataAccess.Resources;

namespace Workflow.API.Infrastructure.DataAccess.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private string _connectionStr;

        public ProjectRepository(IConfiguration config)
        {
            _connectionStr = config.GetConnectionString("default");
        }

        public async Task<IPaginatedList<Project>> GetByUserId(long userId, ProjectSearchOptions? options)
        {
            var getProjectSql = (options?.SkipRows == null || options.FetchRows == null) ? ProjectQueries.GET_ALL_USER_PROJECTS : ProjectQueries.GET_ALL_USER_PROJECTS_PAGINATED;
            var sql = $"{ProjectQueries.GET_USER_PROJECT_COUNT};{getProjectSql}";

            using (var conn = new SqlConnection(_connectionStr))
            {
                using (var multi = await conn.QueryMultipleAsync(sql, new { UserId = userId, options?.SkipRows, options?.FetchRows }))
                {
                    int totalRecords = multi.ReadFirst<int>();
                    var projects = multi.Read<Project>();

                    var paginatedList = new PaginatedList<Project>(
                        projects,
                        totalRecords,
                        options?.SkipRows ?? 0,
                        options?.FetchRows ?? totalRecords
                    );

                    return paginatedList;
                }
            }
        }

        public async Task<Project?> GetByUserIdAndProjectId(long userId, long projectId)
        {
            using (var conn = new SqlConnection(_connectionStr))
            {
                Project? project = await conn.QueryFirstOrDefaultAsync<Project>(ProjectQueries.GET_BY_USER_ID_PRODUCT_ID, new { UserId = userId, ProjectId = projectId });
                return project;
            } 
        }

        public async Task<IEnumerable<ProjectMember>> GetProjectMembersByProjectId(long projectId)
        {
            using (var conn = new SqlConnection(_connectionStr))
            {
                IEnumerable<ProjectMember> members = await conn.QueryAsync<ProjectMember>(ProjectQueries.GET_ALL_MEMBER_BY_PROJECT_ID, new { ProjectId = projectId });
                return members;
            }
        }

        public async Task<bool> HasProjectAccess(long projectId, long userId, ProjectAccessSearchOptions? options = null)
        {
            using (var conn = new SqlConnection(_connectionStr))
            {
                var member = await conn.QueryFirstOrDefaultAsync<ProjectMember>(ProjectQueries.GET_MEMBER_BY_PROJECT_ID_USER_ID, new { ProjectId = projectId, UserId = userId });
                return (member == null || (options?.CheckIfOwner == true && member.IsOwner == false)) ? false : true;
            }
        }

        public async Task<Project> AddWithOwnerUserId(long ownerUserId, Project proj)
        {
            using (var conn = new SqlConnection(_connectionStr))
            {
                await conn.OpenAsync();

                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        long projectId = await conn.QuerySingleAsync<long>(ProjectQueries.ADD_PROJECT, proj, transaction);
                        await conn.ExecuteAsync(ProjectQueries.ADD_PROJECT_USER, new { ProjectId = projectId, UserId = ownerUserId, IsOwner = true }, transaction);

                        await transaction.CommitAsync();

                        proj.Id = projectId;
                        return proj;
                    }
                    catch
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            }
        }

        public async Task AddProjectMember(long projectId, long userId)
        {
            using (var conn = new SqlConnection(_connectionStr))
            {
                await conn.ExecuteAsync(ProjectQueries.ADD_PROJECT_USER, new { ProjectId = projectId, UserId = userId, IsOwner = false });
            }
        }

        public async Task RemoveProjectMember(long projectId, long userId)
        {
            using (var conn = new SqlConnection(_connectionStr))
            {
                await conn.ExecuteAsync(ProjectQueries.DELETE_PROJECT_USER, new { ProjectId = projectId, UserId = userId });
            }
        }
    }
}
