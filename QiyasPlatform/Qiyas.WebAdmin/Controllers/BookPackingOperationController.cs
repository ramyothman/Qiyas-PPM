using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using Qiyas.BusinessLogicLayer;
namespace Qiyas.WebAdmin.Controllers
{
    public class BookPackingOperationController : Controller
    {
        public int PrintingOperationID
        {
            set
            {
                var key = "34FAA431-CF79-4869-9488-93F6AAE81263";
                var Session = HttpContext.Session;
                Session[key] = value;
            }
            get
            {
                var key = "34FAA431-CF79-4869-9488-93F6AAE81263";
                var Session = HttpContext.Session;
                if (Session[key] == null)
                    Session[key] = 0;
                return (int)Session[key];
            }
        }

        public ActionResult Index(int ID = 0)
        {
            string url = string.Format("{0}", Url.Action("Index", "BookPrintingOperation"));
            var model = new BusinessLogicLayer.Entity.PPM.BookPrintingOperation(ID);
            if (model == null || !model.HasObject)
            {
                return RedirectToAction("Index", "BookPrintingOperation");
            }
            MainID = ID;
            ViewBag.PrintingID = ID;
            PrintingOperationID = ID;
            return View(model);

        }

        #region Grid Operations
        [ValidateInput(false)]
        public ActionResult PackingGridViewPartial()
        {
            var model = new BusinessLogicLayer.Components.PPM.BookPackingOperationLogic().GetByBookPrintingID(PrintingOperationID);
            return PartialView("_PackingGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult PackingGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Qiyas.BusinessLogicLayer.Entity.PPM.BookPackingOperation item)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    bool isValid = true;
                    if(item.PackingCalculationTypeID == 2)
                    {
                        if(item.PackingParentID  == 0)
                        {
                            ViewData["EditError"] = Resources.MainResource.EnterPackingParentID;
                            isValid = false;
                        }
                    }
                    int totalPackage = TotalPackages(item);
                    BusinessLogicLayer.Entity.PPM.BookPrintingOperation printingOperation = new BusinessLogicLayer.Entity.PPM.BookPrintingOperation(PrintingOperationID);
                    
                    if (printingOperation != null)
                    {
                        BusinessLogicLayer.Components.PPM.BookPackingOperationLogic logic = new BusinessLogicLayer.Components.PPM.BookPackingOperationLogic();
                        BusinessLogicLayer.Entity.PPM.PackagingType ptype = new BusinessLogicLayer.Entity.PPM.PackagingType(item.PackagingTypeID.Value);
                        int totalItems = ptype.BooksPerPackage == 3 && ptype.ExamModelCount == 1 ? printingOperation.ExamsNeededForA3.Value : printingOperation.PrintsForOneModel.Value;
                        int currentTotal = ptype.BooksPerPackage == 3 && ptype.ExamModelCount == 1 ? logic.GetTotalA3(printingOperation.BookPrintingOperationID) : logic.GetTotal(printingOperation.BookPrintingOperationID);
                        if(totalItems < (totalPackage + currentTotal))
                        {
                            isValid = false;
                            ViewData["EditError"] = Resources.MainResource.TotalPackGreaterThanOverallTotal;
                        }
                    }
                    if(isValid)
                    {
                        BusinessLogicLayer.Entity.PPM.BookPackingOperation entity = new BusinessLogicLayer.Entity.PPM.BookPackingOperation();
                        entity.AllocatedFrom = item.AllocatedFrom;
                        entity.BookPrintingOperationID = PrintingOperationID;
                        entity.CreatedDate = DateTime.Now;
                        entity.ModifiedDate = DateTime.Now;
                        entity.Name = item.Name;
                        entity.PackingParentID = item.PackingParentID;
                        entity.PackageTotal = totalPackage;
                        entity.PackagingTypeID = item.PackagingTypeID;
                        entity.PackingCalculationTypeID = item.PackingCalculationTypeID;
                        entity.PackingValue = item.PackingValue;
                        entity.Save();
                    }
                    
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = new BusinessLogicLayer.Components.PPM.BookPackingOperationLogic().GetByBookPrintingID(PrintingOperationID);
            return PartialView("_PackingGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult PackingGridViewPartialBatch(MVCxGridViewBatchUpdateValues<Qiyas.BusinessLogicLayer.Entity.PPM.BookPackingOperation, int> updateValues)
        {

            try
            {
                foreach (var item in updateValues.Insert)
                {
                    if (updateValues.IsValid(item))
                    {
                        BusinessLogicLayer.Entity.PPM.BookPackingOperation entity = new BusinessLogicLayer.Entity.PPM.BookPackingOperation();
                        entity.AllocatedFrom = item.AllocatedFrom;
                        entity.BookPrintingOperationID = PrintingOperationID;
                        entity.CreatedDate = DateTime.Now;
                        entity.ModifiedDate = DateTime.Now;
                        entity.Name = item.Name;
                        entity.PackageTotal = item.PackageTotal;
                        entity.PackagingTypeID = item.PackagingTypeID;
                        entity.PackingCalculationTypeID = item.PackingCalculationTypeID;
                        entity.PackingValue = item.PackingValue;
                        entity.Save();
                    }
                }
                foreach (var item in updateValues.Update)
                {
                    if (updateValues.IsValid(item))
                    {
                        BusinessLogicLayer.Entity.PPM.BookPackingOperation entity = new BusinessLogicLayer.Entity.PPM.BookPackingOperation(item.BookPackingOperationID);
                        entity.AllocatedFrom = item.AllocatedFrom;
                        entity.ModifiedDate = DateTime.Now;
                        entity.Name = item.Name;
                        entity.PackageTotal = item.PackageTotal;
                        entity.PackagingTypeID = item.PackagingTypeID;
                        entity.PackingCalculationTypeID = item.PackingCalculationTypeID;
                        entity.PackingValue = item.PackingValue;
                        entity.Save();
                    }
                }
                foreach (var ID in updateValues.DeleteKeys)
                {
                    BusinessLogicLayer.Entity.PPM.BookPackingOperation entity = new BusinessLogicLayer.Entity.PPM.BookPackingOperation(Convert.ToInt32(ID));
                    entity.Delete();
                }
            }
            catch (Exception e)
            {
                ViewData["EditError"] = e.Message;
            }
            var model = new BusinessLogicLayer.Components.PPM.BookPackingOperationLogic().GetByBookPrintingID(PrintingOperationID);
            return PartialView("_PackingGridViewPartial", model);
        }

        

        [HttpPost, ValidateInput(false)]
        public ActionResult PackingGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Qiyas.BusinessLogicLayer.Entity.PPM.BookPackingOperation item)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    bool isValid = true;
                    if(item.PackingCalculationTypeID == 2)
                    {
                        if(item.PackingParentID  == 0)
                        {
                            ViewData["EditError"] = Resources.MainResource.EnterPackingParentID;
                            isValid = false;
                        }
                    }
                    BusinessLogicLayer.Entity.PPM.BookPackingOperation entity = new BusinessLogicLayer.Entity.PPM.BookPackingOperation(item.BookPackingOperationID);
                    int totalPackage = TotalPackages(item);
                    BusinessLogicLayer.Entity.PPM.BookPrintingOperation printingOperation = new BusinessLogicLayer.Entity.PPM.BookPrintingOperation(PrintingOperationID);
                    if (printingOperation != null)
                    {
                        BusinessLogicLayer.Components.PPM.BookPackingOperationLogic logic = new BusinessLogicLayer.Components.PPM.BookPackingOperationLogic();
                        BusinessLogicLayer.Entity.PPM.PackagingType ptype = new BusinessLogicLayer.Entity.PPM.PackagingType(item.PackagingTypeID.Value);
                        int totalItems = ptype.BooksPerPackage == 3 && ptype.ExamModelCount == 1 ? printingOperation.ExamsNeededForA3.Value : printingOperation.PrintsForOneModel.Value;
                        int currentTotal = ptype.BooksPerPackage == 3 && ptype.ExamModelCount == 1 ? logic.GetTotalA3(printingOperation.BookPrintingOperationID) : logic.GetTotal(printingOperation.BookPrintingOperationID);
                        currentTotal -= entity.PackageTotal.Value;
                        if (totalItems < (totalPackage + currentTotal))
                        {
                            isValid = false;
                            ViewData["EditError"] = Resources.MainResource.TotalPackGreaterThanOverallTotal;
                        }
                    }
                    if (isValid)
                    {
                        
                        entity.AllocatedFrom = item.AllocatedFrom;
                        entity.ModifiedDate = DateTime.Now;
                        entity.Name = item.Name;
                        entity.PackageTotal = totalPackage;
                        entity.PackagingTypeID = item.PackagingTypeID;
                        entity.PackingParentID = item.PackingParentID;
                        entity.PackingCalculationTypeID = item.PackingCalculationTypeID;
                        entity.PackingValue = item.PackingValue;
                        entity.Save();
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = Resources.MainResource.PleaseCorrectErrors;
            var model = new BusinessLogicLayer.Components.PPM.BookPackingOperationLogic().GetByBookPrintingID(PrintingOperationID);
            return PartialView("_PackingGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult PackingGridViewPartialDelete(System.Int32 BookPackingOperationID)
        {
            
            if (BookPackingOperationID >= 0)
            {
                try
                {
                    BusinessLogicLayer.Entity.PPM.BookPackingOperation entity = new BusinessLogicLayer.Entity.PPM.BookPackingOperation(Convert.ToInt32(BookPackingOperationID));
                    entity.Delete();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = new BusinessLogicLayer.Components.PPM.BookPackingOperationLogic().GetByBookPrintingID(PrintingOperationID);
            return PartialView("_PackingGridViewPartial", model);
        }
        #endregion

        public ActionResult SavePackage(string send, FormCollection form)
        {
            var model = new BusinessLogicLayer.Entity.PPM.BookPrintingOperation(PrintingOperationID);
            return View("Index", model);
        }

        #region Helpers
        private int TotalPackages(BusinessLogicLayer.Entity.PPM.BookPackingOperation item)
        {
            int total = 0;
            BusinessLogicLayer.Entity.PPM.PackagingType ptype = new BusinessLogicLayer.Entity.PPM.PackagingType(item.PackagingTypeID.Value);
            if(ptype == null)
                return 0;
            BusinessLogicLayer.Entity.PPM.BookPrintingOperation printingOperation = new BusinessLogicLayer.Entity.PPM.BookPrintingOperation(PrintingOperationID);
            if (printingOperation == null)
                return 0;
            int totalItems = ptype.BooksPerPackage == 3 && ptype.ExamModelCount == 1 ? printingOperation.ExamsNeededForA3.Value : printingOperation.PrintsForOneModel.Value;

            if(item.PackingCalculationTypeID == 1)
            {
                double t = item.PackingValue.Value / 100.00;
                total = Convert.ToInt32(Math.Ceiling(totalItems * t));
            }
            else if(item.PackingCalculationTypeID == 2)
            {
                BusinessLogicLayer.Entity.PPM.BookPackingOperation operation = new BusinessLogicLayer.Entity.PPM.BookPackingOperation(item.PackingParentID.Value);
                if (operation == null)
                    return 0;
                totalItems = operation.PackageTotal.Value;
                if (operation.PackingCalculationTypeID == 1)
                {
                    double t = item.PackingValue.Value / 100.00;
                    total = Convert.ToInt32(Math.Ceiling(totalItems * t));
                }
                else if(operation.PackingCalculationTypeID == 2)
                {
                    BusinessLogicLayer.Entity.PPM.BookPackingOperation operation2 = new BusinessLogicLayer.Entity.PPM.BookPackingOperation(operation.PackingParentID.Value);
                    if (operation2 == null)
                        return 0;
                    totalItems = operation2.PackageTotal.Value;
                    if (operation2.PackingCalculationTypeID == 1)
                    {
                        double t = item.PackingValue.Value / 100.00;
                        total = Convert.ToInt32(Math.Ceiling(totalItems * t));
                    }
                    else if (operation2.PackingCalculationTypeID == 3)
                    {
                        total = item.PackingValue.Value;
                    }
                }
                else if(operation.PackingCalculationTypeID == 3)
                {
                    total = item.PackingValue.Value;
                }

            }
            else if(item.PackingCalculationTypeID == 3)
            {
                total = item.PackingValue.Value;
            }
            return total;
        }

        public static int MainID;

        public static int GetID(dynamic bag)
        {
            int id = 0;
            try
            {
                id = bag.PrintingID;
            }
            catch (Exception ex) { }
            return id;
        }

        #endregion
    }
}