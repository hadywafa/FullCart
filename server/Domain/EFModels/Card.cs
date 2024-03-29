﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.EFModels
{
    public class Card : AuditableEntity
    {
        // identity auto generated
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required, MinLength(3), MaxLength(50)]
        public string NameOnCard { get; set; }

        // All Cards are only 16 digits
        [Required, MinLength(16), MaxLength(16)]
        public string CardNumber { get; set;}

        [Required, Range(1,12)]
        public int Month { get; set; }

        [Required]
        public int Year { get; set; }

        #region Navigation Property
        // Each card is belonged to one customer
        public ApplicationUser User { get; set; }
        #endregion
    }
}
