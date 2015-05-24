
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
    public partial class RequestWithdrawDetail : EntityBase<Qiyas.DataAccessLayer.RequestWithdrawDetail>
    {

        #region Constructors
        public RequestWithdrawDetail()
        {
            this.entity = new Qiyas.DataAccessLayer.RequestWithdrawDetail();
            isNew = true;
        }

        public RequestWithdrawDetail(int RequestWithdrawDetailID)
        {
            this.entity = context.RequestWithdrawDetails.Where(p => p.RequestWithdrawDetailID == RequestWithdrawDetailID).FirstOrDefault();  
        }
    
        internal RequestWithdrawDetail(Qiyas.DataAccessLayer.RequestWithdrawDetail entity)
        {
            this.entity = entity;
        }

        #endregion

        #region Properties
    
        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "RequestWithdrawDetailID")]
        public int RequestWithdrawDetailID
        {            
            set{ this.entity.RequestWithdrawDetailID = value; }
            get{ return this.entity.RequestWithdrawDetailID; }
        }
    
        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "RequestWithdrawID")]
        public int? RequestWithdrawID
        {            
            set{ this.entity.RequestWithdrawID = value; }
            get{ return this.entity.RequestWithdrawID; }
        }
    
        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "ExamRequirementItemID")]
        public int? ExamRequirementItemID
        {            
            set{ this.entity.ExamRequirementItemID = value; }
            get{ return this.entity.ExamRequirementItemID; }
        }
    
        [Display(Name = "PrintsForOneModel")]
        public int? PrintsForOneModel
        {            
            set{ this.entity.PrintsForOneModel = value; }
            get{ return this.entity.PrintsForOneModel; }
        }
    
        [Display(Name = "ExamsNeededForA3")]
        public int? ExamsNeededForA3
        {            
            set{ this.entity.ExamsNeededForA3 = value; }
            get{ return this.entity.ExamsNeededForA3; }
        }
    
        [Display(Name = "ExamsNeededForA4")]
        public int? ExamsNeededForA4
        {            
            set{ this.entity.ExamsNeededForA4 = value; }
            get{ return this.entity.ExamsNeededForA4; }
        }
    
        [Display(Name = "ExamsNeededForCD")]
        public int? ExamsNeededForCD
        {            
            set{ this.entity.ExamsNeededForCD = value; }
            get{ return this.entity.ExamsNeededForCD; }
        }
    
        [Display(Name = "ShippedPrintsForOneModel")]
        public int? ShippedPrintsForOneModel
        {            
            set{ this.entity.ShippedPrintsForOneModel = value; }
            get{ return this.entity.ShippedPrintsForOneModel; }
        }
    
        [Display(Name = "ShippedExamsNeededForA3")]
        public int? ShippedExamsNeededForA3
        {            
            set{ this.entity.ShippedExamsNeededForA3 = value; }
            get{ return this.entity.ShippedExamsNeededForA3; }
        }
    
        [Display(Name = "ShippedExamsNeededForA4")]
        public int? ShippedExamsNeededForA4
        {            
            set{ this.entity.ShippedExamsNeededForA4 = value; }
            get{ return this.entity.ShippedExamsNeededForA4; }
        }
    
        [Display(Name = "ShippedExamsNeededForCD")]
        public int? ShippedExamsNeededForCD
        {            
            set{ this.entity.ShippedExamsNeededForCD = value; }
            get{ return this.entity.ShippedExamsNeededForCD; }
        }
    
        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "RequestPreparationStatusID")]
        public int? RequestPreparationStatusID
        {            
            set{ this.entity.RequestPreparationStatusID = value; }
            get{ return this.entity.RequestPreparationStatusID; }
        }
    
        #endregion    
    
        #region Methods
        internal override bool? Save(Qiyas.DataAccessLayer.QiyasLinqDataContext context, bool commit)
        {
            if (isNew)
            {
              context.RequestWithdrawDetails.InsertOnSubmit(this.entity);
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
              context.RequestWithdrawDetails.DeleteOnSubmit(this.entity);
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
      