using FluentNHibernate.Mapping;

namespace library.Test.Common.Transaction
{
    public class FooMap : ClassMap<Foo>
    {
        public FooMap()
        {
            Id(x => x.Id);
        }
    }
}
