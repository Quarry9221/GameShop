using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GameShop.UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                null, "",
                new
                {
                    controller = "Game",
                    action = "List",
                    genre = (string)null,
                    page = 1
                }
                );

            routes.MapRoute(
                name: null,
                url: "Page{page}",
                defaults: new {
                    controller = "Game",
                    action = "List",
                    genre = (string)null },
                constraints: new {
                    page = @"\d+" }
            );

            routes.MapRoute(null,
                "{genre}",
                new {
                    controller = "Game",
                    action = "List",
                    page = 1 }
            );

            routes.MapRoute(null,
                "{genre}/Page{page}",
                new {
                    controller = "Game",
                    action = "List" },
                new {
                    page = @"\d+" }
            );
            routes.MapRoute(null, "{controller}/{action}");
        }
    }
}
