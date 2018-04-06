using System.Web;
using System.Web.Mvc;
using System.IO;
using events.Models;

namespace events.Controllers
{
    public class serviceproviderController : Controller
    {
        //
        // GET: /serviceprovider/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult home()
        {
            return View();
        }
        public ActionResult addpack()
        {
           
            return View();
        }
        public ActionResult addpackkk(HttpPostedFileBase files, addpack ob)
        {



        
                 try
            {
                if (files.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(files.FileName);
                    string _path = Path.Combine(Server.MapPath("~/IDPROOF"), _FileName);
                    files.SaveAs(_path);
                    string path1 = "../IDPROOF/" + _FileName;
                    common sql = new common();



                    int c = sql.Execute("insert into catering values('" + Session["spid"].ToString() + "','" + ob.dtype + "','" + ob.pname + "','" + ob.menu + "',0,'" + ob.des + "','" + path1+"','available')");




                    return RedirectToAction("addprice", "serviceprovider");




                }
                else
                {


                    return RedirectToAction("home", "serviceprovider");
                }

            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return RedirectToAction("home", "serviceprovider");
            }


        



    }
        public ActionResult addpricelist(string pname)
        {
            common sql = new common();
            addpack ob1 = new addpack();
            //ob1.dt = sql.GetData("select * from catering");

            ob1.dt = sql.GetData("select * from catering where spid='" + Session["spid"].ToString() +"'");
            Session["cid"] = ob1.dt.Rows[0]["cid"].ToString();
            int c = sql.Execute("insert into price values('" + Session["spid"].ToString() + "','" + Session["cid"].ToString() + "','" + ob1.type + "','" + ob1.iname + "','" + ob1.price + "','available')");
            return RedirectToAction("addprice", "serviceprovider");



        }
        public ActionResult addprice()

        {

            return View();
        }
        //public ActionResult changepas()

        //{
        //    return View();
        //}
        public ActionResult viewpack( addpack ob)

        {
            common sql = new common();
           
            ob.dt = sql.GetData("select * from price where spid='" + Session["spid"].ToString() + "'");
            return View(ob);

      
        }
      
      
        //public ActionResult change1(string password1)

        //{

        //    common sql = new common();
        //    int y = sql.Execute("update login set password='" + password1 + "' where spid='" + Session["spid"].ToString() + "'");

        //    return RedirectToAction("home", "Admin");
        //}
        public ActionResult editview(int id)
        {
            common sql = new common();
            addpack ob = new addpack();
            ob.dt = sql.GetData("select * from price where pid='" + id+ "'");
            Session["pid"] = ob.dt.Rows[0]["pid"].ToString();
            ob.type = ob.dt.Rows[0]["type"].ToString();
            ob.iname = ob.dt.Rows[0]["iname"].ToString();
            ob.price = ob.dt.Rows[0]["price"].ToString();
            return View(ob);
        }
        public ActionResult editpack( int price)
        
        {
            common sql = new common();
            addpack ob = new addpack();

            int y = sql.Execute("update price set price='" + price + "' where pid='" + Session["pid"].ToString() + "'");
            return RedirectToAction("viewpack","serviceprovider");
        }
        public ActionResult deletepack(int id)
        {
            common sql = new common();

            int y = sql.Execute("delete from price where pid='" + id + "'");
            addpack ob = new addpack();
            ob.dt = sql.GetData("select * from price ");


            return RedirectToAction("viewpack", "serviceprovider");


        }
        public ActionResult editview1( SPregister ob)
        {

            common sql = new common();

            //ob.dt = sql.GetData("select * from catering,price where catering.spid=price.spid");
            ob.dt = sql.GetData("select * from catering where spid='" + Session["spid"].ToString()+"'");
            return View(ob);

        }
      
        public ActionResult home1()
        {

            return View();
        }
        public ActionResult adddeco()
        {

            return View();
        }
        [HttpPost]
        public ActionResult adddecoration(HttpPostedFileBase files, packages ob)
        { 

            try
            {
                if (files.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(files.FileName);
                    string _path = Path.Combine(Server.MapPath("~/IDPROOF"), _FileName);
                    files.SaveAs(_path);
                    string path1 = "../IDPROOF/" + _FileName;
                    common sql = new common();
                  

                   
                    int c = sql.Execute("insert into mypack values('" + Session["spid"].ToString() + "','" + ob.dtype + "','" + ob.pname + "','" + ob.cost + "','" + ob.des + "','" + path1 + "',0,'available')");

                   


                    return RedirectToAction("adddeco", "serviceprovider");




                }
                else
                {


                    return RedirectToAction("home1", "serviceprovider");
                }

            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return RedirectToAction("home1", "serviceprovider");
            }


        }

