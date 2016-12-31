using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using core_request_response_middleware.Extensions;
using core_request_response_middleware.Services;
using Microsoft.AspNetCore.Mvc;

namespace core_request_response_middleware.Controllers.Products
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductServices _svc;
        private readonly IMapper _mapper;

        public ProductsController(IProductServices productSvc, IMapper mapper)
        {
            _svc = productSvc;
            _mapper = mapper;
            //_products = GetProductsRepository();
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            var products = _svc.GetProducts;
            var list = products.MapTo<List<ProductModel>>();
            return Ok(list);
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
            var product = products.FirstOrDefault(p => p.Id == id);
            return Ok(product.MapTo<ProductModel>());
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
