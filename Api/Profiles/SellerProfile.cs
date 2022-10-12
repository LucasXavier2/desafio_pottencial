using Api.Entities;
using Api.Models;
using AutoMapper;

namespace Api.Profiles
{
    public class SellerProfile : Profile
    {
        public SellerProfile()
        {
            CreateMap<SellerInputModel, Seller>();
            CreateMap<Seller, SellerViewModel>();
        }
    }
}