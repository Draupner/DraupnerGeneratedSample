using System.Collections.Generic;
using library.Core.Common.Persistence;
using library.Core.Domain.Model;

namespace library.Core.Domain.Repositories
{
    public interface ILibraryCardRepository
    {
        LibraryCard Get(long id);
        void Add(LibraryCard libraryCard);
        void Delete(LibraryCard libraryCard);
        IList<LibraryCard> FindAll();
        Page<LibraryCard> FindPage(int pageNumber, int pageSize, string sortColumn, SortOrder sortOrder);
        long CountAll();
    }
}
