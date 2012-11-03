using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Yearnly.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "AddItemAjaxRoute",
                url: "items/ajaxcreate",
                defaults: new { controller = "Items", action = "AjaxCreate" }
                );

            routes.MapRoute(
                name: "MyItemRoute",
                url: "items/",
                defaults: new { controller = "Users", action = "Items" }
                );

            routes.MapRoute(
                name: "MyListRoute",
                url: "lists/",
                defaults: new { controller = "Users", action = "Lists" }
                );

            routes.MapRoute(
                name: "UserListRoute",
                url: "{username}/lists",
                defaults: new { controller = "Users", action = "Lists" }
                );

            routes.MapRoute(
                name: "UserItemRoute",
                url: "{username}/items",
                defaults: new { controller = "Users", action = "Items" }
                );

            routes.MapRoute(
                name: "UserRoute",
                url: "{username}",
                defaults: new { controller = "Users", action = "Index" }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }

    }
}