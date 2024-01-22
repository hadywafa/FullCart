using System.Collections.Generic;
using Domain.Common;

namespace Domain.EFModels
{
    public class Customer : AuditableEntity
    {
        public string Id { get; set; }

        #region Navigation Propererty

        //1-1 customer is a user
        public virtual ApplicationUser User { get; set; }

        // Each Customer has a collection of OrderSummaries
        public virtual ICollection<Order> Orders { get; set; }

        // 3-ternary relationship Customer review product of specific seller
        public virtual ICollection<CustProSellReviews> CustProSellReviews { get; set; }

        // its not mapping its an implementation of [Customer  m => Wishlist <== m Product]
        public virtual ICollection<CustProWishlist> CustProWishlist { get; set; }

        // its not mapping its an implementation of [Customer  m => Cart <== m Product]
        public virtual ICollection<CustProCart> CustProCart { get; set; }

        #endregion
    }
}