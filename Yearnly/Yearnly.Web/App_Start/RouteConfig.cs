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
                name: "MyFriendsRoute",
                url: "friends/",
                defaults: new { controller = "User", action = "Friends" }
                );

            routes.MapRoute(
                name: "MyItemRoute",
                url: "items/",
                defaults: new { controller = "User", action = "Items" }
                );

            routes.MapRoute(
                name: "MyListRoute",
                url: "lists/",
                defaults: new { controller = "User", action = "Lists" }
                );

            routes.MapRoute(
                name: "FriendListRoute",
                url: "{username}/lists",
                defaults: new { controller = "PublicView", action = "Lists" }
                );

            routes.MapRoute(
                name: "FriendItemRoute",
                url: "{username}/items",
                defaults: new { controller = "PublicView", action = "Items" }
                );

            routes.MapRoute(
                name: "FriendRoute",
                url: "{username}",
                defaults: new { controller = "PublicView", action = "Index" }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }

    }
}