using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab7ORM.Entities
{
    [Table("Users")]
    public class User : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        [Index(IsUnique = true)]
        public string Username { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}