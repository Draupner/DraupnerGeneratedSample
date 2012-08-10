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
    public class BookController : AbstractController
    {
        private const int PageSize = 10;

        private readonly IBookRepository bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

		[Transaction]
        public virtual ActionResult Index(int? page, GridSortOptions sort)
        {
            var books = bookRepository.FindPage(page ?? 1, PageSize, sort.Column, sort.Direction == SortDirection.Ascending ? SortOrder.Ascending: SortOrder.Descending);

            var bookViewModels = Map<Page<Book>, IPagination<BookViewModel>>(books);
            ViewData["sort"] = sort;

            return View(bookViewModels);
        }

		[Transaction]
        public virtual ViewResult Details(long id)
        {
            var book = bookRepository.Get(id);

            if (book == null)
            {
                return ViewNotFound("No book with id: " + id);
            }

            BookViewModel bookViewModel = Map<Book, BookViewModel>(book);
            return View(bookViewModel);
        }

		[Transaction]
        public virtual ViewResult Create()
        {
            var bookViewModel = new BookViewModel();
            return View(bookViewModel);
        }

		[Transaction]
        [HttpPost]
        public virtual ActionResult Create(BookViewModel bookViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(bookViewModel);
            }

            var book = Map<BookViewModel, Book>(bookViewModel);
            bookRepository.Add(book);

            return RedirectToAction("Index");
        }

		[Transaction]
        public virtual ViewResult Edit(long id)
        {
            var book = bookRepository.Get(id);
            if (book == null)
            {
                return ViewNotFound("No book with id: " + id);
            }

            BookViewModel bookViewModel = Map<Book, BookViewModel>(book);
            return View(bookViewModel);
        }

		[Transaction]
        [HttpPost]
        public virtual ActionResult Edit(BookViewModel bookViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(bookViewModel);
            }

            var book = bookRepository.Get(bookViewModel.Id);
            if (book == null)
            {
                return ViewNotFound("No book with id: " + bookViewModel.Id);
            }

            Map(bookViewModel, book);

            return RedirectToAction("Index");
        }

        [Transaction]
		[HttpPost]
        public virtual ActionResult Delete(long id)
        {
            var book = bookRepository.Get(id);
            if (book == null)
            {
                return ViewNotFound("No book with id: " + id);
            }

            bookRepository.Delete(book);

            return RedirectToAction("Index");
        }
    }
}