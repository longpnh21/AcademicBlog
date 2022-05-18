using Core.Enums;

namespace Application.Response
{
    public class BlogResponse
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public BlogStatus Status { get; set; }
        public string CreatorId { get; set; }
        public string ApproverId { get; set; }
    }
}
