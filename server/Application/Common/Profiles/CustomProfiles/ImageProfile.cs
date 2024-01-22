using Application.Common.ExtensionMethods;
using Application.Common.Shared_Models;
using AutoMapper;
using Domain.EFModels;

namespace Application.Common.Profiles.CustomProfiles
{
    public class ImageProfile : Profile
    {
        public ImageProfile()
        {
            // target object  => source object
            CreateMap<Image, ImageDto>()
                .ForMember(tr => tr.Id, src => src.MapFrom(i => i.Id))
                .ForMember(tr => tr.ImageName, src => src.MapFrom(i => i.ImageName));
        }
    }
}
