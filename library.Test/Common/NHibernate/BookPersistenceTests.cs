using FluentNHibernate.Testing;
using library.Core.Domain.Model;
using Ploeh.AutoFixture.Xunit;
using Xunit.Extensions;

namespace library.Test.Common.NHibernate
{
    public class BookPersistenceTests : PersistenceTest
    {
        [Theory, AutoFixtureData]
        public void CanCorrectlyMapBook(Book book)
        {
            new PersistenceSpecification<Book>(UnitOfWorkSession)
                .VerifyTheMappings(book);
        }
    }
}