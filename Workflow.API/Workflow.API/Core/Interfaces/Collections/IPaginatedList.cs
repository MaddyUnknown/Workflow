namespace Workflow.API.Core.Interfaces.Collections
{
    public interface IPaginatedList<T>
    {
        IEnumerable<T> Data { get; }
        int PageNumber { get; }
        int PageSize { get; }
        int TotalRecords { get; }
    }
}
