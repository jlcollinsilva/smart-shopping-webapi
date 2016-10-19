using System.Collections.Generic;
using ShoppingSmartApp.ViewModels;

namespace ShoppingSmartApp.Models
{
    public interface IRepository
    {
        IList<SuperMarketViewModel> FindCatalog(int shoppingListIdToBuy, string zipcode);
        IList<ProductShoppingListViewModel> getProductsByShoppingList(int id);
    }
}