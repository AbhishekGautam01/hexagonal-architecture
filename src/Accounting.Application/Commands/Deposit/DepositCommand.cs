using Accounting.Domain.Accounts;
using Accounting.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Application.Commands.Deposit
{
    public sealed class DepositCommand: ICommand
    {
        public Guid AccountId { get; private set; }
        public Amount Amount { get;private set; }
        private DepositCommand() { }
        public DepositCommand(Guid accountId, Amount amount)
        {
            AccountId = accountId;
            Amount = amount;
        }
    }
}
