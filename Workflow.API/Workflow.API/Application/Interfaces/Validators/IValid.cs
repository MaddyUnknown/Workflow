namespace Workflow.API.Application.Interfaces.Validators
{
    public interface IValid
    {
        public bool Validate(out IEnumerable<string> errors);
    }
}
