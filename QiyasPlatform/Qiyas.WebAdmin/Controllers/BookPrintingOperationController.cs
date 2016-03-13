using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using System.Text;

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
        BusinessLogicLayer.Components.PPM.BookPrintingOperationLogic BookPrintingOperationLogic = new BusinessLogicLayer.Components.PPM.BookPrintingOperationLogic();
        BusinessLogicLayer.Components.PPM.BookPackingOperationLogic BookPackingOperationLogic = new BusinessLogicLayer.Components.PPM.BookPackingOperationLogic();
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

                        if(!BookPackingOperationLogic.HaveA3Packs(printing.BookPrintingOperationID) && printing.ExamsNeededForA3 > 0)
                        {
                            BusinessLogicLayer.Entity.PPM.BookPackingOperation pack = new BusinessLogicLayer.Entity.PPM.BookPackingOperation();
                            BusinessLogicLayer.Entity.PPM.Exam exam = new BusinessLogicLayer.Entity.PPM.Exam(printing.ExamID.Value);
                            BusinessLogicLayer.Entity.PPM.PackagingType ptype = new BusinessLogicLayer.Entity.PPM.PackagingType(1, 3);
                            if(ptype.HasObject)
                            {
                                pack.BookPrintingOperationID = printing.BookPrintingOperationID;
                                pack.CreatedDate = DateTime.Now;
                                pack.ModifiedDate = DateTime.Now;
                                pack.NumberofBooksPerModel = printing.ExamsNeededForA3;
                                pack.PackageTotalPerModel = printing.ExamsNeededForA3 / 3;
                                //pack.PackageTotal = pack.PackageTotalPerModel * exam.ExamModels.Count;
                                pack.PackageTotal = pack.PackageTotalPerModel;
                                pack.PackagingTypeID = ptype.PackagingTypeID;
                                pack.PackingCalculationTypeID = 1;
                                pack.PackingValue = 100;
                                pack.Save();
                            }
                            
                        }
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
                    if(item.OperationStatusID > 1)
                    {
                        ViewData["EditError"] = "حالة الطلب يجب ان تكون طباعة للسماح بالتعديل";
                        var modelErr = new BusinessLogicLayer.Components.PPM.BookPrintingOperationLogic().GetAll();
                        return PartialView("_BookPrintingOperationGridViewPartial", modelErr);
                    }
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

                    if (!BookPackingOperationLogic.HaveA3Packs(printing.BookPrintingOperationID))
                    {
                        BusinessLogicLayer.Entity.PPM.BookPackingOperation pack = new BusinessLogicLayer.Entity.PPM.BookPackingOperation();
                        BusinessLogicLayer.Entity.PPM.Exam exam = new BusinessLogicLayer.Entity.PPM.Exam(printing.ExamID.Value);
                        BusinessLogicLayer.Entity.PPM.PackagingType ptype = new BusinessLogicLayer.Entity.PPM.PackagingType(1, 3);
                        if (ptype.HasObject)
                        {
                            pack.BookPrintingOperationID = printing.BookPrintingOperationID;
                            pack.CreatedDate = DateTime.Now;
                            pack.ModifiedDate = DateTime.Now;
                            pack.NumberofBooksPerModel = printing.ExamsNeededForA3;
                            pack.PackageTotalPerModel = printing.ExamsNeededForA3 / 3;
                            pack.PackageTotal = pack.PackageTotalPerModel * exam.ExamModels.Count;
                            pack.PackagingTypeID = ptype.PackagingTypeID;
                            pack.PackingCalculationTypeID = 1;
                            pack.PackingValue = 100;
                            pack.Save();
                        }

                    }
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
                        if (type.OperationStatusID <= 3 || type.OperationStatusID == 8)
                        {
                            type.Delete();
                        }
                        else
                        {
                            ViewData["EditError"] = "لا يمكن حذف هذا الطلب يجب ان يكون نوع الطلب طباعة او تحزيم";
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

        BusinessLogicLayer.Components.PPM.BookRepackPackageItemLogic repackItemLogic = new BusinessLogicLayer.Components.PPM.BookRepackPackageItemLogic();
        BusinessLogicLayer.Components.PPM.BookRepackPackageLogic repackLogic = new BusinessLogicLayer.Components.PPM.BookRepackPackageLogic();
        int PackID;
        

        public List<BusinessLogicLayer.Entity.PPM.BookRepackPackageItem> BookRepackPackageItemList
        {
            set
            {
                var key = "34FAA431-CF79-4869-1234-93F6BBE81264";
                var Session = HttpContext.Session;
                Session[key] = value;
            }
            get
            {
                var key = "34FAA431-CF79-4869-1234-93F6BBE81264";
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
                var key = "34FAA431-CF79-4869-1235-93F6BBE81265";
                var Session = HttpContext.Session;
                Session[key] = value;
            }
            get
            {
                var key = "34FAA431-CF79-4869-1235-93F6BBE81265";
                var Session = HttpContext.Session;
                if (Session[key] == null)
                    Session[key] = new List<BusinessLogicLayer.Entity.PPM.BookPackItemOperation>();
                return Session[key] as List<BusinessLogicLayer.Entity.PPM.BookPackItemOperation>;
            }
        }
        public int PrintingOperationID
        {
            set
            {
                var key = "34FAA431-CF79-4869-9488-93F6BBE81266";
                var Session = HttpContext.Session;
                Session[key] = value;
            }
            get
            {
                var key = "34FAA431-CF79-4869-9488-93F6BBE81266";
                var Session = HttpContext.Session;
                if (Session[key] == null)
                    Session[key] = 0;
                return (int)Session[key];
            }
        }
        public static int MainID, ExamCount, RepackID;
        private bool SaveCurrentLists(out int parentID)
        {
            bool result = true;


            BusinessLogicLayer.Entity.PPM.BookRepackPackage repackPackage = new BusinessLogicLayer.Entity.PPM.BookRepackPackage();
            repackPackage.CreatedDate = DateTime.Now;
            repackPackage.ModifiedDate = DateTime.Now;
            repackPackage.Save();
            parentID = repackPackage.BookRepackPackageID;
            foreach (var item in BookRepackPackageItemList)
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

            foreach (var item in BookPackItemOperationList)
            {
                item.Save();
            }

            return result;

        }
        //[HttpPost]
        //public ActionResult NumberingPack(List<BusinessLogicLayer.Entity.PPM.BookPackItem> bookPackItems)
        //{
        //    bool isValid = true;
        //    if (PrintingOperationID == null || PrintingOperationID == 0)
        //    {
        //        return Index();
        //    }
        //    var model = new BusinessLogicLayer.Entity.PPM.BookPrintingOperation(PrintingOperationID);
        //    if (isValid)
        //    {

        //        int parentID = 0;
        //        SaveCurrentLists(out parentID);
        //        RepackID = parentID;
        //        BusinessLogicLayer.Components.PPM.BookPackItemOperationLogic logic = new BusinessLogicLayer.Components.PPM.BookPackItemOperationLogic();

        //        List<BusinessLogicLayer.Entity.PPM.BookPackItemOperation> packing = logic.GetPackagingTypeByBookPackID(PackID);
        //        List<BusinessLogicLayer.Entity.PPM.PackagingType> packageTypes = new BusinessLogicLayer.Components.PPM.PackagingTypeLogic().GetAll();
        //        var orderedPackageTypes = (from x in packageTypes orderby x.Total descending select x);
        //        List<BusinessLogicLayer.Entity.PPM.BookPackItemOperation> orderedPack = new List<BusinessLogicLayer.Entity.PPM.BookPackItemOperation>();
        //        List<BusinessLogicLayer.Entity.PPM.BookPackItemOperation> oldPacks = logic.GetPackedByBookPackID(PackID);

        //        List<BusinessLogicLayer.Entity.PPM.BookPackItem> items = new List<BusinessLogicLayer.Entity.PPM.BookPackItem>();
        //        BusinessLogicLayer.Entity.PPM.Exam exam = new BusinessLogicLayer.Entity.PPM.Exam(model.ExamID.Value);

        //        int count = exam.ExamModels.Count;
        //        int serial = new BusinessLogicLayer.Components.PPM.BookPackingOperationLogic().GetLastPackSerial(exam.ExamID) + 1;

        //        List<BusinessLogicLayer.Entity.PPM.BookPackItemOperation> orderedPackSingle = new List<BusinessLogicLayer.Entity.PPM.BookPackItemOperation>();
        //        List<BusinessLogicLayer.Entity.PPM.BookPackItemOperation> orderedPackMultiple = new List<BusinessLogicLayer.Entity.PPM.BookPackItemOperation>();
        //        BusinessLogicLayer.Entity.PPM.BookPackItem parentItem = new BusinessLogicLayer.Entity.PPM.BookPackItem(PackID);

        //        foreach (BusinessLogicLayer.Entity.PPM.PackagingType pckg in orderedPackageTypes)
        //        {
        //            var packOrder = (from x in packing where x.PackagingTypeID == pckg.PackagingTypeID && x.PackingCalculationTypeID != 2 select x).FirstOrDefault();
        //            if (packOrder != null && packOrder.HasObject)
        //            {
        //                if (pckg.ExamModelCount == 1)
        //                    orderedPackSingle.Add(packOrder);
        //                else
        //                    orderedPackMultiple.Add(packOrder);
        //                orderedPack.Add(packOrder);
        //            }

        //        }


        //        foreach (BusinessLogicLayer.Entity.PPM.BookPackItemOperation pack in orderedPackSingle)
        //        {
        //            AddItemToPack(items, packageTypes, packing, oldPacks, model, exam, ref count, ref serial, pack, parentItem, parentID);
        //        }

        //        foreach (BusinessLogicLayer.Entity.PPM.BookPackItemOperation pack in orderedPackMultiple)
        //        {
        //            AddItemToPack(items, packageTypes, packing, oldPacks, model, exam, ref count, ref serial, pack, parentItem, parentID);
        //        }

        //        Qiyas.BusinessLogicLayer.Components.PPM.BookPackItemLogic itemLogic = new BusinessLogicLayer.Components.PPM.BookPackItemLogic();
        //        itemLogic.SaveItems(items);
        //        ViewBag.HasError = false;
        //        ViewBag.NotifyMessage = Resources.MainResource.NumberingPackSuccess;
        //        string url = string.Format("{0}", Url.Action("PrintPacks", "BookPackItemRePack"));
        //        url += "/" + RepackID + "/" + PrintingOperationID;
        //        //Routedi
        //        Dictionary<string, object> dictValues = new Dictionary<string, object>();
        //        dictValues.Add("ID", RepackID);
        //        dictValues.Add("PrintingID", PrintingOperationID);

        //        return RedirectToAction("PrintPacks/" + RepackID + "/" + PrintingOperationID, "BookPackItemRePack", RepackID);
        //        //Response.Redirect(url);
        //        //PrintPacks(parentID, PrintingOperationID);
        //    }
        //    else
        //    {
        //        ViewBag.HasError = true;
        //        ViewBag.NotifyMessage = "يرجي مراجعه الحزم المراد اعادة تحزيمها";
        //    }



        //    return View("Index", model);
        //}

        

        //private void AddItemToPack(List<BusinessLogicLayer.Entity.PPM.BookPackItem> items, List<BusinessLogicLayer.Entity.PPM.PackagingType> packageTypes, List<BusinessLogicLayer.Entity.PPM.BookPackItemOperation> packing, List<BusinessLogicLayer.Entity.PPM.BookPackItemOperation> oldPacks, BusinessLogicLayer.Entity.PPM.BookPrintingOperation model, BusinessLogicLayer.Entity.PPM.Exam exam, ref int count, ref int serial, BusinessLogicLayer.Entity.PPM.BookPackItemOperation pack, BusinessLogicLayer.Entity.PPM.BookPackItem parentItem, int parentRepackID)
        //{
        //    serial = 1;




        //    BusinessLogicLayer.Components.PPM.BookPackItemLogic BookPackItemLogic = new BusinessLogicLayer.Components.PPM.BookPackItemLogic();







        //    foreach (BusinessLogicLayer.Entity.PPM.BookRepackPackageItem vparentItem in BookRepackPackageItemList)
        //    {
        //        parentItem = new BusinessLogicLayer.Entity.PPM.BookPackItem(vparentItem.BookPackItemID.Value);
        //        int i = 0;
        //        var packType = (from x in packageTypes where x.PackagingTypeID == pack.PackagingTypeID select x).FirstOrDefault();
        //        var exists = (from x in oldPacks where x.BookPackItemOperationID == pack.BookPackItemOperationID select x).FirstOrDefault();
        //        BusinessLogicLayer.Entity.PPM.BookPackingOperation packOperation = new BusinessLogicLayer.Entity.PPM.BookPackingOperation(parentItem.BookPackingOperationID.Value);
        //        if (i >= pack.PackageTotal.Value)
        //            break;
        //        if (exists == null || !exists.HasObject)
        //        {
        //            int bookStart = 0;
        //            int bookLast = BookPackItemLogic.GetLastBookSerialForRePackedItem(parentItem.BookPackItemID);
        //            if (bookLast == 0)
        //                bookLast = 1;
        //            var oldPackageCount = BookPackItemLogic.GetPackageBooksCount(parentItem.BookPackItemID);
        //            int remaining = oldPackageCount;
        //            bool isFirst = true;
        //            while (remaining > 0)
        //            {
        //                BusinessLogicLayer.Entity.PPM.BookPackItem item = new BusinessLogicLayer.Entity.PPM.BookPackItem();
        //                item.BookPackingOperationID = packOperation.BookPackingOperationID;
        //                item.BookPackItemOperationID = pack.BookPackItemOperationID;
        //                item.BookPackItemID = PackID;
        //                item.OperationStatusID = 7;
        //                item.ParentID = parentRepackID;
        //                item.PackSerial = serial;
        //                item.ParentBookPackItemID = parentItem.BookPackItemID;
        //                string modelCode = "";
        //                List<BusinessLogicLayer.Entity.PPM.BookPackItemModel> itemModels = new List<BusinessLogicLayer.Entity.PPM.BookPackItemModel>();

        //                bookStart = i == 0 ? bookLast : bookLast + 1;
        //                bookLast = bookStart + (packType.BooksPerPackage.Value - 1);

        //                foreach (BusinessLogicLayer.Entity.PPM.BookPackItemModel examModel in parentItem.ItemModels)
        //                {
        //                    if (packType.ExamModelCount > 1)
        //                    {
        //                        BusinessLogicLayer.Entity.PPM.BookPackItemModel newModel = new BusinessLogicLayer.Entity.PPM.BookPackItemModel();
        //                        newModel.BookPackItemID = item.BookPackItemID;
        //                        newModel.ExamModelID = examModel.ExamModelID;
        //                        modelCode += examModel.ExamModelID + "-";
        //                        item.StartBookSerial = bookStart;
        //                        item.LastBookSerial = bookLast;
        //                        remaining -= packType.BooksPerPackage.Value;

        //                        itemModels.Add(newModel);
        //                    }
        //                    else
        //                    {
        //                        if (isFirst)
        //                        {
        //                            remaining = remaining * parentItem.ItemModels.Count;
        //                            isFirst = false;
        //                        }

        //                        itemModels = new List<BusinessLogicLayer.Entity.PPM.BookPackItemModel>();
        //                        BusinessLogicLayer.Entity.PPM.BookPackItem itemUnit = new BusinessLogicLayer.Entity.PPM.BookPackItem();
        //                        itemUnit.BookPackingOperationID = packOperation.BookPackingOperationID;
        //                        itemUnit.BookPackItemOperationID = pack.BookPackItemOperationID;
        //                        itemUnit.BookPackItemID = PackID;
        //                        itemUnit.OperationStatusID = 7;
        //                        itemUnit.ParentBookPackItemID = parentItem.BookPackItemID;
        //                        itemUnit.StartBookSerial = bookStart;
        //                        itemUnit.LastBookSerial = bookLast;
        //                        itemUnit.PackSerial = serial;
        //                        //itemUnit.PackCode = PrintingOperationID + "-" + pack.BookPackingOperationID + "-" + pack.PackagingTypeID + "-" + examModel.ExamModelID + "-" + serial;
        //                        itemUnit.PackCode = RandomString(12);
        //                        BusinessLogicLayer.Entity.PPM.BookPackItemModel newModel = new BusinessLogicLayer.Entity.PPM.BookPackItemModel();
        //                        newModel.BookPackItemID = item.BookPackItemID;
        //                        newModel.ExamModelID = examModel.ExamModelID;
        //                        modelCode += examModel.ExamModelID + "-";
        //                        itemModels.Add(newModel);
        //                        itemUnit.ParentID = parentRepackID;
        //                        itemUnit.ItemModels = itemModels;
        //                        remaining -= packType.BooksPerPackage.Value;
        //                        items.Add(itemUnit);
        //                        //serial++;
        //                    }

        //                }
        //                if (packType.ExamModelCount > 1)
        //                {
        //                    if (!string.IsNullOrEmpty(modelCode))
        //                        modelCode = modelCode.Remove(modelCode.Length - 1, 1);
        //                    //item.PackCode = PrintingOperationID + "-" + pack.BookPackingOperationID + "-" + pack.PackagingTypeID + "-" + modelCode + "-" + serial;
        //                    item.PackCode = RandomString(12);
        //                    item.ItemModels = itemModels;
        //                    items.Add(item);

        //                }
        //                serial++;
        //                i++;
        //                ///TODO: Add Pack Items for Sub Packs

        //            }
        //        }
        //    }



        //}

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

    }
}