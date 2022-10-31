using Api.Entities;
using Api.Models;
using AutoMapper;

namespace Api.Profiles
{
    public class ProductOrderProfile : Profile
    {
        public ProductOrderProfile()
        {
            CreateMap<ProductOrder, ProductOrderViewModel>();
            CreateMap<ProductOrder, ProductOrderInputModel>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.Id));
            CreateMap<ProductOrderInputModel, ProductOrder>();
        }
    }
}