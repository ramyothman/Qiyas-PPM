
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
    public partial class Exam : EntityBase<Qiyas.DataAccessLayer.Exam>
    {

        #region Constructors
        public Exam()
        {
            this.entity = new Qiyas.DataAccessLayer.Exam();
            isNew = true;
        }

        public Exam(int ExamID)
        {
            this.entity = context.Exams.Where(p => p.ExamID == ExamID).FirstOrDefault();  
        }
    
        internal Exam(Qiyas.DataAccessLayer.Exam entity)
        {
            this.entity = entity;
        }

        #endregion

        #region Properties
    
        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "ExamID")]
        public int ExamID
        {            
            set{ this.entity.ExamID = value; }
            get{ return this.entity.ExamID; }
        }
    
        [Display(Name = "ExamCode")]
        public string ExamCode
        {            
            set{ this.entity.ExamCode = value; }
            get{ return this.entity.ExamCode; }
        }
    
        [Display(Name = "ExamName")]
        public string Name
        {            
            set{ this.entity.Name = value; }
            get{ return this.entity.Name; }
        }
    
        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "ExamSpecialityID")]
        public int? ExamSpecialityID
        {            
            set{ this.entity.ExamSpecialityID = value; }
            get{ return this.entity.ExamSpecialityID; }
        }
    
        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "StudentGenderID")]
        public int? StudentGenderID
        {            
            set{ this.entity.StudentGenderID = value; }
            get{ return this.entity.StudentGenderID; }
        }
    
        [Display(Name = "NumberofSections")]
        public int? NumberofSections
        {            
            set{ this.entity.NumberofSections = value; }
            get{ return this.entity.NumberofSections; }
        }
    
        [Display(Name = "NumberofPages")]
        public int? NumberofPages
        {            
            set{ this.entity.NumberofPages = value; }
            get{ return this.entity.NumberofPages; }
        }
    
        [Display(Name = "TimeForSection")]
        public int? TimeForSection
        {            
            set{ this.entity.TimeForSection = value; }
            get{ return this.entity.TimeForSection; }
        }
    
        [Display(Name = "Notes")]
        public string Notes
        {            
            set{ this.entity.Notes = value; }
            get{ return this.entity.Notes; }
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
    
        #endregion    
    
        #region Methods
        internal override bool? Save(Qiyas.DataAccessLayer.QiyasLinqDataContext context, bool commit)
        {
            if (isNew)
            {
              context.Exams.InsertOnSubmit(this.entity);
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
              context.Exams.DeleteOnSubmit(this.entity);
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
      