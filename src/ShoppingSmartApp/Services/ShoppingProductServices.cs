using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingSmartApp.Models;
using ShoppingSmartApp.ViewModels;

namespace ShoppingSmartApp.Services
{
    public class ShoppingProductServices
    {
        private IGenericRepository _repo;
        private IRepository _repository;

        public ShoppingProductServices(IGenericRepository repo, IRepository repository) {
            _repo = repo;
            _repository = repository;
        }

        /// <summary>
        /// Based in the Generic Repository produce a List of all the tuples of Products in ShoppingList
        /// </summary>
        /// <returns>IEnumerable of ShoppingProduct type</returns>
        public IEnumerable<ShoppingProduct> listShoppingProduct()
        {
            return _repo.Query<ShoppingProduct>();
        }

        /// <summary>
        /// Find an specific tuple of Product-ShoppingList
        /// </summary>
        /// <param name="id">Key of the Tuple</param>
        /// <returns>An instance of ShoppingProduct found</returns>
        public ShoppingProduct getShoppingProduct(int id) {

            return _repo.Query<ShoppingProduct>().FirstOrDefault(sp => sp.Id == id);

        }

        /// <summary>
        /// Insert a new record in the data repository for the ShoppingProduct entity
        /// </summary>
        /// <param name="shoppingproductToCreate">An instance of a new ShoppingProduct object</param>
        public void createShoppingProduct(ShoppingProduct shoppingproductToCreate) {

            _repo.Add(shoppingproductToCreate);

        }

        /// <summary>
        /// Modify a record for the ShoppingProduct entity
        /// </summary>
        /// <param name="shoppingproductToUpdate">An instance of ShoppingProduct object</param>
        public void updateShoppingProduct(ShoppingProduct shoppingproductToUpdate)
        {
            _repo.Update<ShoppingProduct>(shoppingproductToUpdate);

        }

        /// <summary>
        /// Remove a record for the ShoppingProduct entity
        /// </summary>
        /// <param name="shoppingproductToDelete">An instance of ShoppingProduct object</param>
        public void deleteShoppingProduct(ShoppingProduct shoppingproductToDelete)
        {

            _repo.Delete<ShoppingProduct>(shoppingproductToDelete);
        }

        /// <summary>
        /// Verifiy if an specific tuple of Product-ShoppingList already exists in the data repository
        /// </summary>
        /// <param name="shoppingproduct">An instance of ShoppingProduct object to verify</param>
        /// <returns>A string with the result explanation, "OK" mean Do Not Exists</returns>
        public string existsShopping(ShoppingProduct shoppingproduct) {

            var result = _repo.Query<ShoppingProduct>().Where(sp => sp.ProductId == shoppingproduct.ProductId
                                                        && sp.ShoppingListId == shoppingproduct.ShoppingListId);
            if (result.Count() == 0)
            {
                return "OK";
            }
            else {
                return "This Product already exist in this Shopping List";
            }

        }

        /// <summary>
        /// Produce a List with all the products of a specific Shopping List
        /// </summary>
        /// <param name="id">Key of ShoppingList to Find</param>
        /// <returns>IEnumerable of ProductShoppingListViewModel objects</returns>
        public IEnumerable<ProductShoppingListViewModel> productsByShoppingList(int id)
        {
            return _repository.getProductsByShoppingList(id);

        }
    }
}
