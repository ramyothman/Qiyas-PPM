
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
    public partial class PackagingType : EntityBase<Qiyas.DataAccessLayer.PackagingType>
    {

        #region Constructors
        public PackagingType()
        {
            this.entity = new Qiyas.DataAccessLayer.PackagingType();
            isNew = true;
        }

        public PackagingType(int PackagingTypeID)
        {
            this.entity = context.PackagingTypes.Where(p => p.PackagingTypeID == PackagingTypeID).FirstOrDefault();  
        }
    
        internal PackagingType(Qiyas.DataAccessLayer.PackagingType entity)
        {
            this.entity = entity;
        }

        #endregion

        #region Properties
    
        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "PackagingTypeID")]
        public int PackagingTypeID
        {            
            set{ this.entity.PackagingTypeID = value; }
            get{ return this.entity.PackagingTypeID; }
        }
    
        [Display(Name = "PackagingTypeName")]
        public string Name
        {            
            set{ this.entity.Name = value; }
            get{ return this.entity.Name; }
        }
    
        [Display(Name = "ExamModelCount")]
        public int? ExamModelCount
        {            
            set{ this.entity.ExamModelCount = value; }
            get{ return this.entity.ExamModelCount; }
        }
    
        [Display(Name = "BooksPerPackage")]
        public int? BooksPerPackage
        {            
            set{ this.entity.BooksPerPackage = value; }
            get{ return this.entity.BooksPerPackage; }
        }
    
        [Display(Name = "Total")]
        public int? Total
        {            
            set{ this.entity.Total = value; }
            get{ return this.entity.Total; }
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
              context.PackagingTypes.InsertOnSubmit(this.entity);
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
              context.PackagingTypes.DeleteOnSubmit(this.entity);
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
      