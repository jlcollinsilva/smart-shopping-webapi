using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoppingSmartApp.Models;
using ShoppingSmartApp.Services;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ShoppingSmartApp.API
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private ProductServices _productservices;

        public ProductsController(ProductServices productservices)
        {
            this._productservices = productservices;
        } 

        // GET: api/values
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return this._productservices.listProducts();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var product = this._productservices.getProduct(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // POST api/values
        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult Post([FromBody]Product product)
        {
            if (!ModelState.IsValid) {

                return BadRequest(ModelState); 
            }

            if (product.Id == 0) {

                this._productservices.createProduct(product);
            } else {

                this._productservices.updateProduct(product);
            }

            return Ok(product);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult Delete(int id)
        {
            var product = this._productservices.getProduct(id);

            if (product == null)
            {
                return NotFound();
            }

            _productservices.deleteProduct(product);
            return Ok();
        }
    }
}