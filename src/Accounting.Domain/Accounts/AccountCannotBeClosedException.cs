using Accounting.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Domain.Accounts
{
    public sealed class AccountCannotBeClosedException: DomainException
    {
        internal AccountCannotBeClosedException(string message): base(message) { }
    }
}
