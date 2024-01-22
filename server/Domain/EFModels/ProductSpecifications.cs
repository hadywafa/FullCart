using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.EFModels
{
    public class ProductSpecifications : AuditableEntity
    {
        // identity auto generated
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [MaxLength(1000)]
        public string Key { get; set; }

        [MaxLength(1000)]
        public string Name { get; set; }

        //each product have many
        public virtual Product Product { get; set; }
    }
}
