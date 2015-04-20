
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
    public partial class ExamPeriodLogic
    {
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Qiyas.BusinessLogicLayer.Entity.PPM.ExamPeriod> GetAllActive()
        {
            return db.ExamPeriods.Where(c => c.IsActive == true).Select(c => new Qiyas.BusinessLogicLayer.Entity.PPM.ExamPeriod(c) { context = db }).ToList();
        }

        public Qiyas.BusinessLogicLayer.Entity.PPM.ExamPeriod GetByName(string name)
        {
            Qiyas.BusinessLogicLayer.Entity.PPM.ExamPeriod examType = null;
            var ex = db.ExamPeriods.Where(c => c.Name == name).FirstOrDefault();
            if (ex != null)
                examType = new Entity.PPM.ExamPeriod(ex);
            return examType;
        }

        public bool HasDependencies(int ExaminationTypeID)
        {
            return false;
        }
    }
}
      