using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoppingSmartApp.Services;
using ShoppingSmartApp.Models;
using ShoppingSmartApp.ViewModels;
using System.Web.Http;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ShoppingSmartApp.API
{
    [Route("api/[controller]")]
    public class ProductCatalogsController : Controller
    {
        private ProductCatalogServices _productcatalogservices;

        public ProductCatalogsController(ProductCatalogServices productcatalogservices)
        {
            _productcatalogservices = productcatalogservices;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<ProductCatalog> Get()
        {

            return _productcatalogservices.listProductCatalog();
        } 

        // GET api/values/5
        [HttpGet("{id}")]
        public ProductCatalog Get(int id)
        {
            return _productcatalogservices.getProductCatalog(id);
        }

        // Custom GET filtering by Supermarket
        [HttpGet("getAllbySupermarket/{id}")]
        public IEnumerable<ProductsCatalogListViewModel> getAllbySupermarket(int id)
        {
            return _productcatalogservices.catalogBySupermarket(id);
        }
        
        // POST api/values
        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult Post([FromBody]ProductCatalog productcatalog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            if (productcatalog.Id == 0)
            {
                var resultValidationCatalog = this._productcatalogservices.existCatalog(productcatalog);

                if (resultValidationCatalog != "OK") {

                    this.ModelState.AddModelError("400", resultValidationCatalog);
                    return BadRequest(this.ModelState);
                }

                _productcatalogservices.createProductCatalog(productcatalog);

            }else
            {
                _productcatalogservices.updateProductCatalog(productcatalog);
            }

            return Ok(productcatalog);
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
            var productcatalog = _productcatalogservices.getProductCatalog(id);

            if (productcatalog == null)
            {
                return NotFound();
            }

            _productcatalogservices.deleteProductCatalog(productcatalog);

            return Ok(productcatalog);
        }
    }
}