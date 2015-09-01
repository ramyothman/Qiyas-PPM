using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace Qiyas.WebAdmin.Controllers
{
    [Authorize]
    public class ExamController : Controller
    {
        // GET: Exam
        public ActionResult Index()
        {
            return View();
        }

        #region Exam Grid View

        [ValidateInput(false)]
        public ActionResult ExamGridViewPartial()
        {
            var model = new BusinessLogicLayer.Components.PPM.ExamLogic().GetAllView();
            return PartialView("_ExamGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ExamGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Qiyas.BusinessLogicLayer.Entity.PPM.Exam item)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    if (!ExamExists(item.ExamCode, item.ExamID))
                    {
                        BusinessLogicLayer.Entity.PPM.Exam exam = new BusinessLogicLayer.Entity.PPM.Exam();
                        exam.ExamCode = item.ExamCode;
                        exam.ExamSpecialityID = item.ExamSpecialityID;
                        exam.ExamTypeID = item.ExamTypeID;
                        exam.Notes = item.Notes;
                        exam.NumberofPages = item.NumberofPages;
                        exam.NumberofSections = item.NumberofSections;
                        exam.StudentGenderID = item.StudentGenderID;
                        exam.TimeForSection = item.TimeForSection;
                        exam.Name = item.Name;
                        exam.ExamTypeID = item.ExamTypeID;
                        exam.IsActive = item.IsActive;
                        exam.ModifiedDate = DateTime.Now;
                        exam.CreatedDate = DateTime.Now;
                        exam.Save();
                        string[] itemsExamModels = Request["ExamModelCheckBoxList"].Split('|');
                        var examModels = new BusinessLogicLayer.Components.PPM.ExamModelLogic().GetAll();
                        for (int i = 1; i < itemsExamModels.Length; i++)
                        {
                            int examModelIndex = 0;
                            if (i == itemsExamModels.Length - 1)
                                Int32.TryParse(itemsExamModels[i], out examModelIndex);
                            else
                                Int32.TryParse(itemsExamModels[i].Remove(itemsExamModels[i].Length - 1, 1), out examModelIndex);
                            BusinessLogicLayer.Entity.PPM.ExamModelItem mitem = new BusinessLogicLayer.Entity.PPM.ExamModelItem();
                            mitem.ExamID = exam.ExamID;
                            mitem.ExamModelID = examModels[examModelIndex].ExamModelID;
                            mitem.Save();
                        }
                    }
                    else
                    {
                        ViewData["EditError"] = Resources.MainResource.ExamExists;
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
            {
                string msgError = "";
                foreach (ModelState modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        msgError += error.ErrorMessage + " ";
                    }
                }
                ViewData["EditError"] = msgError;
                //ViewData["EditError"] = Resources.MainResource.PleaseCorrectErrors;
            }
                
            var model = new BusinessLogicLayer.Components.PPM.ExamLogic().GetAllView();
            return PartialView("_ExamGridViewPartial", model);
        }

        
        [HttpPost, ValidateInput(false)]
        public ActionResult ExamGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Qiyas.BusinessLogicLayer.Entity.PPM.Exam item)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    if (!ExamExists(item.ExamCode, item.ExamID))
                    {
                        
                        BusinessLogicLayer.Entity.PPM.Exam exam = new BusinessLogicLayer.Entity.PPM.Exam(item.ExamID);
                        

                        exam.ExamCode = item.ExamCode;
                        exam.ExamSpecialityID = item.ExamSpecialityID;
                        exam.ExamTypeID = item.ExamTypeID;
                        exam.Notes = item.Notes;
                        exam.NumberofPages = item.NumberofPages;
                        exam.NumberofSections = item.NumberofSections;
                        exam.StudentGenderID = item.StudentGenderID;
                        exam.TimeForSection = item.TimeForSection;
                        exam.Name = item.Name;
                        exam.ExamTypeID = item.ExamTypeID;
                        exam.IsActive = item.IsActive;
                        exam.ModifiedDate = DateTime.Now;
                        //exam.CreatedDate = DateTime.Now;
                        exam.Save();
                        BusinessLogicLayer.Components.PPM.ExamModelItemLogic logic = new BusinessLogicLayer.Components.PPM.ExamModelItemLogic();
                        logic.DeleteByExamID(exam.ExamID);
                        string[] itemsExamModels = Request["ExamModelCheckBoxList"].Split('|');
                        var examModels = new BusinessLogicLayer.Components.PPM.ExamModelLogic().GetAll();
                        for (int i = 1; i < itemsExamModels.Length; i++ )
                        {
                            int examModelIndex = 0;
                            if(i == itemsExamModels.Length - 1)
                                Int32.TryParse(itemsExamModels[i], out examModelIndex);
                            else
                                Int32.TryParse(itemsExamModels[i].Remove(itemsExamModels[i].Length - 1, 1), out examModelIndex);
                            BusinessLogicLayer.Entity.PPM.ExamModelItem mitem = new BusinessLogicLayer.Entity.PPM.ExamModelItem();
                            mitem.ExamID = exam.ExamID;
                            mitem.ExamModelID = examModels[examModelIndex].ExamModelID;
                            mitem.Save();
                        }
                    }
                    else
                    {
                        ViewData["EditError"] = Resources.MainResource.ExamExists;
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = Resources.MainResource.PleaseCorrectErrors;
            var model = new BusinessLogicLayer.Components.PPM.ExamLogic().GetAllView();
            return PartialView("_ExamGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ExamGridViewPartialDelete(System.Int32 ExamID)
        {
            
            if (ExamID >= 0)
            {
                try
                {
                    BusinessLogicLayer.Components.PPM.ExamLogic logic = new BusinessLogicLayer.Components.PPM.ExamLogic();
                    if (!logic.HasDependencies(ExamID))
                    {
                        BusinessLogicLayer.Entity.PPM.Exam type = new BusinessLogicLayer.Entity.PPM.Exam(ExamID);
                        type.Delete();
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = new BusinessLogicLayer.Components.PPM.ExamLogic().GetAllView();
            return PartialView("_ExamGridViewPartial", model);
        }
        #endregion

        #region Helpers
        private bool ExamExists(string name, int id)
        {
            var currentUser = new BusinessLogicLayer.Entity.PPM.Exam(id);
            var checkUser = new BusinessLogicLayer.Components.PPM.ExamLogic().GetByCode(name);
            if (checkUser == null)
                return false;
            if (!currentUser.HasObject && checkUser != null)
                return true;
            else if (currentUser.HasObject && checkUser != null && currentUser.ExamID != checkUser.ExamID)
                return true;

            return false;
        }
        #endregion
    }
}