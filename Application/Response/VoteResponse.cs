using Core.Enums;

namespace Application.Response
{
    public class VoteResponse
    {
        public string UserId { get; set; }
        public int BlogId { get; set; }
        public VoteType Type { get; set; }
    }
}
