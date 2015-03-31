using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Qiyas.WebAdmin.Common.Security
{
    public class RoleStore<TRole> : IQueryableRoleStore<TRole, int>
       where TRole : BusinessLogicLayer.Entity.RoleSecurity.Role
    {

        #region Properties
        BusinessLogicLayer.Components.RoleSecurity.RoleLogic roleLogic = new BusinessLogicLayer.Components.RoleSecurity.RoleLogic();
        #endregion

        #region Queryable Role Store
        public IQueryable<TRole> Roles
        {
            get { return roleLogic.GetAll().AsQueryable<BusinessLogicLayer.Entity.RoleSecurity.Role>() as IQueryable<TRole>; }
        }

        public System.Threading.Tasks.Task CreateAsync(TRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            BusinessLogicLayer.Entity.RoleSecurity.Role roleEntity = new BusinessLogicLayer.Entity.RoleSecurity.Role();
            roleEntity.Name = role.Name;
            roleEntity.IsActive = true;
            roleEntity.Save();

            return Task.FromResult<object>(null);
        }

        public System.Threading.Tasks.Task DeleteAsync(TRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("user");
            }
            BusinessLogicLayer.Entity.RoleSecurity.Role roleEntity = new BusinessLogicLayer.Entity.RoleSecurity.Role(role.RoleId);
            roleEntity.Delete();

            return Task.FromResult<Object>(null);
        }

        public System.Threading.Tasks.Task<TRole> FindByIdAsync(int roleId)
        {
            BusinessLogicLayer.Entity.RoleSecurity.Role roleEntity = new BusinessLogicLayer.Entity.RoleSecurity.Role(roleId);
            if (roleEntity == null)
                return null;
            return Task.FromResult<TRole>(roleEntity as TRole);
        }

        public System.Threading.Tasks.Task<TRole> FindByNameAsync(string roleName)
        {
            BusinessLogicLayer.Entity.RoleSecurity.Role roleEntity = new BusinessLogicLayer.Entity.RoleSecurity.Role(roleName);
            if (roleEntity == null)
                return null;
            return Task.FromResult<TRole>(roleEntity as TRole);
        }

        public System.Threading.Tasks.Task UpdateAsync(TRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("user");
            }

            role.Save();

            return Task.FromResult<Object>(null);
        }

        public void Dispose()
        {
            roleLogic = null;
        }
        #endregion
    }
}