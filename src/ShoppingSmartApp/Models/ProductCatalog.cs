using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingSmartApp.Models
{
    //Entity for the Products into a Supermarket Catalog

    public class ProductCatalog
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int SuperMarketId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        [Range(0.01, 9999999.99, ErrorMessage = "Unit Price must be a Decimal Number Positive until 9,999,999.99")]
        public Decimal UnitPrice { get; set; }
        public string Status { get; set; } //For future use.
        [MaxLength(250)] 
        public string Comment { get; set; }

        public virtual SuperMarket SuperMarket { get; set; }
        public virtual Product Product { get; set; }
    }
}
