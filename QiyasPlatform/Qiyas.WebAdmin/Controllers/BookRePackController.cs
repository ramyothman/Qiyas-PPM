using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using System.Text;

namespace Qiyas.WebAdmin.Controllers
{
    [Authorize]
    public class BookRePackController : Controller
    {
        public int PrintingOperationID
        {
            set
            {
                var key = "34FAA431-CF79-4869-9488-93F6BBE81263";
                var Session = HttpContext.Session;
                Session[key] = value;
            }
            get
            {
                var key = "34FAA431-CF79-4869-9488-93F6BBE81263";
                var Session = HttpContext.Session;
                if (Session[key] == null)
                    Session[key] = 0;
                return (int)Session[key];
            }
        }


        public List<BusinessLogicLayer.Entity.PPM.BookRepackPackageItem> BookRepackPackageItemList
        {
            set
            {
                var key = "34FAA431-CF79-4869-1234-93F6BBE81263";
                var Session = HttpContext.Session;
                Session[key] = value;
            }
            get
            {
                var key = "34FAA431-CF79-4869-1234-93F6BBE81263";
                var Session = HttpContext.Session;
                if (Session[key] == null)
                    Session[key] = new List< BusinessLogicLayer.Entity.PPM.BookRepackPackageItem>();
                return  Session[key] as List< BusinessLogicLayer.Entity.PPM.BookRepackPackageItem>;
            }
        }

        public List<BusinessLogicLayer.Entity.PPM.BookPackItemOperation> BookPackItemOperationList
        {
            set
            {
                var key = "34FAA431-CF79-4869-1235-93F6BBE81263";
                var Session = HttpContext.Session;
                Session[key] = value;
            }
            get
            {
                var key = "34FAA431-CF79-4869-1235-93F6BBE81263";
                var Session = HttpContext.Session;
                if (Session[key] == null)
                    Session[key] = new List<BusinessLogicLayer.Entity.PPM.BookPackItemOperation>();
                return Session[key] as List<BusinessLogicLayer.Entity.PPM.BookPackItemOperation>;
            }
        }


        public int PackID
        {
            set
            {
                var key = "34FAA431-CF79-4869-9488-93F6AAE83621";
                var Session = HttpContext.Session;
                Session[key] = value;
            }
            get
            {
                var key = "34FAA431-CF79-4869-9488-93F6AAE83621";
                var Session = HttpContext.Session;
                if (Session[key] == null)
                    Session[key] = 0;
                return (int)Session[key];
            }
        }

        // GET: BookRePack
        public ActionResult Index()
        {
            BusinessLogicLayer.Entity.PPM.BookPrintingOperation printing = new BusinessLogicLayer.Entity.PPM.BookPrintingOperation();
            BookPackItemOperationList = new List<BusinessLogicLayer.Entity.PPM.BookPackItemOperation>();
            BookRepackPackageItemList = new List<BusinessLogicLayer.Entity.PPM.BookRepackPackageItem>();
            return View(printing);
        }

        public ActionResult LoadPack(string serial)
        {
            return Json("");
        }

        #region Old Grid Events
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

        #endregion


