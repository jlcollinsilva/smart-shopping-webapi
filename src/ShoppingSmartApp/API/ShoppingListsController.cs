using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingSmartApp.Models;
using ShoppingSmartApp.Services;
using ShoppingSmartApp.ViewModels;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ShoppingSmartApp.API
{
    [Route("api/[controller]")]
    public class ShoppingListsController : Controller
    {
        private ShoppingListServices _shoppinglistservices;

        public ShoppingListsController(ShoppingListServices shoppinglistservices)
        {
            _shoppinglistservices = shoppinglistservices;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<ShoppingList> Get()
        {
            return _shoppinglistservices.listShoppingList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ShoppingList Get(int id)
        {
            return _shoppinglistservices.getShoppingList(id);
        }

        // GET api/values/finddeal/5 Custom Get for to obtain a list of deals that match the customer Product List and Location (Zipcode)
        [HttpGet("findDeal/{id}/{zipcode}")]
       
        public IList<SuperMarketViewModel> findDeal([FromUri]int id, string zipcode)
        {

            return _shoppinglistservices.FindBestDeal(zipcode, id);

        }

        // GET api/values/getMyLists/john  Custom Get for to filter just the Shopping List of an user (consumer) given
        [HttpGet("getMyLists/{username}")]
        public IEnumerable<ShoppingList> getMyLists([FromUri]string username)
        {
            
            return _shoppinglistservices.listShoppingList().Where(sl => sl.Username == username);

        }

        // POST api/values
        [HttpPost]
        [Authorize(Policy = "ConsumerOnly")]
        public IActionResult Post([FromBody]ShoppingList shoppinglist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (shoppinglist.Id == 0)
            {
                _shoppinglistservices.createShoppingList(shoppinglist);
            }else
            {
                _shoppinglistservices.updateShoppingList(shoppinglist);
            }

            return Ok(shoppinglist);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
