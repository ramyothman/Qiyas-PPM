using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace Qiyas.WebAdmin.Controllers
{
    [Authorize]
    public class StateProvinceController : Controller
    {
        // GET: StateProvince
        public ActionResult Index()
        {
            ViewBag.Countries = new BusinessLogicLayer.Components.Persons.CountryRegionLogic().GetAll();
            return View();
        }

        #region State Province Grid
        [ValidateInput(false)]
        public ActionResult StateProvinceGridViewPartial()
        {
            var model = new BusinessLogicLayer.Components.Persons.StateProvinceLogic().GetAll();
            return PartialView("_StateProvinceGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult StateProvinceGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Qiyas.BusinessLogicLayer.Entity.Persons.StateProvince item)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    if (!StateProvinceExists(item.Name, item.StateProvinceId))
                    {
                        Qiyas.BusinessLogicLayer.Entity.Persons.StateProvince state = new BusinessLogicLayer.Entity.Persons.StateProvince();
                        state.CountryRegionCode = item.CountryRegionCode;
                        state.ModifiedDate = DateTime.Now;
                        state.Name = item.Name;
                        state.RowGuid = Guid.NewGuid();
                        state.Save();
                    }
                    else
                    {
                        ViewData["EditError"] = Resources.MainResource.StateProvinceExists;
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = Resources.MainResource.PleaseCorrectErrors;
            
            var model = new BusinessLogicLayer.Components.Persons.StateProvinceLogic().GetAll();
            return PartialView("_StateProvinceGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult StateProvinceGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Qiyas.BusinessLogicLayer.Entity.Persons.StateProvince item)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    if (!StateProvinceExists(item.Name, item.StateProvinceId))
                    {
                        Qiyas.BusinessLogicLayer.Entity.Persons.StateProvince state = new BusinessLogicLayer.Entity.Persons.StateProvince(item.StateProvinceId);
                        state.CountryRegionCode = item.CountryRegionCode;
                        state.ModifiedDate = DateTime.Now;
                        state.Name = item.Name;
                        state.Save();
                    }
                    else
                    {
                        ViewData["EditError"] = Resources.MainResource.StateProvinceExists;
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = Resources.MainResource.PleaseCorrectErrors;
            var model = new BusinessLogicLayer.Components.Persons.StateProvinceLogic().GetAll();
            return PartialView("_StateProvinceGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult StateProvinceGridViewPartialDelete(System.Int32 StateProvinceId)
        {
            
            if (StateProvinceId >= 0)
            {
                try
                {
                    BusinessLogicLayer.Components.Persons.StateProvinceLogic logic = new BusinessLogicLayer.Components.Persons.StateProvinceLogic();
                    if (!logic.HasCities(StateProvinceId))
                    {
                        Qiyas.BusinessLogicLayer.Entity.Persons.StateProvince state = new BusinessLogicLayer.Entity.Persons.StateProvince(StateProvinceId);
                        state.Delete();
                    }
                    else
                    {
                        ViewData["EditError"] = Resources.MainResource.CitiesAssociatedToState;
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = new BusinessLogicLayer.Components.Persons.StateProvinceLogic().GetAll();
            return PartialView("_StateProvinceGridViewPartial", model);
        }

        #endregion

        #region Helpers

        private bool StateProvinceExists(string state, int id)
        {
            var currentUser = new BusinessLogicLayer.Entity.Persons.StateProvince(id);
            var checkUser = new BusinessLogicLayer.Components.Persons.StateProvinceLogic().GetByName(state);
            if (checkUser == null)
                return false;
            if (!currentUser.HasObject && checkUser != null)
                return true;
            else if (currentUser.HasObject && checkUser != null && currentUser.StateProvinceId != checkUser.StateProvinceId)
                return true;

            return false;
        }

        #endregion
    }

    

    
}