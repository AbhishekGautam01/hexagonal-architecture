using Accounting.Domain.Accounts;
using Accounting.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Application.Commands.Register
{
    public sealed class RegisterCommandHandler: ICommandHandler<RegisterCommand, RegisterResult>
    {
        private readonly ICustomerWriteOnlyRepository customerWriteOnlyRepository;
        private readonly IAccountWriteOnlyRepository accountWriteOnlyRepository;

        public RegisterCommandHandler(
            ICustomerWriteOnlyRepository customerWriteOnlyRepository,
            IAccountWriteOnlyRepository accountWriteOnlyRepository)
        {
            this.customerWriteOnlyRepository = customerWriteOnlyRepository;
            this.accountWriteOnlyRepository = accountWriteOnlyRepository;
        }

        public async Task<RegisterResult> Execute(RegisterCommand command)
        {
            Customer customer = new Customer(command.Pin, command.Name);

            Account account = new Account(customer.Id);
            account.Deposit(command.InitialAmount);
            Credit credit = (Credit)account.GetLastTransaction();

            customer.Register(account.Id);

            await customerWriteOnlyRepository.Add(customer);
            await accountWriteOnlyRepository.Add(account, credit);

            RegisterResult result = new RegisterResult(customer, account);
            return result;
        }
    }
}
