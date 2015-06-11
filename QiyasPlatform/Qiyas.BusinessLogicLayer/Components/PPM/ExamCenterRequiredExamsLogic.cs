
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
    public partial class ExamCenterRequiredExamLogic
    {
        public bool RecordExists(int PeriodID, int CenterID, int StatusID)
        {
            var item = db.ExamCenterRequiredExams.Where(c => c.ExamPeriodID == PeriodID && c.ExamCenterID == CenterID && c.RequestPreparationStatusID == StatusID).FirstOrDefault();
            return item != null;
        }


    }
}
      