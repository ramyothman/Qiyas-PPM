
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
    public partial class ExamModelItemLogic
    {
        public static bool Contains(List<BusinessLogicLayer.Entity.PPM.ExamModelItem> models, int id)
        {
            var item = models.Where(x => x.ExamModelID == id).FirstOrDefault();
            return item == null ? false : true;
        }

        public void DeleteByExamID(int ExamID)
        {
            db.DeleteExamModelItemByExamID(ExamID);
        }
    }
}
      