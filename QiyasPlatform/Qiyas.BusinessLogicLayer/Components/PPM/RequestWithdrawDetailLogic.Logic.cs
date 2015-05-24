
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
    public partial class RequestWithdrawDetailLogic : QueryBase
    {
        #region Methods
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Qiyas.BusinessLogicLayer.Entity.PPM.RequestWithdrawDetail> GetAll()
        {
            return db.RequestWithdrawDetails.Select(c => new Qiyas.BusinessLogicLayer.Entity.PPM.RequestWithdrawDetail(c) { context = db }).ToList();
        }
        #endregion
    }
}
      