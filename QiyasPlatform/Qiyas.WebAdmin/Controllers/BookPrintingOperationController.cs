using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace Qiyas.WebAdmin.Controllers
{
    public enum OperationStatus
    {
        Printing = 1,
        Packing = 2
    }
    public class BookPrintingOperationController : Controller
    {
        // GET: BookPrintingOperation
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult BookPrintingOperationGridViewPartial()
        {
            var model = new BusinessLogicLayer.Components.PPM.BookPrintingOperationLogic().GetAll();
            return PartialView("_BookPrintingOperationGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult BookPrintingOperationGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Qiyas.BusinessLogicLayer.Entity.PPM.BookPrintingOperation item)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    BusinessLogicLayer.Entity.PPM.BookPrintingOperation printing = new BusinessLogicLayer.Entity.PPM.BookPrintingOperation();
                    printing.ExamsNeededForA3 = item.ExamsNeededForA3;
                    printing.ExamsNeededForA4 = item.ExamsNeededForA4;
                    printing.ExamsNeededForCD = item.ExamsNeededForCD;
                    printing.ExamID = item.ExamID;
                    printing.Name = item.Name;
                    printing.OperationStatusID = (int)OperationStatus.Printing;
                    printing.PrintsForOneModel = item.PrintsForOneModel;
                    printing.ModifiedDate = DateTime.Now;
                    printing.CreatedDate = DateTime.Now;
                    printing.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = Resources.MainResource.PleaseCorrectErrors;
            var model = new BusinessLogicLayer.Components.PPM.BookPrintingOperationLogic().GetAll();
            return PartialView("_BookPrintingOperationGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult BookPrintingOperationGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Qiyas.BusinessLogicLayer.Entity.PPM.BookPrintingOperation item)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    BusinessLogicLayer.Entity.PPM.BookPrintingOperation printing = new BusinessLogicLayer.Entity.PPM.BookPrintingOperation(item.BookPrintingOperationID);
                    printing.ExamsNeededForA3 = item.ExamsNeededForA3;
                    printing.ExamsNeededForA4 = item.ExamsNeededForA4;
                    printing.ExamsNeededForCD = item.ExamsNeededForCD;
                    printing.ExamID = item.ExamID;
                    printing.Name = item.Name;
                    printing.PrintsForOneModel = item.PrintsForOneModel;
                    printing.ModifiedDate = DateTime.Now;
                    printing.CreatedDate = DateTime.Now;
                    printing.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = new BusinessLogicLayer.Components.PPM.BookPrintingOperationLogic().GetAll();
            return PartialView("_BookPrintingOperationGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult BookPrintingOperationGridViewPartialDelete(System.Int32 BookPrintingOperationID)
        {
            
            if (BookPrintingOperationID >= 0)
            {
                try
                {
                    BusinessLogicLayer.Components.PPM.BookPrintingOperationLogic logic = new BusinessLogicLayer.Components.PPM.BookPrintingOperationLogic();
                    if (!logic.HasDependencies(BookPrintingOperationID))
                    {
                        BusinessLogicLayer.Entity.PPM.BookPrintingOperation type = new BusinessLogicLayer.Entity.PPM.BookPrintingOperation(BookPrintingOperationID);
                        type.Delete();
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = new BusinessLogicLayer.Components.PPM.BookPrintingOperationLogic().GetAll();
            return PartialView("_BookPrintingOperationGridViewPartial", model);
        }
    }
}