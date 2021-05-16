using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Application.Results
{
    public sealed class CustomerResult
    {
        public Guid CustomerId { get; }
        public string PersonNummer { get; }
        public string Name { get; }
        public IReadOnlyList<AccountResult> Accounts { get; }
        public CustomerResult(
            Guid customerId, 
            string personNummer,
            string name, 
            List<AccountResult> accounts)
        {
            CustomerId = customerId;
            PersonNummer = personNummer;
            Name = name;
            Accounts = accounts;
        }
    }
}
