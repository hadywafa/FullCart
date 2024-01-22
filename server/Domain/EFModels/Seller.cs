using System.Collections.Generic;
using Domain.Common;

namespace Domain.EFModels
{
    public class Seller :AuditableEntity
    {
        public string Id { get; set; }

        #region Navigation Propererty

        //1-1 Seller is a user
        public virtual ApplicationUser User { get; set; }

        // Each seller sells a collection of Products
        public virtual ICollection<Product> Products { get; set; }

        // 3-ternary relationship Customer review product of specific seller
        public virtual ICollection<CustProSellReviews> CustProSellReviews { get; set; }
        #endregion

    }
}