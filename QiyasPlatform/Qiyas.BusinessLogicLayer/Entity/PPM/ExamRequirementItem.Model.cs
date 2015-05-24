
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
    public partial class ExamRequirementItem : EntityBase<Qiyas.DataAccessLayer.ExamRequirementItem>
    {

        #region Constructors
        public ExamRequirementItem()
        {
            this.entity = new Qiyas.DataAccessLayer.ExamRequirementItem();
            isNew = true;
        }

        public ExamRequirementItem(int ExamRequirementItemID)
        {
            this.entity = context.ExamRequirementItems.Where(p => p.ExamRequirementItemID == ExamRequirementItemID).FirstOrDefault();  
        }
    
        internal ExamRequirementItem(Qiyas.DataAccessLayer.ExamRequirementItem entity)
        {
            this.entity = entity;
        }

        #endregion

        #region Properties
    
        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "ExamRequirementItemID")]
        public int ExamRequirementItemID
        {            
            set{ this.entity.ExamRequirementItemID = value; }
            get{ return this.entity.ExamRequirementItemID; }
        }
    
        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "ExamCenterRequiredExamsID")]
        public int? ExamCenterRequiredExamsID
        {            
            set{ this.entity.ExamCenterRequiredExamsID = value; }
            get{ return this.entity.ExamCenterRequiredExamsID; }
        }
    
        [Display(Name = "ExamID")]
        public int? ExamID
        {            
            set{ this.entity.ExamID = value; }
            get{ return this.entity.ExamID; }
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

        [Required(ErrorMessage = "RequiredValidation")]
        [Display(Name = "RequestPreparationStatusID")]
        public int? RequestPreparationStatusID
        {
            set { this.entity.RequestPreparationStatusID = value; }
            get { return this.entity.RequestPreparationStatusID; }
        }
        #endregion    
    
        #region Methods
        internal override bool? Save(Qiyas.DataAccessLayer.QiyasLinqDataContext context, bool commit)
        {
            if (isNew)
            {
              context.ExamRequirementItems.InsertOnSubmit(this.entity);
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
              context.ExamRequirementItems.DeleteOnSubmit(this.entity);
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
      