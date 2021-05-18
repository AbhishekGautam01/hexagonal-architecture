using Accounting.Application.Commands;
using Accounting.Application.Commands.Withdraw;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounting.Presentation.UseCases.Withdraw
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ICommandHandler<WithdrawCommand, WithdrawResult> _handler;
        public AccountsController(
            ICommandHandler<WithdrawCommand, WithdrawResult> handler)
        {
            _handler = handler;
        }

        [HttpPatch("withdraw")]
        public async Task<IActionResult> Withdraw([FromBody]WithdrawCommand command)
        {
            WithdrawResult result = await _handler.Execute(command);
            if (result == null)
                return new NoContentResult();
            Model model = new Model(
                result.Transaction.Amount,
                result.Transaction.Description,
                result.Transaction.TransactionDate,
                result.UpdatedBalance
                );
            return new ObjectResult(model);
        }
    }
}
