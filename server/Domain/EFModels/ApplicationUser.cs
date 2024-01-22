using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Domain.EFModels
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "First Name is required")]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required"), MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public decimal Balance { get; set; }

        public string ImageProfile { get; set; }

        public DateTime CreatedAt { get; set; }

        #region Navigation Property
        public virtual Admin Admin { get; set; }
        public virtual Seller Seller { get; set; }
        public virtual Shipper Shipper { get; set; }
        public virtual Customer Customer { get; set; }
        //==================================================
        //1-m User have many addresses
        public virtual Collection<Address> Addresses { get; set; }

        ////1-m User have many Card
        public virtual Collection<Card> Cards { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        #endregion

        public ApplicationUser()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
