using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Core.Entities
{
    public partial class Category
    {
        public Category()
        {
            BlogCategories = new HashSet<BlogCategory>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public virtual ICollection<BlogCategory> BlogCategories { get; set; }
    }
}
