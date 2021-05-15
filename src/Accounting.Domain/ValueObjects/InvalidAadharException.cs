using Accounting.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Domain.ValueObjects
{
    public sealed class InvalidAadharException: DomainException
    {
        internal InvalidAadharException(string message): base(message) { }
    }
}
