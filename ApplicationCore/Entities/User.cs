using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

#nullable disable

namespace Core.Entities
{
    public partial class User : IdentityUser
    {
        public User()
        {
            BlogApprovers = new HashSet<Blog>();
            BlogCreators = new HashSet<Blog>();
            Comments = new HashSet<Comment>();
            Votes = new HashSet<Vote>();
        }

        public override string Id { get; set; } = Guid.NewGuid().ToString();
        public bool IsDeleted { get; set; }
        public string FullName { get; set; }

        public virtual ICollection<Blog> BlogApprovers { get; set; }
        public virtual ICollection<Blog> BlogCreators { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
    }
}
