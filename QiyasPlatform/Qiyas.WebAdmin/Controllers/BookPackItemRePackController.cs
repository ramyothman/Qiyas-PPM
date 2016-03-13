using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using System.Text;
using System.Web.Routing;

namespace Qiyas.WebAdmin.Controllers
{
    public class BookPackItemRePackController : Controller
    {
        #region Declarations
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
                    Session[key] = new List<BusinessLogicLayer.Entity.PPM.BookRepackPackageItem>();
                return Session[key] as List<BusinessLogicLayer.Entity.PPM.BookRepackPackageItem>;
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

        public static List<BusinessLogicLayer.Entity.PPM.BookPackItemOperation> BookPackItemOperationListStatic;


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

        public static int MainID, ExamCount,RepackID;

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


        // GET: BookPackItemRePack
        public ActionResult Index()
        {
            BusinessLogicLayer.Entity.PPM.BookPrintingOperation printing = new BusinessLogicLayer.Entity.PPM.BookPrintingOperation();
            BookPackItemOperationList = new List<BusinessLogicLayer.Entity.PPM.BookPackItemOperation>();
            BookRepackPackageItemList = new List<BusinessLogicLayer.Entity.PPM.BookRepackPackageItem>();
            ViewBag.HasError = false;
            ViewBag.NotifyMessage = "";
            return View(printing);
        }

        #region Post Methods
        BusinessLogicLayer.Components.PPM.BookRepackPackageItemLogic repackItemLogic = new BusinessLogicLayer.Components.PPM.BookRepackPackageItemLogic();
        BusinessLogicLayer.Components.PPM.BookRepackPackageLogic repackLogic = new BusinessLogicLayer.Components.PPM.BookRepackPackageLogic();

        [HttpPost]
        public ActionResult CheckItem(string item)
        {
            var itemPack = new BusinessLogicLayer.Entity.PPM.BookPackItem(item);
            if (itemPack == null || !itemPack.HasObject)
            {
                PackID = 0;
                PrintingOperationID = 0;
                return Json("notexists");
            }

            if (itemPack.ParentID != null)
            {
                PackID = 0;
                PrintingOperationID = 0;
                return Json("notexists");
            }
            PackID = itemPack.BookPackItemID;
            BusinessLogicLayer.Entity.PPM.BookPackingOperation operation = new BusinessLogicLayer.Entity.PPM.BookPackingOperation(itemPack.BookPackingOperationID.Value);
            BusinessLogicLayer.Entity.PPM.BookPrintingOperation printing = new BusinessLogicLayer.Entity.PPM.BookPrintingOperation(operation.BookPrintingOperationID.Value);
            PrintingOperationID = operation.BookPrintingOperationID.Value;

            int countExams = new BusinessLogicLayer.Components.PPM.ExamLogic().GetExamModelCount(printing.ExamID.Value);
            if (countExams > ExamCount)
                ExamCount = countExams;
            var addedItem = repackItemLogic.GetBookRepackItem(item);
            var exItem = BookRepackPackageItemList.Where(c => c.ExamID == addedItem.ExamID).FirstOrDefault();
            if (BookRepackPackageItemList.Count > 0 && exItem == null)
            {
                return Json("notexists");
            }
            BookRepackPackageItemList.Add(addedItem);
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

        [HttpPost]
        public ActionResult NumberingPack(FormCollection form)
        {
            bool isValid = CheckPackValid();
            if (form.GetValue("PassValidation").AttemptedValue.ToString() == "true")
                isValid = true;
            if(PrintingOperationID == null || PrintingOperationID == 0)
            {
                return Index();
            }
            var model = new BusinessLogicLayer.Entity.PPM.BookPrintingOperation(PrintingOperationID);
            if (isValid)
            {

                int parentID = 0;
                SaveCurrentLists(out parentID);
                RepackID = parentID;
                BusinessLogicLayer.Components.PPM.BookPackItemOperationLogic logic = new BusinessLogicLayer.Components.PPM.BookPackItemOperationLogic();
                
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
                    AddItemToPack(items, packageTypes, packing, oldPacks, model, exam, ref count, ref serial, pack, parentItem, parentID);
                }

                foreach (BusinessLogicLayer.Entity.PPM.BookPackItemOperation pack in orderedPackMultiple)
                {
                    AddItemToPack(items, packageTypes, packing, oldPacks, model, exam, ref count, ref serial, pack, parentItem, parentID);
                }

                Qiyas.BusinessLogicLayer.Components.PPM.BookPackItemLogic itemLogic = new BusinessLogicLayer.Components.PPM.BookPackItemLogic();
                itemLogic.SaveItems(items);
                ViewBag.HasError = false;
                ViewBag.NotifyMessage = Resources.MainResource.NumberingPackSuccess;
                string url = string.Format("{0}", Url.Action("PrintPacks", "BookPackItemRePack"));
                url += "/" + RepackID + "/" + PrintingOperationID;
                //Routedi
                Dictionary<string, object> dictValues = new Dictionary<string,object>();
                
                dictValues.Add("ID", RepackID);
                dictValues.Add("PrintingID", PrintingOperationID);
                RouteValueDictionary routeValueDictionary = new RouteValueDictionary( 
    new { controller = "BookPackItemRePack", action = "PrintPacks", ID = RepackID, PrintingID = PrintingOperationID } );

                return RedirectToAction("PrintPacks", routeValueDictionary);
                //Response.Redirect(url);
                //PrintPacks(parentID, PrintingOperationID);
            }
            else
            {
                ViewBag.HasError = true;
                ViewBag.NotifyMessage = "يرجي مراجعه الحزم المراد اعادة تحزيمها";
            }
           
            
            
            return View("Index", model);
        }
        #endregion


