using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class OrdersContextInitializer : DropCreateDatabaseIfModelChanges<Context>
    {
        protected override void Seed(Context context)
        {
            User u = new User();
            u.Name = "admin";
            u.Email = "admin@admin.com";
            u.Password = "111111";
            context.Users.Add(u);
            context.SaveChanges();
        }
    }
}