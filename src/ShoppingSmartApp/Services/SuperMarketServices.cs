using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingSmartApp.Models;


namespace ShoppingSmartApp.Services
{
    public class SuperMarketServices
    {
        private IGenericRepository _repo;

        public SuperMarketServices(IGenericRepository repo)
        {
            this._repo = repo;
        }

        /// <summary>
        /// Based in the Generic Repository produce a List of all the supermarkets in the data repository.
        /// </summary>
        /// <returns>IEnumerable of Supermaket objects</returns>
        public IEnumerable<SuperMarket> listSuperMarket() {

            return _repo.Query<SuperMarket>();
        }

        /// <summary>
        /// Add a new Supermarket in the data repository
        /// </summary>
        /// <param name="supermarketToCreate">An instance of a new Supermarket object</param>
        public void createSuperMarket(SuperMarket supermarketToCreate) {
            _repo.Add<SuperMarket>(supermarketToCreate);
        }

        /// <summary>
        /// Find an specific Supermarket
        /// </summary>
        /// <param name="id">Key of Supermarket</param>
        /// <returns>An instance of the Supermarket found</returns>
        public SuperMarket getSupermarket(int id)
        {
            return _repo.Query<SuperMarket>().FirstOrDefault(s => s.Id == id);
        }

        /// <summary>
        /// Modify the information for a specific Supermarket
        /// </summary>
        /// <param name="supermarketToUpdate">An instance of Supermarket object to change</param>
        public void updateSuperMarket(SuperMarket supermarketToUpdate) {

            _repo.Update<SuperMarket>(supermarketToUpdate);

        }

        /// <summary>
        /// Remove a Supermarket record from the data repository
        /// </summary>
        /// <param name="supermarketToDelete">An instance of Supermarket object to remove</param>
        public void deleteSuperMarket(SuperMarket supermarketToDelete) {

            _repo.Delete<SuperMarket>(supermarketToDelete);

        }  
    }
}
