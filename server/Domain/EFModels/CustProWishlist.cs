using Domain.Common;

namespace Domain.EFModels
{
    public class CustProWishlist :AuditableEntity
    {

        #region Navigation Property
        
        // its not mapping its an implementation of [Customer  m => Wishlist <== m Product]
        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
        
        #endregion
        
    }
}
