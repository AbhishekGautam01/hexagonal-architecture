using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Infrastructure.Persistance.MongoDataAccess.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Aadhar { get; set; }
    }
}
