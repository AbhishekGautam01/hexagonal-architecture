using Accounting.Application.Results;
using Accounting.Domain.Accounts;
using Accounting.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Application.Commands.Register
{
    public sealed class RegisterResult
    {
        public CustomerResult Customer { get; }
        public AccountResult Account { get; }
        public RegisterResult(Customer customer, Account account)
        {
            List<TransactionResult> transactionResults = new List<TransactionResult>();

            foreach (ITransaction transaction in account.GetTransactions())
            {
                transactionResults.Add(
                    new TransactionResult(
                        transaction.Description,
                        transaction.Amount,
                        transaction.TransactionDate));
            }

            Account = new AccountResult(account.Id, account.GetCurrentBalance(), transactionResults);

            List<AccountResult> accountResults = new List<AccountResult>();
            accountResults.Add(Account);

            Customer = new CustomerResult(customer.Id, customer.Aadhar, customer.Name, accountResults);
        }
    }
}
