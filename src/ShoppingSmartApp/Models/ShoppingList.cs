using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingSmartApp.Models
{
    //Entity for the Shopping List created by the Consumers
    public class ShoppingList
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [DataType(DataType.Date,ErrorMessage = "Purchase Date invalid")]
        public DateTime DateList { get; set; } //Potencial Purchase Date current data but future use.
        [Required]
        public string Username { get; set; } //Usename == Consumer

        public ICollection<ShoppingProduct> ShoppingProducts { get; set; }
    }
}
