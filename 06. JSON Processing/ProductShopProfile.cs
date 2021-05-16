using AutoMapper;
using ProductShop.Dto;
using ProductShop.Models;
using System.Linq;

namespace ProductShop
{
    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            this.CreateMap<Product, ProductDetailsDto>();

            this.CreateMap<User, SoldProductsDto>()
                .ForMember(x => x.Count, y => y.MapFrom(x =>
                    x.ProductsSold.Count()))
                .ForMember(x => x.Products, y => y.MapFrom(x =>
                    x.ProductsSold));

            this.CreateMap<User, UserDetailsDto>()
                .ForMember(x => x.SoldProducts, y => y.MapFrom(x => x));

            this.CreateMap<UserDetailsDto[], UserInfoDto>()
                .ForMember(x => x.UsersCount, y => y.MapFrom(x => x.Length))
                .ForMember(x => x.Users, y => y.MapFrom(x => x));
        }
    }
}
