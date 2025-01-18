using System.Threading.Tasks;
using Workflow.API.Application.Models.Requests.Project;
using Workflow.API.Application.Models.Responses.Project;
using Workflow.API.Core.Collections;
using Workflow.API.Core.Entities;
using Workflow.API.Core.Entities.Aggregates;
using Workflow.API.Core.Interfaces.Collections;
using Workflow.API.Core.Models.Options;

namespace Workflow.API.Application.Mappers
{
    public static class ProjectMapper
    {
        public static Project FromProjectCreate(ProjectCreate project)
        {
            return new Project
            {
                Title = project.Title,
            };
        }
    }

    public static class ProjectDetailsMapper
    {
        public static ProjectDetails FromProject(Project project)
        {
            return new ProjectDetails
            {
                Id = project.Id,
                Title = project.Title,
            };
        }

        public static IPaginatedList<ProjectDetails> FromProjectPaginatedList(IPaginatedList<Project> projectPaginatedList)
        {
            return new PaginatedList<ProjectDetails>(
                projectPaginatedList.Data.Select((proj) => ProjectDetailsMapper.FromProject(proj)).ToList(),
                projectPaginatedList.TotalRecords,
                projectPaginatedList.PageNumber,
                projectPaginatedList.PageSize);
        }
    }
    
    public static class ProjectDetailsWithTaskMapper
    {
        public static ProjectDetailsAggregate FromProjectAndTasks(Project project, IEnumerable<TaskItemWithUser> tasks)
        {
            return new ProjectDetailsAggregate
            {
                Id = project.Id,
                Title = project.Title,
                Tasks = tasks.Select(task => TaskDetailsMapper.FromTaskWithUsers(task)).ToList(),
            };
        }
    }

    public static class ProjectSearchOptionsMapper
    {
        public static ProjectSearchOptions FromProjectSearch(ProjectSearch? projectSearchCriteria)
        {
            var pageNum = projectSearchCriteria?.PageNumber;
            var pageSize = projectSearchCriteria?.PageSize;

            return new ProjectSearchOptions
            {
                SkipRows = (pageNum - 1) * pageSize,
                FetchRows = pageSize
            };
        }
    }
}
