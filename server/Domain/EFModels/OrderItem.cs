using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.EFModels
{
    public class OrderItem : AuditableEntity
    {
        // identity auto generated
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public int Quantity { get; set; }

        public decimal Price => Product.SellingPrice * Quantity;

        public decimal Revenue => Product.Revenue * Quantity;


        #region Navigation Property

        // Each OrderItem is related to one order
        //[ForeignKey("Order")]
        //public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        // Each Order is considered as buying a one Product
        //[ForeignKey("product")]
        //public int ProductId { get; set; }
        public virtual Product Product { get; set; }


        #endregion
    }
}
