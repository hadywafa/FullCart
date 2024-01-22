using Application.Features.Order.Queries.GetOrderDetails;
using AutoMapper;
using Domain.EFModels;

namespace Application.Common.Profiles.CustomProfiles
{
    public class OrderConfig : Profile
    {
        public OrderConfig()
        {
            //Order Detail 
            // target object  => source object
            CreateMap<Order, OrderDetailsDto>()
                .ForMember(tr => tr.Id, src => src.MapFrom(i => i.Id))
                .ForMember(tr => tr.DeliveryStatus, src => src.MapFrom(i => i.DeliveryStatus))
                .ForMember(tr => tr.DeliveryStatusDescription, src => src.MapFrom(i => i.DeliveryStatusDescription))
                .ForMember(tr => tr.TotalPrice, src => src.MapFrom(i => i.TotalPrice))
                .ForMember(tr => tr.Products, src => src.MapFrom(i => i.OrderItems));

            //Order Item 
            // target object  => source object
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(tr => tr.Id, src => src.MapFrom(i => i.Id))
                .ForMember(tr => tr.Product, src => src.MapFrom(i => i.Product))
                .ForMember(tr => tr.Quantity, src => src.MapFrom(i => i.Quantity))
                .ForMember(tr => tr.Price, src => src.MapFrom(i => i.Price));
        }
    }
}
