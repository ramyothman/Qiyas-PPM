using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace Qiyas.WebAdmin.Controllers
{
    public class ExamSpecialityController : Controller
    {
        // GET: ExamSpeciality
        public ActionResult Index()
        {
            return View();
        }

        #region Exam Speciality Grid
        [ValidateInput(false)]
        public ActionResult GridViewPartial()
        {
            var model = new BusinessLogicLayer.Components.PPM.ExamSpecialityLogic().GetAll();
            return PartialView("_GridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Qiyas.BusinessLogicLayer.Entity.PPM.ExamSpeciality item)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    if (!ExamSpecialityExists(item.Name, item.ExamSpecialityID))
                    {
                        BusinessLogicLayer.Entity.PPM.ExamSpeciality speciality = new BusinessLogicLayer.Entity.PPM.ExamSpeciality();
                        speciality.Name = item.Name;
                        speciality.ExamTypeID = item.ExamTypeID;
                        speciality.IsActive = item.IsActive;
                        speciality.ModifiedDate = DateTime.Now;
                        speciality.CreatedDate = DateTime.Now;
                        speciality.Save();
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
            var model = new BusinessLogicLayer.Components.PPM.ExamSpecialityLogic().GetAll();
            return PartialView("_GridViewPartial", model);
        }

        
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Qiyas.BusinessLogicLayer.Entity.PPM.ExamSpeciality item)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    if (!ExamSpecialityExists(item.Name, item.ExamSpecialityID))
                    {
                        BusinessLogicLayer.Entity.PPM.ExamSpeciality speciality = new BusinessLogicLayer.Entity.PPM.ExamSpeciality(item.ExamSpecialityID);
                        speciality.ExamTypeID = item.ExamTypeID;
                        speciality.Name = item.Name;
                        speciality.IsActive = item.IsActive;
                        speciality.ModifiedDate = DateTime.Now;
                        speciality.Save();
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
            var model = new BusinessLogicLayer.Components.PPM.ExamSpecialityLogic().GetAll();
            return PartialView("_GridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialDelete(System.Int32 ExamSpecialityID)
        {
            
            if (ExamSpecialityID >= 0)
            {
                try
                {
                    BusinessLogicLayer.Components.PPM.ExamSpecialityLogic logic = new BusinessLogicLayer.Components.PPM.ExamSpecialityLogic();
                    if (!logic.HasDependencies(ExamSpecialityID))
                    {
                        BusinessLogicLayer.Entity.PPM.ExamSpeciality type = new BusinessLogicLayer.Entity.PPM.ExamSpeciality(ExamSpecialityID);
                        type.Delete();
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = new BusinessLogicLayer.Components.PPM.ExamSpecialityLogic().GetAll();
            return PartialView("_GridViewPartial", model);
        }
        #endregion

        #region Partial
        public ActionResult ComboBoxExamSpecialityPartial()
        {
            int typeID = (Request.Params["ExamTypeID"] != null) ? int.Parse(Request.Params["ExamTypeID"]) : -1;
            return PartialView(new BusinessLogicLayer.Components.PPM.ExamSpecialityLogic().GetAllByExamTypeID(typeID));
        }
        #endregion

        #region Helpers
        private bool ExamSpecialityExists(string name, int id)
        {
            var currentUser = new BusinessLogicLayer.Entity.PPM.ExamSpeciality(id);
            var checkUser = new BusinessLogicLayer.Components.PPM.ExamSpecialityLogic().GetByName(name);
            if (checkUser == null)
                return false;
            if (!currentUser.HasObject && checkUser != null)
                return true;
            else if (currentUser.HasObject && checkUser != null && currentUser.ExamSpecialityID != checkUser.ExamSpecialityID)
                return true;

            return false;
        }
        #endregion
    }
}