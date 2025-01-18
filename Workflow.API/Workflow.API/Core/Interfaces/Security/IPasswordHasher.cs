namespace Workflow.API.Core.Interfaces.Security
{
    public interface IPasswordHasher
    {
        public string GenerateHashedPassword(string password);
        public bool ValidateHashedPassword(string password, string hashedPassword);
    }
}
