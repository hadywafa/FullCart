using Application.Common.ExtensionMethods;
using Application.Features.Brands.Queries.GetAllBrands;
using AutoMapper;
using Domain.EFModels;

namespace Application.Common.Profiles.CustomProfiles
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            // target object  => source object
            CreateMap<Brand, BrandDto>()
                .ForMember(dst => dst.Id, src => src.MapFrom(p => p.Id))
                .ForMember(dst => dst.Code, src => src.MapFrom(p => p.Code))
                .ForMember(dst => dst.Name, src => src.MapFrom(p => p.Name))
                .ForMember(dst => dst.Image, src => src.MapFrom(p => p.Image))
                .ForMember(dst => dst.IsTop, src => src.MapFrom(p => p.IsTop));
        }
    }
}
