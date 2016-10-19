using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoppingSmartApp.Data;
using ShoppingSmartApp.Models;
using ShoppingSmartApp.Services;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ShoppingSmartApp.API
{
    [Route("api/[controller]")]
    public class SuperMarketsController : Controller
    {

        private SuperMarketServices _supermarketservices;

        public SuperMarketsController(SuperMarketServices supermarketservices) {
            this._supermarketservices = supermarketservices;
        }


        // GET: api/values
        [HttpGet]
        public IEnumerable<SuperMarket> Get()
        {
            // return _db.SuperMarkets.ToList();
            return _supermarketservices.listSuperMarket();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var supermarket = _supermarketservices.getSupermarket(id);

            if (supermarket == null)
            {
                return NotFound();
            }

            return Ok(supermarket);
        }

        // POST api/values
        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult Post([FromBody]SuperMarket supermarket)
        {
            if (!ModelState.IsValid) {
                return BadRequest(this.ModelState);
            }

            if (supermarket.Id == 0)
            {
                _supermarketservices.createSuperMarket(supermarket);
             
            }
            else {

                _supermarketservices.updateSuperMarket(supermarket);
            }

            return Ok(supermarket);
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
            var supermarket = _supermarketservices.getSupermarket(id);

            if (supermarket == null)
            {
                return NotFound();
            }

            _supermarketservices.deleteSuperMarket(supermarket);
            return Ok(supermarket);
        }
    }
}
