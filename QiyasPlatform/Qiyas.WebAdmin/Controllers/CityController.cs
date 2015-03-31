using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace Qiyas.WebAdmin.Controllers
{
    [Authorize]
    public class CityController : Controller
    {
        // GET: City
        public ActionResult Index()
        {
            return View();
        }

        #region City Grid View
        [ValidateInput(false)]
        public ActionResult CityGridViewPartial()
        {
            var model = new BusinessLogicLayer.Components.Persons.CityLogic().GetAll();
            return PartialView("_CityGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult CityGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Qiyas.BusinessLogicLayer.Entity.Persons.City item)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    if (!CityExists(item.Name, item.CityID))
                    {
                        Qiyas.BusinessLogicLayer.Entity.Persons.City city = new BusinessLogicLayer.Entity.Persons.City();
                        city.StateRegionID = item.StateRegionID;
                        city.Name = item.Name;
                        city.IsActive = item.IsActive;
                        city.Save();
                    }
                    else
                    {
                        ViewData["EditError"] = Resources.MainResource.CityExistsCheck;
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = Resources.MainResource.PleaseCorrectErrors;
            var model = new BusinessLogicLayer.Components.Persons.CityLogic().GetAll();
            return PartialView("_CityGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult CityGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Qiyas.BusinessLogicLayer.Entity.Persons.City item)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    if (!CityExists(item.Name, item.CityID))
                    {
                        Qiyas.BusinessLogicLayer.Entity.Persons.City city = new BusinessLogicLayer.Entity.Persons.City(item.CityID);
                        city.StateRegionID = item.StateRegionID;
                        city.Name = item.Name;
                        city.IsActive = item.IsActive;
                        city.Save();
                    }
                    else
                    {
                        ViewData["EditError"] = Resources.MainResource.CityExistsCheck;
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = Resources.MainResource.PleaseCorrectErrors;
            var model = new BusinessLogicLayer.Components.Persons.CityLogic().GetAll();
            return PartialView("_CityGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult CityGridViewPartialDelete(System.Int32 CityID)
        {
            if (CityID >= 0)
            {
                try
                {
                    BusinessLogicLayer.Components.Persons.CityLogic logic = new BusinessLogicLayer.Components.Persons.CityLogic();
                    if (!logic.HasCities(CityID))
                    {
                        Qiyas.BusinessLogicLayer.Entity.Persons.City city = new BusinessLogicLayer.Entity.Persons.City(CityID);
                        city.Delete();
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
            var model = new BusinessLogicLayer.Components.Persons.CityLogic().GetAll();
            return PartialView("_CityGridViewPartial", model);
        }
        #endregion

        #region Partial
        public ActionResult ComboBoxCityPartial()
        {
            int stateID = (Request.Params["StateProvinceID"] != null) ? int.Parse(Request.Params["StateProvinceID"]) : -1;
            return PartialView(new BusinessLogicLayer.Components.Persons.CityLogic().GetAllByStateProvinceID(stateID));
        }
        #endregion

        #region Helpers

        private bool CityExists(string city, int id)
        {
            var currentUser = new BusinessLogicLayer.Entity.Persons.City(id);
            var checkUser = new BusinessLogicLayer.Components.Persons.CityLogic().GetByName(city);
            if (checkUser == null)
                return false;
            if (!currentUser.HasObject && checkUser != null)
                return true;
            else if (currentUser.HasObject && checkUser != null && currentUser.CityID != checkUser.CityID)
                return true;

            return false;
        }

        #endregion
    }
}