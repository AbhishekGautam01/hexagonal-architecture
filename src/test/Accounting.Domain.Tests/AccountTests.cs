using Accounting.Domain.Accounts;
using Accounting.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Accounting.Domain.Tests
{
    public class AccountTests
    {
        [Fact]
        public void New_Account_Should_Have_100_Credit_After_Deposit()
        {
            Guid customerId = Guid.NewGuid();
            Amount amount = new Amount(100.0);
            Account sut = new Account(customerId);

            sut.Deposit(amount);

            Credit credit = (Credit)sut.GetLastTransaction();

            Assert.Equal(customerId, sut.CustomerId);
            Assert.Equal(100, credit.Amount);
            Assert.Equal("Credit", credit.Description);
            Assert.True(credit.AccountId != Guid.Empty);
        }
        [Fact]
        public void New_Account_With_1000_Balance_Should_Have_900_Credit_After_Withdraw()
        {
            Account sut = new Account(Guid.NewGuid());
            sut.Deposit(1000.0);

            sut.Withdraw(100);

            Assert.Equal(900, sut.GetCurrentBalance());
        }

        [Fact]
        public void New_Account_Should_Allow_Closing()
        {
            Account sut = new Account(Guid.NewGuid());

            sut.Close();

            Assert.True(true);
        }

        [Fact]
        public void Account_With_Funds_Should_Not_Allow_Closing()
        {
            Account sut = new Account(Guid.NewGuid());
            sut.Deposit(100);

            Assert.Throws<AccountCannotBeClosedException>(
                () => sut.Close());
        }


        [Fact]
        public void Account_With_200_Balance_Should_Not_Allow_50000_Withdraw()
        {
            Account sut = new Account(Guid.NewGuid());
            sut.Deposit(200);

            Assert.Throws<InsufficientFundsException>(
                () => sut.Withdraw(5000));
        }

        [Fact]
        public void Account_With_Three_Transactions_Should_Be_Consistent()
        {
            Account sut = new Account(Guid.NewGuid());
            sut.Deposit(200);
            sut.Withdraw(100);
            sut.Deposit(50);

            var transactions = sut.GetTransactions();

            Assert.Equal(3, transactions.Count);
        }

        [Fact]
        public void Account_Should_Be_Loaded()
        {
            TransactionCollection transactions = new TransactionCollection();
            transactions.Add(new Debit(Guid.Empty, 100));

            Account account = Account.Load(
                Guid.Empty,
                Guid.Empty,
                transactions);

            Assert.Single(account.GetTransactions());
            Assert.Equal(Guid.Empty, account.Id);
            Assert.Equal(Guid.Empty, account.CustomerId);
        }

    }
}
