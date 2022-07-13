using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response
{
    public class VoteResponse
    {
        public string UserId { get; set; }
        public int BlogId { get; set; }
        public VoteType Type { get; set; }
    }
}
