
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
    public partial class ExamRequirementItemLogic
    {
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Qiyas.BusinessLogicLayer.Entity.PPM.ExamRequirementItem> GetAllByExamCenterRequiredExamsID(int ID)
        {
            return db.ExamRequirementItems.Where(c=> c.ExamCenterRequiredExamsID == ID).Select(c => new Qiyas.BusinessLogicLayer.Entity.PPM.ExamRequirementItem(c) { context = db }).ToList();
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Qiyas.BusinessLogicLayer.Entity.PPM.ExamRequirementItem> GetAllRemainingForWithdraw_ByExamCenterRequiredExamsID(int ID)
        {
            return db.RemainingExamRequirementItemForWithdraws.Where(c => c.ExamCenterRequiredExamsID == ID).Select(c => new Qiyas.BusinessLogicLayer.Entity.PPM.ExamRequirementItem() { context = db, ExamCenterRequiredExamsID = c.ExamCenterRequiredExamsID, ExamID = c.ExamID, ExamRequirementItemID = c.ExamRequirementItemID, ExamsNeededForA3 = c.ExamsNeededForA3, ExamsNeededForA4 = c.ExamsNeededForA4, ExamsNeededForCD = c.ExamsNeededForCD, PrintsForOneModel = c.PrintsForOneModel, RequestPreparationStatusID = c.RequestPreparationStatusID }).ToList();
        }

        public bool ExamExistsInRequirements(int ID,int ExamID)
        {
            var item = db.ExamRequirementItems.Where(c => c.ExamCenterRequiredExamsID == ID && c.ExamID == ExamID).FirstOrDefault();
            return item != null;
        }

        public bool ExamExistsInRequirements(int ID, int ExamID,int ItemID)
        {
            var item = db.ExamRequirementItems.Where(c => c.ExamCenterRequiredExamsID == ID && c.ExamID == ExamID).FirstOrDefault();
            if (item.ExamRequirementItemID == ItemID)
                return false;
            return item != null;
        }
    }
}
      