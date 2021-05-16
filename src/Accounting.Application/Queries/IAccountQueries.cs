using Accounting.Application.Results;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Application.Queries
{
    public interface IAccountQueries
    {
        Task<AccountResult> GetAccount(Guid accountId);
    }
}
