using System.Collections.Generic;
using Library.Core.Common.Persistence;
using Library.Core.Domain.Model;

namespace Library.Core.Domain.Repositories
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
