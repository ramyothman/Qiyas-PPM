
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
    public partial class ExamSpeciality : EntityBase<Qiyas.DataAccessLayer.ExamSpeciality>
    {

        #region Constructors
        public ExamSpeciality()
        {
            this.entity = new Qiyas.DataAccessLayer.ExamSpeciality();
            isNew = true;
        }

        public ExamSpeciality(int ExamSpecialityID)
        {
            this.entity = context.ExamSpecialities.Where(p => p.ExamSpecialityID == ExamSpecialityID).FirstOrDefault();  
        }
    
        internal ExamSpeciality(Qiyas.DataAccessLayer.ExamSpeciality entity)
        {
            this.entity = entity;
        }

        #endregion

        #region Properties
    
        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "ExamSpecialityID")]
        public int ExamSpecialityID
        {            
            set{ this.entity.ExamSpecialityID = value; }
            get{ return this.entity.ExamSpecialityID; }
        }
    
        [Display(Name = "ExamSpecialityName")]
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
              context.ExamSpecialities.InsertOnSubmit(this.entity);
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
              context.ExamSpecialities.DeleteOnSubmit(this.entity);
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
      