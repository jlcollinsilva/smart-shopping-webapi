using ShoppingSmartApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingSmartApp.ViewModels
{
    //Logical view of Property mix of Supermarket, Product, ProductCatalog and ShoppingProduct
    public class ProductsListViewModel
    {
        public int SuperMarketId { get; set; } //From Supermarket
        public string SuperMarketName { get; set; } //From Supermarket
        public int ProductId { get; set; } //From Product
        public string ProductName { get; set; } //From Product
        public Double ProductQty { get; set; } //From ShoppingProduct
        public Decimal ProductPrice { get; set; } //ProductCatalog
        public Decimal ProductValue { get; set; } //ProductQty multiply by ProductPrice

    }

    //Logical view of Supermarket summary and his Products(Catalog) found in a Shopping List
    public class SuperMarketViewModel
    {
        public int SuperMarketId { get; set; } //from Supermarket
        public string SuperMarketName { get; set; } // from Supermarket
        public int TotalItems { get; set; } //# Items that match with the Shopping List
        public Decimal TotalPrice { get; set; } //Sum of value of items that match with the Shopping List
        public IEnumerable<ProductsListViewModel> ProductsList { get; set; } //See ProductsListViewModel

    }

    //Partial view of combination of Product and ProductCatalog entities
    public class ProductsCatalogListViewModel
    {
        public int SuperMarketId { get; set; } //from ProductCatalog
        public int ProductId { get; set; } //from Product
        public int ProductCatalogId { get; set; } //from ProductCatalog
        public string ProductName { get; set; } //from Product
        public Decimal ProductPrice { get; set; } //from ProductCatalog
    }

    //Partial view of combination of Product and ShoppingProduct
    public class ProductShoppingListViewModel
    {
        public int ShoppingProductId { get; set; } //from ShoppingProduct
        public int ProductId { get; set; } //from Product
        public string ProductName { get; set; } //from Product
        public Double ProductQty { get; set; } //from ShoppingProduct
    }
}