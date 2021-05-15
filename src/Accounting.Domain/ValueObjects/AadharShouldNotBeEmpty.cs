using Accounting.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Domain.ValueObjects
{
    public sealed class AadharShouldNotBeEmptyException : DomainException
    {
        internal AadharShouldNotBeEmptyException(string message) : base(message)
        {
        }
    }
}
