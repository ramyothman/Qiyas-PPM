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

        [HttpPost]
        public ActionResult GetPackagingTypeFrom(string item)
        {
            BusinessLogicLayer.Components.PPM.BookPackingOperationLogic poperation = new BusinessLogicLayer.Components.PPM.BookPackingOperationLogic();
            var items = poperation.GetAllPackagingTypeByBookPrintingID(Convert.ToInt32(item));
            return Json(items);
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


        public ActionResult PackagingTypeFromComboBoxPartial()
        {
            return PartialView("_PackagingTypeFromComboBoxPartial");
        }
    }
}