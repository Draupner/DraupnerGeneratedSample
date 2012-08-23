using FluentNHibernate.Mapping;
using Library.Core.Domain.Model;

namespace Library.Core.Common.NHibernate
{
    public class AuthorMap : ClassMap<Author>
    {
        public AuthorMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
        }
    }
}
