using System;
using System.Collections.Generic;

#nullable disable

namespace Core.Entities
{
    public partial class Tag
    {
        public Tag()
        {
            BlogTags = new HashSet<BlogTag>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<BlogTag> BlogTags { get; set; }
    }
}
