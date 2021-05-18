namespace Accounting.Domain.Tests
{
    using Accounting.Domain.ValueObjects;
    using Xunit;

    public class NameTests
    {
        [Fact]
        public void Empty_Name_Should_Be_Created()
        {
            string empty = string.Empty;

            Assert.Throws<NameShouldNotBeEmptyException>(
                () => new Name(empty));
        }

        [Fact]
        public void Full_Name_Shoud_Be_Created()
        {
            string valid = "Uncle Bob";

            Name name = new Name(valid);

            Assert.Equal(new Name(valid), name);
        }
    }
}
