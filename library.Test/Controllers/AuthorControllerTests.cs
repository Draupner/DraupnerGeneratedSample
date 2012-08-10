using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using library.Core.Common.Persistence;
using library.Core.Domain.Model;
using library.Core.Domain.Repositories;
using library.Web.Common.AutoMapper;
using library.Web.Controllers;
using library.Web.Models;
using MvcContrib.Pagination;
using MvcContrib.Sorting;
using MvcContrib.UI.Grid;
using Xunit;
using Ploeh.AutoFixture;
using Rhino.Mocks;

namespace library.Test.Controllers
{
    public class AuthorControllerTests
    {
        private readonly MockRepository mocks;
        private readonly IAuthorRepository authorRepositoryMock;
        private readonly AuthorController controller;
        private readonly IFixture fixture;
        private readonly AutoMapperConfiguration autoMapperConfiguration;

        public AuthorControllerTests()
        {
            autoMapperConfiguration = new AutoMapperConfiguration();
            autoMapperConfiguration.Configure();
            fixture = new Fixture().Customize(new AutoFixtureCustomization());

            mocks = new MockRepository();
            authorRepositoryMock = mocks.DynamicMock<IAuthorRepository>();
            controller = new AuthorController(authorRepositoryMock);
        }

        [Fact]
        public void ShouldGetIndex()
        {
            var page = new Page<Author>(new List<Author> { new Author(), new Author() }, 5, 10, 100);
            Expect.Call(authorRepositoryMock.FindPage(5, 10, "Id", SortOrder.Ascending)).Return(page);

            mocks.ReplayAll();

            var sortOptions = new GridSortOptions { Column = "Id", Direction = SortDirection.Ascending };
            var result = controller.Index(5, sortOptions) as ViewResult;
            
            mocks.VerifyAll();

            Assert.NotNull(result);

            var pagination = (IPagination<AuthorViewModel>)result.Model;
            Assert.Equal(5, pagination.PageNumber);
            Assert.Equal(10, pagination.PageSize);
            Assert.Equal(100, pagination.TotalItems);
            Assert.Equal(2, pagination.Count());

            Assert.Equal(result.ViewData["sort"], sortOptions);
        }

        [Fact]
        public void ShouldGetDetails()
        {
            var author = fixture.CreateAnonymous<Author>();
            Expect.Call(authorRepositoryMock.Get(42)).Return(author);

            mocks.ReplayAll();

            var result = controller.Details(42);

            mocks.VerifyAll();

            Assert.Equal(author.Id, ((AuthorViewModel)result.Model).Id);
        }

        [Fact]
        public void ShouldGetDetailsWhereNotFound()
        {
            Expect.Call(authorRepositoryMock.Get(42)).Return(null);

            mocks.ReplayAll();

            var result = controller.Details(42);

            mocks.VerifyAll();

            Assert.True(result.ViewName.Contains("NotFound"));
        }

        [Fact]
        public void ShouldGetCreate()
        {
            mocks.ReplayAll();

            var result = controller.Create();

            mocks.VerifyAll();

            Assert.IsType(typeof(AuthorViewModel), result.Model);
        }

        [Fact]
        public void ShouldPostCreate()
        {
            var authorViewModel = new AuthorViewModel();

            Expect.Call(() => authorRepositoryMock.Add(Arg<Author>.Is.Anything));

            mocks.ReplayAll();
            var result = (RedirectToRouteResult)controller.Create(authorViewModel);
            mocks.VerifyAll();

            Assert.True(result.RouteValues.ContainsValue("Index"));
        }

        [Fact]
        public void ShouldPostCreateWhereNotValid()
        {
            var authorViewModel = new AuthorViewModel();
            controller.ModelState.AddModelError("key", "model is invalid");

            mocks.ReplayAll();
            var result = (ViewResult)controller.Create(authorViewModel);
            mocks.VerifyAll();

            Assert.Equal(authorViewModel, result.Model);
        }

        [Fact]
        public void ShouldGetEdit()
        {
            var author = new Author();
            Expect.Call(authorRepositoryMock.Get(12)).Return(author);

            mocks.ReplayAll();
            var result = controller.Edit(12);
            mocks.VerifyAll();

            Assert.IsType(typeof(AuthorViewModel), result.Model);
        }

        [Fact]
        public void ShouldGetEditWhereNotFound()
        {
            Expect.Call(authorRepositoryMock.Get(12)).Return(null);

            mocks.ReplayAll();
            var result = controller.Edit(12);
            mocks.VerifyAll();

            Assert.True(result.ViewName.Contains("NotFound"));
        }

        [Fact]
        public void ShouldPostEdit()
        {
            var authorViewModel = fixture.CreateAnonymous<AuthorViewModel>();
            var author = fixture.CreateAnonymous<Author>();

            Expect.Call(authorRepositoryMock.Get(authorViewModel.Id)).Return(author);

            mocks.ReplayAll();
            var result = (RedirectToRouteResult)controller.Edit(authorViewModel);
            mocks.VerifyAll();

            Assert.True(result.RouteValues.ContainsValue("Index"));
			
            Assert.Equal(authorViewModel.Name, author.Name);
        }

        [Fact]
        public void ShouldNotDoEditWhenNotValid()
        {
            var authorViewModel = fixture.CreateAnonymous<AuthorViewModel>();

            Expect.Call(authorRepositoryMock.Get(0)).IgnoreArguments().Repeat.Never();
            controller.ModelState.AddModelError("key", "model is invalid");

            mocks.ReplayAll();
            var result = (ViewResult)controller.Edit(authorViewModel);
            mocks.VerifyAll();

            Assert.Equal(authorViewModel, result.Model);
        }

        [Fact]
        public void ShouldNotDoEditWhenNotFound()
        {
            var authorViewModel = fixture.CreateAnonymous<AuthorViewModel>();

            Expect.Call(authorRepositoryMock.Get(authorViewModel.Id)).Return(null);

            mocks.ReplayAll();
            var result = (ViewResult)controller.Edit(authorViewModel);
            mocks.VerifyAll();

            Assert.True(result.ViewName.Contains("NotFound"));
        }

        [Fact]
        public void ShouldPostDelete()
        {
            var author = new Author();
            Expect.Call(authorRepositoryMock.Get(12)).Return(author);

            mocks.ReplayAll();
            var result = (RedirectToRouteResult)controller.Delete(12);
            mocks.VerifyAll();

            Assert.True(result.RouteValues.ContainsValue("Index"));
        }

        [Fact]
        public void ShouldPostDeleteWhereNotFound()
        {
            Expect.Call(authorRepositoryMock.Get(12)).Return(null);

            mocks.ReplayAll();
            var result = (ViewResult)controller.Delete(12);
            mocks.VerifyAll();

            Assert.True(result.ViewName.Contains("NotFound"));
        }
    }
}