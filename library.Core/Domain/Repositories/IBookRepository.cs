using System.Collections.Generic;
using library.Core.Common.Persistence;
using library.Core.Domain.Model;

namespace library.Core.Domain.Repositories
{
    public interface IBookRepository
    {
        Book Get(long id);
        void Add(Book book);
        void Delete(Book book);
        IList<Book> FindAll();
        Page<Book> FindPage(int pageNumber, int pageSize, string sortColumn, SortOrder sortOrder);
        long CountAll();
    }
}
