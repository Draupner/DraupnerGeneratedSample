using FluentNHibernate.Testing;
using library.Core.Domain.Model;
using Ploeh.AutoFixture.Xunit;
using Xunit.Extensions;

namespace library.Test.Common.NHibernate
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