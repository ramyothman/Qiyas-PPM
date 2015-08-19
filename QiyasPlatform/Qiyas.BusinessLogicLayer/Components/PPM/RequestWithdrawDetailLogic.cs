
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.ComponentModel;

namespace Qiyas.BusinessLogicLayer.Components.PPM
{
    public partial class RequestWithdrawDetailLogic
    {
        #region Methods
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Qiyas.BusinessLogicLayer.Entity.PPM.RequestWithdrawDetail> ViewWithdrawalReportByRequestWithdrawID(int ID)
        {
            var withdraw = db.ViewWithdrawalReports.Where(c=> c.RequestWithdrawID == ID).Select(c => new Qiyas.BusinessLogicLayer.Entity.PPM.RequestWithdrawDetail() { context = db, ExamCenterID = c.ExamCenterID.Value, ExamCenterName = c.CenterName, ExamCenterRequiredExamsID = c.ExamCenterRequiredExamsID.Value, ExamModel = c.ExamModel, ExamPeriodID = c.ExamPeriodID, ExamPeriodName = c.PeriodName, ExamRequirementItemID = c.ExamRequirementItemID, PackageName = c.PackageName, PackagingTypeID = c.PackagingTypeID.Value, PackCount = c.PackCount.Value, PrintsForOneModel = c.PrintsForOneModel, RequestPreparationStatusID = c.RequestPreparationStatusID, RequestWithdrawDetailID = c.RequestWithdrawDetailID, RequestWithdrawID = c.RequestWithdrawID , ExamCode = c.ExamCode, ExamID = c.ExamID  }).ToList();
            var packagingTypes = (from x in db.PackagingTypes where x.Name == "A3" select x).FirstOrDefault();
            var a3 = (from x in withdraw where x.PackagingTypeID == packagingTypes.PackagingTypeID select x);
            List<Qiyas.BusinessLogicLayer.Entity.PPM.RequestWithdrawDetail> temp = new List<Entity.PPM.RequestWithdrawDetail>();
            foreach(var item in a3)
            {
                item.ExamModel = "";
                var cd = (from x in db.ExamRequirementItems where x.ExamID == item.ExamID && x.ExamCenterRequiredExamsID == item.ExamCenterRequiredExamsID select x).FirstOrDefault();
                if (cd != null)
                {
                    Qiyas.BusinessLogicLayer.Entity.PPM.RequestWithdrawDetail ncd = new Entity.PPM.RequestWithdrawDetail();
                    ncd.ExamCenterID = item.ExamCenterID;
                    ncd.ExamCenterName = item.ExamCenterName;
                    ncd.ExamCenterRequiredExamsID = item.ExamCenterRequiredExamsID;
                    ncd.ExamCode = item.ExamCode;
                    ncd.ExamID = item.ExamID;
                    ncd.ExamModel = "";
                    ncd.ExamPeriodID = item.ExamPeriodID;
                    ncd.ExamPeriodName = item.ExamPeriodName;
                    ncd.ExamRequirementItemID = item.ExamRequirementItemID;
                    ncd.ExamsNeededForA3 = cd.ExamsNeededForA3;
                    ncd.ExamsNeededForA4 = cd.ExamsNeededForA4;
                    ncd.ExamsNeededForCD = cd.ExamsNeededForCD;
                    ncd.PackageName = "CD";
                    ncd.PackagingTypeID = item.PackagingTypeID;
                    ncd.PackCount = cd.ExamsNeededForCD.Value;
                    ncd.PrintsForOneModel = cd.ExamsNeededForCD;
                    ncd.RequestPreparationStatusID = item.RequestPreparationStatusID;
                    ncd.RequestWithdrawDetailID = item.RequestWithdrawDetailID;
                    ncd.RequestWithdrawID = item.RequestWithdrawID;
                    temp.Add(ncd);


                }
            }
            foreach (var item in temp)
            {
                withdraw.Add(item);
            }
            return withdraw;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Qiyas.BusinessLogicLayer.Entity.PPM.RequestWithdrawDetail> ViewWithdrawalReportByExamCenterRequiredExamsID(int ID)
        {
            var withdraw = db.ViewWithdrawalReports.Where(c => c.ExamCenterRequiredExamsID == ID).Select(c => new Qiyas.BusinessLogicLayer.Entity.PPM.RequestWithdrawDetail() { context = db, ExamCenterID = c.ExamCenterID.Value, ExamCenterName = c.CenterName, ExamCenterRequiredExamsID = c.ExamCenterRequiredExamsID.Value, ExamModel = c.ExamModel, ExamPeriodID = c.ExamPeriodID, ExamPeriodName = c.PeriodName, ExamRequirementItemID = c.ExamRequirementItemID, PackageName = c.PackageName, PackagingTypeID = c.PackagingTypeID.Value, PackCount = c.PackCount.Value, PrintsForOneModel = c.PrintsForOneModel, RequestPreparationStatusID = c.RequestPreparationStatusID, RequestWithdrawDetailID = c.RequestWithdrawDetailID, RequestWithdrawID = c.RequestWithdrawID, ExamCode = c.ExamCode, ExamID = c.ExamID }).ToList();
            var packagingTypes = (from x in db.PackagingTypes where x.Name == "A3" select x).FirstOrDefault();
            var a3 = (from x in withdraw where x.PackagingTypeID == packagingTypes.PackagingTypeID select x);
            List<Qiyas.BusinessLogicLayer.Entity.PPM.RequestWithdrawDetail> temp = new List<Entity.PPM.RequestWithdrawDetail>();
            foreach(var item in a3)
            {
                item.ExamModel = "";
                var cd = (from x in db.ExamRequirementItems where x.ExamID == item.ExamID && x.ExamCenterRequiredExamsID == item.ExamCenterRequiredExamsID select x).FirstOrDefault();
                if(cd != null)
                {
                    Qiyas.BusinessLogicLayer.Entity.PPM.RequestWithdrawDetail ncd = new Entity.PPM.RequestWithdrawDetail();
                    ncd.ExamCenterID = item.ExamCenterID;
                    ncd.ExamCenterName = item.ExamCenterName;
                    ncd.ExamCenterRequiredExamsID = item.ExamCenterRequiredExamsID;
                    ncd.ExamCode = item.ExamCode;
                    ncd.ExamID = item.ExamID;
                    ncd.ExamModel = "";
                    ncd.ExamPeriodID = item.ExamPeriodID;
                    ncd.ExamPeriodName = item.ExamPeriodName;
                    ncd.ExamRequirementItemID = item.ExamRequirementItemID;
                    ncd.ExamsNeededForA3 = cd.ExamsNeededForA3;
                    ncd.ExamsNeededForA4 = cd.ExamsNeededForA4;
                    ncd.ExamsNeededForCD = cd.ExamsNeededForCD;
                    ncd.PackageName = "CD";
                    ncd.PackagingTypeID = item.PackagingTypeID;
                    ncd.PackCount = cd.ExamsNeededForCD.Value;
                    ncd.PrintsForOneModel = cd.ExamsNeededForCD;
                    ncd.RequestPreparationStatusID = item.RequestPreparationStatusID;
                    ncd.RequestWithdrawDetailID = item.RequestWithdrawDetailID;
                    ncd.RequestWithdrawID = item.RequestWithdrawID;
                    temp.Add(ncd);


                }
            }
            foreach(var item in temp)
            {
                withdraw.Add(item);
            }
            
            return withdraw;
        }

        public bool HasDataByRequestWithdrawID(int ID)
        {
            return db.ViewWithdrawalReports.Where(c => c.RequestWithdrawID == ID).Count() > 0;
        }

        public bool HasDataByExamCenterRequiredExamsID(int ID)
        {
            return db.ViewWithdrawalReports.Where(c => c.ExamCenterRequiredExamsID == ID).Count() > 0;
        }
        #endregion
    }
}
      