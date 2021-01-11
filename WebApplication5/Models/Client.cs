using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace WebApplication5.Models
{
    public class Client
    {
        public Client()
        {
            this.IsRemoved = false;
            this.Created = DateTime.Now;
            this.Servers =new HashSet<Server>();
            this.ClientAttachments = new HashSet<ClientAttachment>();
            this.UserClients = new HashSet<UserClient>();
        }
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNum { get; set; }
        public string Address { get; set; }
        public DateTime Created { get; set; }
        public bool IsRemoved { get; set; }
        public string FilePath { get; set; }
        [Required]
        public string Password { get; set; }
        public virtual ICollection<UserClient> UserClients { get; set; }
        public virtual ICollection<Server> Servers { get; set; }
        public virtual ICollection<ClientAttachment> ClientAttachments { get; set; }
    }
}