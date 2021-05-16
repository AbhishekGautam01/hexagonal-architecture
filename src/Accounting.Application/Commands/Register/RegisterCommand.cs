using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Application.Commands.Register
{
    public sealed class RegisterCommand: ICommand
    {
        public string Pin { get; }
        public string Name { get; }
        public double InitialAmount { get; }
    }
}
