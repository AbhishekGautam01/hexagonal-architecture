namespace Accounting.Infrastructure.DapperDataAccess.Queries
{
    using Dapper;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    using Accounting.Application.Queries;
    using Accounting.Application.Results;

    public class CustomersQueries : ICustomerQueries
    {
        private readonly string _connectionString;
        private readonly IAccountQueries _accountsQueries;

        public CustomersQueries(string connectionString, IAccountQueries accountsQueries)
        {
            _connectionString = connectionString;
            _accountsQueries = accountsQueries;
        }

        public async Task<CustomerResult> GetCustomer(Guid customerId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string customerSQL = "SELECT * FROM Customer WHERE Id = @customerId";
                Entities.Customer customer = await db
                    .QueryFirstOrDefaultAsync<Entities.Customer>(customerSQL, new { customerId });

                if (customer == null)
                    return null;

                string accountSQL = "SELECT id FROM Account WHERE CustomerId = @customerId";
                IEnumerable<Guid> accounts = await db
                    .QueryAsync<Guid>(accountSQL, new { customerId });

                List<AccountResult> accountCollection = new List<AccountResult>();

                foreach (Guid accountId in accounts)
                {
                    accountCollection.Add(await _accountsQueries.GetAccount(accountId));
                }

                CustomerResult customerResult = new CustomerResult(customer.Id,
                    customer.Name,
                    customer.Aadhar,
                    accountCollection);

                return customerResult;
            }
        }
    }
}