        #region Helper Methods
        private bool CheckPackValid()
        {
            bool result = true;
            int count = GetTotalBooksForModel();
            int currentPacks = 0;
            var packageTypes = new BusinessLogicLayer.Components.PPM.PackagingTypeLogic().GetAll();
            foreach(var item in BookPackItemOperationList)
            {
                var ptype = packageTypes.Where(c => c.PackagingTypeID == item.PackagingTypeID).FirstOrDefault();
                currentPacks += item.PackageTotal.Value * ptype.BooksPerPackage.Value;
            }
            if (currentPacks != count)
                result = false;

            
            foreach(var item in BookRepackPackageItemList)
            {
                if(item.PackageTypeExamModelCount == 1)
                {
                    var tempList = BookRepackPackageItemList.Where(c => c.PackSerial == item.PackSerial);
                    if (tempList.Count() != item.TotalExamModels)
                        result = false;
                }
            }

            
            return result;
        }
        private bool SaveCurrentLists(out int parentID)
        {
            bool result = true;
            

            BusinessLogicLayer.Entity.PPM.BookRepackPackage repackPackage = new BusinessLogicLayer.Entity.PPM.BookRepackPackage();
            repackPackage.CreatedDate = DateTime.Now;
            repackPackage.ModifiedDate = DateTime.Now;
            repackPackage.Save();
            parentID = repackPackage.BookRepackPackageID;
            foreach(var item in BookRepackPackageItemList)
            {
                item.BookRepackPackageID = repackPackage.BookRepackPackageID;
                item.Save();
                BusinessLogicLayer.Entity.PPM.BookPackItem packItem = new BusinessLogicLayer.Entity.PPM.BookPackItem(item.BookPackItemID.Value);
                if (packItem.OperationStatusID != 8)
                {
                    packItem.OperationStatusID = 8;
                    packItem.Save();
                }
            }

            foreach(var item in BookPackItemOperationList)
            {
                item.Save();
            }

            return result;

        }
        private void AddItemToPack(List<BusinessLogicLayer.Entity.PPM.BookPackItem> items, List<BusinessLogicLayer.Entity.PPM.PackagingType> packageTypes, List<BusinessLogicLayer.Entity.PPM.BookPackItemOperation> packing, List<BusinessLogicLayer.Entity.PPM.BookPackItemOperation> oldPacks, BusinessLogicLayer.Entity.PPM.BookPrintingOperation model, BusinessLogicLayer.Entity.PPM.Exam exam, ref int count, ref int serial, BusinessLogicLayer.Entity.PPM.BookPackItemOperation pack, BusinessLogicLayer.Entity.PPM.BookPackItem parentItem, int parentRepackID)
        {
            serial = 1;

            
            
            
            BusinessLogicLayer.Components.PPM.BookPackItemLogic BookPackItemLogic = new BusinessLogicLayer.Components.PPM.BookPackItemLogic();
            
            
            
            
            
                
                
                foreach(BusinessLogicLayer.Entity.PPM.BookRepackPackageItem vparentItem in BookRepackPackageItemList)
                {
                    parentItem = new BusinessLogicLayer.Entity.PPM.BookPackItem(vparentItem.BookPackItemID.Value);
                    int i = 0;
                    var packType = (from x in packageTypes where x.PackagingTypeID == pack.PackagingTypeID select x).FirstOrDefault();
                    var exists = (from x in oldPacks where x.BookPackItemOperationID == pack.BookPackItemOperationID select x).FirstOrDefault();
                    BusinessLogicLayer.Entity.PPM.BookPackingOperation packOperation = new BusinessLogicLayer.Entity.PPM.BookPackingOperation(parentItem.BookPackingOperationID.Value);
                    if (i >= pack.PackageTotal.Value)
                        break;
                    if (exists == null || !exists.HasObject)
                    {
                        int bookStart = 0;
                        int bookLast = BookPackItemLogic.GetLastBookSerialForRePackedItem(parentItem.BookPackItemID);
                        if (bookLast == 0)
                            bookLast = 1;
                        var oldPackageCount = BookPackItemLogic.GetPackageBooksCount(parentItem.BookPackItemID);
                        int remaining = oldPackageCount;
                        bool isFirst = true;
                        while (remaining > 0)
                        {
                            BusinessLogicLayer.Entity.PPM.BookPackItem item = new BusinessLogicLayer.Entity.PPM.BookPackItem();
                            item.BookPackingOperationID = packOperation.BookPackingOperationID;
                            item.BookPackItemOperationID = pack.BookPackItemOperationID;
                            item.BookPackItemID = PackID;
                            item.OperationStatusID = 7;
                            item.ParentID = parentRepackID;
                            item.PackSerial = serial;
                            item.ParentBookPackItemID = parentItem.BookPackItemID;
                            string modelCode = "";
                            List<BusinessLogicLayer.Entity.PPM.BookPackItemModel> itemModels = new List<BusinessLogicLayer.Entity.PPM.BookPackItemModel>();

                            bookStart = i == 0 ? bookLast : bookLast + 1;
                            bookLast = bookStart + (packType.BooksPerPackage.Value - 1);

                            foreach (BusinessLogicLayer.Entity.PPM.BookPackItemModel examModel in parentItem.ItemModels)
                            {
                                if (packType.ExamModelCount > 1)
                                {
                                    BusinessLogicLayer.Entity.PPM.BookPackItemModel newModel = new BusinessLogicLayer.Entity.PPM.BookPackItemModel();
                                    newModel.BookPackItemID = item.BookPackItemID;
                                    newModel.ExamModelID = examModel.ExamModelID;
                                    modelCode += examModel.ExamModelID + "-";
                                    item.StartBookSerial = bookStart;
                                    item.LastBookSerial = bookLast;
                                    remaining -= packType.BooksPerPackage.Value;

                                    itemModels.Add(newModel);
                                }
                                else
                                {
                                    if (isFirst)
                                    {
                                        remaining = remaining * parentItem.ItemModels.Count;
                                        isFirst = false;
                                    }
                                        
                                    itemModels = new List<BusinessLogicLayer.Entity.PPM.BookPackItemModel>();
                                    BusinessLogicLayer.Entity.PPM.BookPackItem itemUnit = new BusinessLogicLayer.Entity.PPM.BookPackItem();
                                    itemUnit.BookPackingOperationID = packOperation.BookPackingOperationID;
                                    itemUnit.BookPackItemOperationID = pack.BookPackItemOperationID;
                                    itemUnit.BookPackItemID = PackID;
                                    itemUnit.OperationStatusID = 7;
                                    itemUnit.ParentBookPackItemID = parentItem.BookPackItemID;
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
                                    itemUnit.ParentID = parentRepackID;
                                    itemUnit.ItemModels = itemModels;
                                    remaining -= packType.BooksPerPackage.Value;
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
                            if (parentItem.ItemModels.Count == 0)
                                remaining = 0;
                            else
                            {
                                serial++;
                                i++;
                            }
                            
                            ///TODO: Add Pack Items for Sub Packs

                        }
                    }
                }
                

            
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
        
        public List<Qiyas.BusinessLogicLayer.Entity.PPM.BookPackItemOperation> GetPackagingTypeByBookPackID(List<BusinessLogicLayer.Entity.PPM.BookPackItemOperation> list)
        {
            var packageTypes = new BusinessLogicLayer.Components.PPM.PackagingTypeLogic().GetAll();
            foreach (var item in list)
            {
                var ptype = packageTypes.Where(c => c.PackagingTypeID == item.PackagingTypeID).FirstOrDefault();
                item.PackagingTypeName = ptype.Name;
            }
            return list;
        }

        private int TotalPackages(BusinessLogicLayer.Entity.PPM.BookPackItemOperation item)
        {
            int total = 0;

            BusinessLogicLayer.Entity.PPM.PackagingType ptype = new BusinessLogicLayer.Entity.PPM.PackagingType(item.PackagingTypeID.Value);

            if (ptype == null)
                return 0;

            BusinessLogicLayer.Entity.PPM.BookPrintingOperation printingOperation = new BusinessLogicLayer.Entity.PPM.BookPrintingOperation(PrintingOperationID);
            if (printingOperation == null)
                return 0;

            bool isA3 = ptype.BooksPerPackage == 3 && ptype.ExamModelCount == 1;
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

        private int GetTotalBooksForModel(bool isA3)
        {
            List<int> ExamIDs = new List<int>();
            int result = 0;
            foreach (var item in BookRepackPackageItemList)
            {
                if (!ExamIDs.Contains(item.ExamID.Value))
                {
                    //if(isA3)
                    //    result += item.ExamsNeededForA3.Value;
                    //else
                    //    result += item.PrintsForOneModel.Value;

                    if(item.PackageTypeExamModelCount > 1)
                        result += item.BooksCount.Value / item.PackageTypeExamModelCount.Value;
                    else
                        result += item.BooksCount.Value;
                    ExamIDs.Add(item.ExamID.Value);
                }
            }
            return result;
        }

        private int GetTotalBooksForModel()
        {
            List<int> ExamIDs = new List<int>();
            int result = 0;
            foreach (var item in BookRepackPackageItemList)
            {
                //if (item.PackageTypeExamModelCount > 1)
                //    result += item.BooksCount.Value / item.PackageTypeExamModelCount.Value;
                //else
                //    result += item.BooksCount.Value;
                result += item.BooksCount.Value;
            }
            return result;
        }

        private int GetTotalItemsA3()
        {
            int result = 0;
            List<BusinessLogicLayer.Entity.PPM.PackagingType> types = new BusinessLogicLayer.Components.PPM.PackagingTypeLogic().GetAll();
            foreach (var item in BookPackItemOperationList)
            {
                var type = types.Where(c => c.BooksPerPackage == 3 && c.ExamModelCount == 1).FirstOrDefault();
                if (item.PackagingTypeID == type.PackagingTypeID)
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
        

        #endregion

        #region Repack Items Grid View
        [ValidateInput(false)]
        public ActionResult RepackPackageGridViewPartial()
        {
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
        #endregion

        #region Book Pack Item Operation Grid View

        [ValidateInput(false)]
        public ActionResult BookPackItemOperationGridViewPartial()
        {
            var model = BookPackItemOperationList;
            return PartialView("_BookPackItemOperationGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult BookPackItemOperationGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Qiyas.BusinessLogicLayer.Entity.PPM.BookPackItemOperation item)
        {
            item.BookPackItemOperationID = BookPackItemOperationList.Count + 1;
            item.BookPackItemID = 1;
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                modelState.Errors.Clear();

            }
            ModelState.SetModelValue("BookPackItemID", new ValueProviderResult(1, "1", System.Globalization.CultureInfo.CurrentCulture));
            if (ModelState.IsValid)
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
                    var itemExists = (from c in BookPackItemOperationList where c.PackagingTypeID == item.PackagingTypeID select c).FirstOrDefault();
                    if(itemExists == null)
                    {

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
                            entity.PackingParentID = item.PackingParentID;
                            entity.PackageTotal = totalPackage;
                            entity.PackagingTypeID = item.PackagingTypeID;
                            entity.PackingCalculationTypeID = item.PackingCalculationTypeID;
                            entity.PackingValue = item.PackingValue;
                            BookPackItemOperationList.Add(entity);
                        
                        }
                    }
                    else
                    {
                        ViewData["EditError"] = "لا يمكن ادخال نوع الحزمة مرتين";
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
            {
                string msgError = "";
                foreach (ModelState modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        msgError += error.ErrorMessage + " ";
                    }
                }
                ViewData["EditError"] = msgError;
            }
                
            var model = BookPackItemOperationList;
            return PartialView("_BookPackItemOperationGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult BookPackItemOperationGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Qiyas.BusinessLogicLayer.Entity.PPM.BookPackItemOperation item)
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
            var model = BookPackItemOperationList;
            return PartialView("_BookPackItemOperationGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult BookPackItemOperationGridViewPartialDelete(System.Int32 BookPackItemOperationID)
        {
            
            if (BookPackItemOperationID >= 0)
            {
                try
                {
                    var item = BookPackItemOperationList.Where(c => c.BookPackItemOperationID == BookPackItemOperationID).FirstOrDefault();
                    BookPackItemOperationList.Remove(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = BookPackItemOperationList;
            return PartialView("_BookPackItemOperationGridViewPartial", model);
        }

        #endregion

        #region Printing

        
        public ActionResult RepackGroup(int bookPrintingId, int repackFrom, int repackTo)
        {
            BusinessLogicLayer.Components.PPM.BookPackingOperationLogic BookPackingOperationLogic = new BusinessLogicLayer.Components.PPM.BookPackingOperationLogic();
            BusinessLogicLayer.Components.PPM.BookPackItemLogic packItemLogic = new BusinessLogicLayer.Components.PPM.BookPackItemLogic();
            BusinessLogicLayer.Components.PPM.BookPackItemLogic BookPackItemLogic = new BusinessLogicLayer.Components.PPM.BookPackItemLogic();
            BusinessLogicLayer.Entity.PPM.BookPackingOperation operation = new BusinessLogicLayer.Entity.PPM.BookPackingOperation();
            BookRepackPackageItemList.Clear();
            BookPackItemOperationList.Clear();
            List<BusinessLogicLayer.Entity.PPM.BookPackItem> bookPackItems = new List<BusinessLogicLayer.Entity.PPM.BookPackItem>();
            PrintingOperationID = bookPrintingId;
            var pitems = BookPackItemLogic.GetAllByPrintingIDandPackagingTypeIDStored(bookPrintingId, repackFrom);

            int packOperationID = 0;
            string name;
            foreach (var item in pitems)
            {
                var addedItem = repackItemLogic.GetBookRepackItem(item.PackCode);
                BookRepackPackageItemList.Add(addedItem);
                PackID = item.BookPackItemID;
                packOperationID = item.BookPackingOperationID.Value;
                
            }
            BookPackItemOperationGridViewPartialAddNew(new BusinessLogicLayer.Entity.PPM.BookPackItemOperation() {  PackagingTypeID = repackTo, PackingCalculationTypeID = 1, PackingValue = 100 });
            FormCollection newFormCollection = new FormCollection();
            newFormCollection.Add("PassValidation", "true");
            var actionResult = NumberingPack(newFormCollection);

            return actionResult;
        }

        Qiyas.WebAdmin.Common.Reports.PrintItemPack report = new Qiyas.WebAdmin.Common.Reports.PrintItemPack();

        public ActionResult PrintPacksTitleDocumentViewerPartial()
        {
            //report = new Common.Reports.PrintItemPack();
            //report.LoadData(RepackID);
            //report.DataSource = new Qiyas.BusinessLogicLayer.Components.PPM.BookPackItemLogic().GetAllByParentID(RepackID);
            return PartialView("_PrintPacksTitleDocumentViewerPartial", report);
        }

        public ActionResult PrintPacksTitleDocumentViewerPartialExport()
        {
            //report = new Common.Reports.PrintItemPack();
            report.DataSource = new Qiyas.BusinessLogicLayer.Components.PPM.BookPackItemLogic().GetAllByParentID(RepackID);
            return DocumentViewerExtension.ExportTo(report, Request);
        }

        public ActionResult PrintPacks(int ID = 0, int PrintingID = 0)
        {
            string url = string.Format("{0}", Url.Action("Index", "BookPackItemRePack"));
            var model = new BusinessLogicLayer.Entity.PPM.BookPrintingOperation(PrintingID);
            if (model == null || !model.HasObject)
            {
                return RedirectToAction("Index", "BookPackItemRePack");
            }
            RepackID = ID;
            ViewBag.HasError = false;
            ViewBag.NotifyMessage = "";
            ViewBag.PrintingID = PrintingID;
            PrintingOperationID = PrintingID;
            return View(model);
        }
        #endregion
    }
}