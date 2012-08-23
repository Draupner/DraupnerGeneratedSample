using System.Collections.Generic;
using Library.Core.Common.Persistence;
using Library.Core.Domain.Model;

namespace Library.Core.Domain.Repositories
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
