﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using System.IO;
using Excel;
using System.Data;

namespace Qiyas.WebAdmin.Controllers
{
    public class ExamCenterNeedsController : Controller
    {

        public int MainID
        {
            set
            {
                var key = "34FAA431-CF79-4869-2867-93F6AAE81263";
                var Session = HttpContext.Session;
                Session[key] = value;
            }
            get
            {
                var key = "34FAA431-CF79-4869-2867-93F6AAE81263";
                var Session = HttpContext.Session;
                if (Session[key] == null)
                    Session[key] = 0;
                return (int)Session[key];
            }
        }


        public int ReportID
        {
            set
            {
                var key = "34FAA431-CF79-4869-2867-93F6ABDE4263";
                var Session = HttpContext.Session;
                Session[key] = value;
            }
            get
            {
                var key = "34FAA431-CF79-4869-2867-93F6ABDE4263";
                var Session = HttpContext.Session;
                if (Session[key] == null)
                    Session[key] = 0;
                return (int)Session[key];
            }
        }

        public string ReportType
        {
            set
            {
                var key = "34FAA431-CF79-4869-2867-93F6ABDE4264";
                var Session = HttpContext.Session;
                Session[key] = value;
            }
            get
            {
                var key = "34FAA431-CF79-4869-2867-93F6ABDE4264";
                var Session = HttpContext.Session;
                if (Session[key] == null)
                    Session[key] = "request";
                return (string)Session[key];
            }
        }

        public List<BusinessLogicLayer.Entity.PPM.ExamRequirementItem> WithdrawExamRequirementItemList
        {
            set
            {
                var key = "34FAA431-CF79-0932-5423-93F6AAE81263";
                var Session = HttpContext.Session;
                Session[key] = value;
            }
            get
            {
                var key = "34FAA431-CF79-0932-5423-93F6AAE81263";
                var Session = HttpContext.Session;
                if (Session[key] == null)
                    Session[key] = ExamRequirementItemLogic.GetAllRemainingForWithdraw_ByExamCenterRequiredExamsID(MainID);
                if (Session[key] == null)
                    Session[key] = new List<BusinessLogicLayer.Entity.PPM.ExamRequirementItem>();
                return Session[key] as List<BusinessLogicLayer.Entity.PPM.ExamRequirementItem>;
            }
        }

        
        // GET: ExamCenterNeeds
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BrowseItems(int ID = 0)
        {
            if (ID == 0)
                return RedirectToAction("Index");

            BusinessLogicLayer.Entity.PPM.ExamCenterRequiredExam item = new BusinessLogicLayer.Entity.PPM.ExamCenterRequiredExam(ID);
            if(!item.HasObject)
                return RedirectToAction("Index");
            ViewBag.HasError = false;
            ViewBag.NotifyMessage = "";
            MainID = ID;
            return View(item);
        }

        public ActionResult ContainerRequest(int ID = 0)
        {
            if (ID == 0)
                return RedirectToAction("Index");

            BusinessLogicLayer.Entity.PPM.ExamCenterRequiredExam item = new BusinessLogicLayer.Entity.PPM.ExamCenterRequiredExam(ID);
            if (!item.HasObject)
                return RedirectToAction("Index");
            ViewBag.HasError = false;
            ViewBag.NotifyMessage = "";
            MainID = ID;
            return View(item);
        }
        
        public ActionResult SendItemsRequest(int ID = 0)
        {
            if (ID == 0)
                return RedirectToAction("Index");
            WithdrawExamRequirementItemList = null;
            BusinessLogicLayer.Entity.PPM.ExamCenterRequiredExam item = new BusinessLogicLayer.Entity.PPM.ExamCenterRequiredExam(ID);
            if(!item.HasObject)
                return RedirectToAction("Index");
            ViewBag.HasError = false;
            ViewBag.NotifyMessage = "";
            MainID = ID;
            return View(item);
        }
        
