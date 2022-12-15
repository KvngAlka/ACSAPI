using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Models
{
    internal class Product
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ProductLongName { get; set; }
        public string ProductCategory { get; set; }
        public int ShortCode { get; set; }
        public string BillingType { get; set; }
        public string ProductStatus { get; set; }



    }
}
