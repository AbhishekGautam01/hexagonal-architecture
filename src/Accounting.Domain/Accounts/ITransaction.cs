using Accounting.Domain.ValueObjects;
using System;

namespace Accounting.Domain.Accounts
{
    public interface ITransaction
    {
        Amount Amount { get; }
        string Description { get; }
        DateTime TransactionDate { get; }
    }
}