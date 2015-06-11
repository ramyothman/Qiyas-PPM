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

                        if(!BookPackingOperationLogic.HaveA3Packs(printing.BookPrintingOperationID))
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
                                pack.PackageTotal = pack.PackageTotalPerModel * exam.ExamModels.Count;
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
                        if (type.OperationStatusID <= 2 || type.OperationStatusID == 8)
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
    }
}