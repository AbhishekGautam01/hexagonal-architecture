using Accounting.Application.Commands;
using Accounting.Application.Commands.CloseAccount;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounting.Presentation.UseCases.CloseAccount
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ICommandHandler<CloseAccountCommand, Guid> _closeAccountCommandHandler;
        public AccountsController(
            ICommandHandler<CloseAccountCommand, Guid> closeAccountHandler)
        {
            _closeAccountCommandHandler = closeAccountHandler;
        }

        [HttpDelete("{accountId}")]
        public async Task<IActionResult> Close(CloseAccountCommand command)
        {
            Guid closeResult = await _closeAccountCommandHandler.Execute(command);
            if(closeResult == Guid.Empty)
            {
                return new NoContentResult();
            }
            return Ok();
        }
    }
}
