
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
    public partial class ShippingBagLogic
    {
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Qiyas.BusinessLogicLayer.Entity.PPM.ShippingBag> GetAllByExamCenterRequiredExamsID(int ID)
        {
            return db.ShippingBags.Where(c=> c.ExamCenterRequiredExamsID == ID).Select(c => new Qiyas.BusinessLogicLayer.Entity.PPM.ShippingBag(c) { context = db }).ToList();
        }
    }
}
      