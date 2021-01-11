using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class User
    {
        public User()
        {
            this.UserClients = new HashSet<UserClient>();
            this.JoinTime = DateTime.Now;
            this.IsRemoved = false;
        }
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public DateTime JoinTime { get; set; }
        public bool IsRemoved { get; set; }
        public virtual ICollection<UserClient> UserClients { get; set; }
        public Guid UserRoleId { get; set; }
        public virtual UserRole UserRole { get; set; }
    }
}