using Application.Features.CartItems.Queries.GetCartItems;
using Application.Features.WishlistItems.Queries.GetWishlistItems;
using AutoMapper;
using Domain.EFModels;

namespace Application.Common.Profiles.CustomProfiles
{
    public class WishlistItemConfig :Profile
    {
        public WishlistItemConfig()
        {
            CreateMap<CustProWishlist, WishlistItemDto>()
                .ForMember(dst => dst.WishlistItem, src => src.MapFrom(p => p.Product));
        }
    }
}
