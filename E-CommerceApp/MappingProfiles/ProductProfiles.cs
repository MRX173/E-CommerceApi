using Application.Products.Commands;
using AutoMapper;
using Domain.ProductAggregate.Entities;
using E_CommerceApp.Contracts.Product.Request;
using E_CommerceApp.Contracts.Product.Response;

namespace E_CommerceApp.MappingProfiles;

public sealed class ProductProfiles : Profile
{
    public ProductProfiles()
    {
        CreateMap<Product, ProductResponse>()
            .ForMember(des => des.Stock,
                opt =>
                    opt.MapFrom(src => src.Inventory.Quantity))
            .ForMember(des => des.Currency,
                opt =>
                    opt.MapFrom(src => src.Price.Currency))
            .ForMember(des => des.Price,
                opt =>
                    opt.MapFrom(src => src.Price.Value))
            .ForMember(des => des.SkuCode,
                opt =>
                    opt.MapFrom(src => src.Sku.Code));
        CreateMap<ProductCreate, CreateProductCommand>();
        CreateMap<ProductUpdate, UpdateProductCommand>();
    }
}