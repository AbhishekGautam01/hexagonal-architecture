using Accounting.Application.Queries;
using Accounting.Application.Results;
using Accounting.Infrastructure.Persistance.MongoDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using Accounting.Infrastructure.Exceptions;

namespace Accounting.Infrastructure.Persistance.MongoDataAccess.Queries
{
    public class CustomerQueries : ICustomerQueries
    {
        private readonly Context _context;
        private readonly IAccountQueries _accountQueries;

        public CustomerQueries(Context context, IAccountQueries accountQueries)
        {
            _context = context;
            _accountQueries = accountQueries;
        }
        public async Task<CustomerResult> GetCustomer(Guid customerId)
        {
            Customer customer = await _context.Customers
              .Find(customer => customer.Id == customerId).SingleOrDefaultAsync();

            if (customer == null)
                throw new CustomerNotFoundException($"The {customerId} does not exist");
            
            List<Guid> accountIds = await _context
                .Accounts
                .Find(e => e.CustomerId == customerId)
                .Project(p => p.Id)
                .ToListAsync();

            List<AccountResult> accountResults = new List<AccountResult>();
            foreach(Guid accountId in accountIds)
            {
                 AccountResult accountResult = await _accountQueries.GetAccount(accountId);
                accountResults.Add(accountResult);
            }

            CustomerResult customerResult = new CustomerResult(customer.Id, customer.Name, customer.Aadhar, accountResults);
            return customerResult;
         }


    }
}
