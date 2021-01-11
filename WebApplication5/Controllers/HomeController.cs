using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class HomeController : Controller
    {
        Context db = new Context();
        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var x = db.Users;
            var role = db.UserRoles.Where(r1 => r1.RoleName == "admin").ToList();
            if (role.Count() == 0)
            {
                UserRole r = new UserRole();
                r.RoleName = "admin";
                db.UserRoles.Add(r);
                db.SaveChanges();
            }

            var isAdmin = db.Users.Where(u => u.Name == "admin").ToList();
            if (isAdmin.Count()==0)
            { 
                var a = db.UserRoles.Where(r2 => r2.RoleName == "admin").FirstOrDefault();
                User u = new User();
                u.Name = "admin";
                u.Email = "admin@admin.com";
                u.Password = "111111";
                u.UserRoleId = a.Id;
                db.Users.Add(u);    
                db.SaveChanges();      
            }

            return Redirect(Url.Action("Login", "Home"));
        }

        //Admin
        public ActionResult Admin()
        {
            if (Session == null)
            {
                return Redirect(Url.Action("Login", "Home"));
            }
            var x = db.Users.Where(u=>u.IsRemoved==false).ToList();
            ViewBag.Clients = db.Clients.Where(c => c.IsRemoved == false).ToArray();
            return View(x);
        }
        /// <summary>
        /// Login
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserLoginModel user)
        {
            var x = db.Users.Where(u => u.Name == user.Name && u.Email == user.Email 
            && u.Password == user.Password).FirstOrDefault();
            if (x != null)
            {
                Session["LoginName"] = x.Email;
                Session["UserId"] = x.Id;
                if (x.UserRole.RoleName == "admin")
                {
                    return Redirect(Url.Action("Admin", "Home"));
                }
                else if(x.UserRole.RoleName=="HR")
                {
                    return Redirect(Url.Action("HR", "Home"));
                }
                else
                {
                    return Redirect(Url.Action("ClientManager", "Home"));
                }
                
            }
            else
            {
                return Redirect(Url.Action("Login","Home"));
            }
        }
        /// <summary>
        /// Reguster
        /// </summary>
        /// <returns></returns>
        //
        public ActionResult Reguster()
        {
            var x = db.UserRoles;
            ViewBag.role = new SelectList(db.UserRoles, "Id", "RoleName");
            return View();
        }
        [HttpPost]
        public ActionResult Reguster(UserRigusterModel user,Guid role)
        {
            var test = db.Users.Where(u => u.Email == user.Email).ToArray();
            if (test.Length==0)
            {
                User newUser = new User();
                newUser.Email = user.Email;
                newUser.Name = user.Name;
                newUser.Password = user.Password;
                newUser.UserRoleId = role;
                db.Users.Add(newUser);
                
                int r = db.SaveChanges();

                if (r > 0)
                {
                    return Redirect(Url.Action("Admin", "Home"));
                }
                else
                {
                    return Redirect(Url.Action("Reguster", "Home"));
                }
            }
            else
            {
                return Content("Email already existing...");
            }
        }
        /// <summary>
        /// Add Client
        /// </summary>
        /// <returns></returns>
        //
        public ActionResult AddClient()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddClient(ClientModel c)
        {
            var test = db.Clients.Where(client => client.Email == c.Email).ToArray();
            if (test.Length == 0)
            {
                int i = 1;
                string target = Server.MapPath("/") + "/ClientFiles/"+c.Name+"/";
                while(Directory.Exists(target))
                {
                    target += i.ToString();
                    i++;
                }
                DirectoryInfo path = new DirectoryInfo(target);
                
                Client newClient = new Client();
                newClient.Name = c.Name;
                newClient.Email = c.Email;
                newClient.PhoneNum = c.PhoneNum;
                newClient.Address = c.Address;
                newClient.FilePath = target;
                newClient.Password = c.Password;
                db.Clients.Add(newClient);
                int r = db.SaveChanges();
                if (r > 0)
                {
                    path.Create();
                    DirectoryInfo dir = new DirectoryInfo(target);
                    UserClient uc = new UserClient();
                    uc.ClientId= newClient.Id;
                    uc.UserId =(Guid) Session["UserId"];
                    db.UserClients.Add(uc);
                    db.SaveChanges();

                    return Redirect(Url.Action("ClientInfo", "Home",new{id=newClient.Id }));
                }
                else
                {
                    return Content("Some wrong here...");
                }
            }
            else
            {
                return Content("This Email Already Existing");
            }
        }
      
        /// <summary>
        /// GetStaff
        /// </summary>
        /// <returns></returns>
        public ActionResult GetStaff(Guid id)
        {
            var x=db.Users.Find(id);
            return View(x);
        }
        /// <summary>
        /// Edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditStaff(Guid id)
        {
            var x = db.Users.Find(id);
            return View(x);
        }
        [HttpPost]
        public ActionResult EditStaff(Guid id,User u)
        {
            var x = db.Users.Find(id);
            x.Name = u.Name;
            x.Password = u.Password;
            x.Email = u.Email;
            x.IsRemoved = u.IsRemoved;
            db.Users.AddOrUpdate(x);
            int r = db.SaveChanges();
            if(r>0)
            {
                return Redirect(Url.Action("Admin", "Home"));
            }
            return Content("some wrong here...");
        }
        /// <summary>
        /// delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteStaff(Guid id)
        {
            var x=db.Users.Find(id);
            x.IsRemoved = true;
            db.Users.AddOrUpdate();
            db.SaveChanges();
            return Redirect(Url.Action("Admin", "Home"));
        }

        /// <summary>
        /// DeleteClient
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteClient(Guid id)
        {
           var x= db.Clients.Find(id);
            x.IsRemoved = true;
            db.Clients.AddOrUpdate();
            db.SaveChanges();
            if ((string)Session["LoginName"] == "admin@admin.com")
            {
                return Redirect(Url.Action("Admin", "Home"));
            }
            return Redirect(Url.Action("Index", "Home"));
        }
        /// <summary>
        /// ////////////////////////
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ClientInfo(Guid id)
        {
            var x = db.Clients.Find(id);
            return View(x);
        }
        /// <summary>
        /// RemovedClient
        /// </summary>
        /// <returns></returns>
        public ActionResult RemovedClient()
        {
            var x=db.Clients.Where(c => c.IsRemoved == true);
            return View(x);
        }
        /// <summary>
        /// RemovedStaff
        /// </summary>
        /// <returns></returns>
        public ActionResult RemovedStaff()
        {
            var x = db.Users.Where(u => u.IsRemoved == true);
            return View(x);
        }

        public ActionResult BackClient(Guid id)
        {
            var x=db.Clients.Find(id);
            x.IsRemoved = false;
            db.Clients.AddOrUpdate(x);
            int r=db.SaveChanges();
            if(r>0)
            {
                return Redirect(Url.Action("Admin", "Home"));
            }
            return Content("Some wrong here...");
        }

        public ActionResult BackStaff(Guid id)
        {
            var x = db.Users.Find(id);
            x.IsRemoved = false;
            db.Users.AddOrUpdate(x);
            int r = db.SaveChanges();
            if (r > 0)
            {
                return Redirect(Url.Action("Admin", "Home"));
            }
            return Content("Some wrong here...");
        }


        public ActionResult Search(string Name)
        {
            var x=db.Clients.Where(c => c.Name == Name).FirstOrDefault();
            if(x!=null)
            {
                return Redirect(Url.Action("ClientInfo", "Home", new { id = x.Id }));
            }
            return Content("No such client");
        }

        public ActionResult AddRole()
        {
            var x = db.UserRoles;
            return View(x);
        }
        [HttpPost]
        public ActionResult AddRole(string role)
        {
            var x = db.UserRoles.Where(r1=>r1.RoleName==role);
            if(x.Count()==0)
            {
                UserRole newUserRole = new UserRole();
                newUserRole.RoleName = role;
                db.UserRoles.Add(newUserRole);
                int r = db.SaveChanges();
                if (r > 0)
                {
                    return View(x);
                }
            }
            
            return Content("Alread has this role...");
        }

        public ActionResult ClientManager()
        {
            if (Session == null)
            {
                return Redirect(Url.Action("Login", "Home"));
            }
            var x= db.Clients.Where(c => c.IsRemoved == false).ToArray();
            return View(x);
        }
        public ActionResult HR()
        {
            var x = db.Users;
            return View(x);
        }
        public ActionResult ClientLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ClientLogin(Client client)
        {
            var x = db.Clients.Where(c => c.Name == client.Name && c.Email == client.Email
            && c.Password == client.Password);
            if (x.ToList().Count() != 0)
            {
                Session["LoginName"] = x.FirstOrDefault().Email;
                Session["UserId"] = x.FirstOrDefault().Id;

                return Redirect(Url.Action("ClientPage", "Home",new { id = x.FirstOrDefault().Id }));
            }
            else
            {
                return Content("The user didn't exist...");
            }
        }

        public ActionResult ClientPage(Guid id)
        {
            var x = db.Clients.Find(id);
            if(x!=null)
            {
                return View(x);
            }
            return View();
        }

    }
}