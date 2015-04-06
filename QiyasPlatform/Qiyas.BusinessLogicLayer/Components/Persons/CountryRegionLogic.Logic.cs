
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
    public partial class CountryRegionLogic : QueryBase
    {
        #region Methods
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Qiyas.BusinessLogicLayer.Entity.Persons.CountryRegion> GetAll()
        {
            return db.CountryRegions.Select(c => new Qiyas.BusinessLogicLayer.Entity.Persons.CountryRegion(c) { context = db }).ToList();
        }
        #endregion
    }
}
      