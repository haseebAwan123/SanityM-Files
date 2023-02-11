using MFaaP.MFWSClient;
using SanityM_Files.Models;
using System.Web.Mvc;

namespace SanityM_Files.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            CViews view = new CViews();
            FolderContentItems res = view.getRootView("Admin", "123");
            
            ViewBag.Views = res;
            return View();
        }

        [HttpGet]
        public ActionResult getViewItems(int VID)
        {
            CViews view = new CViews();
            FolderContentItems res = view.getViewItems("Admin", "123",VID);
            ViewBag.VID = VID;
            ViewBag.Views = res;
            return View("Index");
        }

        [HttpGet]
        public ActionResult getSubViewItems(int VID,int LID)
        {
            CViews view = new CViews();
            FolderContentItems res = view.getSubViewItems("Admin", "123", VID, LID);

            ViewBag.Views = res;
            TempData["Views"] = res;
            return View("Index");
        }

        [HttpGet]
        public ActionResult getMetaData(int type, int ID, int version)
        {
            CObjects objects = new CObjects();
            CAdmin admin = new CAdmin();
            var properties = admin.getProperties("Admin", "123");
            var classes = admin.getClasses("Admin", "123");
            var res = objects.getObjectMetaData("Admin", "123",type,ID,version);

            foreach (var item in res)
            {
                var dataType = item.TypedValue.DataType;
                var isVisible = item.TypedValue.DataType;

            }
            
            ViewBag.PropertyList = properties;
            ViewBag.Classes = classes;
            ViewBag.MetaData = res;
            
            ViewBag.Views = TempData["Views"];
            TempData["Views"] = ViewBag.Views;
            return PartialView("_Objects", res);
        }


        [HttpGet]
        public ActionResult getSubViewItems1(int VID, int LID)
        {
            CViews view = new CViews();
            FolderContentItems res = view.getSubViewItems("Admin", "123", VID, LID);

            ViewBag.Views = res;
            TempData["Views"] = res;
            return PartialView("_Objects", res);
           
        }
    }
}