using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Domain.EFModels
{
    [Index(nameof(Code) , IsUnique = true)]
    public class Brand :AuditableEntity
    {
        //[Key,Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //[Key,Column(Order = 2)]
        [Required, MaxLength(200)]
        public string Code { get; set; }

        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Image { get; set; }
        public bool IsTop { get; set; }


        #region Navigation Property

        //brand can be in many category
        public virtual ICollection<Product> Products { get; set; }

        //brand can be in many category
        public virtual ICollection<Category> Categories { get; set; }

        #endregion
    }
}
