using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Domain.Accounts
{
    public interface IAccountWriteOnlyRepository
    {
        Task Add(Account account, Credit credit);
        Task Update(Account account, Credit credit);
        Task Update(Account account, Debit debit);
        Task Delete(Account account);
    }
}
