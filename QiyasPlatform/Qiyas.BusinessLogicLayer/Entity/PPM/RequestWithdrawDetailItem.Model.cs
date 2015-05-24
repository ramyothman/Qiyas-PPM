
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Qiyas.BusinessLogicLayer.Entity;
using System.ComponentModel.DataAnnotations;

namespace Qiyas.BusinessLogicLayer.Entity.PPM
{
    [DataObject(true)]
    [Serializable]
    public partial class RequestWithdrawDetailItem : EntityBase<Qiyas.DataAccessLayer.RequestWithdrawDetailItem>
    {

        #region Constructors
        public RequestWithdrawDetailItem()
        {
            this.entity = new Qiyas.DataAccessLayer.RequestWithdrawDetailItem();
            isNew = true;
        }

        public RequestWithdrawDetailItem(int RequestWithdrawDetailItemID)
        {
            this.entity = context.RequestWithdrawDetailItems.Where(p => p.RequestWithdrawDetailItemID == RequestWithdrawDetailItemID).FirstOrDefault();  
        }
    
        internal RequestWithdrawDetailItem(Qiyas.DataAccessLayer.RequestWithdrawDetailItem entity)
        {
            this.entity = entity;
        }

        #endregion

        #region Properties
    
        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "RequestWithdrawDetailItemID")]
        public int RequestWithdrawDetailItemID
        {            
            set{ this.entity.RequestWithdrawDetailItemID = value; }
            get{ return this.entity.RequestWithdrawDetailItemID; }
        }
    
        [Display(Name = "RequestWithdrawDetailID")]
        public int? RequestWithdrawDetailID
        {            
            set{ this.entity.RequestWithdrawDetailID = value; }
            get{ return this.entity.RequestWithdrawDetailID; }
        }
    
        [Display(Name = "PackagingTypeID")]
        public int? PackagingTypeID
        {            
            set{ this.entity.PackagingTypeID = value; }
            get{ return this.entity.PackagingTypeID; }
        }
    
        [Display(Name = "PackCount")]
        public int? PackCount
        {            
            set{ this.entity.PackCount = value; }
            get{ return this.entity.PackCount; }
        }
    
        #endregion    
    
        #region Methods
        internal override bool? Save(Qiyas.DataAccessLayer.QiyasLinqDataContext context, bool commit)
        {
            if (isNew)
            {
              context.RequestWithdrawDetailItems.InsertOnSubmit(this.entity);
            }
            else
            {
                
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
              context.RequestWithdrawDetailItems.DeleteOnSubmit(this.entity);
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
      