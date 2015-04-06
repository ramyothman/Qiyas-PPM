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
    public class BusinessEntity : EntityBase<Qiyas.DataAccessLayer.BusinessEntity>
    {
        #region Constructors
        public BusinessEntity()
        {
            this.entity = new Qiyas.DataAccessLayer.BusinessEntity();
            isNew = true;
        }

        public BusinessEntity(int businessEntityId)
        {
            this.entity = context.BusinessEntities.Where(p => p.BusinessEntityId == businessEntityId).FirstOrDefault();
        }

        internal BusinessEntity(Qiyas.DataAccessLayer.BusinessEntity entity)
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



        #endregion

        

        #region Methods
        internal override bool? Save(Qiyas.DataAccessLayer.QiyasLinqDataContext context, bool commit)
        {
            if (isNew)
            {
                ModifiedDate = DateTime.Now;
                context.BusinessEntities.InsertOnSubmit(this.entity);
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
                context.BusinessEntities.DeleteOnSubmit(this.entity);
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
