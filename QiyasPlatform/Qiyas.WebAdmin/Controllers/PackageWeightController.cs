using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace Qiyas.WebAdmin.Controllers
{
    public class PackageWeightController : Controller
    {
        // GET: PackageWeight
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult PackageWeightGridViewPartial()
        {
            var model = new BusinessLogicLayer.Components.PPM.PackageWeightLogic().GetAll();
            return PartialView("_PackageWeightGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult PackageWeightGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Qiyas.BusinessLogicLayer.Entity.PPM.PackageWeight item)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    BusinessLogicLayer.Entity.PPM.PackageWeight package = new BusinessLogicLayer.Entity.PPM.PackageWeight();
                    package.CreatedDate = DateTime.Now;
                    package.ModifiedDate = DateTime.Now;
                    package.Name = item.Name;
                    package.Weight = item.Weight;
                    package.PackageCode = item.PackageCode;
                    package.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = Resources.MainResource.PleaseCorrectErrors;
            
            var model = new BusinessLogicLayer.Components.PPM.PackageWeightLogic().GetAll();
            return PartialView("_PackageWeightGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult PackageWeightGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Qiyas.BusinessLogicLayer.Entity.PPM.PackageWeight item)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    BusinessLogicLayer.Entity.PPM.PackageWeight package = new BusinessLogicLayer.Entity.PPM.PackageWeight(item.PackageWeightID);
                    package.CreatedDate = DateTime.Now;
                    package.ModifiedDate = DateTime.Now;
                    package.Weight = item.Weight;
                    package.PackageCode = item.PackageCode;
                    package.Name = item.Name;
                    package.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = Resources.MainResource.PleaseCorrectErrors;
            var model = new BusinessLogicLayer.Components.PPM.PackageWeightLogic().GetAll();
            return PartialView("_PackageWeightGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult PackageWeightGridViewPartialDelete(System.Int32 PackageWeightID)
        {
            //var model = new object[0];
            if (PackageWeightID >= 0)
            {
                try
                {
                    BusinessLogicLayer.Entity.PPM.PackageWeight package = new BusinessLogicLayer.Entity.PPM.PackageWeight(PackageWeightID);
                    package.Delete();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = new BusinessLogicLayer.Components.PPM.PackageWeightLogic().GetAll();
            return PartialView("_PackageWeightGridViewPartial", model);
        }
    }
}