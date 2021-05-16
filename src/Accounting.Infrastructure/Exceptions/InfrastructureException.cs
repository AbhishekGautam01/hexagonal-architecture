using System;

namespace Accounting.Infrastructure.Exceptions
{
    public class InfrastructureException: Exception
    {
        internal InfrastructureException(string businessMessage)
               : base(businessMessage)
        {
        }
    }
}
