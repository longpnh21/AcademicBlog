using System;
using System.Collections.Generic;

#nullable disable

namespace Core.Entities
{
    public partial class Category
    {
        public Category()
        {
            BlogCategories = new HashSet<BlogCategory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<BlogCategory> BlogCategories { get; set; }
    }
}
