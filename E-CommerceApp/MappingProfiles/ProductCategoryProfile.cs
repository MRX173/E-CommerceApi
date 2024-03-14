using AutoMapper;
using Domain.ProductAggregate;
using E_CommerceApp.Contracts.Product.Response;

namespace E_CommerceApp.MappingProfiles;

public sealed class ProductCategoryProfile : Profile
{
    public ProductCategoryProfile()
    {
        CreateMap<ProductCategory, CategoryResponse>()
            .ForMember(des => des.CategoryName,
                opt => opt
                    .MapFrom(src => src.Name));
    }
}