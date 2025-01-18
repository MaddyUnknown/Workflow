using Workflow.API.Application.Models.Responses.Task;
using Workflow.API.Application.Models.Responses.User;

namespace Workflow.API.Application.Models.Responses.Project
{
    public class ProjectDetailsAggregate
    {
        public long Id { get; set; } = 0;
        public string Title { get; set; } = string.Empty;
        public List<TaskDetails> Tasks { get; set; } = Enumerable.Empty<TaskDetails>().ToList();
    }
}
