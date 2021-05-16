using Accounting.Application.Exceptions;
using Accounting.Domain.Accounts;
using Accounting.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Application.Commands.Withdraw
{
    public sealed class WithdrawResultCommandHandler: ICommandHandler<WithdrawCommand, WithdrawResult>
    {
        private readonly IAccountReadOnlyRepository accountReadOnlyRepository;
        private readonly IAccountWriteOnlyRepository accountWriteOnlyRepository;

        public WithdrawResultCommandHandler(
            IAccountReadOnlyRepository accountReadOnlyRepository,
            IAccountWriteOnlyRepository accountWriteOnlyRepository)
        {
            this.accountReadOnlyRepository = accountReadOnlyRepository;
            this.accountWriteOnlyRepository = accountWriteOnlyRepository;
        }

        public async Task<WithdrawResult> Execute(WithdrawCommand command)
        {
            Account account = await accountReadOnlyRepository.Get(command.AccountId);
            if (account == null)
                throw new AccountNotFoundException($"The account {command.AccountId} does not exists or is already closed.");

            account.Withdraw(command.Amount);
            Debit debit = (Debit)account.GetLastTransaction();

            await accountWriteOnlyRepository.Update(account, debit);

            WithdrawResult result = new WithdrawResult(
                debit,
                account.GetCurrentBalance()
            );

            return result;
        }
    }
}
