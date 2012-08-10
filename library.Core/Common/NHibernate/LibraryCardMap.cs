using FluentNHibernate.Mapping;
using library.Core.Domain.Model;

namespace library.Core.Common.NHibernate
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
