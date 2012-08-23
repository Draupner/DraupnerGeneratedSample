using FluentNHibernate.Mapping;

namespace Library.Test.Common.Transaction
{
    public class FooMap : ClassMap<Foo>
    {
        public FooMap()
        {
            Id(x => x.Id);
        }
    }
}
