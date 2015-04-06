
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
    public partial class ExamModelLogic
    {

        public Entity.PPM.ExamModel GetByName(string name)
        {
            Qiyas.BusinessLogicLayer.Entity.PPM.ExamModel examModel = null;
            var ex = db.ExamModels.Where(c => c.Name == name).FirstOrDefault();
            if (ex != null)
                examModel = new Entity.PPM.ExamModel(ex);
            return examModel;
        }
        public bool HasDependencies(int ExaminationTypeID)
        {
            return false;
        }
    }
}
      