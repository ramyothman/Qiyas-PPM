
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
            return withdraw;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Qiyas.BusinessLogicLayer.Entity.PPM.RequestWithdrawDetail> ViewWithdrawalReportByExamCenterRequiredExamsID(int ID)
        {
            return db.ViewWithdrawalReports.Where(c => c.ExamCenterRequiredExamsID == ID).Select(c => new Qiyas.BusinessLogicLayer.Entity.PPM.RequestWithdrawDetail() { context = db, ExamCenterID = c.ExamCenterID.Value, ExamCenterName = c.CenterName, ExamCenterRequiredExamsID = c.ExamCenterRequiredExamsID.Value, ExamModel = c.ExamModel, ExamPeriodID = c.ExamPeriodID, ExamPeriodName = c.PeriodName, ExamRequirementItemID = c.ExamRequirementItemID, PackageName = c.PackageName, PackagingTypeID = c.PackagingTypeID.Value, PackCount = c.PackCount.Value, PrintsForOneModel = c.PrintsForOneModel, RequestPreparationStatusID = c.RequestPreparationStatusID, RequestWithdrawDetailID = c.RequestWithdrawDetailID, RequestWithdrawID = c.RequestWithdrawID, ExamCode = c.ExamCode, ExamID = c.ExamID }).ToList();
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
      