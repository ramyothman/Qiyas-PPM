
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
    [DataObject(true)]
    public partial class ExamPeriodLogic : QueryBase
    {
        #region Methods
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Qiyas.BusinessLogicLayer.Entity.PPM.ExamPeriod> GetAll()
        {
            return db.ExamPeriods.Select(c => new Qiyas.BusinessLogicLayer.Entity.PPM.ExamPeriod(c) { context = db }).ToList();
        }

        //[DataObjectMethod(DataObjectMethodType.Select)]
        //public List<Qiyas.BusinessLogicLayer.Entity.PPM.ExamPeriod> GetAllActive()
        //{
        //    return db.ExamPeriods.Where(c => c.IsActive == true).Select(c => new Qiyas.BusinessLogicLayer.Entity.PPM.ExamPeriod(c) { context = db }).ToList();
        //}
        #endregion
    }
}
      