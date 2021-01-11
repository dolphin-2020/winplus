using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class UserRole
    {
       public UserRole()
        {
            IsRemoved = false;
            Create = DateTime.Now;
            this.Users = new HashSet<User>();
        }
        public Guid Id { get; set; } = Guid.NewGuid();

        public string RoleName { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime Create { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}