using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace Qiyas.WebAdmin.Controllers
{
    public class ReceiveExamPackController : Controller
    {
        // GET: ReceiveExamPack
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult PrintGridViewPartial()
        {
            var model = new object[0];
            return PartialView("_PrintGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult PrintGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Qiyas.BusinessLogicLayer.Entity.PPM.BookPrintingOperation item)
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
            return PartialView("_PrintGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult PrintGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Qiyas.BusinessLogicLayer.Entity.PPM.BookPrintingOperation item)
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
            return PartialView("_PrintGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult PrintGridViewPartialDelete(System.Int32 BookPrintingOperationID)
        {
            var model = new object[0];
            if (BookPrintingOperationID >= 0)
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
            return PartialView("_PrintGridViewPartial", model);
        }


        public ActionResult ReceivePack(int ID)
        {
            string url = string.Format("{0}/Index/{1}", Url.Action("View", "ReceiveExamPack"), ID);
            var model = new BusinessLogicLayer.Entity.PPM.BookPrintingOperation(ID);
            if (model == null)
                return View();
            else
                return Redirect(url);
        }
    }
}