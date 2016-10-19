using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingSmartApp.Models;

namespace ShoppingSmartApp.Services
{
    public class ProductServices
    {
        private IGenericRepository _repo;

        public ProductServices(IGenericRepository repo) {
            this._repo = repo;
        }

        /// <summary>
        /// Based in Generic Repository produce a list of all Products in the Product entity
        /// </summary>
        /// <returns>IEnumerable of Product type</returns>
        public IEnumerable<Product> listProducts() {

            return _repo.Query<Product>();
        }

        /// <summary>
        /// Given an key Id search for a Product in the data Repository
        /// </summary>
        /// <param name="id">Key of the Product to find</param>
        /// <returns>An instance on Object of type Product</returns>
        public Product getProduct(int id) {

            return _repo.Query<Product>().FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// Create a new record of Product in the data repository
        /// </summary>
        /// <param name="producttocreate">A new Instance of Product object</param>
        public void createProduct(Product producttocreate) {

            _repo.Add<Product>(producttocreate);
        }

        /// <summary>
        /// Remove a record of the Product entity
        /// </summary>
        /// <param name="productToDelete">An instance of the Product to be deleted</param>
        public void deleteProduct(Product productToDelete) {

            _repo.Delete<Product>(productToDelete);
        }

        /// <summary>
        /// Modify the information for a Product
        /// </summary>
        /// <param name="producttoupdate">An instance of Product object with the changes</param>
        public void updateProduct(Product producttoupdate) {

            _repo.Update<Product>(producttoupdate);
        }
    }
}