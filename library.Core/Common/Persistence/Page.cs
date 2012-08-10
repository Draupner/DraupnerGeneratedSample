using System.Collections.Generic;

namespace library.Core.Common.Persistence
{
    public class Page<T>
    {
        public Page(IEnumerable<T> items, int pageNumber, int pageSize, int totalItemCount)
        {
            Items = items;
            PageNumber = pageNumber;
            TotalItemCount = totalItemCount;
            PageSize = pageSize;
        }

        public IEnumerable<T> Items { get; protected set; }
        public int PageNumber { get; protected set; }
        public int TotalItemCount { get; protected set; }
        public int PageSize { get; protected set; }
    }
}