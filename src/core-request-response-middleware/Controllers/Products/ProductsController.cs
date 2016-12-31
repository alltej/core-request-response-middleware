using System;
using System.Linq;
using core_request_response_middleware.Services;
using Microsoft.AspNetCore.Mvc;

namespace core_request_response_middleware.Controllers.Products
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductServices _svc;

        public ProductsController(IProductServices productSvc)
        {
            _svc = productSvc;
            //_products = GetProductsRepository();
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_svc.GetProducts);
        }

        [HttpGet("active")]
        public IActionResult GetActive()
        {
            var products = _svc.GetProducts.ToList();
            return Ok(products.Where(p => p.IsActive));
        }


        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id > 99) //AT: This for exception
            {
                throw new ArgumentException("Out of Range");
            }
            var products = _svc.GetProducts.ToList();
            return Ok(products.FirstOrDefault(p => p.Id == id));
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
