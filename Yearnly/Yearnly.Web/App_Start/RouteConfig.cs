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
                name: "RegisterRoute",
                url: "register/",
                defaults: new { controller = "Account", action = "Register" }
                );

            routes.MapRoute(
                name: "AboutSiteRoute",
                url: "about/",
                defaults: new { controller = "Site", action = "About" }
                );

            routes.MapRoute(
                name: "SiteFeaturesRoute",
                url: "about/features",
                defaults: new { controller = "Site", action = "Features" }
                );

            routes.MapRoute(
                name: "SiteSupportRoute",
                url: "about/support",
                defaults: new { controller = "Site", action = "Support" }
                );

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
                name: "MySpecificListRoute",
                url: "lists/{listParseString}",
                defaults: new { controller = "User", action = "SpecificList" }
                );

            routes.MapRoute(
                name: "FriendListRoute",
                url: "{username}/lists",
                defaults: new { controller = "PublicView", action = "Lists" }
                );

            routes.MapRoute(
                name: "FriendSpecificListRoute",
                url: "{username}/lists/{listParseString}",
                defaults: new { controller = "PublicView", action = "UsersList" }
                );

            routes.MapRoute(
                name: "FriendItemRoute",
                url: "{username}/items",
                defaults: new { controller = "PublicView", action = "Items" }
                );

            routes.MapRoute(
                name: "FriendFriendsRoute",
                url: "{username}/friends",
                defaults: new { controller = "PublicView", action = "Friends" }
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