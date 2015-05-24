using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Qiyas.WebAdmin
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "PrintPacks",
                url: "{controller}/{action}/{ID}/{PrintingID}",
                defaults: new { controller = "BookPackItemRePack", action = "PrintPacks", ID = 0, PrintingID = 0 }
            );

            routes.MapRoute(
                name: "WithdrawReport",
                url: "{controller}/{action}/{ID}/{ReportType}",
                defaults: new { controller = "ExamCenterNeeds", action = "WithdrawReport", ID = 0, ReportType = "request" });
        }
    }
}
