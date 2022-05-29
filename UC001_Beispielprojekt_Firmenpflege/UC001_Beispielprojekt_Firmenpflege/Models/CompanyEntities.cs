using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UC001_Beispielprojekt_Firmenpflege.Models
{
    public class CompanyEntities : DbContext
    {
        public CompanyEntities() : base("CompanyEntities")
        {

        }

        public DbSet<CompanyModel> Company_db { get; set; }
       
    }
}