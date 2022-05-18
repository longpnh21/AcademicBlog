#nullable disable

namespace Core.Entities
{
    public partial class BlogTag
    {
        public int BlogId { get; set; }
        public int TagId { get; set; }

        public virtual Blog Blog { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