        [ValidateInput(false)]
        public ActionResult ExamCenterNeedsGridViewPartial()
        {
            var model = new BusinessLogicLayer.Components.PPM.ExamCenterRequiredExamLogic().GetAll();
            return PartialView("_ExamCenterNeedsGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ExamCenterNeedsGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Qiyas.BusinessLogicLayer.Entity.PPM.ExamCenterRequiredExam item)
        {
            item.RequestPreparationStatusID = 1;
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                modelState.Errors.Clear();

            }
            if (ModelState.IsValid)
            {
                try
                {
                    BusinessLogicLayer.Components.PPM.ExamCenterRequiredExamLogic logic = new BusinessLogicLayer.Components.PPM.ExamCenterRequiredExamLogic();
                    if(!logic.RecordExists(item.ExamPeriodID.Value, item.ExamCenterID.Value, 1))
                    {
                        Qiyas.BusinessLogicLayer.Entity.PPM.ExamCenterRequiredExam saveItem = new BusinessLogicLayer.Entity.PPM.ExamCenterRequiredExam();
                        saveItem.CreatedDate = DateTime.Now;
                        saveItem.ExamCenterID = item.ExamCenterID;
                        saveItem.ExamPeriodID = item.ExamPeriodID;
                        saveItem.ModifiedDate = DateTime.Now;
                        saveItem.RequestPreparationStatusID = 1;
                        saveItem.Save();
                    }
                    else
                    {
                        ViewData["EditError"] = "هذا السجل يوجد بالفعل";
                    }
                    
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = new BusinessLogicLayer.Components.PPM.ExamCenterRequiredExamLogic().GetAll();
            return PartialView("_ExamCenterNeedsGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ExamCenterNeedsGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Qiyas.BusinessLogicLayer.Entity.PPM.ExamCenterRequiredExam item)
        {
         
            if (ModelState.IsValid)
            {
                try
                {
                    Qiyas.BusinessLogicLayer.Entity.PPM.ExamCenterRequiredExam saveItem = new BusinessLogicLayer.Entity.PPM.ExamCenterRequiredExam(item.ExamCenterRequiredExamsID);
                    saveItem.ExamCenterID = item.ExamCenterID;
                    saveItem.ExamPeriodID = item.ExamPeriodID;
                    saveItem.ModifiedDate = DateTime.Now;
                    saveItem.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = new BusinessLogicLayer.Components.PPM.ExamCenterRequiredExamLogic().GetAll();
            return PartialView("_ExamCenterNeedsGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ExamCenterNeedsGridViewPartialDelete(System.Int32 ExamCenterRequiredExamsID)
        {
            
            if (ExamCenterRequiredExamsID >= 0)
            {
                try
                {
                    Qiyas.BusinessLogicLayer.Entity.PPM.ExamCenterRequiredExam saveItem = new BusinessLogicLayer.Entity.PPM.ExamCenterRequiredExam(ExamCenterRequiredExamsID);
                    saveItem.Delete();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = new BusinessLogicLayer.Components.PPM.ExamCenterRequiredExamLogic().GetAll();
            return PartialView("_ExamCenterNeedsGridViewPartial", model);
        }


        BusinessLogicLayer.Components.PPM.ExamRequirementItemLogic ExamRequirementItemLogic = new BusinessLogicLayer.Components.PPM.ExamRequirementItemLogic();
        [ValidateInput(false)]
        public ActionResult ExamRequirementItemGridViewPartial()
        {
            var model = ExamRequirementItemLogic.GetAllByExamCenterRequiredExamsID(MainID);
            return PartialView("_ExamRequirementItemGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ExamRequirementItemGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Qiyas.BusinessLogicLayer.Entity.PPM.ExamRequirementItem item)
        {
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                modelState.Errors.Clear();

            }
            if (ModelState.IsValid)
            {
                try
                {
                    if(!ExamRequirementItemLogic.ExamExistsInRequirements(MainID, item.ExamID.Value))
                    {
                        BusinessLogicLayer.Entity.PPM.ExamRequirementItem saveItem = new BusinessLogicLayer.Entity.PPM.ExamRequirementItem();
                        saveItem.ExamCenterRequiredExamsID = MainID;
                        saveItem.ExamID = item.ExamID;
                        saveItem.ExamsNeededForA3 = item.ExamsNeededForA3;
                        saveItem.ExamsNeededForCD = item.ExamsNeededForCD;
                        saveItem.PrintsForOneModel = item.PrintsForOneModel;
                        saveItem.RequestPreparationStatusID = 1;
                        saveItem.Save();
                    }
                    else
                    {
                        ViewData["EditError"] = "هذا الاختبار مدخل من قبل";
                    }
                    
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = ExamRequirementItemLogic.GetAllByExamCenterRequiredExamsID(MainID);
            return PartialView("_ExamRequirementItemGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ExamRequirementItemGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Qiyas.BusinessLogicLayer.Entity.PPM.ExamRequirementItem item)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    if(!ExamRequirementItemLogic.ExamExistsInRequirements(MainID, item.ExamID.Value, item.ExamRequirementItemID))
                    {
                        BusinessLogicLayer.Entity.PPM.ExamRequirementItem saveItem = new BusinessLogicLayer.Entity.PPM.ExamRequirementItem(item.ExamRequirementItemID);
                        saveItem.ExamID = item.ExamID;
                        saveItem.ExamsNeededForA3 = item.ExamsNeededForA3;
                        saveItem.ExamsNeededForCD = item.ExamsNeededForCD;
                        saveItem.PrintsForOneModel = item.PrintsForOneModel;
                        saveItem.Save();
                    }
                    else
                    {
                        ViewData["EditError"] = "هذا الاختبار مدخل من قبل";
                    }
                    
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = ExamRequirementItemLogic.GetAllByExamCenterRequiredExamsID(MainID);
            return PartialView("_ExamRequirementItemGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ExamRequirementItemGridViewPartialDelete(System.Int32 ExamRequirementItemID)
        {
            
            if (ExamRequirementItemID >= 0)
            {
                try
                {
                    BusinessLogicLayer.Entity.PPM.ExamRequirementItem saveItem = new BusinessLogicLayer.Entity.PPM.ExamRequirementItem(ExamRequirementItemID);
                    saveItem.Delete();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = ExamRequirementItemLogic.GetAllByExamCenterRequiredExamsID(MainID);
            return PartialView("_ExamRequirementItemGridViewPartial", model);
        }

        [HttpPost]
        public ActionResult SaveNeeds()
        {
            
            
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                string Extension = Path.GetExtension(file.FileName);
                switch (Extension)
                {
                    case ".xls":
                    case ".xlsx":
                        if (file != null && file.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            var path = Path.Combine(Server.MapPath("~/ContentData/Excel"), fileName);
                            file.SaveAs(path);
                            FileStream stream = System.IO.File.Open(path, FileMode.Open, FileAccess.Read);
                            try
                            {
                                //1. Reading from a binary Excel file ('97-2003 format; *.xls)
                                IExcelDataReader excelReader = null;
                                    if(Extension == ".xls")
                                        ExcelReaderFactory.CreateBinaryReader(stream);
                                    else
                                        excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                                    

                                
                                excelReader.IsFirstRowAsColumnNames = true;
                                DataSet result = excelReader.AsDataSet();
                                bool isFirst = true;
                                bool isExists = false;
                                //5. Data Reader methods
                                while (excelReader.Read())
                                {
                                    if (!isFirst)
                                    {
                                        string centerId = excelReader.GetString(0);
                                        BusinessLogicLayer.Entity.PPM.ExamCenter center = new BusinessLogicLayer.Entity.PPM.ExamCenter(centerId);
                                        if (!center.HasObject)
                                            continue;
                                        int periodId = 0;
                                        Int32.TryParse(Request["ExamPeriodID"], out periodId);
                                        BusinessLogicLayer.Entity.PPM.ExamCenterRequiredExam rexam = new BusinessLogicLayer.Entity.PPM.ExamCenterRequiredExam(periodId, center.ExaminationCenterID);
                                        if(!rexam.HasObject)
                                        {
                                            rexam = new BusinessLogicLayer.Entity.PPM.ExamCenterRequiredExam();
                                            rexam.ExamPeriodID = periodId;
                                            rexam.ExamCenterID = center.ExaminationCenterID;
                                            rexam.RequestPreparationStatusID = 1;
                                            rexam.Save();
                                        }
                                        string examId = excelReader.GetString(1);
                                        BusinessLogicLayer.Entity.PPM.Exam exam = new BusinessLogicLayer.Entity.PPM.Exam(examId);
                                        if(exam.HasObject)
                                        {
                                            BusinessLogicLayer.Entity.PPM.ExamRequirementItem item = new BusinessLogicLayer.Entity.PPM.ExamRequirementItem(rexam.ExamCenterRequiredExamsID, exam.ExamID);
                                            if (!item.HasObject)
                                            {
                                                item = new BusinessLogicLayer.Entity.PPM.ExamRequirementItem();
                                                item.ExamID = exam.ExamID;
                                                
                                                item.ExamCenterRequiredExamsID = rexam.ExamCenterRequiredExamsID;
                                                item.ExamsNeededForCD = 0;
                                                item.ExamsNeededForA4 = 0;
                                                item.ExamsNeededForA3 = 0;
                                                item.PrintsForOneModel = 0;
                                            }
                                            else
                                            {
                                                isExists = true;
                                            }
                                                

                                            string booksPerModel = excelReader.GetString(2);
                                            string A3 = excelReader.GetString(3);
                                            string CD = excelReader.GetString(4);

                                            int booksPerModelInt = 0, A3Int = 0, CDInt = 0;
                                            Int32.TryParse(booksPerModel, out booksPerModelInt);
                                            Int32.TryParse(A3, out A3Int);
                                            Int32.TryParse(CD, out CDInt);

                                            item.ExamsNeededForA3 += A3Int;
                                            item.ExamsNeededForCD += CDInt;
                                            item.PrintsForOneModel += booksPerModelInt;
                                            item.Save();
                                            
                                        }
                                        
                                    }
                                    else
                                        isFirst = false;
                                    
                                }
                            }
                            catch(Exception ex)
                            {

                            }
                            
                        }
                        break;
                    default:
                        ViewBag.HasError = true;
                        ViewBag.NotifyMessage = "هذا الملف غير مسموح به";
                        break;
                }
                
            }
            //BusinessLogicLayer.Entity.PPM.ExamCenterRequiredExam item = new BusinessLogicLayer.Entity.PPM.ExamCenterRequiredExam(MainID);
            return RedirectToAction("Index");
        }

        [ValidateInput(false)]
        public ActionResult WithdrawGridViewPartial()
        {
            var model = WithdrawExamRequirementItemList;
            return PartialView("_WithdrawGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult WithdrawGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Qiyas.BusinessLogicLayer.Entity.PPM.ExamRequirementItem item)
        {
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                modelState.Errors.Clear();

            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    int serial = 0;
                    bool exists = false;
                    foreach (var temp in WithdrawExamRequirementItemList)
                    {
                        if (temp.ExamRequirementItemID > serial)
                            serial = temp.ExamRequirementItemID;
                        if(temp.ExamID == item.ExamID)
                            exists = true;
                    }
                    if (!exists)
                    {
                        BusinessLogicLayer.Entity.PPM.ExamRequirementItem saveItem = new BusinessLogicLayer.Entity.PPM.ExamRequirementItem();
                        saveItem.ExamRequirementItemID = serial;
                        saveItem.ExamCenterRequiredExamsID = MainID;
                        saveItem.ExamID = item.ExamID;
                        saveItem.ExamsNeededForA3 = item.ExamsNeededForA3;
                        saveItem.ExamsNeededForCD = item.ExamsNeededForCD;
                        saveItem.PrintsForOneModel = item.PrintsForOneModel;
                        saveItem.RequestPreparationStatusID = 1;
                        WithdrawExamRequirementItemList.Add(saveItem);
                    }
                    else
                    {
                        ViewData["EditError"] = "هذا الاختبار مدخل من قبل";
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = WithdrawExamRequirementItemList;
            return PartialView("_WithdrawGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult WithdrawGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Qiyas.BusinessLogicLayer.Entity.PPM.ExamRequirementItem item)
        {
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                modelState.Errors.Clear();

            }
            if (ModelState.IsValid)
            {
                try
                {
                    bool exists = false;
                    foreach (var temp in WithdrawExamRequirementItemList)
                    {
                        
                        if(temp.ExamID == item.ExamID && temp.ExamRequirementItemID != item.ExamRequirementItemID)
                            exists = true;
                    }
                    var saveItem = (from x in WithdrawExamRequirementItemList where x.ExamRequirementItemID == item.ExamRequirementItemID select x).FirstOrDefault();
                    if(!exists)
                    {
                        if(saveItem != null)
                        {
                            saveItem.ExamID = item.ExamID;
                            saveItem.ExamsNeededForA3 = item.ExamsNeededForA3;
                            saveItem.ExamsNeededForCD = item.ExamsNeededForCD;
                            saveItem.PrintsForOneModel = item.PrintsForOneModel;
                        }
                    }
                    else
                    {
                        ViewData["EditError"] = "هذا الاختبار مدخل من قبل";
                    }
                    
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = WithdrawExamRequirementItemList;
            return PartialView("_WithdrawGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult WithdrawGridViewPartialDelete(System.Int32 ExamRequirementItemID)
        {
            
            if (ExamRequirementItemID >= 0)
            {
                try
                {
                    var saveItem = (from x in WithdrawExamRequirementItemList where x.ExamRequirementItemID == ExamRequirementItemID select x).FirstOrDefault();
                    WithdrawExamRequirementItemList.Remove(saveItem);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = WithdrawExamRequirementItemList;
            return PartialView("_WithdrawGridViewPartial", model);
        }

        [HttpPost]
        public ActionResult WithdrawRequest()
        {
            BusinessLogicLayer.Components.PPM.RequestWithdrawLogic RequestWithdrawLogic = new BusinessLogicLayer.Components.PPM.RequestWithdrawLogic();
            BusinessLogicLayer.Components.PPM.RequestWithdrawDetailLogic RequestWithdrawDetailLogic = new BusinessLogicLayer.Components.PPM.RequestWithdrawDetailLogic();
            BusinessLogicLayer.Entity.PPM.RequestWithdraw withdraw = new BusinessLogicLayer.Entity.PPM.RequestWithdraw();
            if(WithdrawExamRequirementItemList.Count > 0)
            {
                BusinessLogicLayer.Entity.PPM.ExamCenterRequiredExam ecr = new BusinessLogicLayer.Entity.PPM.ExamCenterRequiredExam(MainID);
                ecr.RequestPreparationStatusID = 2;
                ecr.Save();
                
                withdraw.CreatedDate = DateTime.Now;
                withdraw.ExamCenterRequiredExamsID = MainID;
                withdraw.ModifiedDate = DateTime.Now;
                withdraw.RequestOrder = RequestWithdrawLogic.GetSerial(MainID);
                withdraw.Save();
                
                foreach (var item in WithdrawExamRequirementItemList)
                {

                    BusinessLogicLayer.Entity.PPM.RequestWithdrawDetail detail = new BusinessLogicLayer.Entity.PPM.RequestWithdrawDetail();
                    detail.ExamsNeededForA3 = item.ExamsNeededForA3;
                    detail.ExamsNeededForA4 = item.ExamsNeededForA4;
                    detail.ExamsNeededForCD = item.ExamsNeededForCD;
                    detail.PrintsForOneModel = item.PrintsForOneModel;
                    detail.ExamRequirementItemID = item.ExamRequirementItemID;
                    detail.RequestPreparationStatusID = 2;
                    detail.RequestWithdrawID = withdraw.RequestWithdrawID;
                    detail.ShippedExamsNeededForA3 = 0;
                    detail.ShippedExamsNeededForA4 = 0;
                    detail.ShippedExamsNeededForCD = 0;
                    detail.ShippedPrintsForOneModel = 0;
                    detail.Save();
                    BusinessLogicLayer.Entity.PPM.Exam exam = new BusinessLogicLayer.Entity.PPM.Exam(item.ExamID.Value);
                    List<BusinessLogicLayer.Components.PPM.TotalRemainingPacks> remainingItems =  RequestWithdrawLogic.GetRemainingPacks();
                    List<BusinessLogicLayer.Entity.PPM.RequestWithdrawDetailItem> wItems = RequestWithdrawLogic.GetPacksForWithdrawalTotals(detail.PrintsForOneModel.Value, item.ExamID.Value, exam.ExamModels, remainingItems);
                    foreach(var m in wItems)
                    {
                        m.RequestWithdrawDetailID = detail.RequestWithdrawDetailID;
                        m.Save();
                        foreach(var c in m.Models)
                        {
                            c.RequestWithdrawDetailItemID = m.RequestWithdrawDetailItemID;
                            c.Save();
                        }
                    }

                }

                
            }
            System.Web.Routing.RouteValueDictionary dict = new System.Web.Routing.RouteValueDictionary();
            dict.Add("ID", withdraw.RequestWithdrawID);
            dict.Add("reportType", "withdraw");
            //BusinessLogicLayer.Entity.PPM.ExamCenterRequiredExam item = new BusinessLogicLayer.Entity.PPM.ExamCenterRequiredExam(MainID);
            return RedirectToAction("WithdrawReport", dict);
        }

        //public ActionResult WithdrawReport(int ID)
        //{
        //    if (ID == 0)
        //        return RedirectToAction("Index");
        //    ViewBag.HasError = false;
        //    ViewBag.NotifyMessage = "";
        //    ReportID = ID;
        //    return View();
        //}

        public ActionResult WithdrawReport(int ID = 0, string ReportType = "request")
        {
            if (ID == 0)
                return RedirectToAction("Index");
            ViewBag.HasError = false;
            ViewBag.NotifyMessage = "";
            ReportID = ID;
            this.ReportType = ReportType;
            return View();
        }

        Qiyas.WebAdmin.Common.Reports.WithdrawReport report = new Qiyas.WebAdmin.Common.Reports.WithdrawReport();

        public ActionResult WithdrawReportDocumentViewerPartial()
        {
            report.LoadData(ReportID, ReportType);
            return PartialView("_WithdrawReportDocumentViewerPartial", report);
        }

        public ActionResult WithdrawReportDocumentViewerPartialExport()
        {
            report.LoadData(ReportID, ReportType);
            return DocumentViewerExtension.ExportTo(report, Request);
        }

        [ValidateInput(false)]
        public ActionResult ContainerRequestGridViewPartial()
        {
            var model = new BusinessLogicLayer.Components.PPM.ContainerRequestPackLogic().GetAllByRequestID(MainID);
            return PartialView("_ContainerRequestGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ContainerRequestGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Qiyas.BusinessLogicLayer.Entity.PPM.ContainerRequestPack item)
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
            return PartialView("_ContainerRequestGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ContainerRequestGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Qiyas.BusinessLogicLayer.Entity.PPM.ContainerRequestPack item)
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
            return PartialView("_ContainerRequestGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ContainerRequestGridViewPartialDelete(System.Int32 ContainerRequestPackID)
        {
            var model = new object[0];
            if (ContainerRequestPackID >= 0)
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
            return PartialView("_ContainerRequestGridViewPartial", model);
        }

        [ValidateInput(false)]
        public ActionResult ViewWithdrawalReportGridViewPartial()
        {
            var model = new BusinessLogicLayer.Components.PPM.RequestWithdrawDetailLogic().ViewWithdrawalReportByExamCenterRequiredExamsID(MainID);
            return PartialView("_ViewWithdrawalReportGridViewPartial", model);
        }

        [HttpPost]
        public ActionResult CheckItem(string item)
        {
            var itemPack = new BusinessLogicLayer.Entity.PPM.BookPackItem(item);
            
            if (itemPack.HasObject)
            {
                BusinessLogicLayer.Entity.PPM.ContainerRequest request = new BusinessLogicLayer.Entity.PPM.ContainerRequest(MainID, true);
                if (!request.HasObject)
                { request = new BusinessLogicLayer.Entity.PPM.ContainerRequest(); request.CreatedDate = DateTime.Now; request.ExamCenterRequiredExamsID = MainID; }

                request.ModifiedDate = DateTime.Now;
                request.Save();
                BusinessLogicLayer.Entity.PPM.ContainerRequestPack pack = new BusinessLogicLayer.Entity.PPM.ContainerRequestPack();
                pack.BookPackItemID = itemPack.BookPackItemID;
                pack.ContainerRequestID = request.ContainerRequestID;
                pack.CreatedDate = DateTime.Now;

                pack.Save();
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

        //
        [HttpPost]
        public ActionResult SaveContainerRequest()
        {
            BusinessLogicLayer.Entity.PPM.ExamCenterRequiredExam ecr = new BusinessLogicLayer.Entity.PPM.ExamCenterRequiredExam(MainID);
            ecr.RequestPreparationStatusID = 3;
            ecr.Save();

            //BusinessLogicLayer.Entity.PPM.ExamCenterRequiredExam item = new BusinessLogicLayer.Entity.PPM.ExamCenterRequiredExam(MainID);
            return RedirectToAction("Index");
        }
    }
}