using Core.Enums;
using System.Collections.Generic;

namespace Application.Response
{
    public class BlogResponse
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public BlogStatus Status { get; set; }
        public string CreatorId { get; set; }
        public string ApproverId { get; set; }
        public IEnumerable<MediaResponse> Media { get; set; }
    }
}
