using Api.Entities;
using Api.Models;
using AutoMapper;

namespace Api.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductInputModel, Product>();
            CreateMap<Product, ProductViewModel>();
            CreateMap<Product, ProductInputModel>();
        }
    }
}