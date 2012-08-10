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
    public class BookAutoMapperTests
    {
        private readonly IFixture fixture;
        private readonly AutoMapperConfiguration autoMapperConfiguration;

        public BookAutoMapperTests()
        {
            fixture = new Fixture().Customize(new AutoFixtureCustomization());
            autoMapperConfiguration = new AutoMapperConfiguration();
            autoMapperConfiguration.Configure();
        }

        [Fact]
        public void ShouldMapBookToViewModel()
        {
            var book = fixture.CreateAnonymous<Book>();
            var viewModel = Mapper.Map<Book, BookViewModel>(book);

            Assert.Equal(book.Id, viewModel.Id);
            Assert.Equal(book.Title, viewModel.Title);
            Assert.Equal(book.ISBN, viewModel.ISBN);
        }

        [Fact]
        public void ShouldMapViewModelToBook()
        {
            var viewModel = fixture.CreateAnonymous<BookViewModel>();
            var book = Mapper.Map<BookViewModel, Book>(viewModel);

            Assert.Equal(viewModel.Id, book.Id);
            Assert.Equal(viewModel.Title, book.Title);
            Assert.Equal(viewModel.ISBN, book.ISBN);
        }

        [Fact]
        public void ShouldMapPageToPagination()
        {
            var page = new Page<Book>(new List<Book>{new Book(), new Book()}, 3, 10, 300);
            var pagination = Mapper.Map<Page<Book>, IPagination<BookViewModel>>(page);

            Assert.Equal(page.PageNumber, pagination.PageNumber);
            Assert.Equal(page.PageSize, pagination.PageSize);
            Assert.Equal(page.TotalItemCount, pagination.TotalItems);
            Assert.Equal(page.Items.Count(), pagination.Count());
        }
    }
}
