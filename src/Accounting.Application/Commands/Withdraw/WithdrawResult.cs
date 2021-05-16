using Accounting.Application.Results;
using Accounting.Domain.Accounts;
using Accounting.Domain.ValueObjects;

namespace Accounting.Application.Commands.Withdraw
{
    public sealed class WithdrawResult
    {
        public TransactionResult Transaction { get; }
        public double UpdatedBalance { get; }

        public WithdrawResult(Debit transaction, Amount updatedBalance)
        {
            Transaction = new TransactionResult(
                transaction.Description,
                transaction.Amount,
                transaction.TransactionDate);

            UpdatedBalance = updatedBalance;
        }
    }
}
