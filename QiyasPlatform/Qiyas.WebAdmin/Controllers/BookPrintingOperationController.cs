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
    [Authorize]
    public class BookPrintingOperationController : Controller
    {
        // GET: BookPrintingOperation
        public ActionResult Index()
        {
            return View();
        }

        #region Grid View Events
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
                    if((item.ExamsNeededForA3.Value % 3) != 0)
                    {
                        ViewData["EditError"] = "يجب ان يكون الايه 3 من مضاعفات الثلاثة";
                    }
                    else
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
                        if(type.OperationStatusID == 1)
                        {
                            type.Delete();
                        }
                        else
                        {
                            ViewData["EditError"] = "لا يمكن حذف هذا الطلب يجب ان يكون نوع الطلب طباعة";
                        }
                        
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
        #endregion

        #region Custom Grid View Button Events
        
        public ActionResult PackExam(int ID)
        {
            string url = string.Format("{0}/Index/{1}", Url.Action("Index", "BookPackingOperation"), ID);
            var model = new BusinessLogicLayer.Entity.PPM.BookPrintingOperation(ID);
            if (model == null)
                return View();
            else
                return Redirect(url);
        }
        #endregion
    }
}