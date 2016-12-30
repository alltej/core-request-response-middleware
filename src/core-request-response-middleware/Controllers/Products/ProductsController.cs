using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace core_request_response_middleware.Controllers.Products
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly List<Product> _products;

        public ProductsController()
        {
            _products = GetProductsRepository();
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_products);
        }

        private static List<Product> GetProductsRepository()
        {
            var results = new List<Product>()
            {
                new Product() {Id = 1, Name = "ABC"},
                new Product() {Id = 2, Name = "DEF"},
                new Product() {Id = 3, Name = "XYZ"},
            };
            return results;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id > 99)
            {
                throw new ArgumentException("Out of Range");
            }
            return Ok(_products.Find(p => p.Id == id));
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
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
