using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using Qiyas.BusinessLogicLayer;
using System.Security.Cryptography;
using System.Text;
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
                    BusinessLogicLayer.Entity.PPM.PackagingType ptype = new BusinessLogicLayer.Entity.PPM.PackagingType(item.PackagingTypeID.Value);
                    //if(item.PackingCalculationTypeID == 1)
                    //{
                    //    totalPackage /= ptype.BooksPerPackage.Value;
                    //}
                    if (printingOperation != null)
                    {
                        BusinessLogicLayer.Components.PPM.BookPackingOperationLogic logic = new BusinessLogicLayer.Components.PPM.BookPackingOperationLogic();
                        
                        int totalItems = ptype.BooksPerPackage == 3 && ptype.ExamModelCount == 1 ? printingOperation.ExamsNeededForA3.Value : printingOperation.PrintsForOneModel.Value;
                        int currentTotal = ptype.BooksPerPackage == 3 && ptype.ExamModelCount == 1 ? logic.GetTotalItemsA3(printingOperation.BookPrintingOperationID) : logic.GetTotalItems(printingOperation.BookPrintingOperationID);
                        int totalPrint = (totalPackage * ptype.BooksPerPackage.Value + currentTotal);
                        int count = new BusinessLogicLayer.Components.PPM.ExamLogic().GetExamModelCount(printingOperation.ExamID.Value);
                        if(count > 1)
                        {
                            totalItems = totalItems * count;
                        }
                        if(totalItems < totalPrint)
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
            int countExams = new BusinessLogicLayer.Components.PPM.ExamLogic().GetExamModelCount(printingOperation.ExamID.Value);
            if(item.PackingCalculationTypeID == 1)
            {
                
                double t = item.PackingValue.Value / 100.00;
                total = Convert.ToInt32(Math.Ceiling(totalItems * t));
                total = total / (ptype.ExamModelCount.Value * ptype.BooksPerPackage.Value);
                if (ptype.ExamModelCount == 1)
                    total = total * countExams;
                else
                    total = total * ptype.ExamModelCount.Value;
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
                    total = total / (ptype.ExamModelCount.Value * ptype.BooksPerPackage.Value);
                    if (ptype.ExamModelCount == 1)
                        total = total * countExams;
                    else
                        total = total * ptype.ExamModelCount.Value;
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
                    total = item.PackingValue.Value / (ptype.BooksPerPackage.Value * ptype.ExamModelCount.Value);
                    if (ptype.ExamModelCount == 1)
                        total = total * countExams;
                    else
                        total = total * ptype.ExamModelCount.Value;
                }

            }
            else if(item.PackingCalculationTypeID == 3)
            {
                total = item.PackingValue.Value / (ptype.BooksPerPackage.Value * ptype.ExamModelCount.Value);
                if (ptype.ExamModelCount == 1)
                    total = total * countExams;
                else
                    total = total * ptype.ExamModelCount.Value;
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

        [HttpPost]
        public ActionResult SavePack(FormCollection form)
        {
            
            var model = new BusinessLogicLayer.Entity.PPM.BookPrintingOperation(PrintingOperationID);
            model.OperationStatusID = 2;
            model.Save();
            ViewBag.HasError = false;
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
            ViewBag.NotifyMessage = Resources.MainResource.ErrorDeletingPack;
            return View("Index", model);
        }

        private void AddItemToPack(List<BusinessLogicLayer.Entity.PPM.BookPackItem> items, List<BusinessLogicLayer.Entity.PPM.PackagingType> packageTypes, List<BusinessLogicLayer.Entity.PPM.BookPackingOperation> packing, List<BusinessLogicLayer.Entity.PPM.BookPackingOperation> oldPacks, BusinessLogicLayer.Entity.PPM.BookPrintingOperation model, BusinessLogicLayer.Entity.PPM.Exam exam, ref int count, ref int serial, BusinessLogicLayer.Entity.PPM.BookPackingOperation pack)
        {
            serial = 1;
            var packType = (from x in packageTypes where x.PackagingTypeID == pack.PackagingTypeID select x).FirstOrDefault();
            var exists = (from x in oldPacks where x.BookPackingOperationID == pack.BookPackingOperationID select x).FirstOrDefault();
            int bookStart = 0;
            int bookLast = 0;
            if (exists == null || !exists.HasObject)
            {
                for (int i = 0; i < pack.PackageTotal.Value; i++)
                {
                    BusinessLogicLayer.Entity.PPM.BookPackItem item = new BusinessLogicLayer.Entity.PPM.BookPackItem();
                    item.BookPackingOperationID = pack.BookPackingOperationID;
                    item.OperationStatusID = 2;

                    item.PackSerial = serial;

                    string modelCode = "";
                    List<BusinessLogicLayer.Entity.PPM.BookPackItemModel> itemModels = new List<BusinessLogicLayer.Entity.PPM.BookPackItemModel>();
                    bookStart = bookLast + 1;
                    bookLast += bookStart + (i + 1) * packType.BooksPerPackage.Value;
                    foreach (BusinessLogicLayer.Entity.PPM.ExamModelItem examModel in exam.ExamModels)
                    {
                        if (packType.ExamModelCount > 1)
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
                            //serial++;
                        }

                    }
                    if (packType.ExamModelCount > 1)
                    {
                        if (!string.IsNullOrEmpty(modelCode))
                            modelCode = modelCode.Remove(modelCode.Length - 1, 1);
                        //item.PackCode = PrintingOperationID + "-" + pack.BookPackingOperationID + "-" + pack.PackagingTypeID + "-" + modelCode + "-" + serial;
                        item.PackCode = RandomString(12);
                        item.ItemModels = itemModels;
                        items.Add(item);

                    }
                    serial++;

                    ///TODO: Add Pack Items for Sub Packs

                }

            }
        }
        [HttpPost]
        public ActionResult NumberingPack(FormCollection form)
        {

            BusinessLogicLayer.Components.PPM.BookPackingOperationLogic logic = new BusinessLogicLayer.Components.PPM.BookPackingOperationLogic();
            var model = new BusinessLogicLayer.Entity.PPM.BookPrintingOperation(PrintingOperationID);
            List<BusinessLogicLayer.Entity.PPM.BookPackingOperation> packing = logic.GetPackagingTypeByBookPrintingID(PrintingOperationID);
            List<BusinessLogicLayer.Entity.PPM.PackagingType> packageTypes = new BusinessLogicLayer.Components.PPM.PackagingTypeLogic().GetAll();
            var orderedPackageTypes = (from x in packageTypes orderby x.Total descending select x);
            List<BusinessLogicLayer.Entity.PPM.BookPackingOperation> orderedPack = new List<BusinessLogicLayer.Entity.PPM.BookPackingOperation>();
            List<BusinessLogicLayer.Entity.PPM.BookPackingOperation> oldPacks = logic.GetPackedByBookPrintingID(PrintingOperationID);

            List<BusinessLogicLayer.Entity.PPM.BookPackItem> items = new List<BusinessLogicLayer.Entity.PPM.BookPackItem>();
            BusinessLogicLayer.Entity.PPM.Exam exam = new BusinessLogicLayer.Entity.PPM.Exam(model.ExamID.Value);

            int count = exam.ExamModels.Count;
            int serial = logic.GetLastPackSerial(exam.ExamID) + 1;

            List<BusinessLogicLayer.Entity.PPM.BookPackingOperation> orderedPackSingle = new List<BusinessLogicLayer.Entity.PPM.BookPackingOperation>();
            List<BusinessLogicLayer.Entity.PPM.BookPackingOperation> orderedPackMultiple = new List<BusinessLogicLayer.Entity.PPM.BookPackingOperation>();
            foreach (BusinessLogicLayer.Entity.PPM.PackagingType pckg in orderedPackageTypes)
            {
                var packOrder = (from x in packing where x.PackagingTypeID == pckg.PackagingTypeID && x.PackingCalculationTypeID != 2 select x).FirstOrDefault();
                if(packOrder != null && packOrder.HasObject)
                {
                    if (pckg.ExamModelCount == 1)
                        orderedPackSingle.Add(packOrder);
                    else
                        orderedPackMultiple.Add(packOrder);
                    orderedPack.Add(packOrder);
                }
                    
            }


            foreach (BusinessLogicLayer.Entity.PPM.BookPackingOperation pack in orderedPackSingle)
            {
                AddItemToPack(items, packageTypes, packing, oldPacks, model, exam, ref count, ref serial, pack);
            }

            foreach (BusinessLogicLayer.Entity.PPM.BookPackingOperation pack in orderedPackMultiple)
            {
                AddItemToPack(items, packageTypes, packing, oldPacks, model, exam, ref count, ref serial, pack);
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
            itemLogic.SaveItems(items);
            ViewBag.HasError = false;
            ViewBag.NotifyMessage = Resources.MainResource.NumberingPackSuccess;
            return View("Index", model);
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
            return PartialView("_PrintPacksTitleDocumentViewerPartial", report);
        }

        public ActionResult PrintPacksTitleDocumentViewerPartialExport()
        {
            report.DataSource = new Qiyas.BusinessLogicLayer.Components.PPM.BookPackItemLogic().GetAllByPrintingID(Qiyas.WebAdmin.Controllers.BookPackingOperationController.MainID);
            return DocumentViewerExtension.ExportTo(report, Request);
        }
    }
}