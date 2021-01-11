using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class Context:DbContext
    {
        public Context() : base("conn") { }
        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Server> Servers { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<ClientAttachment> ClientAttachments { get; set; }
        public DbSet<UserClient> UserClients { get; set; }
        public DbSet<ServerPayment> ServerPayments { get; set; }
        public DbSet<ServerHistory> ServerHistories { get; set; }
    }
}