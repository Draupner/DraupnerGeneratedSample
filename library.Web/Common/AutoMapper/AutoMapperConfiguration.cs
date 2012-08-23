using Library.Web.Models;
using Library.Core.Domain.Model;
using System.Collections.Generic;
using AutoMapper;
using MvcContrib.Pagination;
using Library.Core.Common.Persistence;

namespace Library.Web.Common.AutoMapper
{
    public class AutoMapperConfiguration
    {
        public void Configure()
        {
            Mapper.CreateMap<Page<LibraryCard>, IPagination<LibraryCardViewModel>>().ConvertUsing(x => new CustomPagination<LibraryCardViewModel>(Mapper.Map<IEnumerable<LibraryCard>, IEnumerable<LibraryCardViewModel>>(x.Items), x.PageNumber, x.PageSize, x.TotalItemCount));
            Mapper.CreateMap<LibraryCardViewModel, LibraryCard>();
            Mapper.CreateMap<LibraryCard, LibraryCardViewModel>();
            Mapper.CreateMap<Page<Author>, IPagination<AuthorViewModel>>().ConvertUsing(x => new CustomPagination<AuthorViewModel>(Mapper.Map<IEnumerable<Author>, IEnumerable<AuthorViewModel>>(x.Items), x.PageNumber, x.PageSize, x.TotalItemCount));
            Mapper.CreateMap<AuthorViewModel, Author>();
            Mapper.CreateMap<Author, AuthorViewModel>();
            Mapper.CreateMap<Page<Book>, IPagination<BookViewModel>>().ConvertUsing(x => new CustomPagination<BookViewModel>(Mapper.Map<IEnumerable<Book>, IEnumerable<BookViewModel>>(x.Items), x.PageNumber, x.PageSize, x.TotalItemCount));
            Mapper.CreateMap<BookViewModel, Book>();
            Mapper.CreateMap<Book, BookViewModel>();
        }
    }
}
