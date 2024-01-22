using System;
using Domain.Enums;

namespace Application.Features.Reviews.Queries.GetReviewsByProductId
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string CustomerName { get; set; }
        public string SellerName { get; set; }
        public Rate ProductRating { get; set; }
        public Rate SellerRating { get; set; }
        public string ProductComment { get; set; }
        public string SellerComment { get; set; }
        public bool IsProductCommentAnonymous { get; set; }
        public bool IsSellerCommentAnonymous { get; set; }
        public bool IsAsDescription { get; set; }
        public bool IsDeliveredOnTime { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
