using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Qiyas.BusinessLogicLayer.Entity;

namespace Qiyas.BusinessLogicLayer.Entity.RoleSecurity
{
    [DataObject(true)]
    [Serializable]
    public class PersonRole : EntityBase<Qiyas.DataAccessLayer.PersonRole>
    {
        #region Constructors
        public PersonRole()
        {
            this.entity = new Qiyas.DataAccessLayer.PersonRole();
            isNew = true;
        }

        public PersonRole(int personRoleId)
        {
            this.entity = context.PersonRoles.Where(p => p.PersonRoleId == personRoleId).FirstOrDefault();
        }

        public PersonRole(int roleId, int personId)
        {
            this.entity = context.PersonRoles.Where(p => p.RoleId == roleId && p.PersonId == personId).FirstOrDefault();
        }

        internal PersonRole(Qiyas.DataAccessLayer.PersonRole entity)
        {
            this.entity = entity;
        }
        #endregion

        #region Properties

        public int PersonRoleId
        {

            set
            {
                this.entity.PersonRoleId = value;
            }
            get
            {
                return this.entity.PersonRoleId;
            }
        }

        public int? RoleId
        {

            set
            {
                this.entity.RoleId = value;
            }
            get
            {
                return this.entity.RoleId;
            }
        }

        public int? PersonId
        {

            set
            {
                this.entity.PersonId = value;
            }
            get
            {
                return this.entity.PersonId;
            }
        }


        /// <summary>
        /// This Property represents the ModifiedDate which has DateTime type
        /// </summary>

        public DateTime? ModifiedDate
        {

            set
            {
                this.entity.ModifiedDate = value;
            }
            get
            {
                return this.entity.ModifiedDate;
            }
        }



        #endregion

        #region Methods
        internal override bool? Save(Qiyas.DataAccessLayer.QiyasLinqDataContext context, bool commit)
        {
            if (isNew)
            {
                ModifiedDate = DateTime.Now;
                context.PersonRoles.InsertOnSubmit(this.entity);
            }
            else
            {
                ModifiedDate = DateTime.Now;
            }
            if (commit)
                try
                {
                    context.SubmitChanges();
                    isNew = false;
                    return true;
                }
                catch (Exception ex)
                {
                    lastException = ex;
                    return false;
                }
            else
                return null;

        }
        internal override bool? Delete(Qiyas.DataAccessLayer.QiyasLinqDataContext context, bool commit)
        {
            if (!isNew)
                context.PersonRoles.DeleteOnSubmit(this.entity);
            else
                return false;
            if (commit)
                try
                {
                    context.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    lastException = ex;
                    return false;
                }
            else
                return null;
        }
        public bool Save()
        {
            return Save(context, true).Value;
        }
        public bool Delete()
        {
            return Delete(context, true).Value;
        }
        #endregion

    }
}
