using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Application.Exceptions
{
    internal sealed class CustomerNotFoundException: ApplicationException
    {
        internal CustomerNotFoundException(string message)
            : base(message)
        {

        }
    }
}
