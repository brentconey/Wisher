using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using System.Web.Security;
using Microsoft.Web.WebPages.OAuth;
using Yearnly.Model;

namespace Yearnly.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private YearnlyEntities db;
        public HomeController()
        {
            db = new YearnlyEntities();
        }
        public ActionResult Index(string returnUrl)
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult LandingPage(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (WebSecurity.IsAuthenticated)
            {
                return RedirectToLocal(returnUrl);
            }

            return View("LandingPage");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            return new ExternalLoginResult(provider, Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
        }

        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            public string Provider { get; private set; }
            public string ReturnUrl { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
                OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
            }
        }

        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult ExternalLoginsList(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return PartialView("_ExternalLoginsListPartial", OAuthWebSecurity.RegisteredClientData);
        }

    }
}
