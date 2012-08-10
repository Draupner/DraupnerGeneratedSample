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
    public class AuthorController : AbstractController
    {
        private const int PageSize = 10;

        private readonly IAuthorRepository authorRepository;

        public AuthorController(IAuthorRepository authorRepository)
        {
            this.authorRepository = authorRepository;
        }

		[Transaction]
        public virtual ActionResult Index(int? page, GridSortOptions sort)
        {
            var authors = authorRepository.FindPage(page ?? 1, PageSize, sort.Column, sort.Direction == SortDirection.Ascending ? SortOrder.Ascending: SortOrder.Descending);

            var authorViewModels = Map<Page<Author>, IPagination<AuthorViewModel>>(authors);
            ViewData["sort"] = sort;

            return View(authorViewModels);
        }

		[Transaction]
        public virtual ViewResult Details(long id)
        {
            var author = authorRepository.Get(id);

            if (author == null)
            {
                return ViewNotFound("No author with id: " + id);
            }

            AuthorViewModel authorViewModel = Map<Author, AuthorViewModel>(author);
            return View(authorViewModel);
        }

		[Transaction]
        public virtual ViewResult Create()
        {
            var authorViewModel = new AuthorViewModel();
            return View(authorViewModel);
        }

		[Transaction]
        [HttpPost]
        public virtual ActionResult Create(AuthorViewModel authorViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(authorViewModel);
            }

            var author = Map<AuthorViewModel, Author>(authorViewModel);
            authorRepository.Add(author);

            return RedirectToAction("Index");
        }

		[Transaction]
        public virtual ViewResult Edit(long id)
        {
            var author = authorRepository.Get(id);
            if (author == null)
            {
                return ViewNotFound("No author with id: " + id);
            }

            AuthorViewModel authorViewModel = Map<Author, AuthorViewModel>(author);
            return View(authorViewModel);
        }

		[Transaction]
        [HttpPost]
        public virtual ActionResult Edit(AuthorViewModel authorViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(authorViewModel);
            }

            var author = authorRepository.Get(authorViewModel.Id);
            if (author == null)
            {
                return ViewNotFound("No author with id: " + authorViewModel.Id);
            }

            Map(authorViewModel, author);

            return RedirectToAction("Index");
        }

        [Transaction]
		[HttpPost]
        public virtual ActionResult Delete(long id)
        {
            var author = authorRepository.Get(id);
            if (author == null)
            {
                return ViewNotFound("No author with id: " + id);
            }

            authorRepository.Delete(author);

            return RedirectToAction("Index");
        }
    }
}