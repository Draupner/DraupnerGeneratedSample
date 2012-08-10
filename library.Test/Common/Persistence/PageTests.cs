using System.Collections.Generic;
using library.Core.Common.Persistence;
using Xunit;

namespace library.Test.Common.Persistence
{
    public class PageTests
    {
        [Fact]
        public void ShouldCreatePage()
        {
            var items = new List<Foo>();
            var page = new Page<Foo>(items, 3, 10, 100);

            Assert.Equal(items,page.Items);
            Assert.Equal(3, page.PageNumber);
            Assert.Equal(10, page.PageSize);
            Assert.Equal(100, page.TotalItemCount);
        }
    }

    internal class Foo
    {
        
    }
}
