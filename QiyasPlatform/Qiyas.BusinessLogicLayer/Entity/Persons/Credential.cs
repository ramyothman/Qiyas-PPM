using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Qiyas.BusinessLogicLayer.Entity;

namespace Qiyas.BusinessLogicLayer.Entity.Persons
{
    [DataObject(true)]
    [Serializable]
    public class Credential : EntityBase<Qiyas.DataAccessLayer.Credential>
    {
        #region Constructors
        public Credential()
        {
            if(this.entity == null)
                this.entity = new Qiyas.DataAccessLayer.Credential();
            isNew = true;
        }

        public Credential(int businessEntityId)
        {
            this.entity = context.Credentials.Where(p => p.BusinessEntityId == businessEntityId).FirstOrDefault();
        }

        internal Credential(Qiyas.DataAccessLayer.Credential entity)
        {
            this.entity = entity;
        }
        #endregion

        #region Properties

        /// <summary>
        /// This Property represents the BusinessEntityId which has int type
        /// </summary>

        public int BusinessEntityId
        {

            set
            {
                this.entity.BusinessEntityId = value;
            }
            get
            {
                return this.entity.BusinessEntityId;
            }
        }



        /// <summary>
        /// This Property represents the Username which has string type
        /// </summary>

        public string Username
        {

            set
            {
                if (this.entity == null)
                    this.entity = new DataAccessLayer.Credential();
                this.entity.Username = value;
            }
            get
            {
                return this.entity.Username;
            }
        }



        /// <summary>
        /// This Property represents the PasswordHash which has string type
        /// </summary>

        public string PasswordHash
        {

            set
            {
                this.entity.PasswordHash = value;
            }
            get
            {
                return this.entity.PasswordHash;
            }
        }



        /// <summary>
        /// This Property represents the PasswordSalt which has string type
        /// </summary>

        public string PasswordSalt
        {

            set
            {
                this.entity.PasswordSalt = value;
            }
            get
            {
                return this.entity.PasswordSalt;
            }
        }



        /// <summary>
        /// This Property represents the ActivationCode which has string type
        /// </summary>

        public string ActivationCode
        {

            set
            {
                this.entity.ActivationCode = value;
            }
            get
            {
                return this.entity.ActivationCode;
            }
        }



        /// <summary>
        /// This Property represents the IsActivated which has bool type
        /// </summary>

        public bool? IsActivated
        {

            set
            {
                this.entity.IsActivated = value;
            }
            get
            {
                return this.entity.IsActivated;
            }
        }



        /// <summary>
        /// This Property represents the IsActive which has bool type
        /// </summary>

        public bool IsActive
        {

            set
            {
                if (this.entity == null)
                    this.entity = new DataAccessLayer.Credential();
                this.entity.IsActive = value;
            }
            get
            {
                if (this.entity == null)
                    this.entity = new DataAccessLayer.Credential();
                return this.entity.IsActive;
            }
        }



        /// <summary>
        /// This Property represents the RowGuid which has Guid type
        /// </summary>

        public Guid RowGuid
        {

            set
            {
                this.entity.RowGuid = value;
            }
            get
            {
                return this.entity.RowGuid;
            }
        }



        /// <summary>
        /// This Property represents the ModifiedDate which has DateTime type
        /// </summary>

        public DateTime ModifiedDate
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

        public string Email
        {
            set { this.entity.Email = value; ; }
            get { return this.entity.Email; }
        }

        public string EmailConfirmationCode
        {
            set { this.entity.EmailConfirmationCode = value; ; }
            get { return this.entity.EmailConfirmationCode; }
        }

        public bool? EmailConfirmed
        {
            set { this.entity.EmailConfirmed = value; ; }
            get { return this.entity.EmailConfirmed; }
        }

        public bool? PhoneNumberConfirmed
        {
            set { this.entity.PhoneNumberConfirmed = value; ; }
            get { return this.entity.PhoneNumberConfirmed; }
        }

        

        public string PhoneNumber
        {
            set { this.entity.PhoneNumber = value; ; }
            get { return this.entity.PhoneNumber; }
        }

        public string PhoneNumberConfirmationCode
        {
            set { this.entity.PhoneNumberConfirmationCode = value; ; }
            get { return this.entity.PhoneNumberConfirmationCode; }
        }


        public bool? LockoutEnabled
        {
            set { this.entity.LockoutEnabled = value; ; }
            get { return this.entity.LockoutEnabled; }
        }

        public bool? TwoFactorEnabled
        {
            set { this.entity.TwoFactorEnabled = value; ; }
            get { return this.entity.TwoFactorEnabled; }
        }

        public DateTime? LockoutEndDateUtc
        {
            set { this.entity.LockoutEndDateUtc = value; ; }
            get { return this.entity.LockoutEndDateUtc; }
        }

        public int? AccessFailedCount
        {
            set { this.entity.AccessFailedCount = value; ; }
            get { return this.entity.AccessFailedCount; }
        }

        public int? CreatedBy
        {
            set { this.entity.CreatedBy = value; ; }
            get { return this.entity.CreatedBy; }
        }

        public int? ModifiedBy
        {
            set { this.entity.ModifiedBy = value; ; }
            get { return this.entity.ModifiedBy; }
        }


        public bool IsNew
        {
            set { isNew = value; }
            get { return isNew; }
        }

        #endregion

        #region Methods
        internal override bool? Save(Qiyas.DataAccessLayer.QiyasLinqDataContext context, bool commit)
        {
            if (isNew)
            {
                RowGuid = Guid.NewGuid();
                ModifiedDate = DateTime.Now;
                CreatedDate = DateTime.Now;
                context.Credentials.InsertOnSubmit(this.entity);
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
                context.Credentials.DeleteOnSubmit(this.entity);
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
