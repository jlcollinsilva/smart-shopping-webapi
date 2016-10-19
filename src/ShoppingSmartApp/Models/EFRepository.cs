using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingSmartApp.Models;
using ShoppingSmartApp.Data;
using ShoppingSmartApp.ViewModels;

namespace ShoppingSmartApp.Models
{
    //This repository contain some of function that for their complexity or for to be such specific do not qualify or is not resolve by the Generic Repository
    public class EFRepository : IRepository
    {
        private ApplicationDbContext _db;

        public EFRepository(ApplicationDbContext db)
        {

            this._db = db;
        }

        /// <summary>
        /// Using Product, Catalogs and Supermarket tables produce a result with a summary of the Supermarket information and details of their products.
        /// </summary>
        /// <param name="shoppingListIdToBuy">Key of the Shopping List to Match in the search</param>
        /// <param name="zipcode">Location identify by Zip Code for to match in the search</param>
        /// <returns>Return IList<SuperMarketViewModel> a List of both: Supermarket summary and Products details</returns>
        public IList<SuperMarketViewModel> FindCatalog(int shoppingListIdToBuy, string zipcode)

        {

            var result = (from sp in _db.ShoppingProducts
                          join pc in _db.ProductCatalogs on sp.ProductId equals pc.ProductId
                          join p in _db.Products on sp.ProductId equals p.Id
                          join s in _db.SuperMarkets on pc.SuperMarketId equals s.Id
                          where sp.ShoppingListId == shoppingListIdToBuy && s.ZipCode == zipcode
                          select new ProductsListViewModel
                          {
                              SuperMarketId = pc.SuperMarketId,
                              SuperMarketName = s.Name,
                              ProductId = sp.ProductId,
                              ProductName = p.Name,
                              ProductQty = sp.Quantity,
                              ProductPrice = pc.UnitPrice,
                              ProductValue = Convert.ToDecimal(sp.Quantity) * pc.UnitPrice
                          }).ToList();


            var total = (from sp in _db.ShoppingProducts
                         join pc in _db.ProductCatalogs on sp.ProductId equals pc.ProductId
                         join p in _db.Products on sp.ProductId equals p.Id
                         join s in _db.SuperMarkets on pc.SuperMarketId equals s.Id
                         where sp.ShoppingListId == shoppingListIdToBuy && s.ZipCode == zipcode
                         group new { s.Name, pc.UnitPrice, sp.Quantity } by new { s.Id } into t
                         select new SuperMarketViewModel
                         {
                             SuperMarketId = t.Key.Id,
                             SuperMarketName = t.Max(n => n.Name),
                             TotalItems = t.Count(),
                             TotalPrice = t.Sum(x => Convert.ToDecimal(x.Quantity) * x.UnitPrice),
                             ProductsList = result.Where(r => r.SuperMarketId == t.Key.Id)
                         }).ToList();

            return total;
        }

        /// <summary>
        /// Obtain a filtered List of Products of a given Shopping List
        /// </summary>
        /// <param name="id">Key of the Shopping List</param>
        /// <returns>List of Products of the given Shopping List</returns>
        public IList<ProductShoppingListViewModel> getProductsByShoppingList(int id) {

            var result = (from sp in _db.ShoppingProducts
                          join p in _db.Products on sp.ProductId equals p.Id
                          where sp.ShoppingListId == id
                          select new ProductShoppingListViewModel
                          {
                              ShoppingProductId = sp.Id,
                              ProductId = p.Id,
                              ProductName = p.Name,
                              ProductQty = sp.Quantity
                          }).ToList();

            return result;
        }
    }
}
