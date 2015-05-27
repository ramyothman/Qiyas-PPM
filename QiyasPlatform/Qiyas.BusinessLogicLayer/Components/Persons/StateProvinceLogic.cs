
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
    public partial class StateProvinceLogic
    {
        [DataObjectMethod(DataObjectMethodType.Select)]
        public bool HasCities(int StateRegionID)
        {
            return (from c in db.Cities where c.StateRegionID == StateRegionID select c).Count() > 0;
        }

        public BusinessLogicLayer.Entity.Persons.StateProvince GetByName(string state)
        {
            try
            {
                Entity.Persons.StateProvince stateProvince = null;
                var item = (from x in db.StateProvinces where x.Name == state select x).FirstOrDefault();
                if (item != null)
                {
                    stateProvince = new Entity.Persons.StateProvince(item);
                }
                return stateProvince;
            }
            catch (Exception ex)
            {
                lastException = ex;
                return null;
            }
        }

        public bool HasDependencies(int ExaminationCenterID)
        {
            return false;
        }
    }
}
      