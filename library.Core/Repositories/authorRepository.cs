using library.Core.Common.Persistence;
using library.Core.Common.Persistence.NHibernate;
using library.Core.Domain.Model;
using library.Core.Domain.Repositories;

namespace library.Core.Repositories
{
    public class AuthorRepository : GenericNHibernateRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(IUnitOfWork unitOfWork) : base((NHibernateUnitOfWork) unitOfWork)
        {
        }
    }
}
