using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoppingSmartApp.Models;
using ShoppingSmartApp.Services;
using ShoppingSmartApp.ViewModels;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ShoppingSmartApp.API
{
    [Route("api/[controller]")]
    public class ShoppingProductsController : Controller
    {
        private ShoppingProductServices _shoppingproductservices;
        

        public ShoppingProductsController(ShoppingProductServices shoppingproductservices)
        {
            _shoppingproductservices = shoppingproductservices;
          
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<ShoppingProduct> Get()
        {
            return _shoppingproductservices.listShoppingProduct();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ShoppingProduct Get(int id)
        {
            return _shoppingproductservices.getShoppingProduct(id);
            
        }

        // GET api/values/5 - Custom Get for to obtain just the products of a given Shopping List
        [HttpGet("getProductsByShoppingList/{id}")]
        public IEnumerable<ProductShoppingListViewModel> getProductsByShoppingList(int id)
        {
            return _shoppingproductservices.productsByShoppingList(id);

        }


        // POST api/values
        [HttpPost]
        [Authorize(Policy = "ConsumerOnly")]
        public IActionResult Post([FromBody]ShoppingProduct shoppingproduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (shoppingproduct.Id == 0)
            {
                var validateShoppingProduct = _shoppingproductservices.existsShopping(shoppingproduct);

                if (validateShoppingProduct == "OK")
                {
                    _shoppingproductservices.createShoppingProduct(shoppingproduct);

                }
                else
                {
                    this.ModelState.AddModelError("400", validateShoppingProduct);
                    return BadRequest(ModelState);
                }

            }else
            {
                _shoppingproductservices.updateShoppingProduct(shoppingproduct);
            }

            return Ok(shoppingproduct);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "ConsumerOnly")]
        public IActionResult Delete(int id)
        {
            var shoppingproduct = _shoppingproductservices.getShoppingProduct(id);

            if (shoppingproduct == null)
            {
                return NotFound();
            }

            _shoppingproductservices.deleteShoppingProduct(shoppingproduct);

            return Ok(shoppingproduct);
        }
    }
}