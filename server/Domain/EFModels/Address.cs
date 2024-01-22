using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.EFModels
{
    public class Address : AuditableEntity
    {
        // identity auto generated
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required, MinLength(3), MaxLength(100)]
        public string Street { get; set; }

        [Required, MinLength(3), MaxLength(100)]
        public string City { get; set; }

        // Egyptian postal code consists of 5 digits
        [Range(10000, 99999)]
        public int PostalCode { get; set; }

        public bool IsPrimary { get; set; }

        [Required]
        public string AddressPhone { get; set; }


        #region Navigation Property
        // Each address is belonged to one User
        public ApplicationUser User { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        #endregion

    }
}
