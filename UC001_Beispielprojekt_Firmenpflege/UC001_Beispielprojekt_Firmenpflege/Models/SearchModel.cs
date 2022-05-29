using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UC001_Beispielprojekt_Firmenpflege.Models
{
    public class SearchModel
    {
        public int? CompanyNo { get; set; }
        public string CompanyName { get; set; }
        public string Industry { get; set; }
        public string City { get; set; }
        public string Holding { get; set; }
    }
}