using System.Text.RegularExpressions;

namespace Workflow.API.Application.Utils
{
    public static class ValidatorUtil
    {
        private static Regex USERNAME_REGEX = new Regex(@"^[a-zA-Z0-9_-]{5,20}$");
        private static Regex EMAIL_REGEX = new Regex(@"^[A-Za-z0-9._%-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}$");
        private static Regex PASSWORD_REGEX = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,20}$");
        private static Regex PROJECT_CODE_REGEX = new Regex(@"^[a-zA-Z0-9]{5,10}$");

        public static bool IsValidEmail(string email)
        {
            return EMAIL_REGEX.IsMatch(email);
        }

        public static bool IsValidPassword(string password)
        {
            return PASSWORD_REGEX.IsMatch(password);
        }

        public static bool IsValidUsername(string username)
        {
            return USERNAME_REGEX.IsMatch(username);
        }

        public static bool IsValidProjectCode(string projectCode)
        {
            return PROJECT_CODE_REGEX.IsMatch(projectCode);
        }
    }
}
