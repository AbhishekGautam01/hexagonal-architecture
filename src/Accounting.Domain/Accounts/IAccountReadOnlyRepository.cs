using System;
using System.Threading.Tasks;

namespace Accounting.Domain.Accounts
{
    public interface IAccountReadOnlyRepository
    {
        Task<Account> Get(Guid id);
    }
}
