using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Qiyas.BusinessLogicLayer.Entity;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;

namespace Qiyas.BusinessLogicLayer.Entity.RoleSecurity
{
    [DataObject(true)]
    [Serializable]
    public class Role : EntityBase<Qiyas.DataAccessLayer.Role> , IRole<int>
    {
        #region Constructors
        public Role()
        {
            this.entity = new Qiyas.DataAccessLayer.Role();
            isNew = true;
        }

        public Role(int roleId)
        {
            this.entity = context.Roles.Where(p => p.RoleId == roleId).FirstOrDefault();
        }

        public Role(string roleName)
        {
            this.entity = context.Roles.Where(p => p.Name == roleName).FirstOrDefault();
        }

        internal Role(Qiyas.DataAccessLayer.Role entity)
        {
            this.entity = entity;
        }
        #endregion

        #region Properties

        /// <summary>
        /// This Property represents the BusinessEntityId which has int type
        /// </summary>

        public int RoleId
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


        [Required]
        [StringLength(30)]
        public string Name
        {
            set { this.entity.Name = value; }
            get { return this.entity.Name; }
        }

        public bool? IsActive
        {
            set { this.entity.IsActive = value; }
            get { return this.entity.IsActive; }
        }

        public Guid? RowGuid
        {
            set { this.entity.RowGuid = value; }
            get { return this.entity.RowGuid; }
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
                RowGuid = Guid.NewGuid();
                ModifiedDate = DateTime.Now;
                context.Roles.InsertOnSubmit(this.entity);
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
                context.Roles.DeleteOnSubmit(this.entity);
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


        public int Id
        {
            get { return RoleId; }
        }
    }
}
