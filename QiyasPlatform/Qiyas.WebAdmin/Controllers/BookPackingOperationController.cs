using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

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
    }
}