using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using System.Web.Security;

namespace Yearnly.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            ViewBag.Message = "Get Yearnin'";
            //ViewBag.UserId = WebSecurity.CurrentUserId;
            ViewBag.Test = "This is some testing of git hub";
            return View();
        }
    }
}
