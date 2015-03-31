using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace Qiyas.WebAdmin.Controllers
{
    [Authorize]
    public class ExamModelController : Controller
    {
        // GET: ExamModel
        public ActionResult Index()
        {
            return View();
        }

        #region Exam Model Grid
        [ValidateInput(false)]
        public ActionResult ExamModelGridViewPartial()
        {
            var model = new BusinessLogicLayer.Components.PPM.ExamModelLogic().GetAll();
            return PartialView("_ExamModelGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ExamModelGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Qiyas.BusinessLogicLayer.Entity.PPM.ExamModel item)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    if (!ExamModelExists(item.Name, item.ExamModelID))
                    {
                        BusinessLogicLayer.Entity.PPM.ExamModel entity = new BusinessLogicLayer.Entity.PPM.ExamModel();
                        entity.Name = item.Name;
                        entity.IsActive = item.IsActive;
                        entity.ModifiedDate = DateTime.Now;
                        entity.CreatedDate = DateTime.Now;
                        entity.Save();
                    }
                    else
                    {
                        ViewData["EditError"] = Resources.MainResource.ExamModelExists;
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = Resources.MainResource.PleaseCorrectErrors;
            
            var model = new BusinessLogicLayer.Components.PPM.ExamModelLogic().GetAll();
            return PartialView("_ExamModelGridViewPartial", model);
        }

        
        [HttpPost, ValidateInput(false)]
        public ActionResult ExamModelGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Qiyas.BusinessLogicLayer.Entity.PPM.ExamModel item)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    if (!ExamModelExists(item.Name, item.ExamModelID))
                    {
                        BusinessLogicLayer.Entity.PPM.ExamModel entity = new BusinessLogicLayer.Entity.PPM.ExamModel(item.ExamModelID);
                        entity.Name = item.Name;
                        entity.IsActive = item.IsActive;
                        entity.ModifiedDate = DateTime.Now;
                        entity.Save();
                    }
                    else
                    {
                        ViewData["EditError"] = Resources.MainResource.ExamModelExists;
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = Resources.MainResource.PleaseCorrectErrors;
            
            var model = new BusinessLogicLayer.Components.PPM.ExamModelLogic().GetAll();
            return PartialView("_ExamModelGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ExamModelGridViewPartialDelete(System.Int32 ExamModelID)
        {
            
            if (ExamModelID >= 0)
            {
                try
                {
                    BusinessLogicLayer.Components.PPM.ExamModelLogic logic = new BusinessLogicLayer.Components.PPM.ExamModelLogic();
                    if (!logic.HasDependencies(ExamModelID))
                    {
                        BusinessLogicLayer.Entity.PPM.ExamModel type = new BusinessLogicLayer.Entity.PPM.ExamModel(ExamModelID);
                        type.Delete();
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = new BusinessLogicLayer.Components.PPM.ExamModelLogic().GetAll();
            return PartialView("_ExamModelGridViewPartial", model);
        }
        #endregion

        #region Helpers
        private bool ExamModelExists(string name, int id)
        {
            var currentUser = new BusinessLogicLayer.Entity.PPM.ExamModel(id);
            var checkUser = new BusinessLogicLayer.Components.PPM.ExamModelLogic().GetByName(name);
            if (checkUser == null)
                return false;
            if (!currentUser.HasObject && checkUser != null)
                return true;
            else if (currentUser.HasObject && checkUser != null && currentUser.ExamModelID != checkUser.ExamModelID)
                return true;

            return false;
        }
        #endregion

    }
}