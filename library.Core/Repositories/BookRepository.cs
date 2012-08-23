using Library.Core.Common.Persistence;
using Library.Core.Common.Persistence.NHibernate;
using Library.Core.Domain.Model;
using Library.Core.Domain.Repositories;

namespace Library.Core.Repositories
{
    public class BookRepository : GenericNHibernateRepository<Book>, IBookRepository
    {
        public BookRepository(IUnitOfWork unitOfWork) : base((NHibernateUnitOfWork) unitOfWork)
        {
        }
    }
}
