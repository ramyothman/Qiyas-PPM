
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
    public partial class BookPackingOperation : EntityBase<Qiyas.DataAccessLayer.BookPackingOperation>
    {

        #region Constructors
        public BookPackingOperation()
        {
            this.entity = new Qiyas.DataAccessLayer.BookPackingOperation();
            isNew = true;
        }

        public BookPackingOperation(int BookPackingOperationID)
        {
            this.entity = context.BookPackingOperations.Where(p => p.BookPackingOperationID == BookPackingOperationID).FirstOrDefault();  
        }
    
        internal BookPackingOperation(Qiyas.DataAccessLayer.BookPackingOperation entity)
        {
            this.entity = entity;
        }

        #endregion

        #region Properties
    
        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "BookPackingOperationID")]
        public int BookPackingOperationID
        {            
            set{ this.entity.BookPackingOperationID = value; }
            get{ return this.entity.BookPackingOperationID; }
        }
    
        [Display(Name = "BookPackingOperationName")]
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

        [Display(Name = "BookPrintingOperationID")]
        public int? BookPrintingOperationID
        {
            set { this.entity.BookPrintingOperationID = value; }
            get { return this.entity.BookPrintingOperationID; }
        }

        [Display(Name = "PackingParentID")]
        public int? PackingParentID
        {
            set { this.entity.PackingParentID = value; }
            get { return this.entity.PackingParentID; }
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

        [Display(Name = "PackageTotalPerModel")]
        public int? PackageTotalPerModel
        {
            set { this.entity.PackageTotalPerModel = value; }
            get { return this.entity.PackageTotalPerModel; }
        }

        [Display(Name = "NumberofBooksPerModel")]
        public int? NumberofBooksPerModel
        {
            set { this.entity.NumberofBooksPerModel = value; }
            get { return this.entity.NumberofBooksPerModel; }
        }
    
        #endregion    
    
        #region Methods
        internal override bool? Save(Qiyas.DataAccessLayer.QiyasLinqDataContext context, bool commit)
        {
            if (isNew)
            {
              context.BookPackingOperations.InsertOnSubmit(this.entity);
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
              context.BookPackingOperations.DeleteOnSubmit(this.entity);
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
      