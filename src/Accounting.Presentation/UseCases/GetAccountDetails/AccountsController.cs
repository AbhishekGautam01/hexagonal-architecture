using Accounting.Application.Queries;
using Accounting.Presentation.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounting.Presentation.UseCases.GetAccountDetails
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountQueries _accountsQueries;
        public AccountsController(
            IAccountQueries accountQueries)
        {
            _accountsQueries = accountQueries;
        }

        [HttpGet("{accountId}", Name ="GetAccount")]
        public async Task<IActionResult> Get(Guid accountId)
        {
            var account = await _accountsQueries.GetAccount(accountId);
            List<TransactionModel> transactions = new List<TransactionModel>();

            foreach(var item in account.Transactions)
            {
                var transaction = new TransactionModel(
                    item.Amount,
                    item.Description,
                    item.TransactionDate);
                transactions.Add(transaction);
            }

            return new ObjectResult(new AccountDetailsModel(
                account.AccountId,
                account.CurrentBalance,
                transactions));
        }
    }
}
