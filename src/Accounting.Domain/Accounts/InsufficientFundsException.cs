using Accounting.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Domain.Accounts
{
    public sealed class InsufficientFundsException: DomainException
    {
        internal InsufficientFundsException(string message): base(message) { }
    }
}
