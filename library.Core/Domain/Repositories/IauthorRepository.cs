using System.Collections.Generic;
using library.Core.Common.Persistence;
using library.Core.Domain.Model;

namespace library.Core.Domain.Repositories
{
    public interface IAuthorRepository
    {
        Author Get(long id);
        void Add(Author author);
        void Delete(Author author);
        IList<Author> FindAll();
        Page<Author> FindPage(int pageNumber, int pageSize, string sortColumn, SortOrder sortOrder);
        long CountAll();
    }
}
