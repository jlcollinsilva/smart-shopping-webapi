using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingSmartApp.Models;
using ShoppingSmartApp.ViewModels;

namespace ShoppingSmartApp.Services
{
    public class ProductCatalogServices
    {
        private IGenericRepository _repo;
        private ProductServices _productservices;

        public ProductCatalogServices(IGenericRepository repo) {
            this._repo = repo;
        }

        /// <summary>
        /// Based in Generic Repository produce a List of Products for a given Supermarket
        /// </summary>
        /// <param name="id">Key of the Supermarket </param>
        /// <returns>IEnumerabe with SuperMarketId,ProductId,ProductCatalogId,ProductName and ProductPrice</returns>
        public IEnumerable<ProductsCatalogListViewModel> catalogBySupermarket(int id) {

            return (from pc in _repo.Query<ProductCatalog>()
                    join p in _repo.Query<Product>()
                    on pc.ProductId equals p.Id
                    where pc.SuperMarketId == id
                    select new ProductsCatalogListViewModel
                    {
                        SuperMarketId = pc.SuperMarketId,
                        ProductId = pc.ProductId,
                        ProductCatalogId = pc.Id,
                        ProductName = p.Name,
                        ProductPrice = pc.UnitPrice
                    }).ToList();
    }
        /// <summary>
        /// Based in Generic Repository procude a List of all products in catalogs, a product can be in many catalogs, in those cases in this list the product will be duplicated. 
        /// </summary>
        /// <returns>IEnumerable of ProductCatalog</returns>
        public IEnumerable<ProductCatalog> listProductCatalog()
        {

            return _repo.Query<ProductCatalog>();
        }

        /// <summary>
        /// Based in Generic Repository find a tuple of Product-Catalog.
        /// </summary>
        /// <param name="id">Key of the Tuple</param>
        /// <returns>An object of ProductCatalog type</returns>
        public ProductCatalog getProductCatalog(int id) {

            return _repo.Query<ProductCatalog>().FirstOrDefault(pc => pc.Id == id);
        }

        /// <summary>
        /// Add a new record in the data repository for the entity ProductCatalog
        /// </summary>
        /// <param name="productcatalogToCreate">An instance of ProductCatalog object</param>
        public void createProductCatalog(ProductCatalog productcatalogToCreate) {

            _repo.Add<ProductCatalog>(productcatalogToCreate);
        }

        public void updateProductCatalog(ProductCatalog productcatalogToUpdate) {

            _repo.Update<ProductCatalog>(productcatalogToUpdate);
        }
        /// <summary>
        /// Remove a row from the data repository for the entity ProductCatalog
        /// </summary>
        /// <param name="productcatalogToDelete">An instance of ProductCatalog object</param>
        public void deleteProductCatalog(ProductCatalog productcatalogToDelete) {

            _repo.Delete<ProductCatalog>(productcatalogToDelete);

        }

        /// <summary>
        /// Verify if the tuple of ProductCatalog already exist in the data repository (ProductCatalog entity)
        /// </summary>
        /// <param name="productcatalog">An instance of ProductCatalog object</param>
        /// <returns>A string with the result explanation of the search, "OK" if Do not Exists</returns>
        public string existCatalog(ProductCatalog productcatalog) {

            var result = _repo.Query<ProductCatalog>().Where(pc => pc.ProductId == productcatalog.ProductId &&
                                                            pc.SuperMarketId == productcatalog.SuperMarketId);

            if (result.Count() == 0)
            {
                return "OK";

            }
            else {
                return "This Product Already exist in the Catalog";
            }  
        }
    }
}
