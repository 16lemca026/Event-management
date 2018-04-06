using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using events.Models;
using System.Data;
using System.IO;
namespace events.Controllers
{
    public class AdminController : Controller
    {
        private string message;

        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult admnla()
        {
            return View();
        }
        public ActionResult services()
        {
            return View();
        }
        public ActionResult about()
        {
            return View();
        }
        public ActionResult home()
        {
            return View();
        }
        public ActionResult district()
        {
            return View();
        }
        public ActionResult addevent()
        {
            return View();
        }
        public ActionResult change()
        {
            return View();
        }
        public ActionResult SPreg()
        {
           
            common sql = new common();
            SPregister ob = new SPregister ();
            ob.dt = sql.GetData("select * from SPregister where status='Registred'");
            return View(ob);

        }
        public ActionResult userlist()
        {

            common sql = new common();
            SPregister ob = new SPregister();
            ob.dt = sql.GetData("select * from userreg");
            return View(ob);

        }
        public ActionResult acceptSP(int id)

        {
            //ViewBag.Msg = message;
            common sql = new common();

            int y = sql.Execute("update SPregister set status='Accepted' where spid='" + id + "'");
            SPregister ob = new SPregister();

            ob.dt = sql.GetData("select * from SPregister where spid ='" + id + "'");

            string uname = ob.dt.Rows[0]["username"].ToString();
            int x = sql.Execute("update login set status='Active' where username='" + uname + "'");
            
            return RedirectToAction("SPreg", "Admin");
        }


        public ActionResult rejectSP(int id)
        {
            common sql = new common();

            int y = sql.Execute("update SPregister set status='Rejected' where spid='" + id + "'");
            SPregister ob = new SPregister();
            ob.dt = sql.GetData("select * from SPregister where spid ='" + id + "'");

            string uname = ob.dt.Rows[0]["username"].ToString();
            int x = sql.Execute("update login set status='Pending' where username='" + uname + "'");
            return RedirectToAction("SPreg", "Admin");
        }

        public ActionResult Downloadproof(int id)
        {
            common sql = new common();

            SPregister ob = new SPregister();
            ob.dt = sql.GetData("select * from SPregister where spid='" + id + "'");
            string path1 = ob.dt.Rows[0]["path1"].ToString();



            string CurrentFileName = path1;

            string contentType = string.Empty;
            contentType = "application/pdf";
            return File(CurrentFileName, contentType, CurrentFileName);

        }
        [HttpPost]
        public ActionResult adddis(district ob)
        {


            common sql = new common();
            int c = sql.Execute("insert into district values('" + ob.dname + "')");


         

            return RedirectToAction("dislists", "Admin");


        }
        public ActionResult dislists()
        {
            common sql = new common();
            district ob = new district();
            ob.dt = sql.GetData("select * from district ");
            return View(ob);

        }
        public ActionResult deletedis(int id)
        {
            common sql = new common();

            int y = sql.Execute("delete from district where did='" + id + "'");
            district ob = new district();
            ob.dt = sql.GetData("select * from district ");

           
            return RedirectToAction("dislists", "Admin");
        }
        [HttpPost]
        public ActionResult addevents(eventtype ob)
        {


            common sql = new common();
            int c = sql.Execute("insert into addevent values('" + ob.etype + "')");

              return RedirectToAction("eventlist", "Admin");


        }
        public ActionResult eventlist()
        {
            common sql = new common();
            eventtype ob = new eventtype();
            ob.dt = sql.GetData("select * from addevent ");
            return View(ob);

        }
        public ActionResult deleteevent(int id)
        {
            common sql = new common();

            int y = sql.Execute("delete from addevent where eid='" + id + "'");
            eventtype ob = new eventtype();
            ob.dt = sql.GetData("select * from addevent ");


            return RedirectToAction("eventlist", "Admin");
        }

        public ActionResult acceptedSP()
        {
            common sql = new common();
            SPregister ob = new SPregister();
            ob.dt = sql.GetData("select * from SPregister where status='Accepted'");
            return View(ob);

        }
        public ActionResult rejectedSP()
        {
            common sql = new common();
            SPregister ob = new SPregister();
            ob.dt = sql.GetData("select * from SPregister where status='Rejected'");
            return View(ob);

        }
        public ActionResult accept(int id)
        {
            message = "";
            common sql = new common();

            int y = sql.Execute("update SPregister set status='Accepted' where spid='" + id + "'");
            SPregister ob = new SPregister();
            ob.dt = sql.GetData("select * from SPregister where spid ='" + id + "'");

            string uname = ob.dt.Rows[0]["username"].ToString();
            int x = sql.Execute("update login set status='Active' where username='" + uname + "'");
            message = "Accepted";
            return RedirectToAction("rejectedSP", "Admin");
        }
        public ActionResult reject(int id)
        {
           
            common sql = new common();

            int y = sql.Execute("update SPregister set status='Rejected' where spid='" + id + "'");
            SPregister ob = new SPregister();
            ob.dt = sql.GetData("select * from SPregister where spid ='" + id + "'");

            string uname = ob.dt.Rows[0]["username"].ToString();
            int x = sql.Execute("update login set status='Pending' where username='" + uname + "'");
         
            return RedirectToAction("acceptedSP", "Admin");
        }
        public ActionResult change1( string password1)

        {
          
            common sql = new common();
            int y = sql.Execute("update login set password='"+password1+"' where type='Admin'");
          
            return RedirectToAction("home", "Admin");
        }
        public ActionResult activesp()
        {

            common sql = new common();
            SPregister ob = new SPregister();
            ob.dt = sql.GetData("select * from SPregister where status='Active'");
            return View(ob);

        }
        public ActionResult Logout()
        {


            Session["lid"] = null;
            return RedirectToAction("login", "Site");
        }

        public ActionResult feedback()
        {
            common sql = new common();
            packages ob = new packages();
            ob.dt = sql.GetData("select * from feedback join userreg on feedback.uid=userreg.uid  order by fid desc");
            return View(ob);
        }
        public ActionResult notif()
        {
            common sql = new common();
            packages ob = new packages();

            ob.dt = sql.GetData("select * from notifications join userreg on notifications.uid=userreg.uid  order by noid desc");
            return View(ob);
        }

    }
}
