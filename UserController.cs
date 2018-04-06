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
    public class UserController : Controller
    {
        

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult user()
        {
            return View();
        }
        public ActionResult userser()
        {
            return View();
        }
        public ActionResult userregister()
        {

            return View();
        }
        [HttpPost]
        public ActionResult UploadFile1(HttpPostedFileBase files, SPregister ob)
        {
            try
            {
                if (files.ContentLength > 0)
                {
                    string s = ob.UserName;
                    string _FileName = Path.GetFileName(files.FileName);
                    string _path = Path.Combine(Server.MapPath("~/IDPROOF"), _FileName);
                    files.SaveAs(_path);
                    string path1 = "../IDPROOF/" + _FileName;

                    common sql = new common();
                    int c = sql.Execute("insert into userreg values('" + ob.name + "','" + ob.district + "','" + ob.mobile + "','" + ob.email + "','" + ob.UserName + "','" + ob.password + "','" + path1 + "')");

                    int h = sql.Execute("insert into login values('user','" + ob.UserName + "','" + ob.password + "','Accepted')");



                    return RedirectToAction("login", "Site");




                }
                else
                {


                    return RedirectToAction("userregister", "User");
                }

            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return RedirectToAction("userregister", "User");
            }




        }

        //public ActionResult catering()
        //{

        //    return View();
        //}
        public ActionResult usercat(addpack ob)
        {

            common sql = new common();

            ob.dt = sql.GetData("select * from events where uid='" + Session["uid"].ToString()+ "'");
            Session["date"] = ob.dt.Rows[0]["date"].ToString();
            ob.dt.Clear();
            ob.dt = sql.GetData("select * from catering where status='available'");
           
            return View(ob);

        }
        public ActionResult bookcat( int id)
        {
            common sql = new common();
            addpack ob = new addpack();

            ob.dt = sql.GetData("select * from events where uid='" + Session["uid"].ToString() + "'");
            Session["date"] = ob.dt.Rows[0]["date"].ToString();
            ob.dt1 = sql.GetData("select * from catering join price on catering.cid=price.cid where price.cid='"+id+"'");
            Session["type"] = ob.dt1.Rows[0]["type"].ToString();
            Session["pname"] = ob.dt1.Rows[0]["pname"].ToString();
            //ob.dt = sql.GetData("select * from catering join price on catering.cid=price.cid where price.type='" + Session["type"].ToString() + "' and catering.pname='" + Session["pname"].ToString() + "' and price.status='available' and price.cid='"+id+"'");
            ob.dt = sql.GetData("select * from catering join price on catering.cid=price.cid where  catering.pname='" + Session["pname"].ToString() + "' and price.status='available' and price.cid='" + id + "'");

            return View(ob);

        }
        public ActionResult bookitem(int id)
        {
            common sql = new common();
            addpack ob = new addpack();
            packages ob1 = new packages();
            ob1.dt1 = sql.GetData("select Max(eid) as eid from events where uid='" + Session["uid"].ToString() + "'");
            Session["eid"] = ob1.dt1.Rows[0]["eid"].ToString();
            ob1.dt1 = sql.GetData("select date as date from events where eid='" + Session["eid"].ToString() + "'");

            Session["date"] = ob1.dt1.Rows[0]["date"].ToString();
            ob.dt = sql.GetData("select * from catering join price on catering.cid=price.cid where price.pid='" + id + "'");
            Session["pid"] = ob.dt.Rows[0]["pid"].ToString();
            Session["spid"] = ob.dt.Rows[0]["spid"].ToString();
            Session["cid"] = ob.dt.Rows[0]["cid"].ToString();
            string pname = ob.dt.Rows[0]["pname"].ToString();
            string menu = ob.dt.Rows[0]["menu"].ToString();
            string path1 = ob.dt.Rows[0]["path1"].ToString();
            string type = ob.dt.Rows[0]["type"].ToString();
            string iname = ob.dt.Rows[0]["iname"].ToString();
            Session["price"] = ob.dt.Rows[0]["price"].ToString();
            int c = sql.Execute("insert into booked values('" + Session["uid"].ToString() + "','" + Session["spid"].ToString() + "','" + Session["pid"].ToString() + "','" + Session["cid"].ToString() + "','" + type + "','" + pname + "','" + menu + "','" + iname + "','" + Session["price"].ToString() + "','" + path1 + "','" + Session["date"].ToString() + "','booked','" + Session["eid"].ToString() + "')");
            int d = sql.Execute("update price set status='booked' where pid='"+Session["pid"].ToString()+"'");
            int k = sql.Execute("update catering set status='booked' where cid='" + Session["cid"].ToString() + "'");

            string msg = "successfully booked" + "\n" + "\n" + "Package:" + iname;
            int c1 = sql.Execute("insert into notifications values('" + Session["uid"].ToString() + "','" + Session["spid"].ToString() + "','" + msg + "','" + System.DateTime.Now.ToString() + "')");

            return RedirectToAction("userser", "User");
        }
        //public ActionResult evsearch()
        //{
            
          
        //    return View();

        //}
        public ActionResult bookdec()
        {
            return View();
        }
        public ActionResult decsearch(packages ob)
        {
            common sql = new common();

            ob.dt = sql.GetData("select * from mypack where dtype='" + ob.dtype + "' and status='available'");

            return View(ob);

        }
        public ActionResult bookdecoration(int id)
        {
            common sql = new common();
            addpack ob = new addpack();
            packages ob1 = new packages();

            ob1.dt1 = sql.GetData("select Max(eid) as eid from events where uid='" + Session["uid"].ToString() + "'");
            Session["eid"] = ob1.dt1.Rows[0]["eid"].ToString();
            ob1.dt1 = sql.GetData("select date as date from events where eid='" + Session["eid"].ToString() + "'");

            Session["date"] = ob1.dt1.Rows[0]["date"].ToString();
            ob.dt = sql.GetData("select * from mypack where packid='" + id + "'");
            Session["packid"] = ob.dt.Rows[0]["packid"].ToString();
            Session["spid"] = ob.dt.Rows[0]["spid"].ToString();

            string pname = ob.dt.Rows[0]["pname"].ToString();

            string path1 = ob.dt.Rows[0]["path1"].ToString();

            Session["cost"] = ob.dt.Rows[0]["cost"].ToString();
            //ob1.dt1 = sql.GetData("select * from events where uid='" + Session["uid"].ToString() + "'");
            //Session["eid"] = ob1.dt1.Rows[0]["eid"].ToString();
            //Session["date"] = ob1.dt1.Rows[0]["date"].ToString();
            int c = sql.Execute("insert into booked values('" + Session["uid"].ToString() + "','" + Session["spid"].ToString() + "','" + Session["packid"].ToString() + "',0,'decoration','" + pname + "','decoration','decoration','" + Session["cost"].ToString() + "','" + path1 + "','" + Session["date"].ToString()+"','booked','"+Session["eid"].ToString()+"')");

            int i = sql.Execute("update mypack set status='booked' where packid='"+ Session["packid"].ToString() + "'");
            string msg = "successfully booked" + "\n" + "\n" + "package:" + pname;
            int c1 = sql.Execute("insert into notifications values('" + Session["uid"].ToString() + "','" + Session["spid"].ToString() + "','" + msg + "','" + System.DateTime.Now.ToString() + "')");
            return RedirectToAction("decsearch", "User");
        }
        public ActionResult bookcake(packages ob)

        {
            common sql = new common();

            ob.dt = sql.GetData("select * from mypack where dtype='Cake' and status='available'");
            return View(ob);


        }
        public ActionResult cakebook(int id)
        {
            common sql = new common();
            addpack ob = new addpack();
            packages ob1 = new packages();
            ob1.dt1 = sql.GetData("select Max(eid) as eid from events where uid='" + Session["uid"].ToString() + "'");
            Session["eid"] = ob1.dt1.Rows[0]["eid"].ToString();
            ob1.dt1 = sql.GetData("select date as date from events where eid='" + Session["eid"].ToString() + "'");

            Session["date"] = ob1.dt1.Rows[0]["date"].ToString();
            ob.dt = sql.GetData("select * from mypack where packid='" + id + "'");
            Session["packid"] = ob.dt.Rows[0]["packid"].ToString();
            Session["spid"] = ob.dt.Rows[0]["spid"].ToString();

            string pname = ob.dt.Rows[0]["pname"].ToString();

            string path1 = ob.dt.Rows[0]["path1"].ToString();

            Session["cost"] = ob.dt.Rows[0]["cost"].ToString();
            //ob1.dt1 = sql.GetData("select * from events where uid='" + Session["uid"].ToString() + "'");
            //Session["eid"] = ob1.dt1.Rows[0]["eid"].ToString();
            //Session["date"] = ob1.dt1.Rows[0]["date"].ToString();
            int c = sql.Execute("insert into booked values('" + Session["uid"].ToString() + "','" + Session["spid"].ToString() + "','" + Session["packid"].ToString() + "',0,'cake','" + pname + "','cake','cake','" + Session["cost"].ToString() + "','" + path1 + "','"+Session["date"].ToString()+"','booked','"+Session["eid"].ToString()+"')");
            //ob.dt = sql.GetData("select * from catering join price on catering.cid=price.cid where price.cid='" + id + "'");
            int i = sql.Execute("update mypack set status='booked' where packid='" + Session["packid"].ToString() + "'");
            string msg = "successfully booked" + "\n" + "\n" + "package:" + pname;
            int c1 = sql.Execute("insert into notifications values('" + Session["uid"].ToString() + "','" + Session["spid"].ToString() + "','" + msg + "','" + System.DateTime.Now.ToString() + "')");
            return RedirectToAction("cake", "User");
        }
        public ActionResult cake()
        {

            return View();
        }
        public ActionResult booktrav(packages ob)

        {
            common sql = new common();

            ob.dt = sql.GetData("select * from mypack where dtype='A/C' OR dtype='NON-A/C' and status='available'");
            return View(ob);


        }
        public ActionResult booktraveling(int id)
        {
            common sql = new common();
            addpack ob = new addpack();
            packages ob1 = new packages();
            ob1.dt1 = sql.GetData("select Max(eid) as eid from events where uid='" + Session["uid"].ToString() + "'");
            Session["eid"] = ob1.dt1.Rows[0]["eid"].ToString();
            ob1.dt1 = sql.GetData("select date as date from events where eid='" + Session["eid"].ToString() + "'");

            Session["date"] = ob1.dt1.Rows[0]["date"].ToString();
            ob.dt = sql.GetData("select * from mypack where packid='" + id + "'");
            Session["packid"] = ob.dt.Rows[0]["packid"].ToString();
            Session["spid"] = ob.dt.Rows[0]["spid"].ToString();

            string pname = ob.dt.Rows[0]["pname"].ToString();

            string path1 = ob.dt.Rows[0]["path1"].ToString();
            string dtype = ob.dt.Rows[0]["dtype"].ToString();

            Session["cost"] = ob.dt.Rows[0]["cost"].ToString();
            //ob1.dt1 = sql.GetData("select * from events where uid='" + Session["uid"].ToString() + "'");
            //Session["eid"] = ob1.dt1.Rows[0]["eid"].ToString();
            //Session["date"] = ob1.dt1.Rows[0]["date"].ToString();
            int c = sql.Execute("insert into booked values('" + Session["uid"].ToString() + "','" + Session["spid"].ToString() + "','" + Session["packid"].ToString() + "',0,'" + dtype + "','" + pname + "','vehicle','vehicle','" + Session["cost"].ToString() + "','" + path1 + "','"+Session["date"].ToString()+"','booked','"+Session["eid"]+"')");
            //ob.dt = sql.GetData("select * from catering join price on catering.cid=price.cid where price.cid='" + id + "'");
            int i = sql.Execute("update mypack set status='booked' where packid='" + Session["packid"].ToString() + "'");
            string msg = "successfully booked" + "\n" + "\n" + "package:" + pname;
            int c1 = sql.Execute("insert into notifications values('" + Session["uid"].ToString() + "','" + Session["spid"].ToString() + "','" + msg + "','" + System.DateTime.Now.ToString() + "')");
            return RedirectToAction("booktrav", "User");
        }
        public ActionResult bookphoto(packages ob)
        {

            common sql = new common();

            //ob.dt = sql.GetData("select * from catering,price where catering.spid=price.spid");
            ob.dt = sql.GetData("select * from mypack where dtype='Photo'OR dtype='Video' and status='available'");
            return View(ob);

        }
        public ActionResult bookvideo(int id)
        {
            common sql = new common();
            packages ob = new packages();
            ob.dt1 = sql.GetData("select Max(eid) as eid from events where uid='" + Session["uid"].ToString() + "'");
            Session["eid"] = ob.dt1.Rows[0]["eid"].ToString();
            ob.dt1 = sql.GetData("select date as date from events where eid='" + Session["eid"].ToString() + "'");

            Session["date"] = ob.dt1.Rows[0]["date"].ToString();
            ob.dt = sql.GetData("select * from mypack where packid='" + id + "'");
            Session["packid"] = ob.dt.Rows[0]["packid"].ToString();
            Session["spid"] = ob.dt.Rows[0]["spid"].ToString();

            string pname = ob.dt.Rows[0]["pname"].ToString();

            string path1 = ob.dt.Rows[0]["path1"].ToString();
            string dtype = ob.dt.Rows[0]["dtype"].ToString();

            Session["cost"] = ob.dt.Rows[0]["cost"].ToString();
            //ob.dt1 = sql.GetData("select * from events where uid='" + Session["uid"].ToString() + "'");
            //Session["eid"] = ob.dt1.Rows[0]["eid"].ToString();
            //Session["date"] = ob.dt1.Rows[0]["date"].ToString();
            int c = sql.Execute("insert into booked values('" + Session["uid"].ToString() + "','" + Session["spid"].ToString() + "','" + Session["packid"].ToString() + "',0,'" + dtype + "','" + pname + "','photo','photo','" + Session["cost"].ToString() + "','" + path1 + "','"+Session["date"].ToString()+"','booked','"+Session["eid"].ToString()+"')");
            //ob.dt = sql.GetData("select * from catering join price on catering.cid=price.cid where price.cid='" + id + "'");
            int i = sql.Execute("update mypack set status='booked' where packid='" + Session["packid"].ToString() + "'");
            string msg = "successfully booked" + "\n" + "\n" + "package:" + pname; 
            int c1 = sql.Execute("insert into notifications values('" + Session["uid"].ToString() + "','" + Session["spid"].ToString() + "','" + msg + "','" + System.DateTime.Now.ToString() + "')");
            return RedirectToAction("bookphoto", "User");
        }
        public ActionResult ourwork()
        {

            return View();
        }
       
      
        public ActionResult feedback()
        {

            return View();
        }
        public ActionResult userfeed(packages ob)
        {
            common sql = new common();
            int c = sql.Execute("insert into feedback values('" + Session["uid"].ToString() + "','" + ob.message + "')");
            return RedirectToAction("user", "User");
        }
        public ActionResult viewfeed()
        {

            return View();
        }
        public ActionResult events()
        {

            return View();
        }
        public ActionResult bookevents(packages ob)
        {
            common sql = new common();
            int c = sql.Execute("insert into events values('" + Session["uid"].ToString() + "','" + ob.evtype + "','" + ob.place + "','" + ob.date + "',0)");

            return RedirectToAction("userser", "User");
        }

        public ActionResult cart()
        {
            common sql = new common();
            packages ob = new packages();
           
            ob.dt = sql.GetData("select distinct pname,path1,cid,bid from booked where uid='"+Session["uid"].ToString()+"' and cid!=0 and status='booked'");
            ob.dt1 = sql.GetData("select * from booked where uid='" + Session["uid"].ToString() + "' and menu='decoration' and status='booked'");
            ob.dt2 = sql.GetData("select * from booked where uid='" + Session["uid"].ToString() + "' and menu='Cake' and status='booked'");
            ob.dt3 = sql.GetData("select * from booked where uid='" + Session["uid"].ToString() + "' and menu='vehicle' and status='booked'");
            ob.dt4 = sql.GetData("select * from booked where uid='" + Session["uid"].ToString() + "' and menu='photo' and status='booked'");

            return View(ob);

        }
        public ActionResult notif()
        {
            common sql = new common();
            packages ob = new packages();
            ob.dt = sql.GetData("select * from notifications where uid='" + Session["uid"].ToString() + "' order by noid desc");
            return View(ob);
        }


        public ActionResult payments()
        {

            common sql = new common();
            payment ob = new payment();


            ob.dt = sql.GetData("select sum(cost) as cost from booked where uid='" + Session["uid"].ToString() + "' and cid!= 0");

            ob.catp = ob.dt.Rows[0]["cost"].ToString();
            Session["catp"] = ob.dt.Rows[0]["cost"].ToString();
            ob.dt.Clear();
            ob.dt = sql.GetData("select sum(cost) as cost from booked where uid='" + Session["uid"].ToString() + "' and menu='cake'");
        
            ob.cake = ob.dt.Rows[0]["cost"].ToString();
            Session["cake"] = ob.dt.Rows[0]["cost"].ToString();
            ob.dt.Clear();
            ob.dt = sql.GetData("select sum(cost) as cost from booked where uid='" + Session["uid"].ToString() + "' and menu='decoration'");

            ob.decpay = ob.dt.Rows[0]["cost"].ToString();
            Session["decpay"] = ob.dt.Rows[0]["cost"].ToString();
            ob.dt.Clear();
            ob.dt = sql.GetData("select sum(cost) as cost from booked where uid='" + Session["uid"].ToString() + "' and menu='vehicle'");

            ob.trav = ob.dt.Rows[0]["cost"].ToString();
            Session["trav"] = ob.dt.Rows[0]["cost"].ToString();
            ob.dt.Clear();
            ob.dt = sql.GetData("select sum(cost) as cost from booked where uid='" + Session["uid"].ToString() + "' and menu='photo'");

            ob.phto = ob.dt.Rows[0]["cost"].ToString();
            Session["phto"] = ob.dt.Rows[0]["cost"].ToString();
            ob.dt.Clear();
            
           
            


            return View(ob);
        }
        public ActionResult calculate( payment ob)
        {
            common sql = new common();
           
            ob.dt = sql.GetData("select Max(eid) as eid from events where uid='" + Session["uid"].ToString() + "'");
            Session["eid"] = ob.dt.Rows[0]["eid"].ToString();
            int c = sql.Execute("insert into payment values('" + Session["uid"].ToString() + "','" + Session["eid"].ToString() + "','" + Session["catp"].ToString() + "','"+ ob.nog + "','" + Session["cake"].ToString() + "','" + Session["decpay"].ToString() + "','" + Session["trav"].ToString() + "','" + Session["phto"].ToString() + "', 0,'not paid')");
            ob.dt.Clear();
            //ob.dt = sql.GetData("select * from payment where uid='" + Session["uid"].ToString() + "'");


            //int catp = int.Parse(Session["catp"].ToString());

            //int  nog = int.Parse(Session["nog"].ToString());

            //int cake = int.Parse(Session["cake"].ToString());

            //int decpay = int.Parse(Session["decpay"].ToString());

            //int trav = int.Parse(Session["trav"].ToString());

            //int phto = int.Parse(Session["phto"].ToString());
            //string total = (catp + decpay + cake + trav + phto).ToString();



            return RedirectToAction("calcpay", "User");
        }
        public ActionResult calcpay()
        {
            common sql = new common();
            payment ob = new payment();
            ob.dt = sql.GetData("select Max(eid) as eid from events where uid='" + Session["uid"].ToString() + "'");
            Session["eid"] = ob.dt.Rows[0]["eid"].ToString();
            ob.dt1 = sql.GetData("select * from payment where uid='" + Session["uid"].ToString() + "' and eid='"+ Session["eid"].ToString()+"'");
            Session["eid"] = ob.dt1.Rows[0]["eid"].ToString();
            Session["catp"] = ob.dt1.Rows[0]["catp"].ToString();
            Session["nog"] = ob.dt1.Rows[0]["nog"].ToString();
            Session["decpay"] = ob.dt1.Rows[0]["decpay"].ToString();
            Session["trav"] = ob.dt1.Rows[0]["trav"].ToString();
            Session["phto"] = ob.dt1.Rows[0]["phto"].ToString();
            Session["cake"] = ob.dt1.Rows[0]["cake"].ToString();

            int catp = int.Parse(Session["catp"].ToString());

            int nog = int.Parse(Session["nog"].ToString());

            int cake = int.Parse(Session["cake"].ToString());

            int decpay = int.Parse(Session["decpay"].ToString());

            int trav = int.Parse(Session["trav"].ToString());

            int phto = int.Parse(Session["phto"].ToString());
            ob.total= (catp + decpay + cake + trav + phto).ToString();




            return View(ob);
        }
        public ActionResult calpay()
        {
            payment ob = new payment();
            packages ob1 = new packages();
          if(ob.amtpayed==ob.total)
            {
                common sql = new common();
                int y = sql.Execute("update booked set status='paid' where uid='" + Session["uid"].ToString() + "'");
                ob1.dt1 = sql.GetData("select Max(eid) as eid  from events where uid='" + Session["uid"].ToString() + "'");
                Session["eid"] = ob1.dt1.Rows[0]["eid"].ToString();
                int x = sql.Execute("update events set payment='" + ob.total + "' where eid='" + Session["eid"].ToString() + "'");

                ob.dt = sql.GetData("select * from userreg where uid='" + Session["uid"].ToString() + "'");
                string name = ob.dt.Rows[0]["name"].ToString();
                string msg = "Payment Completed Successfully by" + "\n" + "\n" + "User:" + name;
                int c1 = sql.Execute("insert into notifications values('" + Session["uid"].ToString() + "',0,'" + msg + "','" + System.DateTime.Now.ToString() + "')");

            }
          else
            {
                common sql = new common();
                int y = sql.Execute("update booked set status='Advancepaid' where uid='" + Session["uid"].ToString() + "'");
                ob1.dt1 = sql.GetData("select Max(eid) as eid from events where uid='" + Session["uid"].ToString() + "'");
                Session["eid"] = ob.dt.Rows[0]["eid"].ToString();
                int x = sql.Execute("update events set payment='" + ob.total + "' where eid='" + Session["eid"].ToString() + "'");
                ob.dt = sql.GetData("select * from userreg where uid='" + Session["uid"].ToString() + "'");
                string name = ob.dt.Rows[0]["name"].ToString();
                string msg = "Advance Paid by" + "\n" + "\n" + "User:" + name;
                int c1 = sql.Execute("insert into notifications values('" + Session["uid"].ToString() + "',0,'" + msg + "','" + System.DateTime.Now.ToString() + "')");
            }
            return RedirectToAction("notif", "User");

        }
        public ActionResult viewcatbook(packages ob)
        {

            common sql = new common();

            ob.dt = sql.GetData("select * from booked where uid='"+Session["uid"].ToString()+"' and status='booked' and cid!=0");
            return View(ob);
        }

        public ActionResult cancelbook(int id)
        {
            common sql = new common();
            int c = sql.Execute("update booked set status='canceled' where bid='"+id+"'");
            int d = sql.Execute("update mypack set status='available' where spid =(select spid from booked where bid='"+id+"')");
            return RedirectToAction("cart", "User");
        }
        public ActionResult cancelbook1(int id)
        {
            common sql = new common();
            int c = sql.Execute("update booked set status='canceled' where bid='" + id + "'");
            int d = sql.Execute("update price set status='available' where spid =(select spid from booked where bid='" + id + "')");
            return RedirectToAction("cart", "User");
        }
        public ActionResult cancelbook2(int id)
        {
            common sql = new common();
            int c = sql.Execute("update booked set status='canceled' where cid=(select cid from booked where bid='"+id+"')");
            int d = sql.Execute("update price set status='available' where spid =(select spid from booked where bid='" + id + " ') and status='booked'");
            int v = sql.Execute("update catering set status='canceled' where cid=(select cid from booked where bid='" + id + "')");

            return RedirectToAction("cart", "User");
        }

    }
}