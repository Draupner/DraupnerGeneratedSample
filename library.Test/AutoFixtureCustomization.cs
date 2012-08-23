using Library.Core.Domain.Model;
using Ploeh.AutoFixture;

namespace Library.Test
{
    public class AutoFixtureCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<LibraryCard>(x => x.Without(y => y.Id).Without(y => y.Loans));
            fixture.Customize<Author>(x => x.Without(y => y.Id));
            fixture.Customize<Book>(x => x.Without(y => y.Id).Without(y => y.Authors));
            fixture.Customize(new MultipleCustomization());
        }
    }
}
