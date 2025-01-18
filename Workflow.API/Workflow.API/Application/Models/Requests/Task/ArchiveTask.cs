using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Workflow.API.Application.Constants;
using Workflow.API.Application.Interfaces.Validators;
using Workflow.API.Core.Entities;

namespace Workflow.API.Application.Models.Requests.Task
{
    public class ArchiveTask : IValid
    {
        public long TaskId { get; set; }

        public bool Validate(out IEnumerable<string> errors)
        {
            var errorList = new List<string>();

            if (TaskId <= 0)
            {
                errorList.Add(string.Format(ValidationConstants.GREATER_THAN, nameof(TaskId), 0));
            }

            errors = errorList;
            return errorList.Count == 0 ? true : false;
        }
    }
}
