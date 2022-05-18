using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Core.Entities
{
    public partial class Comment
    {
        public Comment()
        {
            InverseReference = new HashSet<Comment>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public int BlogId { get; set; }
        [MaxLength(1000)]
        [Required]
        public string Content { get; set; }
        [Required]
        public string UserId { get; set; }
        public int? ReferenceId { get; set; }

        public virtual Blog Blog { get; set; }
        public virtual Comment Reference { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Comment> InverseReference { get; set; }
    }
}
