using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
   
    public class ServerController : Controller
    {
        Context db = new Context();
        // GET: Server
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddServer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddServer(ServerModel sm,Guid id)
        {
            Server newserver = new Server();
            newserver.Name = sm.Name;
            newserver.description = sm.description;
            newserver.Price = sm.Price;
            newserver.DueTime = sm.DueTime;
            newserver.ClientId = id;
            db.Servers.Add(newserver);
            int r= db.SaveChanges();
            
            if(r>0)
            {
                ServerPayment p = new ServerPayment();
                p.Payment = sm.PayMent;
                p.ServerId = newserver.Id;
                db.ServerPayments.Add(p);

                db.SaveChanges();
                return Redirect(Url.Action("ClientInfo", "Home", new { id = id }));
            }
            return Content("Some wrong here...");
        }

        public ActionResult PayMent(Guid clientId, Guid id,string payment)
        {
            ServerPayment p = new ServerPayment();
            p.ServerId = id;
            p.Payment = decimal.Parse(payment);//
            db.ServerPayments.Add(p);
           int r= db.SaveChanges();
            if(r>0)
            {

                return Redirect(Url.Action("ClientInfo", "Home", new { id = clientId }));
            }
            return Content("Some wrong here..."); 
        }
        /// <summary>
        /// Change status
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangeStatu()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangeStatu(Guid id,Guid clientId, string  statu)
        {
           var x= db.Servers.Find(id);
            x.ServerStatu = statu;
            db.Servers.AddOrUpdate(x);
           int r= db.SaveChanges();
            if(r>0)
            {
                return Redirect(Url.Action("ClientInfo", "Home", new { id = clientId }));
            }
            return Content("Some wrong here...");
        }
        /// <summary>
        /// Change client information
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ChangdeClientInfo(Guid id)
        {
            var x=  db.Clients.Find(id);
            return View(x);
        }
        [HttpPost]
        public ActionResult ChangdeClientInfo(Guid id,ClientModel c)
        {
            var x = db.Clients.Find(id);
            x.Address = c.Address;
            x.Email = c.Email;
            x.PhoneNum = c.PhoneNum;
            x.Name = c.Name;
            x.Password = c.Password;
            db.Clients.AddOrUpdate(x);
            int r=db.SaveChanges();
            ViewBag.client = x;
            if (r>0)
            {
                return Redirect(Url.Action("ClientInfo", "Home", new { id = id }));
            }
            return Content("Some wrong here..."); 
        }
        /// <summary>
        /// Upload file
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UploadFile(Guid id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadFile(FormCollection form, Guid id)
        {
            var x = db.Clients.Find(id);

            if (Request.Files.Count == 0)
            {
                //Request.Files.Count 文件数为0上传不成功
                return View();
            }
            var file = Request.Files[0];
            if (file.ContentLength == 0)
            {
                //文件大小大（以字节为单位）为0时，做一些操作
                return Content("File is zero length");
            }
            else
            {
                //文件大小不为0
                file = Request.Files[0];
                //保存成自己的文件全路径,newfile就是你上传后保存的文件,
                //服务器上的UpLoadFile文件夹必须有读写权限
               //string target = Server.MapPath("/")+ ("/ClientFiles/");//取得目标文件夹的路径
               
                string filename = file.FileName;//取得文件名字
                string path = x.FilePath + filename;//获取存储的目标地址
                if( System.IO.File.Exists(path))
                {
                    return Content("The file already existing...");
                }
                file.SaveAs(path);
                return Redirect(Url.Action("ClientInfo", "Home", new { id = id }));
            }
        }
        /// <summary>
        /// Show files
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowFiles(Guid id)
        {
            var x= db.Clients.Find(id);
            ViewBag.path = x.FilePath;

            List<string> f=new List<string>();
            string[] files = Directory.GetFiles(x.FilePath,"*.*").ToArray();
            foreach (var item in files)
            {
                f.Add(Path.GetFileName(item));
            }
            
            return View(f);
        }
        public FileStreamResult download(string filePath, string fileName)
        {
            string fn = fileName;//客户端保存的文件名
            string fp = filePath+fileName; //Server.MapPath("~/Document/123.txt");//路径
            return File(new FileStream(fp, FileMode.Open), "text/plain",fn);
        }
        /* public ActionResult Download(string filePath, string fileName)
         {
             Encoding encoding;
             string outputFileName = null;
             fileName = fileName.Replace("'", "");

             string browser = Request.UserAgent.ToUpper();
             if (browser.Contains("MS") == true && browser.Contains("IE") == true)
             {
                 outputFileName = HttpUtility.UrlEncode(fileName);
                 encoding = Encoding.Default;
             }
             else if (browser.Contains("FIREFOX") == true)
             {
                 outputFileName = fileName;
                 encoding = Encoding.GetEncoding("GB2312");
             }
             else
             {
                 outputFileName = HttpUtility.UrlEncode(fileName);
                 encoding = Encoding.Default;
             }
             FileStream fs = new FileStream(filePath, FileMode.Open);
             byte[] bytes = new byte[(int)fs.Length];
             fs.Read(bytes, 0, bytes.Length);
             fs.Close();
             Response.Charset = "UTF-8";
             Response.ContentType = "application/octet-stream";
             Response.ContentEncoding = encoding;
             Response.AddHeader("Content-Disposition", "attachment; filename=" + outputFileName);
             Response.BinaryWrite(bytes);
             Response.Flush();
             Response.End();
             return new EmptyResult();
         }*/
    }
}