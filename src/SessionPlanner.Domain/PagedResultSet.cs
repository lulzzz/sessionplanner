using System.Collections.Generic;

namespace SessionPlanner.Domain
{
    public sealed class PagedResultSet<T> where T : class
    {
        public PagedResultSet(int pageIndex, int pageSize, long totalRecords, IEnumerable<T> records)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalRecords = totalRecords;
            Records = records;
        }

        public int PageIndex { get; }
        public int PageSize { get; }
        public long TotalRecords { get; }
        public IEnumerable<T> Records { get; }
    }
}