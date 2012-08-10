using System.Collections.Generic;
using System.Linq;
using library.Core.Common.Persistence;
using library.Core.Domain.Model;
using library.Core.Domain.Repositories;
using library.Core.Repositories;
using Xunit;
using Ploeh.AutoFixture;

namespace library.Test.Repositories
{
    public class LibraryCardRepositoryTests : PersistenceTest
    {
        private readonly ILibraryCardRepository repository;
        private readonly IFixture fixture;

        public LibraryCardRepositoryTests()
        {
            repository = new LibraryCardRepository(UnitOfWork);
            fixture = new Fixture().Customize(new AutoFixtureCustomization());
        }

        [Fact]
        public void ShouldAdd()
        {
            var libraryCard = fixture.CreateAnonymous<LibraryCard>();

            repository.Add(libraryCard);
            Assert.NotNull(libraryCard.Id);

            using (var session = CreateSession())
            {
                int count = session.QueryOver<LibraryCard>().RowCount();
                Assert.Equal(1, count);

                LibraryCard persistedLibraryCard = session.QueryOver<LibraryCard>().List().FirstOrDefault();
                Assert.Equal(libraryCard.Id, persistedLibraryCard.Id);
                Assert.Equal(libraryCard.Number, persistedLibraryCard.Number);
            }
        }

        [Fact]
        public void ShouldGet()
        {
            LibraryCard persistedLibraryCard;
            using (var session = CreateSession())
            {
                persistedLibraryCard = fixture.CreateAnonymous<LibraryCard>();
                session.Save(persistedLibraryCard);
                session.Flush();
            }

            var libraryCard = repository.Get(persistedLibraryCard.Id);
            Assert.NotNull(libraryCard);
            Assert.Equal(libraryCard.Id, persistedLibraryCard.Id);
            Assert.Equal(libraryCard.Number, persistedLibraryCard.Number);
        }

        [Fact]
        public void ShouldDelete()
        {
            var id = CreateAndPersist(1).FirstOrDefault();

            var libraryCard = repository.Get(id);
            repository.Delete(libraryCard);
            UnitOfWork.SaveChanges();

            using (var session = CreateSession())
            {
                var count = session.QueryOver<LibraryCard>().RowCount();
                Assert.Equal(0, count);
            }
        }

        [Fact]
        public void ShouldCountAll()
        {
            CreateAndPersist(4);

            var count = repository.CountAll();
            Assert.Equal(4, count);
        }

        [Fact]
        public void ShouldGetAll()
        {
            CreateAndPersist(3);

            var all = repository.FindAll();
            Assert.Equal(3, all.Count);
        }

        [Fact]
        public void ShouldGetAllPagedSortedAscending()
        {
            var ids = CreateAndPersist(5);

            var page1 = repository.FindPage(1, 2, "Id", SortOrder.Ascending);
            Assert.Equal(2, page1.Items.Count());
            Assert.Equal(5, page1.TotalItemCount);

            var page2 = repository.FindPage(2, 2, "Id", SortOrder.Ascending);
            Assert.Equal(2, page2.Items.Count());
            Assert.Equal(5, page2.TotalItemCount);

            var page3 = repository.FindPage(3, 2, "Id", SortOrder.Ascending);
            Assert.Equal(1, page3.Items.Count());
            Assert.Equal(5, page3.TotalItemCount);

            var allIds = page1.Items.Union(page2.Items).Union(page3.Items).Select(u => u.Id).ToList();

            ids.Sort();
            Assert.Equal(ids, allIds);
        }

        [Fact]
        public void ShouldGetAllPagedSortedDescinding()
        {
            var ids = CreateAndPersist(5);

            var page1 = repository.FindPage(1, 2, "Id", SortOrder.Descending);
            Assert.Equal(2, page1.Items.Count());
            Assert.Equal(5, page1.TotalItemCount);

            var page2 = repository.FindPage(2, 2, "Id", SortOrder.Descending);
            Assert.Equal(2, page2.Items.Count());
            Assert.Equal(5, page2.TotalItemCount);

            var page3 = repository.FindPage(3, 2, "Id", SortOrder.Descending);
            Assert.Equal(1, page3.Items.Count());
            Assert.Equal(5, page3.TotalItemCount);

            var allIds = page1.Items.Union(page2.Items).Union(page3.Items).Select(u => u.Id).ToList();

            ids.Sort((x,y) => y.CompareTo(x));
            Assert.Equal(ids, allIds);
        }

        private List<long> CreateAndPersist(int count)
        {
            var ids = new List<long>();
            using (var session = CreateSession())
            {
                for (int i = 0; i < count; i++)
                {
                    var libraryCard = fixture.CreateAnonymous<LibraryCard>();
                    session.Save(libraryCard);
                    session.Flush();
                    ids.Add(libraryCard.Id); 
                }
            }
            return ids;
        }
    }
}
