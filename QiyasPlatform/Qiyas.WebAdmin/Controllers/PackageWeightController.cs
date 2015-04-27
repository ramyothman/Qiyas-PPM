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
            ViewBag.HasError = false;
            ViewBag.NotifyMessage = "";
            return View();
        }

        [ValidateInput(false)]
        public ActionResult PackageWeightGridViewPartial()
        {
            var model = new BusinessLogicLayer.Components.PPM.PackageWeightLogic().GetAllView();
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

            var model = new BusinessLogicLayer.Components.PPM.PackageWeightLogic().GetAllView();
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
            var model = new BusinessLogicLayer.Components.PPM.PackageWeightLogic().GetAllView();
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
                    var itemPack = new BusinessLogicLayer.Entity.PPM.BookPackItem(package.PackageCode.Value);
                    if (itemPack.OperationStatusID > 3)
                        ViewData["EditError"] = "لا يمكن حذف هذه الحزمة";
                    else
                    {
                        itemPack.Weight = null;
                        itemPack.OperationStatusID = 2;
                        itemPack.Save();
                        package.Delete();
                    }
                        
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = new BusinessLogicLayer.Components.PPM.PackageWeightLogic().GetAllView();
            return PartialView("_PackageWeightGridViewPartial", model);
        }

        [HttpPost]
        public ActionResult SaveWeight(string item, decimal weight)
        {

            var itemPack = new BusinessLogicLayer.Entity.PPM.BookPackItem(item);
            if (itemPack.OperationStatusID > 3)
                return Json("notsaved");
            if (itemPack.HasObject)
            {
                BusinessLogicLayer.Components.PPM.PackageWeightLogic logic = new BusinessLogicLayer.Components.PPM.PackageWeightLogic();
                var packageOperation = new BusinessLogicLayer.Entity.PPM.BookPackingOperation(itemPack.BookPackingOperationID.Value);
                if(itemPack.OperationStatusID == 2)
                    itemPack.OperationStatusID = 3;
                itemPack.Weight = weight;
                itemPack.Save();
                BusinessLogicLayer.Entity.PPM.PackageWeight package = new BusinessLogicLayer.Entity.PPM.PackageWeight(item);
                if (!package.HasObject)
                {
                    package = new BusinessLogicLayer.Entity.PPM.PackageWeight();
                    package.CreatedDate = DateTime.Now;
                }
                package.ModifiedDate = DateTime.Now;
                package.PackageCode = itemPack.BookPackItemID;
                package.Weight = weight;
                package.Save();
                logic.CheckPackWeightCompleted(packageOperation.BookPrintingOperationID.Value);
                return Json("saved");
            }
            else
            {
                return Json("notsaved");
            }
            
        }

        [HttpPost]
        public ActionResult CheckItem(string item)
        {
            var itemPack = new BusinessLogicLayer.Entity.PPM.BookPackItem(item);
            if (itemPack.HasObject)
            {
                return Json("exists");
            }
            else
            {
                return Json("notexists");
            }
            
            //var model = new BusinessLogicLayer.Entity.PPM.BookPrintingOperation(PrintingOperationID);
            //model.OperationStatusID = 2;
            //model.Save();
            //ViewBag.HasError = false;
            //ViewBag.NotifyMessage = Resources.MainResource.SaveSuccess;
            
        }
    }
}