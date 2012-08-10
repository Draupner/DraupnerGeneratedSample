using FluentNHibernate.Mapping;
using library.Core.Domain.Model;

namespace library.Core.Common.NHibernate
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
