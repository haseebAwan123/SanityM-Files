using MFaaP.MFWSClient;
using SanityM_Files.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SanityM_Files.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            CViews view = new CViews();
            FolderContentItems res = view.getRootView("Admin", "123");

            ViewBag.Views = res;

            //CObjects obj = new CObjects();
            //obj.updateAsync().ConfigureAwait(false);


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
            //var rt = objects.updateAsync();
            CAdmin admin = new CAdmin();
            var properties = admin.getProperties("Admin", "123");
            var classes = admin.getClasses("Admin", "123");
            var res = objects.getObjectMetaData("Admin", "123",type,ID,version);

            //foreach (var item in res)
            //{
            //    var dataType = item.TypedValue.DataType;
            //    var isVisible = item.TypedValue.DataType;

            //}
            
            ViewBag.PropertyList = properties;
            ViewBag.Classes = classes;
            ViewBag.MetaData = res;
            ViewBag.ObjectID = ID;
            ViewBag.ObjectType = type;


            ViewBag.Views = TempData["Views"];
            TempData["Views"] = ViewBag.Views;
            return PartialView("_Objects", res);
        }

        [HttpPost]
        public JsonResult SaveDetailedInfo(string Data, string O_ID, string type)
        {
            dynamic metaData = new JavaScriptSerializer().Deserialize<dynamic>(Data);
            List<PropertyValue> payLoad = new List<PropertyValue>();
            CObjects obj = new CObjects();
            foreach (var item in metaData)
            {
                var ID = Convert.ToString(item.Key);
                ID = ID.Replace("p", "");
                if (ID.Contains("-"))
                {
                    ID = ID.Split('-')[0];
                    //ID = ID.Replace(ID.Substring(ID.IndexOf("-"), ID.Length),"");
                }

                PropertyDef DataType = obj.GetProperty(ID);
                var dataType = DataType.DataType.ToString();
                ID = Convert.ToInt32(ID);

                var value = item.Value;
                
                switch (dataType)
                {

                    case "Uninitialized":  // dt = 0
                        break;

                    case "Text":
                        payLoad.Add(new PropertyValue()
                        {
                            PropertyDef = ID, // The built-in "Name or Title" property Id.
                            TypedValue = new TypedValue()
                            {
                                DataType = MFDataType.Text,
                                Value = value
                            }
                        });
                        break;

                    case "Integer":
                        var Integer = Convert.ToInt32(value);
                        payLoad.Add(new PropertyValue()
                        {
                            PropertyDef = ID, // The built-in "Name or Title" property Id.
                            TypedValue = new TypedValue()
                            {
                                DataType = MFDataType.Floating,
                                Value = Integer
                            }
                        });
                        break;

                    case "Floating":
                        var floatNum = Convert.ToDouble(value);
                        payLoad.Add(new PropertyValue()
                        {
                            PropertyDef = ID, // The built-in "Name or Title" property Id.
                            TypedValue = new TypedValue()
                            {
                                DataType = MFDataType.Floating,
                                Value = floatNum
                            }
                        });

                        break;

                    case "Date":
                        var date = Convert.ToDateTime(value);
                        payLoad.Add(new PropertyValue()
                        {
                            PropertyDef = ID, // The built-in "Name or Title" property Id.
                            TypedValue = new TypedValue()
                            {
                                DataType = MFDataType.Date,
                                Value = date
                            }
                        });
                        break;

                    case "Time":
                        var time = Convert.ToDateTime(value);
                        payLoad.Add(new PropertyValue()
                        {
                            PropertyDef = ID, // The built-in "Name or Title" property Id.
                            TypedValue = new TypedValue()
                            {
                                DataType = MFDataType.Time,
                                Value = time
                            }
                        });
                        break;

                    case "Timestamp":
                        var Timestamp = Convert.ToDateTime(value);
                        payLoad.Add(new PropertyValue()
                        {
                            PropertyDef = ID, // The built-in "Name or Title" property Id.
                            TypedValue = new TypedValue()
                            {
                                DataType = MFDataType.Timestamp,
                                Value = Timestamp
                            }
                        });
                        break;

                    case "Boolean":
                        payLoad.Add(new PropertyValue()
                        {
                            PropertyDef = ID, // The built-in "Name or Title" property Id.
                            TypedValue = new TypedValue()
                            {
                                DataType = MFDataType.Boolean,
                                Value = value
                            }
                        });
                        break;

                    case "Lookup":
                        
                        payLoad.Add(new PropertyValue()
                        {
                            PropertyDef = ID,
                            TypedValue = new TypedValue()
                            {
                                
                                Lookup = new Lookup()
                                {
                                    Item = Convert.ToInt32(value), 

                                },
                                DataType = MFDataType.Lookup
                            }
                        });
                        break;

                    case "MultiSelectLookup":

                        var exist = payLoad.Any(p => p.PropertyDef == ID);
                        if (exist)
                        {
                            var existItem = payLoad.Find(p => p.PropertyDef == ID);
                            payLoad.Remove(existItem);
                            existItem.TypedValue.Lookups.Add(new Lookup()
                            {
                                Item = Convert.ToInt32(value),

                            });
                            //existItem = existItem.
                            
                            payLoad.Add(existItem);
                        }
                        else
                        {
                            payLoad.Add(new PropertyValue()
                            {
                                PropertyDef = ID,
                                TypedValue = new TypedValue()
                                {
                                    Lookups = new List<Lookup>()
                                {
                                    new Lookup()
                                    {
                                        Item = Convert.ToInt32(value),

                                    }
                                }
                                ,
                                    DataType = MFDataType.MultiSelectLookup
                                }
                            });
                        }

                        
                        break;

                    case "Integer64":
                        payLoad.Add(new PropertyValue()
                        {
                            PropertyDef = ID, // The built-in "Name or Title" property Id.
                            TypedValue = new TypedValue()
                            {
                                DataType = MFDataType.Integer64,
                                Value = value
                            }
                        });
                        break;

                    case "FILETIME":
                        payLoad.Add(new PropertyValue()
                        {
                            PropertyDef = ID, // The built-in "Name or Title" property Id.
                            TypedValue = new TypedValue()
                            {
                                DataType = MFDataType.FILETIME,
                                Value = value
                            }
                        });
                        break;

                    case "MultiLineText":
                        payLoad.Add(new PropertyValue()
                        {
                            PropertyDef = ID, // The built-in "Name or Title" property Id.
                            TypedValue = new TypedValue()
                            {
                                DataType = MFDataType.MultiLineText,
                                Value = value
                            }
                        });
                        break;

                    case "ACL":
                        payLoad.Add(new PropertyValue()
                        {
                            PropertyDef = ID, // The built-in "Name or Title" property Id.
                            TypedValue = new TypedValue()
                            {
                                DataType = MFDataType.ACL,
                                Value = value
                            }
                        });
                        break;

                    default:
                        break;
                }

            }

            payLoad.Add(new PropertyValue()
            {
                PropertyDef = 22, // The built-in "Name or Title" property Id.
                TypedValue = new TypedValue()
                {
                    DataType = MFDataType.Boolean,
                    Value = false
                }
            });

            var res =obj.updateAsync(payLoad,O_ID,type);

            return Json("");
        }


    
        public void makeProperties()
        {
           // var arr = [
//    {
//    "PropertyDef": 100,
//    "TypedValue": {
//      "Lookup": {
//        "Item": 4
//      },
//      "DataType": 9
//    }
//  },
//    {
//  	"PropertyDef": 0,
//    "TypedValue": {
//      "Value": "Mckenzie Tom",
//      "DataType": 1
//    }
//  },
//  {
//    "PropertyDef": 22,
//    "TypedValue": {
//      "Value": false,
//      "DataType": 8
//    }
//  },
//  {
//  	"PropertyDef": 1027,
//    "TypedValue": {
//     "Lookups": [
//         {
//        "Item": 2
//      }
//     ],
//      "DataType": 10
//    }
//  },
//  {
//  	"PropertyDef": 1029,
//    "TypedValue": {
//     "Lookups": [
//         {
//        "Item": 2
//      }
//     ],
//      "DataType": 10
//    }
//  },
//  {
//  	"PropertyDef": 1032,
//    "TypedValue": {
//      "Lookup": {
//        "Item": 8
//      },
//      "DataType": 9
//    }
//  },
//  {
//  	"PropertyDef": 1031,
//    "TypedValue": {
//      "Value": "212121212",
//      "DataType": 1
//    }
//  },

//{
//  	"PropertyDef": 1045,
//    "TypedValue": {
//      "Value": "123",
//      "DataType": 13
//    }
//  }
//]
        }



    }
}
