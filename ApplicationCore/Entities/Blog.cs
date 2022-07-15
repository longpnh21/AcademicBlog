using Core.Enums;
using System;
using System.Collections.Generic;

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

        public int Id { get; set; }
        public string Content { get; set; }
        public BlogStatus Status { get; set; }
        public string CreatorId { get; set; }
        public string ApproverId { get; set; }
        public DateTime ModifiedTime { get; set; }

        public virtual User Approver { get; set; }
        public virtual User Creator { get; set; }
        public virtual ICollection<BlogCategory> BlogCategories { get; set; }
        public virtual ICollection<BlogTag> BlogTags { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Media> Media { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
    }
}
