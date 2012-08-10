using library.Core.Domain.Model;
using Ploeh.AutoFixture;

namespace library.Test
{
    public class AutoFixtureCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<Author>(x => x.Without(y => y.Id));
            fixture.Customize<LibraryCard>(x => x.Without(y => y.Id).Without(y => y.Loans));
            fixture.Customize<Author>(x => x.Without(y => y.Id));
            fixture.Customize<Book>(x => x.Without(y => y.Id).Without(y => y.Authors));
        }
    }
}
