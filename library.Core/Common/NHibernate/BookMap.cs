using FluentNHibernate.Mapping;
using library.Core.Domain.Model;

namespace library.Core.Common.NHibernate
{
  using FluentNHibernate;

  public class BookMap : ClassMap<Book>
    {
        public BookMap()
        {
            Id(x => x.Id);
            Map(x => x.Title);
            Map(x => x.ISBN);
            HasMany<Author>(x => x.Authors);
        }
    }
}