        public ActionResult viewdec(packages ob)

        {
            common sql = new common();

            ob.dt = sql.GetData("select * from mypack where spid='" + Session["spid"].ToString() + "'");
            return View(ob);


        }
        public ActionResult editdec(int id)
        {
            common sql = new common();
            packages ob = new packages();
            ob.dt = sql.GetData("select * from mypack where packid='" + id + "'");
            Session["packid"] = ob.dt.Rows[0]["packid"].ToString();
            ob.dtype = ob.dt.Rows[0]["dtype"].ToString();
            ob.pname = ob.dt.Rows[0]["pname"].ToString();
            ob.cost = ob.dt.Rows[0]["cost"].ToString();
            return View(ob);
        }
        public ActionResult editdecoration(int cost)

        {
            common sql = new common();
            packages ob = new packages();

            int y = sql.Execute("update mypack set cost='"+cost+"' where packid='" + Session["packid"].ToString() + "'");
            return RedirectToAction("viewdec", "serviceprovider");
        }

        public ActionResult deletedec(int id)
        {
            common sql = new common();

            int y = sql.Execute("delete from mypack where packid='" + id + "'");
            packages ob = new packages();
            ob.dt = sql.GetData("select * from mypack ");


            return RedirectToAction("viewdec", "serviceprovider");


        }
        public ActionResult home2()
        {

            return View();
        }
        public ActionResult addcake()
        {

            return View();
        }
        [HttpPost]
        public ActionResult UploadFile1(HttpPostedFileBase files, packages ob)
        {
            try
            {
                if (files.ContentLength > 0)
                {
                    //string s = ob.files;

                    string _FileName = Path.GetFileName(files.FileName);
                    string _path = Path.Combine(Server.MapPath("~/IDPROOF"), _FileName);
                    files.SaveAs(_path);
                    string path1 = "../IDPROOF/" + _FileName;
                    common sql = new common();
                    int c = sql.Execute("insert into mypack values('" + Session["spid"].ToString() + "','Cake','" + ob.cname + "','" + ob.cost + "','" + ob.des + "','" +path1+"',0,'available')");

                   

                    return RedirectToAction("addcake", "serviceprovider");





                }
                else
                {
                    return RedirectToAction("home2", "serviceprovider");
                }

            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return RedirectToAction("home2", "serviceprovider");
            }
        }
        public ActionResult viewcake(packages ob)

        {
            common sql = new common();

            ob.dt = sql.GetData("select * from mypack where spid='" + Session["spid"].ToString() + "'");
            return View(ob);


        }
        public ActionResult editcake(int id)
        {
            common sql = new common();
            packages ob = new packages();
            ob.dt = sql.GetData("select * from mypack where packid='" + id + "'");
            Session["packid"] = ob.dt.Rows[0]["packid"].ToString();
            //ob.dtype = ob.dt.Rows[0]["dtype"].ToString();
            ob.cname = ob.dt.Rows[0]["pname"].ToString();
            ob.cost = ob.dt.Rows[0]["cost"].ToString();
            return View(ob);
        }
        public ActionResult editcakes(int cost)

        {
            common sql = new common();
            packages ob = new packages();

            int y = sql.Execute("update mypack set cost='" + cost + "' where packid='" + Session["packid"].ToString() + "'");
            return RedirectToAction("viewcake", "serviceprovider");
        }

