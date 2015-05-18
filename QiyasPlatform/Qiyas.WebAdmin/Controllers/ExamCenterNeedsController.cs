using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace Qiyas.WebAdmin.Controllers
{
    public class ExamCenterNeedsController : Controller
    {
        // GET: ExamCenterNeeds
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult ExamCenterNeedsGridViewPartial()
        {
            var model = new BusinessLogicLayer.Components.PPM.ExamCenterRequiredExamLogic().GetAll();
            return PartialView("_ExamCenterNeedsGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ExamCenterNeedsGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Qiyas.BusinessLogicLayer.Entity.PPM.ExamCenterRequiredExam item)
        {
            item.RequestPreparationStatusID = 1;
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                modelState.Errors.Clear();

            }
            if (ModelState.IsValid)
            {
                try
                {
                    Qiyas.BusinessLogicLayer.Entity.PPM.ExamCenterRequiredExam saveItem = new BusinessLogicLayer.Entity.PPM.ExamCenterRequiredExam();
                    saveItem.CreatedDate = DateTime.Now;
                    saveItem.ExamCenterID = item.ExamCenterID;
                    saveItem.ExamPeriodID = item.ExamPeriodID;
                    saveItem.ModifiedDate = DateTime.Now;
                    saveItem.RequestPreparationStatusID = 1;
                    saveItem.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = new BusinessLogicLayer.Components.PPM.ExamCenterRequiredExamLogic().GetAll();
            return PartialView("_ExamCenterNeedsGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ExamCenterNeedsGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Qiyas.BusinessLogicLayer.Entity.PPM.ExamCenterRequiredExam item)
        {
         
            if (ModelState.IsValid)
            {
                try
                {
                    Qiyas.BusinessLogicLayer.Entity.PPM.ExamCenterRequiredExam saveItem = new BusinessLogicLayer.Entity.PPM.ExamCenterRequiredExam(item.ExamCenterRequiredExamsID);
                    saveItem.ExamCenterID = item.ExamCenterID;
                    saveItem.ExamPeriodID = item.ExamPeriodID;
                    saveItem.ModifiedDate = DateTime.Now;
                    saveItem.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = new BusinessLogicLayer.Components.PPM.ExamCenterRequiredExamLogic().GetAll();
            return PartialView("_ExamCenterNeedsGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ExamCenterNeedsGridViewPartialDelete(System.Int32 ExamCenterRequiredExamsID)
        {
            
            if (ExamCenterRequiredExamsID >= 0)
            {
                try
                {
                    Qiyas.BusinessLogicLayer.Entity.PPM.ExamCenterRequiredExam saveItem = new BusinessLogicLayer.Entity.PPM.ExamCenterRequiredExam(ExamCenterRequiredExamsID);
                    saveItem.Delete();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = new BusinessLogicLayer.Components.PPM.ExamCenterRequiredExamLogic().GetAll();
            return PartialView("_ExamCenterNeedsGridViewPartial", model);
        }
    }
}