using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Application.Commands.CloseAccount
{
    public sealed class CloseAccountCommand : ICommand
    {
        private CloseAccountCommand() { }
        public CloseAccountCommand(Guid accountId)
        {
            AccountId = accountId;
        }
        public Guid AccountId { get; private set; }
    }
}
