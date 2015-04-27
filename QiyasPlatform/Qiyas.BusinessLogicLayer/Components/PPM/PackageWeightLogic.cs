
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
    public partial class PackageWeightLogic
    {
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Qiyas.BusinessLogicLayer.Entity.PPM.PackageWeight> GetAllView()
        {
            var items = (from x in db.PackageWeightViews select new Qiyas.BusinessLogicLayer.Entity.PPM.PackageWeight { CreatedDate = x.CreatedDate, CreatorID = x.CreatorID, ModifiedByID = x.ModifiedByID, ModifiedDate = x.ModifiedDate, Name = x.Name, PackageCode = x.PackageCode, PackageWeightID = x.PackageWeightID, PackCode = x.PackCode, Serial = x.PackSerial.Value, Weight = x.Weight, context = this.db }).ToList();
            return items;   
        }

        public void CheckPackWeightCompleted(int PrintID)
        {
            db.CheckPackWeightComplete(PrintID);
        }
    }
}
      