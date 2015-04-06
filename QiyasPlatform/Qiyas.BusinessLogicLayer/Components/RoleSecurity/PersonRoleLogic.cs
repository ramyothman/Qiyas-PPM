using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
namespace Qiyas.BusinessLogicLayer.Components.RoleSecurity
{
    public class PersonRoleLogic : QueryBase
    {

        public List<Qiyas.BusinessLogicLayer.Entity.RoleSecurity.PersonRole> GetByUserId(int userId)
        {
            var items = db.PersonRoles.Where(x => x.PersonId == userId).Select(c => new Qiyas.BusinessLogicLayer.Entity.RoleSecurity.PersonRole(c) { context = db }).ToList();
            return items;
        }

        public List<string> GetByUserIdStringList(int userId)
        {
            List<string> roles = new List<string>();
            var proles = from t1 in db.PersonRoles join t2 in db.Roles on t1.RoleId equals t2.RoleId where t1.PersonId == userId select t2;
            foreach(var role in proles)
            {
                roles.Add(role.Name);
            }
            return roles;
        }

        public List<Qiyas.BusinessLogicLayer.Entity.RoleSecurity.PersonRole> GetAll()
        {
            try
            {
                return db.PersonRoles.OrderBy(c => c.RoleId).Select(c => new Qiyas.BusinessLogicLayer.Entity.RoleSecurity.PersonRole(c) { context = db }).ToList();
            }
            catch (Exception ex)
            {
                lastException = ex;
                return null;
            }
        }

        public void DeleteByUserId(int userID)
        {
            var proles = (from x in db.PersonRoles where x.PersonId == userID select x);

            foreach (DataAccessLayer.PersonRole prole in proles)
            {
                db.PersonRoles.DeleteOnSubmit(prole);
            }
            db.SubmitChanges();
        }

    }
}
