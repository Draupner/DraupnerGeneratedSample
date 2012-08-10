using System.Web.Mvc;
using library.Core.Common.Persistence;
using library.Core.Common.Transactions;
using library.Core.Domain.Model;
using library.Core.Domain.Repositories;
using library.Web.Models;
using MvcContrib.Pagination;
using MvcContrib.Sorting;
using MvcContrib.UI.Grid;

namespace library.Web.Controllers
{
    public class LibraryCardController : AbstractController
    {
        private const int PageSize = 10;

        private readonly ILibraryCardRepository libraryCardRepository;

        public LibraryCardController(ILibraryCardRepository libraryCardRepository)
        {
            this.libraryCardRepository = libraryCardRepository;
        }

		[Transaction]
        public virtual ActionResult Index(int? page, GridSortOptions sort)
        {
            var libraryCards = libraryCardRepository.FindPage(page ?? 1, PageSize, sort.Column, sort.Direction == SortDirection.Ascending ? SortOrder.Ascending: SortOrder.Descending);

            var libraryCardViewModels = Map<Page<LibraryCard>, IPagination<LibraryCardViewModel>>(libraryCards);
            ViewData["sort"] = sort;

            return View(libraryCardViewModels);
        }

		[Transaction]
        public virtual ViewResult Details(long id)
        {
            var libraryCard = libraryCardRepository.Get(id);

            if (libraryCard == null)
            {
                return ViewNotFound("No libraryCard with id: " + id);
            }

            LibraryCardViewModel libraryCardViewModel = Map<LibraryCard, LibraryCardViewModel>(libraryCard);
            return View(libraryCardViewModel);
        }

		[Transaction]
        public virtual ViewResult Create()
        {
            var libraryCardViewModel = new LibraryCardViewModel();
            return View(libraryCardViewModel);
        }

		[Transaction]
        [HttpPost]
        public virtual ActionResult Create(LibraryCardViewModel libraryCardViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(libraryCardViewModel);
            }

            var libraryCard = Map<LibraryCardViewModel, LibraryCard>(libraryCardViewModel);
            libraryCardRepository.Add(libraryCard);

            return RedirectToAction("Index");
        }

		[Transaction]
        public virtual ViewResult Edit(long id)
        {
            var libraryCard = libraryCardRepository.Get(id);
            if (libraryCard == null)
            {
                return ViewNotFound("No libraryCard with id: " + id);
            }

            LibraryCardViewModel libraryCardViewModel = Map<LibraryCard, LibraryCardViewModel>(libraryCard);
            return View(libraryCardViewModel);
        }

		[Transaction]
        [HttpPost]
        public virtual ActionResult Edit(LibraryCardViewModel libraryCardViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(libraryCardViewModel);
            }

            var libraryCard = libraryCardRepository.Get(libraryCardViewModel.Id);
            if (libraryCard == null)
            {
                return ViewNotFound("No libraryCard with id: " + libraryCardViewModel.Id);
            }

            Map(libraryCardViewModel, libraryCard);

            return RedirectToAction("Index");
        }

        [Transaction]
		[HttpPost]
        public virtual ActionResult Delete(long id)
        {
            var libraryCard = libraryCardRepository.Get(id);
            if (libraryCard == null)
            {
                return ViewNotFound("No libraryCard with id: " + id);
            }

            libraryCardRepository.Delete(libraryCard);

            return RedirectToAction("Index");
        }
    }
}