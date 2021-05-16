﻿namespace Accounting.Infrastructure.EntityFrameworkDataAccess.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Accounting.Application.Queries;
    using Accounting.Application.Results;
    using Accounting.Infrastructure.EntityFrameworkDataAccess.Entities;
    using Accounting.Infrastructure.Exceptions;
    using Microsoft.EntityFrameworkCore;

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
            Entities.Customer customer = await _context
                .Customers
                .FindAsync(customerId);

            List<Entities.Account> accounts = await _context
                .Accounts
                .Where(e => e.CustomerId == customerId)
                .ToListAsync();

            if (customer == null)
                throw new CustomerNotFoundException($"The customer {customerId} does not exists or is not processed yet.");

            List<AccountResult> accountsResult = new List<AccountResult>();

            foreach (Account account in accounts)
            {
                AccountResult accountResult = await _accountsQueries.GetAccount(account.Id);
                accountsResult.Add(accountResult);
            }

            CustomerResult customerResult = new CustomerResult(
                customer.Id, customer.Name, customer.SSN,
                accountsResult);

            return await Task.FromResult<CustomerResult>(customerResult);
        }
    }
}
