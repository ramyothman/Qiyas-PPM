
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
    public partial class BookPackItemOperation : EntityBase<Qiyas.DataAccessLayer.BookPackItemOperation>
    {

        #region Constructors
        public BookPackItemOperation()
        {
            this.entity = new Qiyas.DataAccessLayer.BookPackItemOperation();
            isNew = true;
        }

        public BookPackItemOperation(int BookPackItemOperationID)
        {
            this.entity = context.BookPackItemOperations.Where(p => p.BookPackItemOperationID == BookPackItemOperationID).FirstOrDefault();  
        }
    
        internal BookPackItemOperation(Qiyas.DataAccessLayer.BookPackItemOperation entity)
        {
            this.entity = entity;
        }

        #endregion

        #region Properties
    
        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "BookPackItemOperationID")]
        public int BookPackItemOperationID
        {            
            set{ this.entity.BookPackItemOperationID = value; }
            get{ return this.entity.BookPackItemOperationID; }
        }
    
        [Display(Name = "BookPackItemOperationName")]
        public string Name
        {            
            set{ this.entity.Name = value; }
            get{ return this.entity.Name; }
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
    
        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "BookPackItemID")]
        public int? BookPackItemID
        {            
            set{ this.entity.BookPackItemID = value; }
            get{ return this.entity.BookPackItemID; }
        }
    
        [Display(Name = "PackagingTypeID")]
        public int? PackagingTypeID
        {            
            set{ this.entity.PackagingTypeID = value; }
            get{ return this.entity.PackagingTypeID; }
        }
    
        [Display(Name = "PackingCalculationTypeID")]
        public int? PackingCalculationTypeID
        {            
            set{ this.entity.PackingCalculationTypeID = value; }
            get{ return this.entity.PackingCalculationTypeID; }
        }
    
        [Display(Name = "AllocatedFrom")]
        public string AllocatedFrom
        {            
            set{ this.entity.AllocatedFrom = value; }
            get{ return this.entity.AllocatedFrom; }
        }
    
        [Display(Name = "PackingValue")]
        public int? PackingValue
        {            
            set{ this.entity.PackingValue = value; }
            get{ return this.entity.PackingValue; }
        }
    
        [Display(Name = "PackageTotal")]
        public int? PackageTotal
        {            
            set{ this.entity.PackageTotal = value; }
            get{ return this.entity.PackageTotal; }
        }
    
        [Display(Name = "PackingParentID")]
        public int? PackingParentID
        {            
            set{ this.entity.PackingParentID = value; }
            get{ return this.entity.PackingParentID; }
        }
    
        #endregion    
    
        #region Methods
        internal override bool? Save(Qiyas.DataAccessLayer.QiyasLinqDataContext context, bool commit)
        {
            if (isNew)
            {
              context.BookPackItemOperations.InsertOnSubmit(this.entity);
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
              context.BookPackItemOperations.DeleteOnSubmit(this.entity);
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
      