using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using library.Core.Common.Persistence;
using library.Core.Domain.Model;
using library.Web.Common.AutoMapper;
using library.Web.Models;
using MvcContrib.Pagination;
using Xunit;
using Ploeh.AutoFixture;

namespace library.Test.Common.AutoMapper
{
    public class AuthorAutoMapperTests
    {
        private readonly IFixture fixture;
        private readonly AutoMapperConfiguration autoMapperConfiguration;

        public AuthorAutoMapperTests()
        {
            fixture = new Fixture().Customize(new AutoFixtureCustomization());
            autoMapperConfiguration = new AutoMapperConfiguration();
            autoMapperConfiguration.Configure();
        }

        [Fact]
        public void ShouldMapAuthorToViewModel()
        {
            var author = fixture.CreateAnonymous<Author>();
            var viewModel = Mapper.Map<Author, AuthorViewModel>(author);

            Assert.Equal(author.Id, viewModel.Id);
            Assert.Equal(author.Name, viewModel.Name);
        }

        [Fact]
        public void ShouldMapViewModelToAuthor()
        {
            var viewModel = fixture.CreateAnonymous<AuthorViewModel>();
            var author = Mapper.Map<AuthorViewModel, Author>(viewModel);

            Assert.Equal(viewModel.Id, author.Id);
            Assert.Equal(viewModel.Name, author.Name);
        }

        [Fact]
        public void ShouldMapPageToPagination()
        {
            var page = new Page<Author>(new List<Author>{new Author(), new Author()}, 3, 10, 300);
            var pagination = Mapper.Map<Page<Author>, IPagination<AuthorViewModel>>(page);

            Assert.Equal(page.PageNumber, pagination.PageNumber);
            Assert.Equal(page.PageSize, pagination.PageSize);
            Assert.Equal(page.TotalItemCount, pagination.TotalItems);
            Assert.Equal(page.Items.Count(), pagination.Count());
        }
    }
}
