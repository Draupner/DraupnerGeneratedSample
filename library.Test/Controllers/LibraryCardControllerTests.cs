using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Library.Core.Common.Persistence;
using Library.Core.Domain.Model;
using Library.Core.Domain.Repositories;
using Library.Web.Common.AutoMapper;
using Library.Web.Controllers;
using Library.Web.Models;
using MvcContrib.Pagination;
using MvcContrib.Sorting;
using MvcContrib.UI.Grid;
using Xunit;
using Ploeh.AutoFixture;
using Rhino.Mocks;

namespace Library.Test.Controllers
{
    public class LibraryCardControllerTests
    {
        private readonly MockRepository mocks;
        private readonly ILibraryCardRepository libraryCardRepositoryMock;
        private readonly LibraryCardController controller;
        private readonly IFixture fixture;
        private readonly AutoMapperConfiguration autoMapperConfiguration;

        public LibraryCardControllerTests()
        {
            autoMapperConfiguration = new AutoMapperConfiguration();
            autoMapperConfiguration.Configure();
            fixture = new Fixture().Customize(new AutoFixtureCustomization());

            mocks = new MockRepository();
            libraryCardRepositoryMock = mocks.DynamicMock<ILibraryCardRepository>();
            controller = new LibraryCardController(libraryCardRepositoryMock);
        }

        [Fact]
        public void ShouldGetIndex()
        {
            var page = new Page<LibraryCard>(new List<LibraryCard> { new LibraryCard(), new LibraryCard() }, 5, 10, 100);
            Expect.Call(libraryCardRepositoryMock.FindPage(5, 10, "Id", SortOrder.Ascending)).Return(page);

            mocks.ReplayAll();

            var sortOptions = new GridSortOptions { Column = "Id", Direction = SortDirection.Ascending };
            var result = controller.Index(5, sortOptions) as ViewResult;
            
            mocks.VerifyAll();

            Assert.NotNull(result);

            var pagination = (IPagination<LibraryCardViewModel>)result.Model;
            Assert.Equal(5, pagination.PageNumber);
            Assert.Equal(10, pagination.PageSize);
            Assert.Equal(100, pagination.TotalItems);
            Assert.Equal(2, pagination.Count());

            Assert.Equal(result.ViewData["sort"], sortOptions);
        }

        [Fact]
        public void ShouldGetDetails()
        {
            var libraryCard = fixture.CreateAnonymous<LibraryCard>();
            Expect.Call(libraryCardRepositoryMock.Get(42)).Return(libraryCard);

            mocks.ReplayAll();

            var result = controller.Details(42);

            mocks.VerifyAll();

            Assert.Equal(libraryCard.Id, ((LibraryCardViewModel)result.Model).Id);
        }

        [Fact]
        public void ShouldGetDetailsWhereNotFound()
        {
            Expect.Call(libraryCardRepositoryMock.Get(42)).Return(null);

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

            Assert.IsType(typeof(LibraryCardViewModel), result.Model);
        }

        [Fact]
        public void ShouldPostCreate()
        {
            var libraryCardViewModel = new LibraryCardViewModel();

            Expect.Call(() => libraryCardRepositoryMock.Add(Arg<LibraryCard>.Is.Anything));

            mocks.ReplayAll();
            var result = (RedirectToRouteResult)controller.Create(libraryCardViewModel);
            mocks.VerifyAll();

            Assert.True(result.RouteValues.ContainsValue("Index"));
        }

        [Fact]
        public void ShouldPostCreateWhereNotValid()
        {
            var libraryCardViewModel = new LibraryCardViewModel();
            controller.ModelState.AddModelError("key", "model is invalid");

            mocks.ReplayAll();
            var result = (ViewResult)controller.Create(libraryCardViewModel);
            mocks.VerifyAll();

            Assert.Equal(libraryCardViewModel, result.Model);
        }

        [Fact]
        public void ShouldGetEdit()
        {
            var libraryCard = new LibraryCard();
            Expect.Call(libraryCardRepositoryMock.Get(12)).Return(libraryCard);

            mocks.ReplayAll();
            var result = controller.Edit(12);
            mocks.VerifyAll();

            Assert.IsType(typeof(LibraryCardViewModel), result.Model);
        }

        [Fact]
        public void ShouldGetEditWhereNotFound()
        {
            Expect.Call(libraryCardRepositoryMock.Get(12)).Return(null);

            mocks.ReplayAll();
            var result = controller.Edit(12);
            mocks.VerifyAll();

            Assert.True(result.ViewName.Contains("NotFound"));
        }

        [Fact]
        public void ShouldPostEdit()
        {
            var libraryCardViewModel = fixture.CreateAnonymous<LibraryCardViewModel>();
            var libraryCard = fixture.CreateAnonymous<LibraryCard>();

            Expect.Call(libraryCardRepositoryMock.Get(libraryCardViewModel.Id)).Return(libraryCard);

            mocks.ReplayAll();
            var result = (RedirectToRouteResult)controller.Edit(libraryCardViewModel);
            mocks.VerifyAll();

            Assert.True(result.RouteValues.ContainsValue("Index"));
			
            Assert.Equal(libraryCardViewModel.Number, libraryCard.Number);
        }

        [Fact]
        public void ShouldNotDoEditWhenNotValid()
        {
            var libraryCardViewModel = fixture.CreateAnonymous<LibraryCardViewModel>();

            Expect.Call(libraryCardRepositoryMock.Get(0)).IgnoreArguments().Repeat.Never();
            controller.ModelState.AddModelError("key", "model is invalid");

            mocks.ReplayAll();
            var result = (ViewResult)controller.Edit(libraryCardViewModel);
            mocks.VerifyAll();

            Assert.Equal(libraryCardViewModel, result.Model);
        }

        [Fact]
        public void ShouldNotDoEditWhenNotFound()
        {
            var libraryCardViewModel = fixture.CreateAnonymous<LibraryCardViewModel>();

            Expect.Call(libraryCardRepositoryMock.Get(libraryCardViewModel.Id)).Return(null);

            mocks.ReplayAll();
            var result = (ViewResult)controller.Edit(libraryCardViewModel);
            mocks.VerifyAll();

            Assert.True(result.ViewName.Contains("NotFound"));
        }

        [Fact]
        public void ShouldPostDelete()
        {
            var libraryCard = new LibraryCard();
            Expect.Call(libraryCardRepositoryMock.Get(12)).Return(libraryCard);

            mocks.ReplayAll();
            var result = (RedirectToRouteResult)controller.Delete(12);
            mocks.VerifyAll();

            Assert.True(result.RouteValues.ContainsValue("Index"));
        }

        [Fact]
        public void ShouldPostDeleteWhereNotFound()
        {
            Expect.Call(libraryCardRepositoryMock.Get(12)).Return(null);

            mocks.ReplayAll();
            var result = (ViewResult)controller.Delete(12);
            mocks.VerifyAll();

            Assert.True(result.ViewName.Contains("NotFound"));
        }
    }
}