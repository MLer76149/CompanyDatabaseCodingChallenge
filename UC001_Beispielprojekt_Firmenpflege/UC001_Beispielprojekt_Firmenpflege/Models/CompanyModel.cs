using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UC001_Beispielprojekt_Firmenpflege.Models
{
    public class CompanyModel
    {
        [Key]
        public int CompanyNo { get; set; }

        [Required]
        public string CompanyName { get; set; }

        [Required]
        public string Industry { get; set; }

        [Required]
        public int AmountEmployees { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Holding { get; set; }

        [Required]
        [Range(0, 4)]
        public int Level { get; set; }


        


    }
}