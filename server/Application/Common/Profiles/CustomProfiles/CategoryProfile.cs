using Application.Features.Categories.Queries.GetProductCategoriesTree;
using AutoMapper;
using Domain.EFModels;

namespace Application.Common.Profiles.CustomProfiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            //Source => target
            //then 
            // tr => src => x
            CreateMap<Category, CategoryDto>()
                .ForMember(tr => tr.Id, src => src.MapFrom(c => c.Id))
                .ForMember(tr => tr.Code, src => src.MapFrom(c => c.Code))
                .ForMember(tr => tr.Name, src => src.MapFrom(c => c.Name))
                .ForMember(tr => tr.NameAr, src => src.MapFrom(c => c.NameArabic))
                .ForMember(tr => tr.ParentId, src => src.MapFrom(c => c.ParentCatId));
            //.ForMember(tr => tr.Brands, src => src.MapFrom(c => c.Brands));
        }
    }
}
