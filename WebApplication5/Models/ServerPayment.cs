using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class ServerPayment
    {
        public ServerPayment()
        {
            this.Create = DateTime.Now;
            this.IsRemoved = false;
        }
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Create { get; set; }
        public decimal Payment { get; set; }
        public Guid ServerId { get; set; }
        public bool IsRemoved { get; set; }
        public virtual Server Server { get; set; }
    }
}