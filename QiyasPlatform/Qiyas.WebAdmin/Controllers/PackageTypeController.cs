using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace Qiyas.WebAdmin.Controllers
{
    [Authorize]
    public class PackageTypeController : Controller
    {
        // GET: PackageType
        public ActionResult Index()
        {
            return View();
        }

        #region Package Type Grid

        [ValidateInput(false)]
        public ActionResult PackageTypeGridViewPartial()
        {
            var model = new BusinessLogicLayer.Components.PPM.PackagingTypeLogic().GetAll();
            return PartialView("_PackageTypeGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult PackageTypeGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Qiyas.BusinessLogicLayer.Entity.PPM.PackagingType item)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    if (!PackageExists(item.BooksPerPackage.Value, item.ExamModelCount.Value, item.PackagingTypeID))
                    {
                        Qiyas.BusinessLogicLayer.Entity.PPM.PackagingType package = new BusinessLogicLayer.Entity.PPM.PackagingType();
                        package.BooksPerPackage = item.BooksPerPackage;
                        package.CreatedDate = DateTime.Now;
                        package.ExamModelCount = item.ExamModelCount;
                        package.IsActive = item.IsActive;
                        package.ModifiedDate = DateTime.Now;
                        package.Name = item.Name;
                        package.Total = item.Total;
                        package.Save();
                    }
                    else
                    {
                        ViewData["EditError"] = Resources.MainResource.PackageTypeExists;
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = Resources.MainResource.PleaseCorrectErrors;
            var model = new BusinessLogicLayer.Components.PPM.PackagingTypeLogic().GetAll();
            return PartialView("_PackageTypeGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult PackageTypeGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Qiyas.BusinessLogicLayer.Entity.PPM.PackagingType item)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    if (!PackageExists(item.BooksPerPackage.Value, item.ExamModelCount.Value, item.PackagingTypeID))
                    {
                        Qiyas.BusinessLogicLayer.Entity.PPM.PackagingType package = new BusinessLogicLayer.Entity.PPM.PackagingType(item.PackagingTypeID);
                        package.BooksPerPackage = item.BooksPerPackage;
                        package.ExamModelCount = item.ExamModelCount;
                        package.IsActive = item.IsActive;
                        package.ModifiedDate = DateTime.Now;
                        package.Name = item.Name;
                        package.Total = item.Total;
                        package.Save();
                    }
                    else
                    {
                        ViewData["EditError"] = Resources.MainResource.PackageTypeExists;
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = new BusinessLogicLayer.Components.PPM.PackagingTypeLogic().GetAll();
            return PartialView("_PackageTypeGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult PackageTypeGridViewPartialDelete(System.Int32 PackagingTypeID)
        {
            if (PackagingTypeID >= 0)
            {
                try
                {
                    BusinessLogicLayer.Components.PPM.PackagingTypeLogic logic = new BusinessLogicLayer.Components.PPM.PackagingTypeLogic();
                    if (!logic.HasDependencies(PackagingTypeID))
                    {
                        Qiyas.BusinessLogicLayer.Entity.PPM.PackagingType package = new BusinessLogicLayer.Entity.PPM.PackagingType(PackagingTypeID);
                        package.Delete();
                    }
                    else
                    {
                        ViewData["EditError"] = Resources.MainResource.CityHasAssociatedData;
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = new BusinessLogicLayer.Components.PPM.PackagingTypeLogic().GetAll();
            return PartialView("_PackageTypeGridViewPartial", model);
        }
        #endregion

        #region Helpers

        private bool PackageExists(int BookCount, int ExamModelCount, int id)
        {
            var currentUser = new BusinessLogicLayer.Entity.PPM.PackagingType(id);
            var checkUser = new BusinessLogicLayer.Components.PPM.PackagingTypeLogic().GetByBookCountandExamModelCount(BookCount, ExamModelCount);
            if (checkUser == null)
                return false;
            if (!currentUser.HasObject && checkUser != null)
                return true;
            else if (currentUser.HasObject && checkUser != null && currentUser.PackagingTypeID != checkUser.PackagingTypeID)
                return true;

            return false;
        }

        #endregion
    }
}