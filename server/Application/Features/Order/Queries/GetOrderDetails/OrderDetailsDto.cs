using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Application.Features.Order.Queries.GetOrderDetails
{
    internal class OrderDetailsDto
    {
        public int Id { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
        public string DeliveryStatusDescription { get; set; }
        public decimal TotalPrice { get; set; }
        public ICollection<OrderItemDto> Products { get; set; }
    }
}
