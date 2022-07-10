using Core.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Core.Entities
{
    public partial class Blog
    {
        public Blog()
        {
            BlogCategories = new HashSet<BlogCategory>();
            BlogTags = new HashSet<BlogTag>();
            Comments = new HashSet<Comment>();
            Media = new HashSet<Media>();
            Votes = new HashSet<Vote>();
        }

        [Key]
        public int Id { get; set; }
        [MaxLength(4000)]
        [Required]
        public string Content { get; set; }
        [Required]
        public BlogStatus Status { get; set; }
        [Required]
        public string CreatorId { get; set; }
        public string? ApproverId { get; set; }

        public virtual User? Approver { get; set; }
        public virtual User Creator { get; set; }
        public virtual ICollection<BlogCategory> BlogCategories { get; set; }
        public virtual ICollection<BlogTag> BlogTags { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Media> Media { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
    }
}
