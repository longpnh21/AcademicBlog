using Core.Enums;

#nullable disable

namespace Core.Entities
{
    public partial class Vote
    {
        public string UserId { get; set; }
        public int BlogId { get; set; }
        public VoteType Type { get; set; }

        public virtual Blog Blog { get; set; }
        public virtual User User { get; set; }
    }
}
