
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
    public partial class RequestWithdrawDetailItemModel : EntityBase<Qiyas.DataAccessLayer.RequestWithdrawDetailItemModel>
    {

        #region Constructors
        public RequestWithdrawDetailItemModel()
        {
            this.entity = new Qiyas.DataAccessLayer.RequestWithdrawDetailItemModel();
            isNew = true;
        }

        public RequestWithdrawDetailItemModel(int RequestWithdrawDetailItemModelID)
        {
            this.entity = context.RequestWithdrawDetailItemModels.Where(p => p.RequestWithdrawDetailItemModelID == RequestWithdrawDetailItemModelID).FirstOrDefault();  
        }
    
        internal RequestWithdrawDetailItemModel(Qiyas.DataAccessLayer.RequestWithdrawDetailItemModel entity)
        {
            this.entity = entity;
        }

        #endregion

        #region Properties
    
        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "RequestWithdrawDetailItemModelID")]
        public int RequestWithdrawDetailItemModelID
        {            
            set{ this.entity.RequestWithdrawDetailItemModelID = value; }
            get{ return this.entity.RequestWithdrawDetailItemModelID; }
        }
    
        [Display(Name = "RequestWithdrawDetailItemID")]
        public int? RequestWithdrawDetailItemID
        {            
            set{ this.entity.RequestWithdrawDetailItemID = value; }
            get{ return this.entity.RequestWithdrawDetailItemID; }
        }
    
        [Display(Name = "ExamModelID")]
        public int? ExamModelID
        {            
            set{ this.entity.ExamModelID = value; }
            get{ return this.entity.ExamModelID; }
        }
    
        #endregion    
    
        #region Methods
        internal override bool? Save(Qiyas.DataAccessLayer.QiyasLinqDataContext context, bool commit)
        {
            if (isNew)
            {
              context.RequestWithdrawDetailItemModels.InsertOnSubmit(this.entity);
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
              context.RequestWithdrawDetailItemModels.DeleteOnSubmit(this.entity);
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
      