using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Products.Queries.GetProductsBref;

namespace Application.Features.Order.Queries.GetOrderDetails
{
    internal class OrderItemDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public  decimal Price  { get; set; }
        public  ProductBrefDto Product { get; set; }
    }
}
