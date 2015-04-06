
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
    public partial class ExamPeriod : EntityBase<Qiyas.DataAccessLayer.ExamPeriod>
    {

        #region Constructors
        public ExamPeriod()
        {
            this.entity = new Qiyas.DataAccessLayer.ExamPeriod();
            isNew = true;
        }

        public ExamPeriod(int ExamPeriodID)
        {
            this.entity = context.ExamPeriods.Where(p => p.ExamPeriodID == ExamPeriodID).FirstOrDefault();  
        }
    
        internal ExamPeriod(Qiyas.DataAccessLayer.ExamPeriod entity)
        {
            this.entity = entity;
        }

        #endregion

        #region Properties
    
        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "ExamPeriodID")]
        public int ExamPeriodID
        {            
            set{ this.entity.ExamPeriodID = value; }
            get{ return this.entity.ExamPeriodID; }
        }
    
        [Display(Name = "ExamPeriodName")]
        public string Name
        {            
            set{ this.entity.Name = value; }
            get{ return this.entity.Name; }
        }
    
        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "ExamTypeID")]
        public int? ExamTypeID
        {            
            set{ this.entity.ExamTypeID = value; }
            get{ return this.entity.ExamTypeID; }
        }
    
        [Display(Name = "ExamYear")]
        public int? ExamYear
        {            
            set{ this.entity.ExamYear = value; }
            get{ return this.entity.ExamYear; }
        }
    
        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "StudentGenderID")]
        public int? StudentGenderID
        {            
            set{ this.entity.StudentGenderID = value; }
            get{ return this.entity.StudentGenderID; }
        }
    
        [Display(Name = "StartDate")]
        public DateTime? StartDate
        {            
            set{ this.entity.StartDate = value; }
            get{ return this.entity.StartDate; }
        }
    
        [Display(Name = "IsActive")]
        public bool? IsActive
        {            
            set{ this.entity.IsActive = value; }
            get{ return this.entity.IsActive; }
        }
    
        [Display(Name = "CreatorID")]
        public int? CreatorID
        {            
            set{ this.entity.CreatorID = value; }
            get{ return this.entity.CreatorID; }
        }
    
        [Display(Name = "CreatedDate")]
        public DateTime? CreatedDate
        {            
            set{ this.entity.CreatedDate = value; }
            get{ return this.entity.CreatedDate; }
        }
    
        [Display(Name = "ModifiedByID")]
        public int? ModifiedByID
        {            
            set{ this.entity.ModifiedByID = value; }
            get{ return this.entity.ModifiedByID; }
        }
    
        [Display(Name = "ModifiedDate")]
        public DateTime? ModifiedDate
        {            
            set{ this.entity.ModifiedDate = value; }
            get{ return this.entity.ModifiedDate; }
        }
    
        [Display(Name = "EndDate")]
        public DateTime? EndDate
        {            
            set{ this.entity.EndDate = value; }
            get{ return this.entity.EndDate; }
        }
    
        #endregion    
    
        #region Methods
        internal override bool? Save(Qiyas.DataAccessLayer.QiyasLinqDataContext context, bool commit)
        {
            if (isNew)
            {
              context.ExamPeriods.InsertOnSubmit(this.entity);
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
              context.ExamPeriods.DeleteOnSubmit(this.entity);
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
      