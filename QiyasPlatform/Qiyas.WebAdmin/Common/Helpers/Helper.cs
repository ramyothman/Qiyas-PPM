using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Qiyas.WebAdmin.Common.Helpers
{
    public static class Helper
    {
        public static dynamic GetPageViewBag(this HtmlHelper html)
        {
            if (html == null || html.ViewContext == null) //this means that the page is root or parial view
            {
                return html.ViewBag;
            }

            ControllerBase controller = html.ViewContext.Controller;

            while (controller.ControllerContext.IsChildAction)  //traverse hierachy to get root controller
            {
                controller = controller.ControllerContext.ParentActionViewContext.Controller;
            }

            return controller.ViewBag;
        }
    }
}