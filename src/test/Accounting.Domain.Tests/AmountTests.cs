namespace Accounting.Domain.Tests
{
    using Accounting.Domain.ValueObjects;
    using Xunit;

    public class AmountTests
    {
        [Fact]
        public void Positive_Amount_Should_Be_Created()
        {
            double positiveAmount = 500;

            Amount amount = new Amount(positiveAmount);
            
            Assert.Equal<double>(positiveAmount, amount);
        }

        [Fact]
        public void Amount_With_100_Minus_70_Should_Be_30()
        {
            Amount hundred = new Amount(100);
            Amount seventy = new Amount(70);

            Amount amount = hundred - seventy;

            Assert.Equal(30, amount);
        }

        [Fact]
        public void Amount_With_100_Larger_Than_70()
        {
            Amount hundred = new Amount(100);
            Amount seventy = new Amount(70);

            Assert.True(hundred > seventy);
        }

        [Fact]
        public void Amount_With_30_Less_Than_Equal_30()
        {
            Amount thirty = new Amount(30);
            Amount seventy = new Amount(70);

            Assert.True(thirty <= seventy);
        }

        [Fact]
        public void Amount_With_10_Larger_Than_Equal_10()
        {
            Amount thirty = new Amount(10);
            Amount seventy = new Amount(10);

            Assert.True(thirty >= seventy);
        }
    }
}
