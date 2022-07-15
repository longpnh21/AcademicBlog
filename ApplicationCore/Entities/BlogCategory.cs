using System;
using System.Collections.Generic;

#nullable disable

namespace Core.Entities
{
    public partial class BlogCategory
    {
        public int BlogId { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Blog { get; set; }
        public virtual Category Category { get; set; }
    }
}
