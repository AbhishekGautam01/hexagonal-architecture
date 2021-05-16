using Accounting.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Accounting.Infrastructure.Persistance.MongoDataAccess.Repositories
{
    public class CustomerRepository : ICustomerReadOnlyRepository, ICustomerWriteOnlyRepository
    {
        private readonly Context _context;

        public CustomerRepository(Context context)
        {
            _context = context;
        }

        public async Task Add(Customer customer)
        {
            Entities.Customer customerEntity = new Entities.Customer
            {
                Id = customer.Id,
                Name = customer.Name,
                Aadhar = customer.Aadhar,
            };
            await _context.Customers.InsertOneAsync(customerEntity);
        }

        public async Task<Customer> Get(Guid id)
        {
            Entities.Customer customer = await _context.Customers.
                  Find(customer => customer.Id == id)
                  .SingleOrDefaultAsync();
            
            List<Guid> accountIds = await _context
                .Accounts
                .Find(account => account.CustomerId == id)
                .Project(p => p.Id)
                .ToListAsync();
            
            AccountCollection accountCollection = new AccountCollection();
            foreach (var accountId in accountIds)
            {
                accountCollection.Add(accountId);
            }

            return Customer.Load(
                customer.Id,
                customer.Name,
                customer.Aadhar,
                accountCollection);
        }


        public async Task Update(Customer customer)
        {
            Entities.Customer customerEntity = new Entities.Customer
            {
                Id = customer.Id,
                Name = customer.Name,
                Aadhar = customer.Aadhar,
            };
            var filter = Builders<Entities.Customer>.Filter.Eq(e => e.Id, customerEntity.Id);
            await _context.Customers.ReplaceOneAsync(filter, customerEntity);
        }
    }
}
