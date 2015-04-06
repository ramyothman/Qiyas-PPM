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
    public class UserLoginProvider : EntityBase<Qiyas.DataAccessLayer.UserLoginProvider>
    {
        #region Constructors
        public UserLoginProvider()
        {
            this.entity = new Qiyas.DataAccessLayer.UserLoginProvider();
            isNew = true;
        }

        public UserLoginProvider(int UserLoginProviderId)
        {
            this.entity = context.UserLoginProviders.Where(p => p.UserLoginProviderId == UserLoginProviderId).FirstOrDefault();
        }

        internal UserLoginProvider(Qiyas.DataAccessLayer.UserLoginProvider entity)
        {
            this.entity = entity;
        }
        #endregion

        #region Properties

        /// <summary>
        /// This Property represents the BusinessEntityId which has int type
        /// </summary>

        public int UserLoginProviderId
        {

            set
            {
                this.entity.UserLoginProviderId = value;
            }
            get
            {
                return this.entity.UserLoginProviderId;
            }
        }

        public int? UserId
        {

            set
            {
                this.entity.UserId = value;
            }
            get
            {
                return this.entity.UserId;
            }
        }

        public string LoginProvider
        {
            set { this.entity.LoginProvider = value; }
            get { return this.entity.LoginProvider; }
        }

        public string ProviderKey
        {
            set { this.entity.ProviderKey = value; }
            get { return this.entity.ProviderKey; }
        }


        /// <summary>
        /// This Property represents the ModifiedDate which has DateTime type
        /// </summary>

        public DateTime? CreatedDate
        {

            set
            {
                this.entity.CreatedDate = value;
            }
            get
            {
                return this.entity.CreatedDate;
            }
        }



        #endregion

        #region Methods
        internal override bool? Save(Qiyas.DataAccessLayer.QiyasLinqDataContext context, bool commit)
        {
            if (isNew)
            {
                CreatedDate = DateTime.Now;
                context.UserLoginProviders.InsertOnSubmit(this.entity);
            }
            else
            {
                CreatedDate = DateTime.Now;
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
                context.UserLoginProviders.DeleteOnSubmit(this.entity);
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
