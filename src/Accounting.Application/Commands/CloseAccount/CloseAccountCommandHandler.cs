using Accounting.Application.Exceptions;
using Accounting.Domain.Accounts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Application.Commands.CloseAccount
{
    public sealed class CloseAccountCommandHandler : ICommandHandler<CloseAccountCommand, Guid>
    {
        private readonly IAccountReadOnlyRepository _accountReadOnlyRepository;
        private readonly IAccountWriteOnlyRepository _accountWriteOnlyRepository;
        public CloseAccountCommandHandler(
            IAccountReadOnlyRepository accountReadOnlyRepository,
            IAccountWriteOnlyRepository accountWriteOnlyRepository)
        {
            _accountReadOnlyRepository = accountReadOnlyRepository;
            _accountWriteOnlyRepository = accountWriteOnlyRepository;
        }
        public async Task<Guid> Execute(CloseAccountCommand command)
        {
            Account account = await _accountReadOnlyRepository.Get(command.AccountId);
            if (account == null)
                throw new AccountNotFoundException($"The account {command.AccountId} does not exist or it is already closed");
            account.Close();
            await _accountWriteOnlyRepository.Delete(account);
            return account.Id;
        }
    }
}
