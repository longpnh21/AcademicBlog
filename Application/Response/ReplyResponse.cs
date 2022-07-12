using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response
{
    public class ReplyResponse
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
    }
}
