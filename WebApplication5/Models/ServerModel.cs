using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class ServerModel
    {
        
        [Required]
        public string Name { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public DateTime DueTime { get; set; }
        public decimal PayMent { get; set; }
    }
}