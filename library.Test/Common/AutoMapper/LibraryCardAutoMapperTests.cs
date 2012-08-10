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
    public class LibraryCardAutoMapperTests
    {
        private readonly IFixture fixture;
        private readonly AutoMapperConfiguration autoMapperConfiguration;

        public LibraryCardAutoMapperTests()
        {
            fixture = new Fixture().Customize(new AutoFixtureCustomization());
            autoMapperConfiguration = new AutoMapperConfiguration();
            autoMapperConfiguration.Configure();
        }

        [Fact]
        public void ShouldMapLibraryCardToViewModel()
        {
            var libraryCard = fixture.CreateAnonymous<LibraryCard>();
            var viewModel = Mapper.Map<LibraryCard, LibraryCardViewModel>(libraryCard);

            Assert.Equal(libraryCard.Id, viewModel.Id);
            Assert.Equal(libraryCard.Number, viewModel.Number);
        }

        [Fact]
        public void ShouldMapViewModelToLibraryCard()
        {
            var viewModel = fixture.CreateAnonymous<LibraryCardViewModel>();
            var libraryCard = Mapper.Map<LibraryCardViewModel, LibraryCard>(viewModel);

            Assert.Equal(viewModel.Id, libraryCard.Id);
            Assert.Equal(viewModel.Number, libraryCard.Number);
        }

        [Fact]
        public void ShouldMapPageToPagination()
        {
            var page = new Page<LibraryCard>(new List<LibraryCard>{new LibraryCard(), new LibraryCard()}, 3, 10, 300);
            var pagination = Mapper.Map<Page<LibraryCard>, IPagination<LibraryCardViewModel>>(page);

            Assert.Equal(page.PageNumber, pagination.PageNumber);
            Assert.Equal(page.PageSize, pagination.PageSize);
            Assert.Equal(page.TotalItemCount, pagination.TotalItems);
            Assert.Equal(page.Items.Count(), pagination.Count());
        }
    }
}
