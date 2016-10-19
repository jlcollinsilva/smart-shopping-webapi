using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingSmartApp.Models
{
    //Entity for the Supermarket master data
    public class SuperMarket
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Address1 { get; set; }
        [MaxLength(50)]
        public string City { get; set; }
        [MaxLength(2)]
        public string State { get; set; }
        [MaxLength(5)]
        [Range(00000,99999)]
        [RegularExpression(@"^\d{5}",ErrorMessage = "The Zip Code must be a 5 digit valid US Zip Code")]
        [Required]
        public string ZipCode { get; set; }

        public virtual ICollection<ProductCatalog> ProductCatalogs { get; set; }
        public virtual ICollection<ShoppingProduct> ShoppingProducts { get; set; }
    }
}