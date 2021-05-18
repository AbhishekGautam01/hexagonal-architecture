using Accounting.Application.Commands;
using Accounting.Application.Commands.Deposit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounting.Presentation.UseCases.Deposit
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ICommandHandler<DepositCommand, DepositResult> _depositCommandHandler;
        public AccountsController(
            ICommandHandler<DepositCommand, DepositResult> depositCommandHandler)
        {
            _depositCommandHandler = depositCommandHandler;
        }

        [HttpPatch("Deposit")]
        public async Task<IActionResult> Deposit([FromBody]DepositCommand command)
        {
            DepositResult depositResult = await _depositCommandHandler.Execute(command);
            if (depositResult == null)
                return new NoContentResult();
            Model model = new Model(
                depositResult.Transaction.Amount,
                depositResult.Transaction.Description,
                depositResult.Transaction.TransactionDate,
                depositResult.UpdatedBalance);
            return new ObjectResult(model);
        }
    }
}
