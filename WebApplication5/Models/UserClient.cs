using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class UserClient
    {

        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public Guid ClientId { get; set; }
        public virtual Client Client { get; set; }
    }
}