using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class ServerHistory
    {
        public ServerHistory()
        {
            this.Create = DateTime.Now;
            this.IsRemoved = false;
        }
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ServerId { get; set; }
        public virtual Server Server { get; set; }
        public string description { get; set; }
        public DateTime Create { get; set; }
        public bool IsRemoved { get; set; }
        
    }
}