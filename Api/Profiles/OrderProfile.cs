using Api.Entities;
using Api.Models;
using AutoMapper;

namespace Api.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderViewModel>();
            CreateMap<Order, OrderInputModel>()
                .ForMember(dest => dest.SellerId, opt => 
                    opt.MapFrom(src => src.Seller.Id));
                                CreateMap<ProductOrder, ProductOrderInputModel>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.Id));
            CreateMap<OrderInputModel, Order>();
        }
    }
}