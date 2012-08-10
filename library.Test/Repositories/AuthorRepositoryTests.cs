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
    public class AuthorRepositoryTests : PersistenceTest
    {
        private readonly IAuthorRepository repository;
        private readonly IFixture fixture;

        public AuthorRepositoryTests()
        {
            repository = new AuthorRepository(UnitOfWork);
            fixture = new Fixture().Customize(new AutoFixtureCustomization());
        }

        [Fact]
        public void ShouldAdd()
        {
            var author = fixture.CreateAnonymous<Author>();

            repository.Add(author);
            Assert.NotNull(author.Id);

            using (var session = CreateSession())
            {
                int count = session.QueryOver<Author>().RowCount();
                Assert.Equal(1, count);

                Author persistedAuthor = session.QueryOver<Author>().List().FirstOrDefault();
                Assert.Equal(author.Id, persistedAuthor.Id);
                Assert.Equal(author.Name, persistedAuthor.Name);
            }
        }

        [Fact]
        public void ShouldGet()
        {
            Author persistedAuthor;
            using (var session = CreateSession())
            {
                persistedAuthor = fixture.CreateAnonymous<Author>();
                session.Save(persistedAuthor);
                session.Flush();
            }

            var author = repository.Get(persistedAuthor.Id);
            Assert.NotNull(author);
            Assert.Equal(author.Id, persistedAuthor.Id);
            Assert.Equal(author.Name, persistedAuthor.Name);
        }

        [Fact]
        public void ShouldDelete()
        {
            var id = CreateAndPersist(1).FirstOrDefault();

            var author = repository.Get(id);
            repository.Delete(author);
            UnitOfWork.SaveChanges();

            using (var session = CreateSession())
            {
                var count = session.QueryOver<Author>().RowCount();
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
                    var author = fixture.CreateAnonymous<Author>();
                    session.Save(author);
                    session.Flush();
                    ids.Add(author.Id); 
                }
            }
            return ids;
        }
    }
}
