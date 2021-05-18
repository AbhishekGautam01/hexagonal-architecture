namespace Accounting.Domain.Tests
{
    using Accounting.Domain.ValueObjects;
    using Xunit;

    public class AadharTests
    {
        [Fact]
        public void Empty_SSN_Should_Not_Be_Created()
        {
            //
            // Arrange
            string empty = string.Empty;

            //
            // Act and Assert
            Assert.Throws<AadharShouldNotBeEmptyException>(
                () => new Aadhar(empty));
        }

        [Fact]
        public void Valid_SSN_Should_Be_Created()
        {
            string valid = "1234567890123";

            Aadhar SSN = new Aadhar(valid);

            Assert.Equal(valid, SSN);
        }
    }
}
