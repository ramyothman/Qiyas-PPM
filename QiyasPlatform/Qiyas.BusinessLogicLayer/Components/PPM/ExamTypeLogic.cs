
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
    public partial class ExamTypeLogic
    {
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Qiyas.BusinessLogicLayer.Entity.PPM.ExamType> GetAllActive()
        {
            return db.ExamTypes.Where(c => c.IsActive == true).Select(c => new Qiyas.BusinessLogicLayer.Entity.PPM.ExamType(c) { context = db }).ToList();
        }

        public Qiyas.BusinessLogicLayer.Entity.PPM.ExamType GetByName(string name)
        {
            Qiyas.BusinessLogicLayer.Entity.PPM.ExamType examType = null;
            var ex = db.ExamTypes.Where(c => c.Name == name).FirstOrDefault();
            if (ex != null)
                examType = new Entity.PPM.ExamType(ex);
            return examType;
        }

        public bool HasDependencies(int ExaminationTypeID)
        {
            return false;
        }
    }
}
      