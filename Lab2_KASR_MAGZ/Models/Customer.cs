using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2_KASR_MAGZ.Models
{
    public class Customer
    {
        [Required]
        public string NameCustomer { get; set; }

        [Required]
        public string Nit { get; set; }

        [Required]
        public string Direction { get; set; }
    }
}
