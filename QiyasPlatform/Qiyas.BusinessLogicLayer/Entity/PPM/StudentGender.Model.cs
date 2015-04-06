
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
    public partial class StudentGender : EntityBase<Qiyas.DataAccessLayer.StudentGender>
    {

        #region Constructors
        public StudentGender()
        {
            this.entity = new Qiyas.DataAccessLayer.StudentGender();
            isNew = true;
        }

        public StudentGender(int StudentGenderID)
        {
            this.entity = context.StudentGenders.Where(p => p.StudentGenderID == StudentGenderID).FirstOrDefault();  
        }
    
        internal StudentGender(Qiyas.DataAccessLayer.StudentGender entity)
        {
            this.entity = entity;
        }

        #endregion

        #region Properties
    
        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "StudentGenderID")]
        public int StudentGenderID
        {            
            set{ this.entity.StudentGenderID = value; }
            get{ return this.entity.StudentGenderID; }
        }
    
        [Display(Name = "StudentGenderName")]
        public string Name
        {            
            set{ this.entity.Name = value; }
            get{ return this.entity.Name; }
        }
    
        #endregion    
    
        #region Methods
        internal override bool? Save(Qiyas.DataAccessLayer.QiyasLinqDataContext context, bool commit)
        {
            if (isNew)
            {
              context.StudentGenders.InsertOnSubmit(this.entity);
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
              context.StudentGenders.DeleteOnSubmit(this.entity);
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
      