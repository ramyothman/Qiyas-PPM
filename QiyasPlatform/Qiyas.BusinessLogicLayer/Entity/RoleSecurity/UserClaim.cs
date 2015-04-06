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
    public class UserClaim : EntityBase<Qiyas.DataAccessLayer.UserClaim>
    {
        #region Constructors
        public UserClaim()
        {
            this.entity = new Qiyas.DataAccessLayer.UserClaim();
            isNew = true;
        }

        public UserClaim(int userClaimId)
        {
            this.entity = context.UserClaims.Where(p => p.UserClaimId == userClaimId).FirstOrDefault();
        }

        internal UserClaim(Qiyas.DataAccessLayer.UserClaim entity)
        {
            this.entity = entity;
        }
        #endregion

        #region Properties

        /// <summary>
        /// This Property represents the BusinessEntityId which has int type
        /// </summary>

        public int UserClaimId
        {

            set
            {
                this.entity.UserClaimId = value;
            }
            get
            {
                return this.entity.UserClaimId;
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

        public string ClaimValue
        {
            set { this.entity.ClaimValue = value; }
            get { return this.entity.ClaimValue; }
        }

        public string ClaimType
        {
            set { this.entity.ClaimType = value; }
            get { return this.entity.ClaimType; }
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
                context.UserClaims.InsertOnSubmit(this.entity);
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
                context.UserClaims.DeleteOnSubmit(this.entity);
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
