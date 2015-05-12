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
            var model = new object[0];
            return PartialView("_ExamCenterNeedsGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ExamCenterNeedsGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Qiyas.BusinessLogicLayer.Entity.PPM.ExamCenterRequiredExam item)
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
            return PartialView("_ExamCenterNeedsGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ExamCenterNeedsGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Qiyas.BusinessLogicLayer.Entity.PPM.ExamCenterRequiredExam item)
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
            return PartialView("_ExamCenterNeedsGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ExamCenterNeedsGridViewPartialDelete(System.Int32 ExamCenterRequiredExamsID)
        {
            var model = new object[0];
            if (ExamCenterRequiredExamsID >= 0)
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
            return PartialView("_ExamCenterNeedsGridViewPartial", model);
        }
    }
}