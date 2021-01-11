using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace WebApplication5.Models
{
    public class Server
    {
        public Server()
        {
            this.IsRemoved = false;
            this.Create = DateTime.Now;
            this.IsPaied = false;
            this.IsFinished = false;
            this.ServerHistories = new HashSet<ServerHistory>();
            this.ServerPayments = new HashSet<ServerPayment>();
            this.ServerStatu = "Begining...";
        }
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Name { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        public decimal Price { get; set; }
        //public decimal AdvancePayment { get; set; }//
        public virtual ICollection<ServerPayment> ServerPayments { get; set; }
        public bool IsPaied { get; set; }
        public string ServerStatu { get; set; }

        public virtual ICollection<ServerHistory> ServerHistories { get; set; }

        public bool IsRemoved { get; set; }


        public bool IsFinished { get; set; }
        public DateTime Create { get; set; }
        public DateTime DueTime { get; set; }
        public Guid ClientId { get; set; }
        public virtual Client Client { get; set; }

    }
}