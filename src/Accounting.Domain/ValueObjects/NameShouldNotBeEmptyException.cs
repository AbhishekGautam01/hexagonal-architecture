using Accounting.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Domain.ValueObjects
{
    public sealed class NameShouldNotBeEmptyException: DomainException
    {
        internal NameShouldNotBeEmptyException(string message): base(message)
        {
        }
    }
}
