using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UC001_Beispielprojekt_Firmenpflege.Models
{
    public class CompanyNumberIndustryViewModel
    {
        public IEnumerable<CompanyModel> Companies { get; set; }
        public SelectList Number { get; set; }
        public SelectList IndustrySelect { get; set; }
    }
}