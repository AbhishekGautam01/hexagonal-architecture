using Accounting.Application.Commands;
using Accounting.Application.Commands.Register;
using Accounting.Presentation.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounting.Presentation.UseCases.Register
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICommandHandler<RegisterCommand, RegisterResult> _handler;
        public CustomersController(ICommandHandler<RegisterCommand, RegisterResult> handler)
        {
            _handler = handler;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RegisterCommand command)
        {
            RegisterResult result = await _handler.Execute(command);

            List<TransactionModel> transactions = new List<TransactionModel>();
            foreach (var item in result.Account.Transactions)
            {
                var transaction = new TransactionModel(
                    item.Amount,
                    item.Description,
                    item.TransactionDate);
                transactions.Add(transaction);
            }

            AccountDetailsModel account = new AccountDetailsModel(
                result.Account.AccountId,
                result.Account.CurrentBalance,
                transactions);
            List<AccountDetailsModel> accounts = new List<AccountDetailsModel>();
            accounts.Add(account);

            Model model = new Model(
                result.Customer.CustomerId,
                result.Customer.PersonNummer,
                result.Customer.Name,
                accounts);
            return CreatedAtRoute("GetCustomer", new { customerId = model.CustomerId }, model);
        }

    }
}
