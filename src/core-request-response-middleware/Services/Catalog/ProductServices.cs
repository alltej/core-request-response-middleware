using System.Collections.Generic;

namespace core_request_response_middleware.Services
{
    public interface IProductServices
    {
        IEnumerable<Product> GetProducts { get; }

    }

    public class ProductServices: IProductServices
    {
        public IEnumerable<Product> GetProducts => new List<Product>()
        {
            new Product() {Id = 1, Name = "Product 1", IsActive = true },
            new Product() {Id = 2, Name = "Product 2", IsActive = true} ,
            new Product() {Id = 3, Name = "Product 3", IsActive = true} ,
            new Product() {Id = 4, Name = "Product 4 - (Discontinued)", IsActive = false }
        };
    }
}