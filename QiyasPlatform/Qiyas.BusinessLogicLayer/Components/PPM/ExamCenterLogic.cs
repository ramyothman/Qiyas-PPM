
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
    public partial class ExamCenterLogic
    {
        public BusinessLogicLayer.Entity.PPM.ExamCenter GetByName(string state)
        {
            try
            {
                Entity.PPM.ExamCenter center = null;
                var item = (from x in db.ViewExamCenters where x.Name == state select x).FirstOrDefault();
                if (item != null)
                {
                    center = new Entity.PPM.ExamCenter(item);
                }
                return center;
            }
            catch (Exception ex)
            {
                lastException = ex;
                return null;
            }
        }
        
    }
}
      