using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounting.Presentation.Model
{
    public sealed class AccountDetailsModel
    {
        public Guid AccountId { get; }
        public double CurrentBalance { get; }
        public List<TransactionModel> Transactions { get; }

        public AccountDetailsModel(Guid accountId, double currentBalance, List<TransactionModel> transactions)
        {
            AccountId = accountId;
            CurrentBalance = currentBalance;
            Transactions = transactions;
        }
    }
}
