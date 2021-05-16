using System;
using System.Threading.Tasks;

namespace Accounting.Domain.Customers
{
    public interface ICustomerReadOnlyRepository
    {
        Task<Customer> Get(Guid id);
    }
}
