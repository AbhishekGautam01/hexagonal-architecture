namespace Accounting.Infrastructure.DapperDataAccess.Entities
{
    using System;

    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Aadhar { get; set; }
    }
}
