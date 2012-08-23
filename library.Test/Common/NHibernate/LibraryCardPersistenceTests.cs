using FluentNHibernate.Testing;
using Library.Core.Domain.Model;
using Ploeh.AutoFixture.Xunit;
using Xunit.Extensions;

namespace Library.Test.Common.NHibernate
{
    public class LibraryCardPersistenceTests : PersistenceTest
    {
        [Theory, AutoFixtureData]
        public void CanCorrectlyMapLibraryCard(LibraryCard libraryCard)
        {
            new PersistenceSpecification<LibraryCard>(UnitOfWorkSession)
                .VerifyTheMappings(libraryCard);
        }
    }
}