using Domain.Common;

namespace Domain.EFModels
{
    public class CustProCart : AuditableEntity
    {
        public int Quantity { get; set; }

        #region Navigation Property
        
        // its not mapping its an implementation of [Customer  m => Wishlist <== m Product]
        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
        
        #endregion
    }
}
