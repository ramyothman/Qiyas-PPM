﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using Qiyas.BusinessLogicLayer;
using System.Security.Cryptography;
using System.Text;
using System.IO;
namespace Qiyas.WebAdmin.Controllers
{
    [Authorize]
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
            ViewBag.HasError = false;
            ViewBag.NotifyMessage = "";
            ViewBag.PrintingID = ID;
            ViewBag.IsSaved = model.OperationStatusID > 1;
            PrintingOperationID = ID;
            return View(model);

        }

        public ActionResult PrintPacks(int ID = 0)
        {
            string url = string.Format("{0}", Url.Action("Index", "BookPrintingOperation"));
            var model = new BusinessLogicLayer.Entity.PPM.BookPrintingOperation(ID);
            if (model == null || !model.HasObject)
            {
                return RedirectToAction("Index", "BookPrintingOperation");
            }
            
            MainID = ID;
            ViewBag.HasError = false;
            ViewBag.NotifyMessage = "";
            ViewBag.PrintingID = ID;
            PrintingOperationID = ID;
            ViewBag.IsSaved = model.OperationStatusID > 1;

            report.DataSource = new Qiyas.BusinessLogicLayer.Components.PPM.BookPackItemLogic().GetAllByPrintingIDForPrinting(Qiyas.WebAdmin.Controllers.BookPackingOperationController.MainID);
            var stream = new MemoryStream();
            report.ExportToPdf(stream);
            return File(stream.GetBuffer(), "application/pdf");
            //return View(model);
        }

        #region Grid Operations
        [ValidateInput(false)]
        public ActionResult PackingGridViewPartial()
        {
            var model = new BusinessLogicLayer.Components.PPM.BookPackingOperationLogic().GetByBookPrintingID(PrintingOperationID);
            var modelPrinting = new BusinessLogicLayer.Entity.PPM.BookPrintingOperation(MainID);
            ViewBag.IsSaved = modelPrinting.OperationStatusID > 1;
            return PartialView("_PackingGridViewPartial", model);
        }

        [ValidateInput(false)]
        public ActionResult RePackingGridViewPartial()
        {
            var model = new BusinessLogicLayer.Components.PPM.ViewBookRepackOperationLogic().GetAll(PrintingOperationID);
            var modelPrinting = new BusinessLogicLayer.Entity.PPM.BookPrintingOperation(MainID);
            ViewBag.IsSaved = modelPrinting.OperationStatusID > 1;
            return PartialView("_RePackingGridViewPartial", model);
        }

        public int GetTotalPerBooks()
        {
            int totalBooksPerModel = 0;
            var model = new BusinessLogicLayer.Components.PPM.BookPackingOperationLogic().GetByBookPrintingID(PrintingOperationID);
            foreach (var item in model)
            {
                if (item.PackingCalculationTypeID == 2)
                    continue;
                var ptype = new BusinessLogicLayer.Entity.PPM.PackagingType(item.PackagingTypeID.Value);
                if (ptype.BooksPerPackage == 3 && ptype.ExamModelCount == 1)
                {
                    //totalA3 += item.PackageTotal.Value;
                }
                else
                {
                    if (item.NumberofBooksPerModel.HasValue)
                        totalBooksPerModel += item.NumberofBooksPerModel.Value;
                    
                }

            }
            return totalBooksPerModel;
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
                        if (item.PackingParentID == null || item.PackingParentID == 0)
                        {
                            ViewData["EditError"] = Resources.MainResource.EnterPackingParentID;
                            isValid = false;
                            var models = new BusinessLogicLayer.Components.PPM.BookPackingOperationLogic().GetByBookPrintingID(PrintingOperationID);
                            var modelPrintings = new BusinessLogicLayer.Entity.PPM.BookPrintingOperation(MainID);
                            ViewBag.IsSaved = modelPrintings.OperationStatusID > 1;
                            return PartialView("_PackingGridViewPartial", models);
                        }
                    }
                    int totalPackage = TotalPackages(item);
                    if(totalPackage == 0)
                    {
                        ViewData["EditError"] = "يجب ان يكون العدد اكبر من صفر";
                        isValid = false;
                        var models = new BusinessLogicLayer.Components.PPM.BookPackingOperationLogic().GetByBookPrintingID(PrintingOperationID);
                        var modelPrintings = new BusinessLogicLayer.Entity.PPM.BookPrintingOperation(MainID);
                        ViewBag.IsSaved = modelPrintings.OperationStatusID > 1;
                        return PartialView("_PackingGridViewPartial", models);
                    }
                    BusinessLogicLayer.Entity.PPM.BookPrintingOperation printingOperation = new BusinessLogicLayer.Entity.PPM.BookPrintingOperation(PrintingOperationID);
                    var packType = new BusinessLogicLayer.Entity.PPM.PackagingType(item.PackagingTypeID.Value);
                    if(item.PackingCalculationTypeID == 3)
                    {
                        if ((item.PackingValue.Value % packType.BooksPerPackage.Value) != 0)
                        {
                            ViewData["EditError"] = Resources.MainResource.AllowDivisionForPackageValue;
                            isValid = false;
                        }
                        if(packType.ExamModelCount.Value == 1 && packType.BooksPerPackage.Value == 3)
                        {
                            if (item.PackingValue > printingOperation.ExamsNeededForA3)
                            {
                                ViewData["EditError"] = Resources.MainResource.EnterNumberLessThanBooksCount;
                                isValid = false;
                            }
                        }
                        else
                        {
                            if (item.PackingValue > printingOperation.PrintsForOneModel)
                            {
                                ViewData["EditError"] = Resources.MainResource.EnterNumberLessThanBooksCount;
                                isValid = false;
                            }
                        }
                        
                    }
                    int totalPerBook = GetTotalPerBooks();
                    if(item.NumberofBooksPerModel.HasValue)
                    {
                        if(item.PackingCalculationTypeID != 2)
                        {
                            totalPerBook += item.NumberofBooksPerModel.Value;
                            if (totalPerBook > printingOperation.PrintsForOneModel.Value)
                            {
                                ViewData["EditError"] = "العدد اكبر من المتاح";
                                isValid = false;
                                var models = new BusinessLogicLayer.Components.PPM.BookPackingOperationLogic().GetByBookPrintingID(PrintingOperationID);
                                var modelPrintings = new BusinessLogicLayer.Entity.PPM.BookPrintingOperation(MainID);
                                ViewBag.IsSaved = modelPrintings.OperationStatusID > 1;
                                return PartialView("_PackingGridViewPartial", models);
                            }
                        }
                        else
                        {
                            
                            int totalBooksPerModel = 0;
                            var modelSub = new BusinessLogicLayer.Components.PPM.BookPackingOperationLogic().GetByBookPrintingID(PrintingOperationID);
                            foreach (var itemSub in modelSub)
                            {
                                if (itemSub.PackingCalculationTypeID != 2 && itemSub.PackingParentID != item.PackingParentID)
                                    continue;

                                if (itemSub.NumberofBooksPerModel.HasValue)
                                    totalBooksPerModel += itemSub.NumberofBooksPerModel.Value;
                            }
                            BusinessLogicLayer.Entity.PPM.BookPackingOperation subOP = new BusinessLogicLayer.Entity.PPM.BookPackingOperation(item.PackingParentID.Value);
                            int totalForParent = subOP.NumberofBooksPerModel.Value;
                            totalBooksPerModel += item.NumberofBooksPerModel.Value;
                            if (totalBooksPerModel > totalForParent)
                            {
                                ViewData["EditError"] = "العدد اكبر من المتاح";
                                isValid = false;
                                var models = new BusinessLogicLayer.Components.PPM.BookPackingOperationLogic().GetByBookPrintingID(PrintingOperationID);
                                var modelPrintings = new BusinessLogicLayer.Entity.PPM.BookPrintingOperation(MainID);
                                ViewBag.IsSaved = modelPrintings.OperationStatusID > 1;
                                return PartialView("_PackingGridViewPartial", models);
                            }
                        }
                        
                        
                    }
                    
                    BusinessLogicLayer.Entity.PPM.PackagingType ptype = new BusinessLogicLayer.Entity.PPM.PackagingType(item.PackagingTypeID.Value);
                    //if(item.PackingCalculationTypeID == 1)
                    //{
                    //    totalPackage /= ptype.BooksPerPackage.Value;
                    //}
                    if (printingOperation != null)
                    {
                        BusinessLogicLayer.Components.PPM.BookPackingOperationLogic logic = new BusinessLogicLayer.Components.PPM.BookPackingOperationLogic();
                        if(logic.ContainPackType(MainID, ptype.PackagingTypeID))
                        {
                            ViewData["EditError"] = "لا يمكن اضافة نوع التحزيم اكثر من مرة";
                            isValid = false;
                        }
                        int totalItems = ptype.BooksPerPackage == 3 && ptype.ExamModelCount == 1 ? printingOperation.ExamsNeededForA3.Value : printingOperation.PrintsForOneModel.Value;
                        int currentTotal = ptype.BooksPerPackage == 3 && ptype.ExamModelCount == 1 ? logic.GetTotalItemsA3(printingOperation.BookPrintingOperationID) : logic.GetTotalItems(printingOperation.BookPrintingOperationID);
                        int totalPrint = (totalPackage * ptype.BooksPerPackage.Value + currentTotal);
                        int count = new BusinessLogicLayer.Components.PPM.ExamLogic().GetExamModelCount(printingOperation.ExamID.Value);
                        if(count > 1)
                        {
                            totalItems = totalItems * count;
                        }
                        if(totalItems < totalPrint && item.PackingCalculationTypeID.Value != 2)
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
                        entity.NumberofBooksPerModel = item.NumberofBooksPerModel;
                        entity.PackageTotalPerModel = item.PackageTotalPerModel;
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
            var modelPrinting = new BusinessLogicLayer.Entity.PPM.BookPrintingOperation(MainID);
            ViewBag.IsSaved = modelPrinting.OperationStatusID > 1;
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
                        if (logic.ContainPackTypeExclExisting(MainID, item.PackagingTypeID.Value, item.BookPackingOperationID))
                        {
                            ViewData["EditError"] = "لا يمكن اضافة نوع التحزيم اكثر من مرة";
                            isValid = false;
                        }
                        BusinessLogicLayer.Entity.PPM.PackagingType ptype = new BusinessLogicLayer.Entity.PPM.PackagingType(item.PackagingTypeID.Value);
                        int totalItems = ptype.BooksPerPackage == 3 && ptype.ExamModelCount == 1 ? printingOperation.ExamsNeededForA3.Value : printingOperation.PrintsForOneModel.Value;
                        int currentTotal = ptype.BooksPerPackage == 3 && ptype.ExamModelCount == 1 ? logic.GetTotalA3(printingOperation.BookPrintingOperationID) : logic.GetTotal(printingOperation.BookPrintingOperationID);
                        currentTotal -= entity.PackageTotal.Value;
                        int totalPrint = (totalPackage + currentTotal) * ptype.BooksPerPackage.Value;
                        if (ptype.ExamModelCount > 1)
                        {
                            totalItems = totalItems * ptype.ExamModelCount.Value;
                        }
                        if (totalItems < totalPrint)
                        {
                            isValid = false;
                            ViewData["EditError"] = Resources.MainResource.TotalPackGreaterThanOverallTotal;
                        }
                        if(totalPackage == 0)
                        {
                            isValid = false;
                            ViewData["EditError"] = "العدد المسموح يجب ان يكون اكبر من صفر";
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
            var modelPrinting = new BusinessLogicLayer.Entity.PPM.BookPrintingOperation(MainID);
            ViewBag.IsSaved = modelPrinting.OperationStatusID > 1;
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
            var modelPrinting = new BusinessLogicLayer.Entity.PPM.BookPrintingOperation(MainID);
            ViewBag.IsSaved = modelPrinting.OperationStatusID > 1;
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
            int countExams = new BusinessLogicLayer.Components.PPM.ExamLogic().GetExamModelCount(printingOperation.ExamID.Value);
            if(item.PackingCalculationTypeID == 1)
            {
                
                double t = item.PackingValue.Value / 100.00;
                total = Convert.ToInt32(Math.Ceiling(totalItems * t));
                
                
                if (ptype.ExamModelCount == 1)
                {
                    total = total / (ptype.ExamModelCount.Value * ptype.BooksPerPackage.Value);
                    item.PackageTotalPerModel = total;
                    item.NumberofBooksPerModel = total * ptype.BooksPerPackage.Value;
                    total = total * countExams;
                }
                else
                {
                    total = total / (ptype.BooksPerPackage.Value);
                    item.PackageTotalPerModel = total;
                    item.NumberofBooksPerModel = total * ptype.BooksPerPackage.Value;
                    //total = total;
                }
                    
            }
            else if(item.PackingCalculationTypeID == 2)
            {
                BusinessLogicLayer.Entity.PPM.BookPackingOperation operation = new BusinessLogicLayer.Entity.PPM.BookPackingOperation(item.PackingParentID.Value);
                if (operation == null)
                    return 0;
                totalItems = operation.NumberofBooksPerModel.Value;
                
                if (operation.PackingCalculationTypeID == 1)
                {

                    double t = item.PackingValue.Value / 100.00;
                    
                    total = Convert.ToInt32(Math.Ceiling(totalItems * t));


                    if (ptype.ExamModelCount == 1)
                    {
                        total = total / (ptype.ExamModelCount.Value * ptype.BooksPerPackage.Value);
                        item.PackageTotalPerModel = total;
                        item.NumberofBooksPerModel = total * ptype.BooksPerPackage.Value;
                        total = total * countExams;
                    }
                    else
                    {
                        total = total / (ptype.BooksPerPackage.Value);
                        item.PackageTotalPerModel = total;
                        item.NumberofBooksPerModel = total * ptype.BooksPerPackage.Value;
                        //total = total;
                    }
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
                    
                    if (ptype.ExamModelCount == 1)
                    {
                        total = item.PackingValue.Value / (ptype.BooksPerPackage.Value * ptype.ExamModelCount.Value);
                        item.PackageTotalPerModel = total;
                        item.NumberofBooksPerModel = total * ptype.BooksPerPackage.Value;
                        total = total * countExams;
                    }
                    else
                    {
                        total = item.PackingValue.Value / (ptype.BooksPerPackage.Value);
                        item.PackageTotalPerModel = total;
                        item.NumberofBooksPerModel = total * ptype.BooksPerPackage.Value;
                        //total = total * ptype.ExamModelCount.Value;
                    }
                        
                }

            }
            else if(item.PackingCalculationTypeID == 3)
            {
                
                if (ptype.ExamModelCount == 1)
                {
                    total = item.PackingValue.Value / (ptype.BooksPerPackage.Value * ptype.ExamModelCount.Value);
                    item.PackageTotalPerModel = total;
                    item.NumberofBooksPerModel = total * ptype.BooksPerPackage.Value;
                    total = total * countExams;
                }
                else
                {
                    total = item.PackingValue.Value / (ptype.BooksPerPackage.Value);
                    item.PackageTotalPerModel = total;
                    item.NumberofBooksPerModel = total * ptype.BooksPerPackage.Value;
                    //total = total * ptype.ExamModelCount.Value;
                }
                    
            }
            return total;
        }

        public static int MainID, ExamCount;

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

        [HttpPost]
        public ActionResult SavePack(FormCollection form)
        {
            
            var model = new BusinessLogicLayer.Entity.PPM.BookPrintingOperation(PrintingOperationID);
            model.OperationStatusID = 2;
            model.Save();
            ViewBag.HasError = false;
            ViewBag.IsSaved = model.OperationStatusID > 1;
            ViewBag.NotifyMessage = Resources.MainResource.SaveSuccess;
            return View("Index", model);
        }

        [HttpPost]
        public ActionResult DeletePack(FormCollection form)
        {
            BusinessLogicLayer.Components.PPM.BookPackingOperationLogic logic = new BusinessLogicLayer.Components.PPM.BookPackingOperationLogic();
            var model = new BusinessLogicLayer.Entity.PPM.BookPrintingOperation(PrintingOperationID);
            if(model.OperationStatusID == 1 || model.OperationStatusID == 2)
            {
                logic.DeletePacksByPrintingID(PrintingOperationID);
                return RedirectToAction("Index", "BookPrintingOperation");
            }
            ViewBag.HasError = true;
            ViewBag.IsSaved = model.OperationStatusID > 1;
            ViewBag.NotifyMessage = Resources.MainResource.ErrorDeletingPack;
            return View("Index", model);
        }

        private void AddItemToPack(List<BusinessLogicLayer.Entity.PPM.BookPackItem> items, List<BusinessLogicLayer.Entity.PPM.PackagingType> packageTypes, List<BusinessLogicLayer.Entity.PPM.BookPackingOperation> packing, List<BusinessLogicLayer.Entity.PPM.BookPackingOperation> oldPacks, BusinessLogicLayer.Entity.PPM.BookPrintingOperation model, BusinessLogicLayer.Entity.PPM.Exam exam, ref int bookLast, ref int serial, BusinessLogicLayer.Entity.PPM.BookPackingOperation pack, ref int PackItemIDKey)
        {
            serial = 1;
            //incrementPacks = 0;
            var packType = (from x in packageTypes where x.PackagingTypeID == pack.PackagingTypeID select x).FirstOrDefault();
            var exists = (from x in oldPacks where x.BookPackingOperationID == pack.BookPackingOperationID select x).FirstOrDefault();
            int bookStart = 0;
            //int bookLast = count;
            if (exists == null || !exists.HasObject)
            {
                for (int i = 0; i < pack.PackageTotal.Value; i++)
                {
                    #region SubPack
                    int SubBooks = 0;
                    for(int j = 0; j < pack.SingleChildPackingOperations.Count; j++)
                    {
                        
                        AddItemToPack(items, packageTypes, pack.ChildPackingOperations, oldPacks, model, exam, ref bookLast, ref serial, pack.SingleChildPackingOperations[j], ref PackItemIDKey);
                        i += pack.SingleChildPackingOperations[j].PackageTotal.Value;
                        SubBooks += pack.SingleChildPackingOperations[j].NumberofBooksPerModel.Value;
                    }

                    for (int j = 0; j < pack.MultiChildPackingOperations.Count; j++)
                    {

                        AddItemToPack(items, packageTypes, pack.ChildPackingOperations, oldPacks, model, exam, ref bookLast, ref serial, pack.MultiChildPackingOperations[j], ref PackItemIDKey);
                        i += pack.MultiChildPackingOperations[j].PackageTotal.Value;
                        SubBooks += pack.MultiChildPackingOperations[j].NumberofBooksPerModel.Value;
                    }
                    if (SubBooks >= pack.NumberofBooksPerModel.Value)
                        break;
                    #endregion

                    #region Main Pack
                    BusinessLogicLayer.Entity.PPM.BookPackItem item = new BusinessLogicLayer.Entity.PPM.BookPackItem();
                    item.BookPackingOperationID = pack.BookPackingOperationID;
                    item.OperationStatusID = 2;

                    item.PackSerial = serial;

                    string modelCode = "";
                    List<BusinessLogicLayer.Entity.PPM.BookPackItemModel> itemModels = new List<BusinessLogicLayer.Entity.PPM.BookPackItemModel>();
                    bool addModelsToCount = true;
                    bookStart = bookLast + 1;
                    bookLast = bookStart + (packType.BooksPerPackage.Value - 1);
                    bool isA3 = (packType.ExamModelCount == 1 && packType.BooksPerPackage == 3);
                    foreach (BusinessLogicLayer.Entity.PPM.ExamModelItem examModel in exam.ExamModels)
                    {
                        
                        if (packType.ExamModelCount > 1 || isA3)
                        {
                            
                            BusinessLogicLayer.Entity.PPM.BookPackItemModel newModel = new BusinessLogicLayer.Entity.PPM.BookPackItemModel();
                            newModel.BookPackItemID = item.BookPackItemID;
                            newModel.ExamModelID = examModel.ExamModelID;
                            modelCode += examModel.ExamModelID + "-";
                            item.StartBookSerial = bookStart;
                            item.LastBookSerial = bookLast;
                            itemModels.Add(newModel);
                        }
                        else
                        {
                            
                            itemModels = new List<BusinessLogicLayer.Entity.PPM.BookPackItemModel>();
                            BusinessLogicLayer.Entity.PPM.BookPackItem itemUnit = new BusinessLogicLayer.Entity.PPM.BookPackItem();
                            itemUnit.BookPackItemID = PackItemIDKey++;
                            itemUnit.BookPackingOperationID = pack.BookPackingOperationID;
                            itemUnit.OperationStatusID = 2;

                            itemUnit.StartBookSerial = bookStart;
                            itemUnit.LastBookSerial = bookLast;
                            itemUnit.PackSerial = serial;
                            //itemUnit.PackCode = PrintingOperationID + "-" + pack.BookPackingOperationID + "-" + pack.PackagingTypeID + "-" + examModel.ExamModelID + "-" + serial;
                            itemUnit.PackCode = RandomString(12);
                            BusinessLogicLayer.Entity.PPM.BookPackItemModel newModel = new BusinessLogicLayer.Entity.PPM.BookPackItemModel();
                            newModel.BookPackItemID = item.BookPackItemID;
                            newModel.ExamModelID = examModel.ExamModelID;
                            modelCode += examModel.ExamModelID + "-";
                            itemModels.Add(newModel);
                            itemUnit.ItemModels = itemModels;
                            items.Add(itemUnit);
                            if(addModelsToCount)
                            {
                                i += exam.ExamModels.Count - 1;
                                addModelsToCount = false;
                            }
                            //serial++;
                        }

                    }
                    if (packType.ExamModelCount > 1 || isA3)
                    {
                        if (!string.IsNullOrEmpty(modelCode))
                            modelCode = modelCode.Remove(modelCode.Length - 1, 1);
                        //item.PackCode = PrintingOperationID + "-" + pack.BookPackingOperationID + "-" + pack.PackagingTypeID + "-" + modelCode + "-" + serial;
                        item.PackCode = RandomString(12);
                        item.BookPackItemID = PackItemIDKey++;
                        item.ItemModels = itemModels;
                        items.Add(item);

                    }
                    SubBooks += packType.BooksPerPackage.Value;
                    if (SubBooks >= pack.NumberofBooksPerModel.Value)
                        break;
                    serial++;

                    
                    #endregion
                }

            }
        }
        [HttpPost]
        public ActionResult NumberingPack(FormCollection form)
        {
            BusinessLogicLayer.Components.PPM.BookPackingOperationLogic logic = new BusinessLogicLayer.Components.PPM.BookPackingOperationLogic();
            var model = new BusinessLogicLayer.Entity.PPM.BookPrintingOperation(PrintingOperationID);
            List<BusinessLogicLayer.Entity.PPM.BookPackingOperation> packing = logic.GetAllPackagingTypeByBookPrintingID(PrintingOperationID);
            List<BusinessLogicLayer.Entity.PPM.PackagingType> packageTypes = new BusinessLogicLayer.Components.PPM.PackagingTypeLogic().GetAll();
            var orderedPackageTypes = (from x in packageTypes orderby x.Total descending select x);
            List<BusinessLogicLayer.Entity.PPM.BookPackingOperation> orderedPack = new List<BusinessLogicLayer.Entity.PPM.BookPackingOperation>();
            List<BusinessLogicLayer.Entity.PPM.BookPackingOperation> oldPacks = logic.GetPackedByBookPrintingID(PrintingOperationID);

            List<BusinessLogicLayer.Entity.PPM.BookPackItem> items = new List<BusinessLogicLayer.Entity.PPM.BookPackItem>();
            BusinessLogicLayer.Entity.PPM.Exam exam = new BusinessLogicLayer.Entity.PPM.Exam(model.ExamID.Value);

            int count = exam.ExamModels.Count;
            int serial = logic.GetLastPackSerial(exam.ExamID) + 1;
            int bookSerial = 0;

            BusinessLogicLayer.Entity.PPM.BookPackItem itemID = new BusinessLogicLayer.Entity.PPM.BookPackItem(true);
            int PackItemIDKey = itemID.BookPackItemID;
            List<BusinessLogicLayer.Entity.PPM.BookPackingOperation> orderedPackSingle = new List<BusinessLogicLayer.Entity.PPM.BookPackingOperation>();
            List<BusinessLogicLayer.Entity.PPM.BookPackingOperation> orderedPackMultiple = new List<BusinessLogicLayer.Entity.PPM.BookPackingOperation>();
            bool isValidSubPacks = true;
            foreach (BusinessLogicLayer.Entity.PPM.PackagingType pckg in orderedPackageTypes)
            {
                var packOrder = (from x in packing where x.PackagingTypeID == pckg.PackagingTypeID select x).FirstOrDefault();
                
                if(packOrder != null && packOrder.HasObject)
                {
                    if (packOrder.PackingCalculationTypeID != 2)
                    {
                        if (pckg.ExamModelCount == 1 && pckg.BooksPerPackage == 3)
                            orderedPackMultiple.Add(packOrder);
                        else if (pckg.ExamModelCount == 1)
                            orderedPackSingle.Add(packOrder);
                        else
                            orderedPackMultiple.Add(packOrder);
                        orderedPack.Add(packOrder);
                    }
                    else
                    {
                        if(packOrder.PackingParentID.HasValue)
                        {
                            var packOrderParent = (from x in packing where x.BookPackingOperationID == packOrder.PackingParentID.Value select x).FirstOrDefault();
                            if (pckg.ExamModelCount == 1)
                                packOrderParent.SingleChildPackingOperations.Add(packOrder);
                            else
                                packOrderParent.MultiChildPackingOperations.Add(packOrder);
                            packOrderParent.ChildPackingOperations.Add(packOrder);
                        }
                    }
                }
            }

            #region Validation Check for sub packs
            bool isValidPacks = true;
            int sumAll = 0;
            foreach (BusinessLogicLayer.Entity.PPM.BookPackingOperation pack in orderedPack)
            {
                var pPackType = packageTypes.Where(c => c.BooksPerPackage == 3 && c.ExamModelCount == 1).FirstOrDefault();
                if(pPackType.PackagingTypeID != pack.PackagingTypeID.Value)
                    sumAll += pack.NumberofBooksPerModel.Value;
                if(pack.ChildPackingOperations.Count > 0)
                {
                    int sum = 0;
                    foreach (var packChild in pack.ChildPackingOperations)
                    {
                        if (packChild.NumberofBooksPerModel.HasValue)
                            sum += packChild.NumberofBooksPerModel.Value / packChild.PackageTotalPerModel.Value;
                    }
                    sum = (pack.NumberofBooksPerModel.Value / pack.PackageTotalPerModel.Value) - sum;
                    if (sum < 0)
                        isValidSubPacks = false;
                    var tempPackageType = (from x in packageTypes where x.PackagingTypeID == pack.PackagingTypeID.Value select x).FirstOrDefault();
                    if (tempPackageType != null)
                    {
                        if (sum > tempPackageType.BooksPerPackage.Value)
                            isValidSubPacks = false;
                        if ((tempPackageType.BooksPerPackage.Value % sum) != 0)
                            isValidSubPacks = false;
                    }
                }
            }
            if (sumAll > model.PrintsForOneModel.Value)
                isValidPacks = false;
            #endregion
            if (!isValidSubPacks)
            {
                ViewBag.HasError = true;
                ViewBag.IsSaved = model.OperationStatusID > 1;
                ViewBag.NotifyMessage = Resources.MainResource.ErrorSavingNumberPacksSubPacksProblem;
                System.Web.Routing.RouteValueDictionary tempDict = new System.Web.Routing.RouteValueDictionary();
                tempDict.Add("ID", MainID);
                return View("Index", model);
            }

            if (!isValidPacks)
            {
                ViewBag.HasError = true;
                ViewBag.IsSaved = model.OperationStatusID > 1;
                ViewBag.NotifyMessage = Resources.MainResource.NotMatchedQuantity;
                System.Web.Routing.RouteValueDictionary tempDict = new System.Web.Routing.RouteValueDictionary();
                tempDict.Add("ID", MainID);
                return View("Index", model);
            }

            
            
            model.OperationStatusID = 2;
            model.Save();

            foreach (BusinessLogicLayer.Entity.PPM.BookPackingOperation pack in orderedPackSingle)
            {
                AddItemToPack(items, packageTypes, packing, oldPacks, model, exam, ref bookSerial, ref serial, pack, ref PackItemIDKey);
            }

            foreach (BusinessLogicLayer.Entity.PPM.BookPackingOperation pack in orderedPackMultiple)
            {
                AddItemToPack(items, packageTypes, packing, oldPacks, model, exam, ref bookSerial, ref serial, pack, ref PackItemIDKey);
            }
                
            
            /*
             * BusinessLogicLayer.Entity.PPM.PackagingType ptype = new BusinessLogicLayer.Entity.PPM.PackagingType(pack.PackagingTypeID.Value);
                foreach(BusinessLogicLayer.Entity.PPM.ExamModelItem modelItem in exam.ExamModels)
                {
                    
                    for(int j = 0; j < model.PrintsForOneModel; j++)
                    {
                        BusinessLogicLayer.Entity.PPM.BookPackItem item = new BusinessLogicLayer.Entity.PPM.BookPackItem();
                        item.BookPackingOperationID = pack.BookPackingOperationID;
                        item.OperationStatusID = 
                    }
                }
             */
            Qiyas.BusinessLogicLayer.Components.PPM.BookPackItemLogic itemLogic = new BusinessLogicLayer.Components.PPM.BookPackItemLogic();
            itemLogic.SaveItemsOptimized(items);
            ViewBag.HasError = false;
            ViewBag.IsSaved = model.OperationStatusID > 1;
            ViewBag.NotifyMessage = Resources.MainResource.NumberingPackSuccess;
            System.Web.Routing.RouteValueDictionary dict = new System.Web.Routing.RouteValueDictionary();
            dict.Add("ID", MainID);
            return RedirectToAction("PrintPacks", dict);
            //return View("Index", model);
        }

        string RandomString(int length, string allowedChars = "0123456789")
        {
            if (length < 0) throw new ArgumentOutOfRangeException("length", "length cannot be less than zero.");
            if (string.IsNullOrEmpty(allowedChars)) throw new ArgumentException("allowedChars may not be empty.");

            const int byteSize = 0x100;
            var allowedCharSet = new HashSet<char>(allowedChars).ToArray();
            if (byteSize < allowedCharSet.Length) throw new ArgumentException(String.Format("allowedChars may contain no more than {0} characters.", byteSize));

            // Guid.NewGuid and System.Random are not particularly random. By using a
            // cryptographically-secure random number generator, the caller is always
            // protected, regardless of use.
            using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                var result = new StringBuilder();
                var buf = new byte[128];
                while (result.Length < length)
                {
                    rng.GetBytes(buf);
                    for (var i = 0; i < buf.Length && result.Length < length; ++i)
                    {
                        // Divide the byte into allowedCharSet-sized groups. If the
                        // random value falls into the last group and the last group is
                        // too small to choose from the entire allowedCharSet, ignore
                        // the value in order to avoid biasing the result.
                        var outOfRangeStart = byteSize - (byteSize % allowedCharSet.Length);
                        if (outOfRangeStart <= buf[i]) continue;
                        result.Append(allowedCharSet[buf[i] % allowedCharSet.Length]);
                    }
                }
                return result.ToString();
            }
        }

        Qiyas.WebAdmin.Common.Reports.PrintItemPack report = new Qiyas.WebAdmin.Common.Reports.PrintItemPack();

        public ActionResult PrintPacksTitleDocumentViewerPartial()
        {
            report.DataSource = new Qiyas.BusinessLogicLayer.Components.PPM.BookPackItemLogic().GetAllByPrintingIDForPrinting(Qiyas.WebAdmin.Controllers.BookPackingOperationController.MainID);
            //return DocumentViewerExtension.ExportTo(report, Request);
            //report.CreateDocument(true);
            return PartialView("_PrintPacksTitleDocumentViewerPartial", report);
        }

        public ActionResult PrintPacksTitleDocumentViewerPartialExport()
        {
            report.DataSource = new Qiyas.BusinessLogicLayer.Components.PPM.BookPackItemLogic().GetAllByPrintingIDForPrinting(Qiyas.WebAdmin.Controllers.BookPackingOperationController.MainID);
            
            return DocumentViewerExtension.ExportTo(report, Request);
        }


        [HttpPost]
        public ActionResult GetPackingTotals()
        {
            BusinessLogicLayer.Components.PPM.PackagingTypeLogic PackagingTypeLogic = new BusinessLogicLayer.Components.PPM.PackagingTypeLogic();
            Qiyas.WebAdmin.Models.PackageOperationTotal totalObject = new Models.PackageOperationTotal();
            var model = new BusinessLogicLayer.Components.PPM.BookPackingOperationLogic().GetByBookPrintingID(PrintingOperationID);
            BusinessLogicLayer.Entity.PPM.BookPrintingOperation printingOperation = new BusinessLogicLayer.Entity.PPM.BookPrintingOperation(PrintingOperationID);
            int totalCount = 0;
            int totalPerModel = 0;
            int totalBooksPerModel = 0;
            int totalA3 = 0;
            var remaining = printingOperation.PrintsForOneModel.Value;
            foreach(var item in model)
            {
                if (item.PackingCalculationTypeID == 2)
                    continue;
                var ptype = new BusinessLogicLayer.Entity.PPM.PackagingType(item.PackagingTypeID.Value);
                if(ptype.BooksPerPackage == 3 && ptype.ExamModelCount == 1)
                {
                    totalA3 += item.PackageTotal.Value;
                }
                else
                {
                    totalCount += item.PackageTotal.Value;
                    if (item.NumberofBooksPerModel.HasValue)
                        totalBooksPerModel += item.NumberofBooksPerModel.Value;
                    if (item.PackageTotalPerModel.HasValue)
                        totalPerModel += item.PackageTotalPerModel.Value;
                }
                
            }

            totalObject.Total = totalCount;
            totalObject.TotalA3 = totalA3;
            totalObject.TotalBooksPerModel = totalBooksPerModel;
            totalObject.TotalPerModel = totalPerModel;
            totalObject.RemainingPerModel = remaining - totalBooksPerModel;
            //totalCount + "," + totalBooksPerModel + "," + totalPerModel + "," + totalA3
            return Json(totalObject);
            

        }

    }
}