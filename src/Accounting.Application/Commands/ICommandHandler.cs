using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Application.Commands
{
    public interface ICommandHandler<T, U> where T: ICommand
    {
        Task<U> Execute(T command);
    }
}
