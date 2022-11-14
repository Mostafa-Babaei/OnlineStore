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

            CreateMap<Contact, ContactPageDto>().ReverseMap();
            CreateMap<Contact, InsertContactDto>().ReverseMap();
            CreateMap<Contact, CreateContactDto>().ReverseMap();

            CreateMap<ApplicationUser, EditUserDto>().ReverseMap();

            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, InsertProductDto>().ReverseMap();
            CreateMap<Product, EditProductDto>().ReverseMap();

            CreateMap<Discount, DiscountDto>().ReverseMap();
            CreateMap<Discount, InsertDiscountDto>().ReverseMap();
            CreateMap<Discount, EditDiscountDto>().ReverseMap();
            CreateMap<UseDiscount, InsertUseDiscountDto>().ReverseMap();
            CreateMap<UseDiscount, UseDiscount>().ReverseMap();

            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Order, InsertOrderDto>().ReverseMap();
            CreateMap<Order, EditOrderDto>().ReverseMap();

            CreateMap<BankDto, EditBankDto>().ReverseMap();

        }
    }

}
