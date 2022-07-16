using System.Collections.Generic;

#nullable disable

namespace Core.Entities
{
    public partial class Comment
    {
        public Comment()
        {
            InverseReference = new HashSet<Comment>();
        }

        public int Id { get; set; }
        public int BlogId { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public int? ReferenceId { get; set; }

        public virtual Blog Blog { get; set; }
        public virtual Comment Reference { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Comment> InverseReference { get; set; }
    }
}
