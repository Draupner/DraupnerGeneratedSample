using FluentNHibernate.Mapping;
using Library.Core.Domain.Model;

namespace Library.Core.Common.NHibernate
{
    public class LibraryCardMap : ClassMap<LibraryCard>
    {
        public LibraryCardMap()
        {
            Id(x => x.Id);
            Map(x => x.Number);
            HasMany(x => x.Loans);
        }
    }
}
