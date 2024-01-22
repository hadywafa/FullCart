using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Enums;

namespace Domain.EFModels
{
    public class Order : AuditableEntity
    {
        // identity auto generated
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        // Each Order is made by one Customer

        public Customer Customer { get; set; }

        public Shipper Shipper { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalPrice { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal Discount { get; set; }

        public DeliveryStatus DeliveryStatus { get; set; }

        public PaymentMethod PaymentMethod { get; set; }


        //DeliveryStatusDescription---Mohamed
        public string DeliveryStatusDescription { get; set; }

        // Each Order has a collection of Items
        public virtual ICollection<OrderItem> OrderItems { get; set; }

        //[ForeignKey("Address")]
        //public int AddressId { get; set; }
        public virtual Address Address { get; set; }

        public Order()
        {
            OrderDate = DateTime.Now;
            DeliveryStatus = DeliveryStatus.Processing;
            DeliveryStatusDescription = "Your Orders is placed, awaiting for shipping";
        }

        public void CalcTotalPrice()
        {
            foreach (var order in OrderItems)
            {
                TotalPrice += (order.Price-(order.Price * Discount));
                TotalRevenue += order.Revenue;
            }
        }
    }
}
