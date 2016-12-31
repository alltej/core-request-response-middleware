using AutoMapper;
using core_request_response_middleware.Services;

namespace core_request_response_middleware.Controllers.Products
{
    public class ProductToModelMapper : Profile
    {
        public ProductToModelMapper()
        {
            CreateMap<Product, ProductModel>()
                .ForMember(dest => dest.Id,
                    opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name,
                    opts => opts.MapFrom(src => src.Name));
                //.ReverseMap();

        }
    }
}