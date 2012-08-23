using System.Collections.Generic;
using Library.Core.Common.Persistence;
using Library.Core.Common.Persistence.NHibernate;

namespace Library.Test.Common.Transaction
{
    public interface IFooRepository
    {
        Foo Get(long id);
        void Add(Foo entity);
        void Delete(Foo entity);
        IList<Foo> FindAll();
        Page<Foo> FindPage(int pageNumber, int pageSize, string sortColumn, SortOrder sortOrder);
        long CountAll();
    }

    public class FooRepository : GenericNHibernateRepository<Foo>, IFooRepository
    {
        public FooRepository(IUnitOfWork unitOfWork) : base((NHibernateUnitOfWork) unitOfWork)
        {
        }
    }
}
