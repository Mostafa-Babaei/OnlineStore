using AutoMapper;
using Application.Model;
using Domain.Entities;
using infrastructure.Identity;
using infrastructure.Models;
using Domain.Models;

namespace infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Category Mapping

            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, InsertCategoryDto>().ReverseMap();
            CreateMap<Category, EditCategoryDto>().ReverseMap();

            #endregion

            #region Product mapping 

            CreateMap<Contact, ContactPageDto>().ReverseMap();
            CreateMap<Contact, InsertContactDto>().ReverseMap();
            CreateMap<Contact, CreateContactDto>().ReverseMap();

            #endregion


            #region Brand mapping 

            CreateMap<Brand, BrandDto>().ReverseMap();
            CreateMap<Brand, InsertBrandDto>().ReverseMap();
            CreateMap<Brand, EditBrandDto>().ReverseMap();

            #endregion

            CreateMap<ApplicationUser, EditUserDto>().ReverseMap();

            CreateMap<Discount, DiscountDto>().ReverseMap();
            CreateMap<Discount, InsertDiscountDto>().ReverseMap();
            CreateMap<Discount, EditDiscountDto>().ReverseMap();
            CreateMap<UseDiscount, InsertUseDiscountDto>().ReverseMap();
            CreateMap<UseDiscount, UseDiscount>().ReverseMap();

            CreateMap<ShopOrder, OrderDto>().ReverseMap();
            CreateMap<ShopOrder, InsertOrderDto>().ReverseMap();
            CreateMap<ShopOrder, EditOrderDto>().ReverseMap();

        }
    }

}
