using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class ClientAttachment
    {
        public ClientAttachment()
        {
            this.Create = DateTime.Now;
            this.IsRemoved = false;
        }
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Description { get; set; }
        public string FilePath { get; set; }
        public DateTime Create { get; set; }
        public bool IsRemoved { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public Guid ClientId { get; set; }
        public virtual Client Client { get; set; }
    }
}