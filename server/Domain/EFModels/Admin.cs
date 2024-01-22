using Domain.Common;

namespace Domain.EFModels
{
    public class Admin : AuditableEntity
    {
        public string Id { get; set; }

        #region Navigation Propererty

        //1-1 Admin is a user
        public virtual ApplicationUser User { get; set; }

        #endregion

    }
}