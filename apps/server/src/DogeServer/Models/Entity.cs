using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DogeServer.Models
{
    public abstract class Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid? ID { get; set; }

        public DateTime? Created { get; set; }
        public DateTime? Deleted { get; set; }
    }
}
