using Accounting.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Application.Commands.Withdraw
{
    public sealed class WithdrawCommand: ICommand
    {
        public Guid AccountId { get; }
        public Amount Amount { get; }
        public WithdrawCommand(Guid accountId, Amount amount)
        {
            AccountId = accountId;
            Amount = amount;
        }
    }
}
