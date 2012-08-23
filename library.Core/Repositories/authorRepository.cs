using Library.Core.Common.Persistence;
using Library.Core.Common.Persistence.NHibernate;
using Library.Core.Domain.Model;
using Library.Core.Domain.Repositories;

namespace Library.Core.Repositories
{
    public class AuthorRepository : GenericNHibernateRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(IUnitOfWork unitOfWork) : base((NHibernateUnitOfWork) unitOfWork)
        {
        }
    }
}