        public ActionResult deletecake(int id)
        {
            common sql = new common();

            int y = sql.Execute("delete from mypack where packid='" + id + "'");
            packages ob = new packages();
            ob.dt = sql.GetData("select * from mypack ");


            return RedirectToAction("viewcake", "serviceprovider");


        }
        public ActionResult home3()
        {

            return View();
        }
        public ActionResult addtrav()
        {

            return View();
        }
        public ActionResult traveling(HttpPostedFileBase files, packages ob)
        {
            try
            {
                if (files.ContentLength > 0)
                {
                    //string s = ob.files;

                    string _FileName = Path.GetFileName(files.FileName);
                    string _path = Path.Combine(Server.MapPath("~/IDPROOF"), _FileName);
                    files.SaveAs(_path);
                    string path1 = "../IDPROOF/" + _FileName;
                    common sql = new common();
                    int c = sql.Execute("insert into mypack values('" + Session["spid"].ToString() + "','"+ob.type+"','" + ob.vname + "','" + ob.charge + "', '"+ob.des+"','" + path1 + "','"+ob.seat+"','available')");



                    return RedirectToAction("addtrav", "serviceprovider");





                }
                else
                {
                    return RedirectToAction("home3", "serviceprovider");
                }

            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return RedirectToAction("home3", "serviceprovider");
            }
        }
        public ActionResult viewtrav(packages ob)

        {
            common sql = new common();

            ob.dt = sql.GetData("select * from mypack where spid='" + Session["spid"].ToString() + "'");
            return View(ob);


        }
        public ActionResult edittrav(int id)
        {
            common sql = new common();
            packages ob = new packages();
            ob.dt = sql.GetData("select * from mypack where packid='" + id + "'");
            Session["packid"] = ob.dt.Rows[0]["packid"].ToString();
            //ob.dtype = ob.dt.Rows[0]["dtype"].ToString();
            ob.vname = ob.dt.Rows[0]["pname"].ToString();
            ob.type = ob.dt.Rows[0]["dtype"].ToString();
            
            ob.charge = ob.dt.Rows[0]["cost"].ToString();
            return View(ob);
        }
        public ActionResult edittraveling(int charge)

        {
            common sql = new common();
            packages ob = new packages();

            int y = sql.Execute("update mypack set cost='" + charge + "' where packid='" + Session["packid"].ToString() + "'");
            return RedirectToAction("viewtrav", "serviceprovider");
        }
        public ActionResult home4()
        {

            return View();
        }
        public ActionResult addphoto()
        {

            return View();
        }
        public ActionResult photography(HttpPostedFileBase files, packages ob)
        {
            try
            {
                if (files.ContentLength > 0)
                {
                    //string s = ob.files;

                    string _FileName = Path.GetFileName(files.FileName);
                    string _path = Path.Combine(Server.MapPath("~/IDPROOF"), _FileName);
                    files.SaveAs(_path);
                    string path1 = "../IDPROOF/" + _FileName;
                    common sql = new common();
                    int c = sql.Execute("insert into mypack values('" + Session["spid"].ToString() + "','" + ob.type + "','"+ob.pname+"','" + ob.cost + "', '"+ob.des+"','" + path1 + "',0,'available')");



                    return RedirectToAction("addphoto", "serviceprovider");





                }
                else
                {
                    return RedirectToAction("home4", "serviceprovider");
                }

            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return RedirectToAction("home3", "serviceprovider");
            }
        }
        public ActionResult viewphoto(packages ob)

        {
            common sql = new common();

            ob.dt = sql.GetData("select * from mypack where spid='" + Session["spid"].ToString() + "'");
            return View(ob);


        }
        public ActionResult editphoto(int id)
        {
            common sql = new common();
            packages ob = new packages();
            ob.dt = sql.GetData("select * from mypack where packid='" + id + "'");
            Session["packid"] = ob.dt.Rows[0]["packid"].ToString();
            //ob.dtype = ob.dt.Rows[0]["dtype"].ToString();
            ob.type = ob.dt.Rows[0]["dtype"].ToString();
           
            ob.cost = ob.dt.Rows[0]["cost"].ToString();
            return View(ob);
        }
        public ActionResult edits(int cost)

        {
            common sql = new common();
            packages ob = new packages();

            int y = sql.Execute("update mypack set cost='" + cost + "' where packid='" + Session["packid"].ToString() + "'");
            return RedirectToAction("viewphoto", "serviceprovider");
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

            ob.dt = sql.GetData("select * from notifications join userreg on notifications.uid=userreg.uid where notifications.spid='" + Session["spid"].ToString() + "' order by noid desc");
            return View(ob);
        }
    }

}
