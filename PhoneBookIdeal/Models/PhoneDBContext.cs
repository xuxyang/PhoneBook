using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PhoneBookIdeal.Models
{
    public class PhoneDBContext : DbContext
    {
        public PhoneDBContext() : base("name=PhoneDBContext")
        {
        }

        public System.Data.Entity.DbSet<PhoneBookIdeal.Models.Phone> Phones { get; set; }
    
    }
}
