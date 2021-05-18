namespace Accounting.Domain.Tests
{
    using Xunit;
    using System;
    using Accounting.Domain.Customers;
    using Accounting.Domain.Accounts;

    public class CustomerTests
    {
        [Fact]
        public void Customer_Should_Be_Registered_With_1_Account()
        {
            Customer sut = new Customer(
                "741214-3054",
                "Sammy Fredriksson");

            var account = new Account(sut.Id);

            sut.Register(account.Id);

            Assert.Single(sut.Accounts);
        }

        [Fact]
        public void Customer_Should_Be_Loaded()
        {
            AccountCollection accounts = new AccountCollection();
            accounts.Add(Guid.NewGuid());

            Guid customerId = Guid.NewGuid();

            Customer customer = Customer.Load(
                customerId,
                "Uncle bob",
                "1234567890123",
                accounts);

            Assert.Equal(customerId, customer.Id);
            Assert.Equal("Uncle bob", customer.Name);
            Assert.Equal("1234567890123", customer.Aadhar);
        }
    }
}
