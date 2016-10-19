using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingSmartApp.Models
{
    //Entity for the Products into a Shopping List of the Consumer
    public class ShoppingProduct
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int ShoppingListId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        [Range(1, 1000000, ErrorMessage = "Quantity must be a Number between 1 and 1,000,000")]
        public double Quantity { get; set; }
        public Boolean Priority { get; set; } //It is a Priority in the List?

        public virtual ShoppingList ShoppingList { get; set; }
        public virtual Product Product { get; set; }
        public virtual SuperMarket SuperMarket { get; set; }
    }
}
