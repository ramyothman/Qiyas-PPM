using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace Qiyas.WebAdmin.Controllers
{
    [Authorize]
    public class ExamPeriodController : Controller
    {
        // GET: ExamPeriod
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult ExamPeriodGridViewPartial()
        {
            ViewBag.ExamYear = Convert.ToInt32(BusinessLogicLayer.Tools.GregToHijriYear(DateTime.Today));
            var model = new BusinessLogicLayer.Components.PPM.ExamPeriodLogic().GetAll();
            return PartialView("_ExamPeriodGridViewPartial", model);
        }

        #region Exam Period Grid View
        [HttpPost, ValidateInput(false)]
        public ActionResult ExamPeriodGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Qiyas.BusinessLogicLayer.Entity.PPM.ExamPeriod item)
        {
            ViewBag.ExamYear = Convert.ToInt32(BusinessLogicLayer.Tools.GregToHijriYear(DateTime.Today));
            if (ModelState.IsValid)
            {
                try
                {
                    if (!ExamPeriodExists(item.Name, item.ExamPeriodID))
                    {
                        if(item.StartDate > item.EndDate)
                        {
                            ViewData["EditError"] = "تاريخ البداية يجب ان يكون اقل من تاريخ النهاية";
                        }
                        else
                        {
                            BusinessLogicLayer.Entity.PPM.ExamPeriod period = new BusinessLogicLayer.Entity.PPM.ExamPeriod();
                            period.Name = item.Name;
                            period.ExamTypeID = item.ExamTypeID;
                            period.StudentGenderID = item.StudentGenderID;
                            period.IsActive = item.IsActive;
                            period.ExamYear = item.ExamYear;
                            period.StartDate = item.StartDate;
                            period.EndDate = item.EndDate;
                            period.ModifiedDate = DateTime.Now;
                            period.CreatedDate = DateTime.Now;
                            period.Save();
                        }
                        
                    }
                    else
                    {
                        ViewData["EditError"] = Resources.MainResource.ExamSpecialityTitle;
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = Resources.MainResource.PleaseCorrectErrors;
            
            var model = new BusinessLogicLayer.Components.PPM.ExamPeriodLogic().GetAll();
            return PartialView("_ExamPeriodGridViewPartial", model);
        }

        
        [HttpPost, ValidateInput(false)]
        public ActionResult ExamPeriodGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Qiyas.BusinessLogicLayer.Entity.PPM.ExamPeriod item)
        {
            ViewBag.ExamYear = Convert.ToInt32(BusinessLogicLayer.Tools.GregToHijriYear(DateTime.Today));
            if (ModelState.IsValid)
            {
                try
                {
                    if (!ExamPeriodExists(item.Name, item.ExamPeriodID))
                    {
                        if (item.EndDate > item.StartDate)
                        {
                            ViewData["EditError"] = "تاريخ البداية يجب ان يكون اقل من تاريخ النهاية";
                        }
                        else
                        {
                            BusinessLogicLayer.Entity.PPM.ExamPeriod period = new BusinessLogicLayer.Entity.PPM.ExamPeriod(item.ExamPeriodID);
                            period.Name = item.Name;
                            period.ExamTypeID = item.ExamTypeID;
                            period.IsActive = item.IsActive;
                            period.StudentGenderID = item.StudentGenderID;
                            period.ExamYear = item.ExamYear;
                            period.StartDate = item.StartDate;
                            period.EndDate = item.EndDate;
                            period.ModifiedDate = DateTime.Now;
                            period.CreatedDate = DateTime.Now;
                            period.Save();
                        }
                        
                    }
                    else
                    {
                        ViewData["EditError"] = Resources.MainResource.ExamSpecialityTitle;
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = Resources.MainResource.PleaseCorrectErrors;
            
            var model = new BusinessLogicLayer.Components.PPM.ExamPeriodLogic().GetAll();
            return PartialView("_ExamPeriodGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ExamPeriodGridViewPartialDelete(System.Int32 ExamPeriodID)
        {
            ViewBag.ExamYear = Convert.ToInt32(BusinessLogicLayer.Tools.GregToHijriYear(DateTime.Today));
            if (ExamPeriodID >= 0)
            {
                try
                {
                    BusinessLogicLayer.Components.PPM.ExamPeriodLogic logic = new BusinessLogicLayer.Components.PPM.ExamPeriodLogic();
                    if (!logic.HasDependencies(ExamPeriodID))
                    {
                        BusinessLogicLayer.Entity.PPM.ExamPeriod type = new BusinessLogicLayer.Entity.PPM.ExamPeriod(ExamPeriodID);
                        type.Delete();
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            
            var model = new BusinessLogicLayer.Components.PPM.ExamPeriodLogic().GetAll();
            return PartialView("_ExamPeriodGridViewPartial", model);
        }
        #endregion

        #region Helpers
        private bool ExamPeriodExists(string name, int id)
        {
            var currentUser = new BusinessLogicLayer.Entity.PPM.ExamPeriod(id);
            var checkUser = new BusinessLogicLayer.Components.PPM.ExamPeriodLogic().GetByName(name);
            if (checkUser == null)
                return false;
            if (!currentUser.HasObject && checkUser != null)
                return true;
            else if (currentUser.HasObject && checkUser != null && currentUser.ExamPeriodID != checkUser.ExamPeriodID)
                return true;

            return false;
        }
        #endregion

    }
}