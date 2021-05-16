using Accounting.Application.Exceptions;
using Accounting.Domain.Accounts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Application.Commands.Deposit
{
    public sealed class DepositCommandHandler : ICommandHandler<DepositCommand, DepositResult>
    {
        private readonly IAccountReadOnlyRepository _accountReadOnlyRepository;
        private readonly IAccountWriteOnlyRepository _accountWriteOnlyRepository;
        public DepositCommandHandler(
            IAccountReadOnlyRepository accountReadOnlyRepository,
            IAccountWriteOnlyRepository accountWriteOnlyRepository)
        {
            _accountReadOnlyRepository = accountReadOnlyRepository;
            _accountWriteOnlyRepository = accountWriteOnlyRepository;
        }
        public async Task<DepositResult> Execute(DepositCommand command)
        {
            Account account = await _accountReadOnlyRepository.Get(command.AccountId);
            if (account == null)
                throw new AccountNotFoundException($"The account {command.AccountId} does not exist or it is already closed");
            account.Deposit(command.Amount);
            Credit credit = (Credit)account.GetLastTransaction();
            await _accountWriteOnlyRepository.Update(
                account,
                credit);
            DepositResult result = new DepositResult(
                credit,
                account.GetCurrentBalance());
            return result;
        }
    }
}
