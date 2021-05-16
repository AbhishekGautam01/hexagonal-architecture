using Accounting.Application.Queries;
using Accounting.Application.Results;
using Accounting.Domain.Customers;
using Accounting.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Infrastructure.Persistance.InMemoryDataAccess.Queries
{
    public class CustomersQueries : ICustomerQueries
    {
        private readonly Context _context;
        private readonly IAccountQueries _accountsQueries;

        public CustomersQueries(Context context, IAccountQueries accountsQueries)
        {
            _context = context;
            _accountsQueries = accountsQueries;
        }
        public async Task<CustomerResult> GetCustomer(Guid customerId)
        {
            Customer customer = _context
                .Customers
                .Where(e => e.Id == customerId)
                .SingleOrDefault();

            if (customer == null)
                throw new CustomerNotFoundException($"The customer {customerId} does not exists or is not processed yet.");

            List<AccountResult> accountsResult = new List<AccountResult>();

            foreach (Guid accountId in customer.Accounts)
            {
                AccountResult accountResult = await _accountsQueries.GetAccount(accountId);
                accountsResult.Add(accountResult);
            }

            CustomerResult customerResult = new CustomerResult(
                customer.Id, customer.Name, customer.Aadhar,
                accountsResult);

            return await Task.FromResult<CustomerResult>(customerResult);
        }
    }
}
