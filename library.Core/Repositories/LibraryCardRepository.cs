using Library.Core.Common.Persistence;
using Library.Core.Common.Persistence.NHibernate;
using Library.Core.Domain.Model;
using Library.Core.Domain.Repositories;

namespace Library.Core.Repositories
{
    public class LibraryCardRepository : GenericNHibernateRepository<LibraryCard>, ILibraryCardRepository
    {
        public LibraryCardRepository(IUnitOfWork unitOfWork) : base((NHibernateUnitOfWork) unitOfWork)
        {
        }
    }
}
