using DapperFrame.Entity;
using DapperFrame.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DapperWebAPI.Controllers
{
    public class HomeController : Controller
    {

        public JsonResult Index()
        {
            var tableConfigList = JsonConfigurationHelper.GetAppSettings<TableConfiguration>();
            var tableConfigEntity = tableConfigList.Where(q => q.Name == "WALIUJR_SYS").Select(q => q.ConnectString).FirstOrDefault();


            return Json(tableConfigEntity, JsonRequestBehavior.AllowGet);

        }
    }
}
