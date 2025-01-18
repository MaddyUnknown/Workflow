using Workflow.API.Core.Interfaces.Collections;

namespace Workflow.API.Core.Collections
{
    public class PaginatedList<T> : IPaginatedList<T>
    {
        private IEnumerable<T> _data;
        private int _pageNumber;
        private int _pageSize;
        private int _totalRecords;


        public static PaginatedList<T> Empty
        {
            get => new PaginatedList<T>(Enumerable.Empty<T>().ToList(), 0, 0, 0);
        }


        public PaginatedList(IEnumerable<T> data, int totalRecords, int pageNumber, int pageSize)
        {
            _data = data;
            _totalRecords = totalRecords;
            _pageNumber = pageNumber;
            _pageSize = pageSize;
        }


        public IEnumerable<T> Data => this._data;

        public int PageNumber => this._pageNumber;

        public int PageSize => this._pageSize;

        public int TotalRecords => this._totalRecords;
    }
}
