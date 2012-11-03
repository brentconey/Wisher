using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using System.Web.Security;

namespace Yearnly.Web.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            ViewBag.Message = "Get Yearnin'";
            ViewBag.UserId = WebSecurity.CurrentUserId;
            return View();
        }
    }
}
