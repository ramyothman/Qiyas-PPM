using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Expressions;
using DevExpress.Web.Mvc;

namespace Qiyas.WebAdmin.Controllers
{
    [Authorize]
    public class ExamTypeController : Controller
    {
        public ActionResult Index()
        {
            //List<BusinessLogicLayer.Entity.PPM.ExamType> types = new BusinessLogicLayer.Components.PPM.ExamTypeLogic().GetAll();
            //if(types == null)
            //    types = new List<BusinessLogicLayer.Entity.PPM.ExamType>();

            return View();
        }

        // GET: ExamType
        #region Exam Type Methods

        [ValidateInput(false)]
        public ActionResult ExamTypeGridViewPartial()
        {
            var model = new BusinessLogicLayer.Components.PPM.ExamTypeLogic().GetAll();
            return PartialView("_ExamTypeGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ExamTypeGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Qiyas.BusinessLogicLayer.Entity.PPM.ExamType item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if(!ExamTypeExists(item.Name, item.ExaminationTypeID))
                    {
                        item.ModifiedDate = DateTime.Now;
                        item.CreatedDate = DateTime.Now;
                        item.Save();
                    }
                    else
                    {
                        ViewData["EditError"] = Resources.MainResource.ExamTypeExists;
                    }
                    
                    // Insert here a code to insert the new item in your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = Resources.MainResource.PleaseCorrectErrors;
            var model = new BusinessLogicLayer.Components.PPM.ExamTypeLogic().GetAll();
            return PartialView("_ExamTypeGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ExamTypeGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Qiyas.BusinessLogicLayer.Entity.PPM.ExamType item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    if (!ExamTypeExists(item.Name, item.ExaminationTypeID))
                    {
                        BusinessLogicLayer.Entity.PPM.ExamType type = new BusinessLogicLayer.Entity.PPM.ExamType(item.ExaminationTypeID);
                        type.Name = item.Name;
                        type.IsActive = item.IsActive;
                        type.ModifiedDate = DateTime.Now;
                        type.Save();
                    }
                    else
                    {
                        ViewData["EditError"] = Resources.MainResource.ExamTypeExists;
                    }
                    
                    // Insert here a code to update the item in your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = Resources.MainResource.PleaseCorrectErrors;
            var model = new BusinessLogicLayer.Components.PPM.ExamTypeLogic().GetAll();
            return PartialView("_ExamTypeGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ExamTypeGridViewPartialDelete(System.Int32 ExaminationTypeID)
        {

            if (ExaminationTypeID >= 0)
            {
                try
                {
                    BusinessLogicLayer.Components.PPM.ExamTypeLogic logic = new BusinessLogicLayer.Components.PPM.ExamTypeLogic();
                    if(!logic.HasDependencies(ExaminationTypeID))
                    {
                        BusinessLogicLayer.Entity.PPM.ExamType type = new BusinessLogicLayer.Entity.PPM.ExamType(ExaminationTypeID);
                        type.Delete();
                    }
                    
                    // Insert here a code to delete the item from your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = new BusinessLogicLayer.Components.PPM.ExamTypeLogic().GetAll();
            return PartialView("_ExamTypeGridViewPartial", model);
        }
        #endregion


        #region Helpers

        private bool ExamTypeExists(string name, int id)
        {
            var currentUser = new BusinessLogicLayer.Entity.PPM.ExamType(id);
            var checkUser = new BusinessLogicLayer.Components.PPM.ExamTypeLogic().GetByName(name);
            if (checkUser == null)
                return false;
            if (!currentUser.HasObject && checkUser != null)
                return true;
            else if (currentUser.HasObject && checkUser != null && currentUser.ExaminationTypeID != checkUser.ExaminationTypeID)
                return true;

            return false;
        }

        #endregion
    }
}