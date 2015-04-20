using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace Qiyas.WebAdmin.Controllers
{
    public class BookRePackController : Controller
    {
        // GET: BookRePack
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult RepackGridViewPartial()
        {
            var model = new object[0];
            return PartialView("_RepackGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult RepackGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Qiyas.BusinessLogicLayer.Entity.PPM.BookPackingOperation item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_RepackGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult RepackGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Qiyas.BusinessLogicLayer.Entity.PPM.BookPackingOperation item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_RepackGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult RepackGridViewPartialDelete(System.Int32 BookPackingOperationID)
        {
            var model = new object[0];
            if (BookPackingOperationID >= 0)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_RepackGridViewPartial", model);
        }
    }
}