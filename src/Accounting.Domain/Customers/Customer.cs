using Accounting.Domain.SeedWork;
using Accounting.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Domain.Customers
{
    public sealed class Customer: IAggregateRoot
    {
        public Guid Id { get; private set; }
        public Name Name { get; private set; }
        public Aadhar Aadhar { get; private set; }
        public IReadOnlyCollection<Guid> Accounts
        {
            get
            {
                IReadOnlyCollection<Guid> readOnly = _accounts.GetAccountIds();
                return readOnly;
            }
        }

        private AccountCollection _accounts;

        public Customer(Aadhar ssn, Name name)
        {
            Id = Guid.NewGuid();
            Aadhar = ssn;
            Name = name;
            _accounts = new AccountCollection();
        }

        public void Register(Guid accountId)
        {
            _accounts.Add(accountId);
        }

        private Customer() { }

        public static Customer Load(Guid id, Name name, Aadhar aadhar, AccountCollection accounts)
        {
            Customer customer = new Customer();
            customer.Id = id;
            customer.Name = name;
            customer.Aadhar = aadhar;
            customer._accounts = accounts;
            return customer;
        }
    }
}
