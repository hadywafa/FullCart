﻿using Application.Features.Reviews.Queries.GetReviewsByProductId;
using AutoMapper;
using Domain.EFModels;

namespace Application.Common.Profiles.CustomProfiles
{
    public class ReviewProfile :Profile
    {
        public ReviewProfile()
        {
            CreateMap<CustProSellReviews, ReviewDto>()
                .ForMember(tr => tr.Id, src => src.MapFrom(r => r.Id))
                .ForMember(tr => tr.ProductId, src => src.MapFrom(r => r.Product.Id))
                .ForMember(tr => tr.CustomerName, src => src.MapFrom(r => r.Customer.User.FirstName))
                .ForMember(tr => tr.SellerName, src => src.MapFrom(r => r.Seller.User.FirstName))
                .ForMember(tr => tr.ProductRating, src => src.MapFrom(r => r.ProductRate))
                .ForMember(tr => tr.SellerRating, src => src.MapFrom(r => r.SellerRate))
                .ForMember(tr => tr.ProductComment, src => src.MapFrom(r => r.ProductComment))
                .ForMember(tr => tr.SellerComment, src => src.MapFrom(r => r.SellerComment))
                .ForMember(tr => tr.IsProductCommentAnonymous, src => src.MapFrom(r => r.IsProductCommentAnonymous))
                .ForMember(tr => tr.IsSellerCommentAnonymous, src => src.MapFrom(r => r.IsSellerCommentAnonymous))
                .ForMember(tr => tr.IsAsDescription, src => src.MapFrom(r => r.IsAsDescription))
                .ForMember(tr => tr.IsDeliveredOnTime, src => src.MapFrom(r => r.IsDeliveredOnTime))
                .ForMember(tr => tr.CreatedAt, src => src.MapFrom(r => r.ReviewDate));
        }
    }
}
