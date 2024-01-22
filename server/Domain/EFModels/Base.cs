using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.EFModels
{
    public class Base
    {
        // identity auto generated
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}
