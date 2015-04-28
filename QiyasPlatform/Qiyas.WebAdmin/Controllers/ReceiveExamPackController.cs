using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace Qiyas.WebAdmin.Controllers
{
    [Authorize]
    public class ReceiveExamPackController : Controller
    {
        public int PrintingOperationID
        {
            set
            {
                var key = "34FAA431-CF79-4869-9488-93F6AAE81226";
                var Session = HttpContext.Session;
                Session[key] = value;
            }
            get
            {
                var key = "34FAA431-CF79-4869-9488-93F6AAE81226";
                var Session = HttpContext.Session;
                if (Session[key] == null)
                    Session[key] = 0;
                return (int)Session[key];
            }
        }
        public static int MainID;
        // GET: ReceiveExamPack
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Receive(int ID = 0)
        {
            string url = string.Format("{0}", Url.Action("Index", "ReceiveExamPack"));
            var model = new BusinessLogicLayer.Entity.PPM.BookPrintingOperation(ID);
            if (model == null || !model.HasObject)
            {
                return RedirectToAction("Index", "ReceiveExamPack");
            }
            MainID = ID;
            ViewBag.HasError = false;
            ViewBag.NotifyMessage = "";
            ViewBag.PrintingID = ID;
            ViewBag.IsReceived = model.OperationStatusID != 4;
            PrintingOperationID = ID;
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult PrintGridViewPartial()
        {
            var model = new BusinessLogicLayer.Components.PPM.BookPrintingOperationLogic().GetAllReadyToReceive();
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
            string url = string.Format("{0}/{1}", Url.Action("Receive", "ReceiveExamPack"), ID);
            var model = new BusinessLogicLayer.Entity.PPM.BookPrintingOperation(ID);
            if (model == null)
                return View();
            else
                return Redirect(url);
        }

        [ValidateInput(false)]
        public ActionResult ReceivePackGridViewPartial()
        {
            var model = new BusinessLogicLayer.Components.PPM.BookPackingOperationLogic().GetByBookPrintingID(PrintingOperationID);
            return PartialView("_ReceivePackGridViewPartial", model);
        }

        
        [HttpPost]
        public ActionResult ReceiveOrder(FormCollection form)
        {
            
            var model = new BusinessLogicLayer.Entity.PPM.BookPrintingOperation(PrintingOperationID);
            model.OperationStatusID = 5;
            model.Save();
            model.UpdateItemPackStatus(7);
            ViewBag.HasError = false;
            ViewBag.NotifyMessage = Resources.MainResource.ReceivedSuccessfully;
            ViewBag.IsReceived = model.OperationStatusID != 4;
            return View("Receive", model);
        }
    }
}