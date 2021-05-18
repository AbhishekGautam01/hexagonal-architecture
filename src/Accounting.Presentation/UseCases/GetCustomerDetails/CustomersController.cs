using Accounting.Application.Queries;
using Accounting.Application.Results;
using Accounting.Presentation.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounting.Presentation.UseCases.GetCustomerDetails
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerQueries _customerQueries;
        public CustomersController(ICustomerQueries customerQueries)
        {
            _customerQueries = customerQueries;
        }

        [HttpGet("{customerId}", Name = "GetCustomer")]
        public async Task<IActionResult> GetCustomer(Guid customerId)
        {
            CustomerResult customer = await _customerQueries.GetCustomer(customerId);
            if(customer == null)
            {
                return new NoContentResult();
            }
            List<AccountDetailsModel> accounts = new List<AccountDetailsModel>();
            foreach(var account in customer.Accounts)
            {
                List<TransactionModel> transactions = new List<TransactionModel>();
                foreach (var item in account.Transactions)
                {
                    var transaction = new TransactionModel(
                        item.Amount,
                        item.Description,
                        item.TransactionDate);
                    transactions.Add(transaction);
                }
                accounts.Add(new AccountDetailsModel(
                    account.AccountId,
                    account.CurrentBalance,
                    transactions));
            }

            CustomerDetailsModel model = new CustomerDetailsModel(
                customer.CustomerId,
                customer.PersonNummer,
                customer.Name,
                accounts);
            return new ObjectResult(model);
        }
    }
}
