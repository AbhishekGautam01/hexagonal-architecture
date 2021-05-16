using Accounting.Domain.Accounts;
using Accounting.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Accounting.Infrastructure.Persistance.InMemoryDataAccess
{
    public class Context
    {
        public Collection<Customer> Customers { get; set; }
        public Collection<Account> Accounts { get; set; }
        public Context()
        {
            Customers = new Collection<Customer>();
            Accounts = new Collection<Account>();
        }
    }
}
