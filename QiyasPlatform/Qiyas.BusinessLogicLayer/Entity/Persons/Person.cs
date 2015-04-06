using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Qiyas.BusinessLogicLayer.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Qiyas.BusinessLogicLayer.Entity.Persons
{
    [DataObject(true)]
    [Serializable]
    public class Person : EntityBase<Qiyas.DataAccessLayer.Person>, IUser
    {
        #region Constructors
        public Person()
        {
            this.entity = new Qiyas.DataAccessLayer.Person();
            isNew = true;
        }

        public Person(int businessEntityId)
        {
            this.entity = context.Persons.Where(p => p.BusinessEntityId == businessEntityId).FirstOrDefault();
        }

        internal Person(Qiyas.DataAccessLayer.Person entity)
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
        /// This Property represents the FirstName which has string type
        /// </summary>

        public string FirstName
        {

            set
            {
                this.entity.FirstName = value;
            }
            get
            {
                return this.entity.FirstName;
            }
        }



        /// <summary>
        /// This Property represents the MiddleName which has string type
        /// </summary>

        public string MiddleName
        {

            set
            {
                this.entity.MiddleName = value;
            }
            get
            {
                return this.entity.MiddleName;
            }
        }



        /// <summary>
        /// This Property represents the LastName which has string type
        /// </summary>

        public string LastName
        {

            set
            {
                this.entity.LastName = value;
            }
            get
            {
                return this.entity.LastName;
            }
        }



        /// <summary>
        /// This Property represents the DisplayName which has string type
        /// </summary>

        public string DisplayName
        {

            set
            {
                this.entity.DisplayName = value;
            }
            get
            {
                return this.entity.DisplayName;
            }
        }



        /// <summary>
        /// This Property represents the Title which has string type
        /// </summary>

        public string Title
        {

            set
            {
                this.entity.Title = value;
            }
            get
            {
                return this.entity.Title;
            }
        }



        /// <summary>
        /// This Property represents the NameStyle which has bool type
        /// </summary>

        public bool? NameStyle
        {

            set
            {
                this.entity.NameStyle = value;
            }
            get
            {
                return this.entity.NameStyle;
            }
        }



        /// <summary>
        /// This Property represents the EmailPromotion which has int type
        /// </summary>

        public int? EmailPromotion
        {

            set
            {
                this.entity.EmailPromotion = value;
            }
            get
            {
                return this.entity.EmailPromotion;
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



        /// <summary>
        /// This Property represents the CreatedDate which has DateTime type
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



        /// <summary>
        /// This Property represents the NationalityCode which has string type
        /// </summary>

        public string NationalityCode
        {

            set
            {
                this.entity.NationalityCode = value;
            }
            get
            {
                return this.entity.NationalityCode;
            }
        }



        /// <summary>
        /// This Property represents the Gender which has string type
        /// </summary>

        public char? Gender
        {

            set
            {
                this.entity.Gender = value;
            }
            get
            {
                return this.entity.Gender;
            }
        }



        /// <summary>
        /// This Property represents the DateofBirth which has DateTime type
        /// </summary>

        public DateTime? DateofBirth
        {

            set
            {
                this.entity.DateofBirth = value;
            }
            get
            {
                return this.entity.DateofBirth;
            }
        }



        /// <summary>
        /// This Property represents the PersonImage which has string type
        /// </summary>

        public string PersonImage
        {

            set
            {
                this.entity.PersonImage = value;
            }
            get
            {
                return this.entity.PersonImage;
            }
        }



        #endregion

        #region Child Relationships
        private Credential _CurrentCredential;
        public Credential CurrentCredential
        {
            set { _CurrentCredential = value; }
            get
            {
                if(_CurrentCredential == null)
                {
                    _CurrentCredential = new Credential(BusinessEntityId);
                    if (_CurrentCredential == null)
                        _CurrentCredential = new Credential();
                }
                return _CurrentCredential;
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
                CreatedDate = DateTime.Now;
                context.Persons.InsertOnSubmit(this.entity);
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
                context.Persons.DeleteOnSubmit(this.entity);
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

        #region IUser

        public bool IsActive
        {
            get
            {
                return CurrentCredential.IsActive;
            }
            set
            {
                if (CurrentCredential == null)
                    CurrentCredential = new Credential();
                CurrentCredential.IsActive = value;
            }
        }
        public string UserName
        {
            get
            {
                return CurrentCredential.Username;
            }
            set
            {
                if (CurrentCredential == null)
                    CurrentCredential = new Credential();
                CurrentCredential.Username = value;
            }
        }

        public string Email
        {
            get
            {
                return CurrentCredential.Email;
            }
            set
            {
                if (CurrentCredential == null)
                    CurrentCredential = new Credential();
                CurrentCredential.Email = value;
            }
        }
        
        [StringLength(100, ErrorMessage = "StringLengthValidation", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password
        {
            set 
            {
                if (CurrentCredential == null)
                    CurrentCredential = new Credential();
                CurrentCredential.PasswordHash = value;
            }
            get { return CurrentCredential.PasswordHash; }
        }

        public string SecurityStamp
        {
            get
            {
                return CurrentCredential.PasswordSalt;
            }
            set
            {
                if (CurrentCredential == null)
                    CurrentCredential = new Credential();
                CurrentCredential.PasswordSalt = value;
            }
        }
        #endregion


        public string Id
        {
            get { return BusinessEntityId.ToString(); }
        }

        public Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Person> manager)
        {
            return Task.FromResult(GenerateUserIdentity(manager));
        }

        public ClaimsIdentity GenerateUserIdentity(UserManager<Person> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity =  manager.CreateIdentity<Person, string>(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

    }
}
