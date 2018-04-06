using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using events.Models;
using WebMatrix.WebData;
using System.Data;
using System.Data.SqlClient;

namespace events.Controllers
{
    public class SiteController : Controller
    {
        //
        // GET: /Site/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult gallery()
        {
            return View();
        }
        public ActionResult home()
        {
            return View();
        }
        public ActionResult login()
        {
            return View();
        }
        public ActionResult contact()
        {
            return View();
        }
        public ActionResult SPregister()
        {
           
            return View();
        }
       

        public ActionResult validatelogin(login ob)
        {
            common sql = new common();

            
            ob.dt = sql.GetData("select * from login where username='" + ob.username + "' and password='" + ob.password + "'");
            if (ob.dt.Rows.Count > 0)
            {
             
                string status = ob.dt.Rows[0]["status"].ToString();
           

                if (status == "Pending")
                {
                    ob.message = "Inactive User";
                    return RedirectToAction("login", "Site", ob);

                }
                else
                {
                    string type = ob.dt.Rows[0]["type"].ToString();
                    if (type == "service provider")
                    {
                        SPregister ob1 = new SPregister();

                        ob1.dt1 = sql.GetData("select * from SPregister where username='" + ob.username + "'");
                        Session["spid"] = ob1.dt1.Rows[0]["spid"].ToString();
                        Session["category"] = ob1.dt1.Rows[0]["category"].ToString();
                        Session["name"] = ob1.dt1.Rows[0]["name"].ToString();
                        Session["inpath"] = ob1.dt1.Rows[0]["path1"].ToString();
                        string category = ob1.dt1.Rows[0]["category"].ToString();

                  if (category == "catering")
                        {
                            return RedirectToAction("home", "serviceprovider");
                        }
                        else if(category=="decoration")
                        {
                            return RedirectToAction("home1", "serviceprovider");
                        }
                        else if(category == "cake")
                        {
                            return RedirectToAction("home2", "serviceprovider");
                        }
                      else if (category == "traveling")
                        {
                            return RedirectToAction("home3", "serviceprovider");
                        }
                  else
                        {
                            return RedirectToAction("home4", "serviceprovider");
                        }

                    }
                    else if(type=="user")
                    {
                        SPregister ob1 = new SPregister();
                        ob1.dt1 = sql.GetData("select * from userreg where username='" + ob.username + "'");
                        Session["uid"] = ob1.dt1.Rows[0]["uid"].ToString();
                        return RedirectToAction("user", "User");

                    }

                    else if (type == "Admin")
                    {
                        return RedirectToAction("home", "Admin");
                       
                    }
                    else
                    {
                        ob.message = "Inavlid User";
                        return RedirectToAction("login", "Site", ob);
                    }

                }



            }
            else
            {
                ob.message = "Inavlid User";
                return RedirectToAction("login", "Site", ob);

            }
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
                    string _path = Path.Combine(Server.MapPath("~/sp"), _FileName);
                    files.SaveAs(_path);
                    string path1 = "../sp/" + _FileName;

                    common sql = new common();
                    int c = sql.Execute("insert into SPregister values('" + ob.category + "','" + ob.name + "','" + ob.oname + "','" + ob.district + "','" + ob.state + "','" + ob.mobile + "','" + ob.landline + "','" + ob.email + "','" + ob.UserName + "','" + ob.password + "','Registred','"+path1+"')");

                    int h = sql.Execute("insert into login values('service provider','" + ob.UserName + "','" + ob.password + "','Pending')");


                  
                    return RedirectToAction("login", "Site");




                }
                else
                {
                  
                  
                    return RedirectToAction("SPregister", "Site");
                }

            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return RedirectToAction("SPregister", "Site");
            }


          

                }
               

           
        
        [AllowAnonymous]
        public string CheckUserName(string input)
        {
            bool ifuser = WebSecurity.UserExists(input);

            if (ifuser == false)
            {
                return "Available";
            }

            if (ifuser == true)
            {
                return "Not Available";
            }

            return "";
        }

        [HttpPost]
        public JsonResult doesUserNameExist(string UserName)
        {

            return Json(IsUserAvailable(UserName));


        }
        public bool IsUserAvailable(string UserName)
        {
            common sql = new common();
            SPregister ob = new SPregister();
            bool status;
            ob.dt = sql.GetData("select * from login where username='" + UserName + "'");

            if (ob.dt.Rows.Count > 0)
            {
                status = false;
            }
            else
            {
                status = true;
            }

            //Already registered  




            return status;

        }

    }
}
