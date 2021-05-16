using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Infrastructure.Persistance.MongoDataAccess.Entities
{
    public class Debit
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
