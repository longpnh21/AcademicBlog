#nullable disable

namespace Core.Entities
{
    public partial class Media
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public string Link { get; set; }

        public virtual Blog Blog { get; set; }
    }
}
