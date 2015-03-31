using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace Qiyas.WebAdmin.Controllers
{
    [Authorize]
    public class ExamCenterController : Controller
    {
        // GET: ExamCenter
        public ActionResult Index()
        {
            return View();
        }

        #region Exam Center Grid
        [ValidateInput(false)]
        public ActionResult ExamCenterGridViewPartial()
        {
            var model = new BusinessLogicLayer.Components.PPM.ExamCenterLogic().GetAll();
            return PartialView("_ExamCenterGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ExamCenterGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Qiyas.BusinessLogicLayer.Entity.PPM.ExamCenter item)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    try
                    {
                        if (!ExamCenterExists(item.CenterCode, item.ExaminationCenterID))
                        {
                            Qiyas.BusinessLogicLayer.Entity.PPM.ExamCenter center = new BusinessLogicLayer.Entity.PPM.ExamCenter();
                            center.CityID = item.CityID;
                            center.Name = item.Name;
                            center.StudentGenderID = item.StudentGenderID;
                            center.ModifiedDate = DateTime.Now;
                            center.CreatedDate = DateTime.Now;
                            center.CenterCode = item.CenterCode;
                            center.IsActive = item.IsActive;
                            center.Save();
                        }
                        else
                        {
                            ViewData["EditError"] = Resources.MainResource.ExamCenterExistsCheck;
                        }
                    }
                    catch (Exception e)
                    {
                        ViewData["EditError"] = e.Message;
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = Resources.MainResource.PleaseCorrectErrors;
            var model = new BusinessLogicLayer.Components.PPM.ExamCenterLogic().GetAll();
            return PartialView("_ExamCenterGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ExamCenterGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Qiyas.BusinessLogicLayer.Entity.PPM.ExamCenter item)
        {
            
            if (ModelState.IsValid)
            {
                try
                {

                    if (!ExamCenterExists(item.CenterCode, item.ExaminationCenterID))
                    {
                        Qiyas.BusinessLogicLayer.Entity.PPM.ExamCenter center = new BusinessLogicLayer.Entity.PPM.ExamCenter(item.ExaminationCenterID);
                        if(item.CenterCode == center.CenterCode)
                        {
                            center.CityID = item.CityID;
                            center.Name = item.Name;
                            center.StudentGenderID = item.StudentGenderID;
                            center.ModifiedDate = DateTime.Now;
                            center.CenterCode = item.CenterCode;
                            center.IsActive = item.IsActive;
                            center.Save();
                        }
                        else
                        {
                            ViewData["EditError"] = Resources.MainResource.ExamCodeCantBeModified;
                        }
                        
                    }
                    else
                    {
                        ViewData["EditError"] = Resources.MainResource.ExamCenterExistsCheck;
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = Resources.MainResource.PleaseCorrectErrors;
            var model = new BusinessLogicLayer.Components.PPM.ExamCenterLogic().GetAll();
            return PartialView("_ExamCenterGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ExamCenterGridViewPartialDelete(System.Int32 ExaminationCenterID)
        {
            
            if (ExaminationCenterID >= 0)
            {
                try
                {
                    BusinessLogicLayer.Components.Persons.StateProvinceLogic logic = new BusinessLogicLayer.Components.Persons.StateProvinceLogic();
                    if (!logic.HasDependencies(ExaminationCenterID))
                    {
                        Qiyas.BusinessLogicLayer.Entity.PPM.ExamCenter center = new BusinessLogicLayer.Entity.PPM.ExamCenter(ExaminationCenterID);
                        center.Delete();
                    }
                    else
                    {
                        ViewData["EditError"] = Resources.MainResource.ExamCenterHasAssociatedData;
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = new BusinessLogicLayer.Components.PPM.ExamCenterLogic().GetAll();
            return PartialView("_ExamCenterGridViewPartial", model);
        }
        #endregion

        #region Helpers

        private bool ExamCenterExists(string code, int id)
        {
            var currentUser = new BusinessLogicLayer.Entity.PPM.ExamCenter(id);
            var checkUser = new BusinessLogicLayer.Components.PPM.ExamCenterLogic().GetByName(code);
            if (checkUser == null)
                return false;
            if (!currentUser.HasObject && checkUser != null)
                return true;
            else if (currentUser.HasObject && checkUser != null && currentUser.ExaminationCenterID != checkUser.ExaminationCenterID)
                return true;

            return false;
        }

        #endregion

    }
}