using Application.Common.ExtensionMethods;
using Application.Features.Products.Queries.GetProductDetailsById;
using Application.Features.Products.Queries.GetProductsBref;
using AutoMapper;
using Domain.EFModels;

namespace Application.Common.Profiles.CustomProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            // Product Details Dto
            CreateMap<Product, ProductDetailsDto>()
                .ForMember(dst => dst.Id, src => src.MapFrom(p => p.Id))
                .ForMember(dst => dst.ModelNumber, src => src.MapFrom(p => p.ModelNumber))
                .ForMember(dst => dst.Name, src => src.MapFrom(p => p.Name))
                .ForMember(dst => dst.NameArabic, src => src.MapFrom(p => p.NameArabic))
                .ForMember(dst => dst.Price, src => src.MapFrom(p => p.SellingPrice))
                .ForMember(dst => dst.Quantity, src => src.MapFrom(p => p.Quantity))
                .ForMember(dst => dst.Discount, src => src.MapFrom(p => p.Discount))
                .ForMember(dst => dst.Description, src => src.MapFrom(p => p.Description))
                .ForMember(dst => dst.ImageThumb, src => src.MapFrom(p => p.ImageThumb))
                .ForMember(dst => dst.ImagesGallery, src => src.MapFrom(p => p.ImagesGallery)) //*
                .ForMember(dst => dst.CategoryId, src => src.MapFrom(p => p.Category.Id))
                .ForMember(dst => dst.Highlights, src => src.MapFrom(p => p.ProductHighlights)) //*
                .ForMember(dst => dst.Specifications, src => src.MapFrom(p => p.Specifications)) //*
                .ForMember(dst => dst.Available, src => src.MapFrom(p => p.IsAvailable))
                .ForMember(dst => dst.BrandId, src => src.MapFrom(p => p.Brand.Id))
                .ForMember(dst => dst.BrandCode, src => src.MapFrom(p => p.Brand.Code))
                .ForMember(dst => dst.BrandName, src => src.MapFrom(p => p.Brand.Name))
                .ForMember(dst => dst.SellerId, src => src.MapFrom(p => p.Seller.Id))
                .ForMember(dst => dst.SellerName, src => src.MapFrom(p => p.Seller.User.FirstName))
                .ForMember(
                    dst => dst.MaxQuantityPerOrder,
                    src => src.MapFrom(p => p.MaxQuantityPerOrder)
                );

            // Product Bref Dto
            CreateMap<Product, ProductBrefDto>()
                .ForMember(dst => dst.Id, src => src.MapFrom(p => p.Id))
                .ForMember(dst => dst.Name, src => src.MapFrom(p => p.Name))
                .ForMember(dst => dst.NameAr, src => src.MapFrom(p => p.NameArabic))
                .ForMember(dst => dst.Description, src => src.MapFrom(p => p.Description))
                .ForMember(dst => dst.DescriptionAr, src => src.MapFrom(p => p.DescriptionArabic))
                .ForMember(dst => dst.Price, src => src.MapFrom(p => p.SellingPrice))
                .ForMember(dst => dst.Discount, src => src.MapFrom(p => p.Discount))
                .ForMember(dst => dst.ImageUrl, src => src.MapFrom(p => p.ImageThumb))
                .ForMember(dst => dst.IsFreeDelivered, src => src.MapFrom(p => p.IsFreeDelivered))
                .ForMember(dst => dst.Quantity, src => src.MapFrom(p => p.Quantity))
                .ReverseMap();

            // Product Highlights Dto
            CreateMap<ProductHighlights, ProductHighlightsDto>()
                .ForMember(tr => tr.Feature, src => src.MapFrom(h => h.Feature));

            // Product Specifications Dto
            CreateMap<ProductSpecifications, ProductSpecsDto>()
                .ForMember(tr => tr.Key, src => src.MapFrom(s => s.Key))
                .ForMember(tr => tr.Value, src => src.MapFrom(s => s.Name));
        }
    }
}
