
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
    public partial class ExamCenter : EntityBase<Qiyas.DataAccessLayer.ViewExamCenter>
    {

        #region Constructors
        public ExamCenter()
        {
            this.entity = new Qiyas.DataAccessLayer.ViewExamCenter();
            isNew = true;
        }

        public ExamCenter(int ExaminationCenterID)
        {
            this.entity = context.ViewExamCenters.Where(p => p.ExaminationCenterID == ExaminationCenterID).FirstOrDefault();  
        }

        public ExamCenter(string Code)
        {
            this.entity = context.ViewExamCenters.Where(p => p.CenterCode == Code).FirstOrDefault();
        }

        internal ExamCenter(Qiyas.DataAccessLayer.ViewExamCenter entity)
        {
            this.entity = entity;
        }

        #endregion

        #region Properties
    
        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "ExaminationCenterID")]
        public int ExaminationCenterID
        {            
            set{ this.entity.ExaminationCenterID = value; }
            get{ return this.entity.ExaminationCenterID; }
        }
    
        [Display(Name = "CenterCode")]
        public string CenterCode
        {            
            set{ this.entity.CenterCode = value; }
            get{ return this.entity.CenterCode; }
        }
    
        [Display(Name = "ExamCenterName")]
        public string Name
        {            
            set{ this.entity.Name = value; }
            get{ return this.entity.Name; }
        }
    

        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "CityID")]
        public int? CityID
        {            
            set{ this.entity.CityID = value; }
            get{ return this.entity.CityID; }
        }
    
        [Display(Name = "StudentGenderID")]
        public int? StudentGenderID
        {            
            set{ this.entity.StudentGenderID = value; }
            get{ return this.entity.StudentGenderID; }
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
        private Qiyas.DataAccessLayer.ExamCenter GetCenter( int centerId)
        {
            Qiyas.DataAccessLayer.ExamCenter center = new DataAccessLayer.ExamCenter();
            if (centerId != 0)
                center = context.ExamCenters.Where(c => c.ExaminationCenterID == centerId).FirstOrDefault();
            center.CenterCode = this.entity.CenterCode;
            center.CityID = this.entity.CityID;
            center.CreatedDate = this.entity.CreatedDate;
            center.CreatorID = this.entity.CreatorID;
            center.IsActive = this.entity.IsActive;
            center.ExaminationCenterID = this.entity.ExaminationCenterID;
            center.ModifiedByID = this.entity.ModifiedByID;
            center.ModifiedDate = this.entity.ModifiedDate;
            center.Name = this.Name;
            center.StudentGenderID = this.StudentGenderID;
            return center;
        }

        private Qiyas.DataAccessLayer.ExamCenter GetCenter(int centerId, DataAccessLayer.QiyasLinqDataContext db)
        {
            Qiyas.DataAccessLayer.ExamCenter center = new DataAccessLayer.ExamCenter();
            if (centerId != 0)
                center = db.ExamCenters.Where(c => c.ExaminationCenterID == centerId).FirstOrDefault();
            center.CenterCode = this.entity.CenterCode;
            center.CityID = this.entity.CityID;
            center.CreatedDate = this.entity.CreatedDate;
            center.CreatorID = this.entity.CreatorID;
            center.IsActive = this.entity.IsActive;
            center.ExaminationCenterID = this.entity.ExaminationCenterID;
            center.ModifiedByID = this.entity.ModifiedByID;
            center.ModifiedDate = this.entity.ModifiedDate;
            center.Name = this.Name;
            center.StudentGenderID = this.StudentGenderID;
            return center;
        }
        internal override bool? Save(Qiyas.DataAccessLayer.QiyasLinqDataContext context, bool commit)
        {
            DataAccessLayer.QiyasLinqDataContext db = new DataAccessLayer.QiyasLinqDataContext();
            Qiyas.DataAccessLayer.ExamCenter center = null;
            if (isNew)
            {

                center = GetCenter(0,db);
                context.ExamCenters.InsertOnSubmit(center);
            }
            else
            {
                center = GetCenter(ExaminationCenterID,db);
            }
            if (commit)
      
              try
              {
                db.SubmitChanges();
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
              context.ExamCenters.DeleteOnSubmit(GetCenter(ExaminationCenterID));
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
      