using System;

namespace Accounting.Application.Exceptions
{
    public class ApplicationException: Exception
    {
        internal ApplicationException(string businessMessage): base(businessMessage)
        {
        }
    }
}
