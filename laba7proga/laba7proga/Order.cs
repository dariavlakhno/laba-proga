using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab7ORM.Entities
{
    [Table("Orders")]
    public class Order : BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string ProductName { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int ProductCount { get; set; }

        [ForeignKey("User")]
        public long UserId { get; set; }

        [Required]
        public virtual User User { get; set; }
    }
}