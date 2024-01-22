using Application.Features.CartItems.Queries.GetCartItems;
using AutoMapper;
using Domain.EFModels;

namespace Application.Common.Profiles.CustomProfiles
{
    public class CartItemProfile : Profile
    {
        public CartItemProfile()
        {
            CreateMap<CustProCart, CartItemDto>()
                .ForMember(tr => tr.Quantity, src => src.MapFrom(c => c.Quantity))
                .ForMember(tr => tr.ProductBref, src => src.MapFrom(c => c.Product));
        }
    }
}