        #region Grid Operations
        [ValidateInput(false)]
        public ActionResult PackingGridViewPartial()
        {

            var model = BookPackItemOperationList;//new BusinessLogicLayer.Components.PPM.BookPackItemOperationLogic().GetByBookPackID(PackID);
            return PartialView("_RepackGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult PackingGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Qiyas.BusinessLogicLayer.Entity.PPM.BookPackItemOperation item)
        {

            //if (ModelState.IsValid)
            {
                try
                {
                    bool isValid = true;
                    if (BookRepackPackageItemList.Count == 0)
                    {
                        ViewData["EditError"] = Resources.MainResource.NoRepackItemsSelected;
                        isValid = false;
                        var modelError = new BusinessLogicLayer.Components.PPM.BookPackItemOperationLogic().GetByBookPackID(PackID);
                        return PartialView("_RepackGridViewPartial", modelError);
                    }
                    if (item.PackingCalculationTypeID == 2)
                    {
                        if (item.PackingParentID == 0)
                        {
                            ViewData["EditError"] = Resources.MainResource.EnterPackingParentID;
                            isValid = false;
                        }
                    }

                    
                    int totalPackage = TotalPackages(item);
                    BusinessLogicLayer.Entity.PPM.BookPackItem packItem = new BusinessLogicLayer.Entity.PPM.BookPackItem(PackID);
                    BusinessLogicLayer.Entity.PPM.BookPackingOperation package = new BusinessLogicLayer.Entity.PPM.BookPackingOperation(packItem.BookPackingOperationID.Value);
                    BusinessLogicLayer.Entity.PPM.BookPrintingOperation printingOperation = new BusinessLogicLayer.Entity.PPM.BookPrintingOperation(PrintingOperationID);
                    BusinessLogicLayer.Entity.PPM.PackagingType ptype = new BusinessLogicLayer.Entity.PPM.PackagingType(item.PackagingTypeID.Value);
                    
                    if (printingOperation != null)
                    {
                        BusinessLogicLayer.Components.PPM.BookPackItemOperationLogic logic = new BusinessLogicLayer.Components.PPM.BookPackItemOperationLogic();

                        int totalItems = GetTotalBooksForModel(true);
                        int currentTotal = ptype.BooksPerPackage == 3 && ptype.ExamModelCount == 1 ? GetTotalItemsA3() : GetTotalItems();
                        int totalPrint = (totalPackage * ptype.BooksPerPackage.Value + currentTotal);
                        int count = new BusinessLogicLayer.Components.PPM.ExamLogic().GetExamModelCount(printingOperation.ExamID.Value);
                        if (count > 1)
                        {
                            totalItems = totalItems * count;
                        }
                        if (totalItems < totalPrint)
                        {
                            isValid = false;
                            ViewData["EditError"] = Resources.MainResource.TotalPackGreaterThanOverallTotal;
                        }
                    }
                    if (isValid)
                    {
                        BusinessLogicLayer.Entity.PPM.BookPackItemOperation entity = new BusinessLogicLayer.Entity.PPM.BookPackItemOperation();
                        entity.BookPackItemOperationID = BookPackItemOperationList.Count + 1;
                        entity.AllocatedFrom = item.AllocatedFrom;
                        entity.BookPackItemID = packItem.BookPackItemID;
                        entity.PackingParentID = package.BookPackingOperationID;
                        entity.CreatedDate = DateTime.Now;
                        entity.ModifiedDate = DateTime.Now;
                        entity.Name = item.Name;
                        entity.PackingParentID = package.PackageTotal;
                        entity.PackageTotal = totalPackage;
                        entity.PackagingTypeID = item.PackagingTypeID;
                        entity.PackingCalculationTypeID = item.PackingCalculationTypeID;
                        entity.PackingValue = item.PackingValue;
                        BookPackItemOperationList.Add(entity);
                    }


                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            //else
            //    ViewData["EditError"] = "Please, correct all errors.";
            var model = BookPackItemOperationList;//new BusinessLogicLayer.Components.PPM.BookPackItemOperationLogic().GetByBookPackID(PackID);
            return PartialView("_RepackGridViewPartial", model);
        }

        private int GetTotalBooksForModel(bool isA3)
        {
            List<int> ExamIDs = new List<int>();
            int result = 0;
            foreach(var item in BookRepackPackageItemList)
            {
                if(!ExamIDs.Contains(item.ExamID.Value))
                {
                    //if(isA3)
                    //    result += item.ExamsNeededForA3.Value;
                    //else
                    //    result += item.PrintsForOneModel.Value;
                    result += item.BooksCount.Value;
                    ExamIDs.Add(item.ExamID.Value);
                }
            }
            return result;
        }

        private int GetTotalItemsA3()
        {
            int result = 0;
            List<BusinessLogicLayer.Entity.PPM.PackagingType> types = new BusinessLogicLayer.Components.PPM.PackagingTypeLogic().GetAll();
            foreach(var item in BookPackItemOperationList)
            {
                var type = types.Where(c => c.BooksPerPackage == 3 && c.ExamModelCount == 1).FirstOrDefault();
                if(item.PackagingTypeID == type.PackagingTypeID)
                    result += item.PackageTotal.Value;
            }
            return result;
        }

        private int GetTotalItems()
        {
            int result = 0;
            List<BusinessLogicLayer.Entity.PPM.PackagingType> types = new BusinessLogicLayer.Components.PPM.PackagingTypeLogic().GetAll();
            foreach (var item in BookPackItemOperationList)
            {
                var type = types.Where(c => c.BooksPerPackage == 3 && c.ExamModelCount == 1).FirstOrDefault();
                if (item.PackagingTypeID != type.PackagingTypeID)
                    result += item.PackageTotal.Value;
            }
            return result;
        }
        

        [HttpPost, ValidateInput(false)]
        public ActionResult PackingGridViewPartialDelete(System.Int32 BookPackItemOperationID)
        {

            if (BookPackItemOperationID >= 0)
            {
                try
                {
                    var item = BookPackItemOperationList.Where(c => c.BookPackItemOperationID == BookPackItemOperationID).FirstOrDefault();
                    BookPackItemOperationList.Remove(item);
                    //BusinessLogicLayer.Entity.PPM.BookPackItemOperation entity = new BusinessLogicLayer.Entity.PPM.BookPackItemOperation(BookPackItemOperationID);
                    //entity.Delete();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = BookPackItemOperationList;// new BusinessLogicLayer.Components.PPM.BookPackItemOperationLogic().GetByBookPackID(PackID);
            return PartialView("_RepackGridViewPartial", model);
        }
        #endregion

        #region Methods
        private void AddItemToPack(List<BusinessLogicLayer.Entity.PPM.BookPackItem> items, List<BusinessLogicLayer.Entity.PPM.PackagingType> packageTypes, List<BusinessLogicLayer.Entity.PPM.BookPackItemOperation> packing, List<BusinessLogicLayer.Entity.PPM.BookPackItemOperation> oldPacks, BusinessLogicLayer.Entity.PPM.BookPrintingOperation model, BusinessLogicLayer.Entity.PPM.Exam exam, ref int count, ref int serial, BusinessLogicLayer.Entity.PPM.BookPackItemOperation pack, BusinessLogicLayer.Entity.PPM.BookPackItem parentItem)
        {
            serial = 1;
            
            if(parentItem.OperationStatusID != 8)
            {
                parentItem.OperationStatusID = 8;
                parentItem.Save();
            }
            BusinessLogicLayer.Entity.PPM.BookPackingOperation packOperation = new BusinessLogicLayer.Entity.PPM.BookPackingOperation(parentItem.BookPackingOperationID.Value);
            var packType = (from x in packageTypes where x.PackagingTypeID == pack.PackagingTypeID select x).FirstOrDefault();
            var exists = (from x in oldPacks where x.BookPackItemOperationID == pack.BookPackItemOperationID select x).FirstOrDefault();
            int bookStart = 0;
            int bookLast = 0;
            if (exists == null || !exists.HasObject)
            {
                for (int i = 0; i < pack.PackageTotal.Value; i++)
                {
                    BusinessLogicLayer.Entity.PPM.BookPackItem item = new BusinessLogicLayer.Entity.PPM.BookPackItem();
                    item.BookPackingOperationID = packOperation.BookPackingOperationID;
                    item.BookPackItemOperationID = pack.BookPackItemOperationID;
                    item.BookPackItemID = PackID;
                    item.OperationStatusID = 7;
                    
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
                            item.BookPackingOperationID = packOperation.BookPackingOperationID;
                            item.BookPackItemOperationID = pack.BookPackItemOperationID;
                            item.BookPackItemID = PackID;
                            itemUnit.OperationStatusID = 7;

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

            BusinessLogicLayer.Components.PPM.BookPackItemOperationLogic logic = new BusinessLogicLayer.Components.PPM.BookPackItemOperationLogic();
            var model = new BusinessLogicLayer.Entity.PPM.BookPrintingOperation(PrintingOperationID);
            List<BusinessLogicLayer.Entity.PPM.BookPackItemOperation> packing = logic.GetPackagingTypeByBookPackID(PackID);
            List<BusinessLogicLayer.Entity.PPM.PackagingType> packageTypes = new BusinessLogicLayer.Components.PPM.PackagingTypeLogic().GetAll();
            var orderedPackageTypes = (from x in packageTypes orderby x.Total descending select x);
            List<BusinessLogicLayer.Entity.PPM.BookPackItemOperation> orderedPack = new List<BusinessLogicLayer.Entity.PPM.BookPackItemOperation>();
            List<BusinessLogicLayer.Entity.PPM.BookPackItemOperation> oldPacks = logic.GetPackedByBookPackID(PackID);

            List<BusinessLogicLayer.Entity.PPM.BookPackItem> items = new List<BusinessLogicLayer.Entity.PPM.BookPackItem>();
            BusinessLogicLayer.Entity.PPM.Exam exam = new BusinessLogicLayer.Entity.PPM.Exam(model.ExamID.Value);

            int count = exam.ExamModels.Count;
            int serial = new BusinessLogicLayer.Components.PPM.BookPackingOperationLogic().GetLastPackSerial(exam.ExamID) + 1;

            List<BusinessLogicLayer.Entity.PPM.BookPackItemOperation> orderedPackSingle = new List<BusinessLogicLayer.Entity.PPM.BookPackItemOperation>();
            List<BusinessLogicLayer.Entity.PPM.BookPackItemOperation> orderedPackMultiple = new List<BusinessLogicLayer.Entity.PPM.BookPackItemOperation>();
            BusinessLogicLayer.Entity.PPM.BookPackItem parentItem = new BusinessLogicLayer.Entity.PPM.BookPackItem(PackID);
            foreach (BusinessLogicLayer.Entity.PPM.PackagingType pckg in orderedPackageTypes)
            {
                var packOrder = (from x in packing where x.PackagingTypeID == pckg.PackagingTypeID && x.PackingCalculationTypeID != 2 select x).FirstOrDefault();
                if (packOrder != null && packOrder.HasObject)
                {
                    if (pckg.ExamModelCount == 1)
                        orderedPackSingle.Add(packOrder);
                    else
                        orderedPackMultiple.Add(packOrder);
                    orderedPack.Add(packOrder);
                }

            }


            foreach (BusinessLogicLayer.Entity.PPM.BookPackItemOperation pack in orderedPackSingle)
            {
                AddItemToPack(items, packageTypes, packing, oldPacks, model, exam, ref count, ref serial, pack, parentItem);
            }

            foreach (BusinessLogicLayer.Entity.PPM.BookPackItemOperation pack in orderedPackMultiple)
            {
                AddItemToPack(items, packageTypes, packing, oldPacks, model, exam, ref count, ref serial, pack, parentItem);
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


        #region Helpers
        private int TotalPackages(BusinessLogicLayer.Entity.PPM.BookPackItemOperation item)
        {
            int total = 0;

            BusinessLogicLayer.Entity.PPM.PackagingType ptype = new BusinessLogicLayer.Entity.PPM.PackagingType(item.PackagingTypeID.Value);

            if (ptype == null)
                return 0;

            BusinessLogicLayer.Entity.PPM.BookPrintingOperation printingOperation = new BusinessLogicLayer.Entity.PPM.BookPrintingOperation(PrintingOperationID);
            if (printingOperation == null)
                return 0;

            bool isA3 = ptype.BooksPerPackage == 3 && ptype.ExamModelCount == 1 ;
            int totalItems = GetTotalBooksForModel(isA3);
            int countExams = new BusinessLogicLayer.Components.PPM.ExamLogic().GetExamModelCount(printingOperation.ExamID.Value);
            if (item.PackingCalculationTypeID == 1)
            {

                double t = item.PackingValue.Value / 100.00;
                total = Convert.ToInt32(Math.Ceiling(totalItems * t));
                total = total / (ptype.ExamModelCount.Value * ptype.BooksPerPackage.Value);
                if (ptype.ExamModelCount == 1)
                    total = total * countExams;
                else
                    total = total * ptype.ExamModelCount.Value;
            }
            else if (item.PackingCalculationTypeID == 2)
            {
                BusinessLogicLayer.Entity.PPM.BookPackItemOperation operation = new BusinessLogicLayer.Entity.PPM.BookPackItemOperation(item.PackingParentID.Value);
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
                else if (operation.PackingCalculationTypeID == 2)
                {
                    BusinessLogicLayer.Entity.PPM.BookPackItemOperation operation2 = new BusinessLogicLayer.Entity.PPM.BookPackItemOperation(operation.PackingParentID.Value);
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
                else if (operation.PackingCalculationTypeID == 3)
                {

                    if (ptype.ExamModelCount == 1)
                    {
                        total = item.PackingValue.Value / (ptype.BooksPerPackage.Value * ptype.ExamModelCount.Value);
                        total = total * countExams;
                    }
                    else
                    {
                        total = item.PackingValue.Value / (ptype.BooksPerPackage.Value);
                        //total = total * ptype.ExamModelCount.Value;
                    }

                }

            }
            else if (item.PackingCalculationTypeID == 3)
            {

                if (ptype.ExamModelCount == 1)
                {
                    total = item.PackingValue.Value / (ptype.BooksPerPackage.Value * ptype.ExamModelCount.Value);
                    total = total * countExams;
                }
                else
                {
                    total = item.PackingValue.Value / (ptype.BooksPerPackage.Value);
                    //total = total * ptype.ExamModelCount.Value;
                }

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
        #endregion

        [HttpPost]
        public ActionResult CheckItem(string item)
        {
            var itemPack = new BusinessLogicLayer.Entity.PPM.BookPackItem(item);
            if(itemPack == null || !itemPack.HasObject)
            {
                PackID = 0;
                PrintingOperationID = 0;
                return Json("notexists");
            }

            if(itemPack.ParentID != null)
            {
                PackID = 0;
                PrintingOperationID = 0;
                return Json("notexists");
            }
            PackID = itemPack.BookPackItemID;
            BusinessLogicLayer.Entity.PPM.BookPackingOperation operation = new BusinessLogicLayer.Entity.PPM.BookPackingOperation(itemPack.BookPackingOperationID.Value);
            PrintingOperationID = operation.BookPrintingOperationID.Value;
            BookRepackPackageItemList.Add(repackItemLogic.GetBookRepackItem(item));
            if (itemPack.HasObject)
            {
                return Json("exists");
            }
            else
            {
                return Json("notexists");
            }

            //var model = new BusinessLogicLayer.Entity.PPM.BookPrintingOperation(PrintingOperationID);
            //model.OperationStatusID = 2;
            //model.Save();
            //ViewBag.HasError = false;
            //ViewBag.NotifyMessage = Resources.MainResource.SaveSuccess;

        }

        BusinessLogicLayer.Components.PPM.BookRepackPackageItemLogic repackItemLogic = new BusinessLogicLayer.Components.PPM.BookRepackPackageItemLogic();
        BusinessLogicLayer.Components.PPM.BookRepackPackageLogic repackLogic = new BusinessLogicLayer.Components.PPM.BookRepackPackageLogic();

        [ValidateInput(false)]
        public ActionResult RepackPackageGridViewPartial()
        {
            var model = BookRepackPackageItemList;
            return PartialView("_RepackPackageGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult RepackPackageGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Qiyas.BusinessLogicLayer.Entity.PPM.BookRepackPackageItem item)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = BookRepackPackageItemList;
            return PartialView("_RepackPackageGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult RepackPackageGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Qiyas.BusinessLogicLayer.Entity.PPM.BookRepackPackageItem item)
        {
            
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
            var model = BookRepackPackageItemList;
            return PartialView("_RepackPackageGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult RepackPackageGridViewPartialDelete(System.Int32? BookPackItemID)
        {
            
            if (BookPackItemID != null)
            {
                try
                {
                    repackItemLogic.RemoveFromList(BookRepackPackageItemList, BookPackItemID.Value);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = BookRepackPackageItemList;
            return PartialView("_RepackPackageGridViewPartial", model);
        }
    }
}