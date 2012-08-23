using System;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit;

namespace Library.Test
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AutoFixtureData : AutoDataAttribute
    {
        public AutoFixtureData() : base(new Fixture().Customize(new AutoFixtureCustomization())) 
        {
        }
    }
}