
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
    public partial class CityLogic
    {

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Qiyas.BusinessLogicLayer.Entity.Persons.City> GetAllByStateProvinceID(int StateProvinceID)
        {
            return db.Cities.Where(c => c.StateRegionID == StateProvinceID).OrderBy(c => c.Name).Select(c => new Qiyas.BusinessLogicLayer.Entity.Persons.City(c) { context = db }).ToList();
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Qiyas.BusinessLogicLayer.Entity.Persons.City> GetAllActiveByStateProvinceID(int StateProvinceID)
        {
            return db.Cities.Where(c => c.StateRegionID == StateProvinceID && c.IsActive == true).OrderBy(c => c.Name).Select(c => new Qiyas.BusinessLogicLayer.Entity.Persons.City(c) { context = db }).ToList();
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Qiyas.BusinessLogicLayer.Entity.Persons.City> GetAllActive()
        {
            return db.Cities.Where(c => c.IsActive == true).Select(c => new Qiyas.BusinessLogicLayer.Entity.Persons.City(c) { context = db }).ToList();
        }

        public BusinessLogicLayer.Entity.Persons.City GetByName(string state)
        {
            try
            {
                Entity.Persons.City city = null;
                var item = (from x in db.Cities where x.Name == state select x).FirstOrDefault();
                if (item != null)
                {
                    city = new Entity.Persons.City(item);
                }
                return city;
            }
            catch (Exception ex)
            {
                lastException = ex;
                return null;
            }
        }

        public bool HasCities(int CityID)
        {
            return false;
        }
    }
}
      