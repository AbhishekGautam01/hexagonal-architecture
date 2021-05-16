using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Infrastructure.Persistance.MongoDataAccess.Entities
{
    public class Account
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
    }
}
