using SanityM_Files.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SanityM_Files.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            ViewBag.Error = "";
            return View();

        }

        [HttpPost]
        public ActionResult Index(string Name,string Password)
        {
            var vaults = new ExpandoObject();
            try
            {
                
                if (Name != "" && Password != "")
                {
                    CLogin obj = new CLogin();
                    var res = obj.Login(Name, Password);

                    if (res)
                    {
                        //ViewBag.Error = "Login to application failed.";
                        return RedirectToAction("Index","Home");
                    }
                    else
                    {
                        ViewBag.Error = "Invalid UserName/Password.";
                        return View();
                    }
                   
                }
                else
                {
                    ViewBag.Error = "UserName/Password Not Filled.";
                    return View();
                }

               
                
            }
            catch (Exception ex)
            {

                ViewBag.Error = ex.Message ;
                return View();
            }
        }

        public ActionResult Vaults()
        {
            ViewBag.Error = "";
            return View();

        }
    }
}