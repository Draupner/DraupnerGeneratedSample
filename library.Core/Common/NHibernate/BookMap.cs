using FluentNHibernate.Mapping;
using Library.Core.Domain.Model;

namespace Library.Core.Common.NHibernate
{
    public class  BookMap : ClassMap<Book>
    {
        public BookMap()
        {
            Id(x => x.Id);
            Map(x => x.Title);
            Map(x => x.ISBN);
            HasMany(x => x.Authors);
        }
    }
}
