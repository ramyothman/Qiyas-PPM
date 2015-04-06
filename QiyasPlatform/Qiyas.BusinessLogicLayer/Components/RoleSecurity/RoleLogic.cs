using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
namespace Qiyas.BusinessLogicLayer.Components.RoleSecurity
{
    public class RoleLogic : QueryBase
    {


        public List<Qiyas.BusinessLogicLayer.Entity.RoleSecurity.Role> GetAll()
        {
            try
            {
                return db.Roles.OrderBy(c => c.Name).Select(c => new Qiyas.BusinessLogicLayer.Entity.RoleSecurity.Role(c) { context = db }).ToList();
            }
            catch (Exception ex)
            {
                lastException = ex;
                return null;
            }
        }

    }
}
