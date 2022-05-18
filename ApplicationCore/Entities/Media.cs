#nullable disable

using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public partial class Media
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int BlogId { get; set; }
        [Required]
        public string Link { get; set; }

        public virtual Blog Blog { get; set; }
    }
}
