using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingSmartApp.Models
{
    //Entity for Universal/Generic Products definition
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
        [Required]
        public string Unit { get; set; }

        public ICollection<ShoppingProduct> ShoppingProducts { get; set; }
        public ICollection<ProductCatalog> ProductCatalogs { get; set; }

    }
}
