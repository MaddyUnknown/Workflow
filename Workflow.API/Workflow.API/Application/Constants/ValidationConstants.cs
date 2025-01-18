namespace Workflow.API.Application.Constants
{
    public static class ValidationConstants
    {
        public readonly static string NOT_EMPTY = "'{0}' cannot be empty";
        
        public readonly static string NOT_EQUAL_TO = "'{0}' cannot be '{1}'";


        public readonly static string INVALID_PROPERTY = "'{1}' is not a valid '{0}'";

        public readonly static string GREATER_THAN = "'{0}' must be greater than '{1}'";

        public readonly static string NULL_OR_GREATER_THAN = "'{0}' must be null or greater than '{1}'";

        public readonly static string LENGTH_NOT_GREATER_THAN = "Length of '{0}' cannot exceed '{1}'";

        public readonly static string LENGTH_BETWEEN = "Length of '{0}' must be between {1} and {2}";
    }
}
