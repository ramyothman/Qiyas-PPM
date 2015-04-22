
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
    public partial class ExamCenterRequiredExam : EntityBase<Qiyas.DataAccessLayer.ExamCenterRequiredExam>
    {

        #region Constructors
        public ExamCenterRequiredExam()
        {
            this.entity = new Qiyas.DataAccessLayer.ExamCenterRequiredExam();
            isNew = true;
        }

        public ExamCenterRequiredExam(int ExamCenterRequiredExamsID)
        {
            this.entity = context.ExamCenterRequiredExams.Where(p => p.ExamCenterRequiredExamsID == ExamCenterRequiredExamsID).FirstOrDefault();  
        }

        internal ExamCenterRequiredExam(Qiyas.DataAccessLayer.ExamCenterRequiredExam entity)
        {
            this.entity = entity;
        }

        #endregion

        #region Properties
    
        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "ExamCenterRequiredExamsID")]
        public int ExamCenterRequiredExamsID
        {            
            set{ this.entity.ExamCenterRequiredExamsID = value; }
            get{ return this.entity.ExamCenterRequiredExamsID; }
        }
    
        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "ExamPeriodID")]
        public int? ExamPeriodID
        {            
            set{ this.entity.ExamPeriodID = value; }
            get{ return this.entity.ExamPeriodID; }
        }
    
        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "ExamCenterID")]
        public int? ExamCenterID
        {            
            set{ this.entity.ExamCenterID = value; }
            get{ return this.entity.ExamCenterID; }
        }
    
        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "RequestPreparationStatusID")]
        public int? RequestPreparationStatusID
        {            
            set{ this.entity.RequestPreparationStatusID = value; }
            get{ return this.entity.RequestPreparationStatusID; }
        }
    
        [Display(Name = "FileNeedsPath")]
        public string FileNeedsPath
        {            
            set{ this.entity.FileNeedsPath = value; }
            get{ return this.entity.FileNeedsPath; }
        }
    
        [Display(Name = "CreatedDate")]
        public DateTime? CreatedDate
        {            
            set{ this.entity.CreatedDate = value; }
            get{ return this.entity.CreatedDate; }
        }
    
        [Display(Name = "ModifiedDate")]
        public DateTime? ModifiedDate
        {            
            set{ this.entity.ModifiedDate = value; }
            get{ return this.entity.ModifiedDate; }
        }
    
        [Display(Name = "CreatedBy")]
        public int? CreatedBy
        {            
            set{ this.entity.CreatedBy = value; }
            get{ return this.entity.CreatedBy; }
        }
    
        [Display(Name = "ModifiedBy")]
        public int? ModifiedBy
        {            
            set{ this.entity.ModifiedBy = value; }
            get{ return this.entity.ModifiedBy; }
        }
    
        #endregion    
    
        #region Methods
        internal override bool? Save(Qiyas.DataAccessLayer.QiyasLinqDataContext context, bool commit)
        {
            if (isNew)
            {
              context.ExamCenterRequiredExams.InsertOnSubmit(this.entity);
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
              context.ExamCenterRequiredExams.DeleteOnSubmit(this.entity);
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
      