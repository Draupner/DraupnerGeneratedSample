using FluentNHibernate.Testing;
using library.Core.Domain.Model;
using Ploeh.AutoFixture.Xunit;
using Xunit.Extensions;

namespace library.Test.Common.NHibernate
{
    public class AuthorPersistenceTests : PersistenceTest
    {
        [Theory, AutoFixtureData]
        public void CanCorrectlyMapAuthor(Author author)
        {
            new PersistenceSpecification<Author>(UnitOfWorkSession)
                .VerifyTheMappings(author);
        }
    }
}