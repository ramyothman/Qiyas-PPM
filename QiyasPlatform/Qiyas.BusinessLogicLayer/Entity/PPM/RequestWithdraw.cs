
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Qiyas.BusinessLogicLayer.Entity;
using System.ComponentModel.DataAnnotations;

namespace Qiyas.BusinessLogicLayer.Entity.PPM
{
    public partial class RequestWithdraw
    {
        public static RequestWithdraw GetByExamCenterRequiredExamsID(int ExamCenterRequiredExamsID)
        {
            Qiyas.DataAccessLayer.QiyasLinqDataContext context = new DataAccessLayer.QiyasLinqDataContext();
            var entity = context.RequestWithdraws.Where(p => p.ExamCenterRequiredExamsID == ExamCenterRequiredExamsID).FirstOrDefault();
            RequestWithdraw withdraw = new RequestWithdraw(entity);
            return withdraw;
        }
    }
}
      