using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingSmartApp.Models;
using ShoppingSmartApp.ViewModels;

namespace ShoppingSmartApp.Services
{
    public class ShoppingListServices
    {
        private IGenericRepository _repo;
        private IRepository _repository;

        public ShoppingListServices(IGenericRepository repo, IRepository repository)
        {
            _repo = repo;
            _repository = repository;

        }

        /// <summary>
        /// Based in Generic Repository produce a List of all Shopping List
        /// </summary>
        /// <returns>IEnumerable of ShoppingList type</returns>
        public IEnumerable<ShoppingList> listShoppingList()
        {
            return _repo.Query<ShoppingList>();
        }

        /// <summary>
        /// Search for a specific Shopping List
        /// </summary>
        /// <param name="id">Key of ShoppingList</param>
        /// <returns>An instance of ShoppingList object</returns>
        public ShoppingList getShoppingList(int id)
        {
            return _repo.Query<ShoppingList>().FirstOrDefault(sl => sl.Id == id);
        }

        /// <summary>
        /// Create a new Shipping List record in the data repository
        /// </summary>
        /// <param name="shoppinglistToCreate">An instance of new ShoppingList object</param>
        public void createShoppingList(ShoppingList shoppinglistToCreate)
        {
            _repo.Add<ShoppingList>(shoppinglistToCreate);
        }

        /// <summary>
        /// Modify a Shopping List row in the data repository
        /// </summary>
        /// <param name="shoppinglistToUpdate">An instance of ShoppingList object to be changed</param>
        public void updateShoppingList(ShoppingList shoppinglistToUpdate)
        {
            _repo.Update<ShoppingList>(shoppinglistToUpdate);
        }

        /// <summary>
        /// Remove a row of ShoppingList from the data Repository
        /// </summary>
        /// <param name="shoppinglistToDelete">An instance of ShoppingList object to be deleted</param>
        public void deleteShoppingList(ShoppingList shoppinglistToDelete)
        {
            _repo.Delete<ShoppingList>(shoppinglistToDelete);
        }


        /// <summary>
        /// Using several database tables produce a result with a summary of the Supermarket information and details of their products.
        /// </summary>
        /// <param name="shoppingListIdToBuy">Key of the Shopping List to Match in the search</param>
        /// <param name="zipcode">Location identify by 5 digit Zip Code for to match in the search</param>
        /// <returns>Return IList<SuperMarketViewModel> a List of both: Supermarket summary and Products details</returns>
        public IList<SuperMarketViewModel> FindBestDeal(string zipcode,int shoppinglistToBuyId) 
        {
            return _repository.FindCatalog(shoppinglistToBuyId,zipcode);

           }
        }
    }