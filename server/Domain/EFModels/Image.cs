using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.EFModels
{
    public class Image : AuditableEntity, IHasDomainEvent
    {
        // identity auto generated
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }    
        
        public string ImageName { get; set; }

        public List<DomainEvent> DomainEvents { get; set; }
        
        #region Navigation Property

        [ForeignKey("Product")]
        public int? ProductId { get; set; }
        public virtual Product Product { get; set; }

        // Each ImageName is belonged to one Category
        [ForeignKey("Category")]
        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }

        #endregion

       
    }
}