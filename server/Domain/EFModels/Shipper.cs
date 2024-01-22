using System.Collections.Generic;
using Domain.Common;

namespace Domain.EFModels
{
    public class Shipper : AuditableEntity
    {
        public string Id { get; set; }

        #region Navigation Propererty

        //1-1 Shipper is a user
        public virtual ApplicationUser User { get; set; }

        //one shipper shipped many orders
        public ICollection<Order> Orders { get; set; }

        #endregion

    }
}