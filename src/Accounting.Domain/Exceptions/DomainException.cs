using System;

namespace Accounting.Domain.Exceptions
{
    public class DomainException : Exception
    {
        internal DomainException(string businessMessage) : base(businessMessage) { }
    }
}
