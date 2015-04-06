
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.ComponentModel;

namespace Qiyas.BusinessLogicLayer.Components.Persons
{
    [DataObject(true)]
    public partial class StateProvinceLogic : QueryBase
    {
        #region Methods
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Qiyas.BusinessLogicLayer.Entity.Persons.StateProvince> GetAll()
        {
            return db.StateProvinces.Select(c => new Qiyas.BusinessLogicLayer.Entity.Persons.StateProvince(c) { context = db }).ToList();
        }
        #endregion
    }
}
      