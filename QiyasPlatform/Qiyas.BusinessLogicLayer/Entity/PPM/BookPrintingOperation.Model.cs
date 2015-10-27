
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
    public partial class BookPrintingOperation : EntityBase<Qiyas.DataAccessLayer.BookPrintingOperation>
    {

        #region Constructors
        public BookPrintingOperation()
        {
            this.entity = new Qiyas.DataAccessLayer.BookPrintingOperation();
            isNew = true;
        }

        public BookPrintingOperation(int BookPrintingOperationID)
        {
            this.entity = context.BookPrintingOperations.Where(p => p.BookPrintingOperationID == BookPrintingOperationID).FirstOrDefault();  
        }
    
        internal BookPrintingOperation(Qiyas.DataAccessLayer.BookPrintingOperation entity)
        {
            this.entity = entity;
        }

        #endregion

        #region Properties
    
        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "BookPrintingOperationID")]
        public int BookPrintingOperationID
        {            
            set{ this.entity.BookPrintingOperationID = value; }
            get{ return this.entity.BookPrintingOperationID; }
        }
    
        [Display(Name = "BookPrintingOperationName")]
        public string Name
        {            
            set{ this.entity.Name = value; }
            get{ return this.entity.Name; }
        }
    
        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "ExamID")]
        public int? ExamID
        {            
            set{ this.entity.ExamID = value; }
            get
            {
                if (this.entity == null)
                    return 0;
                return this.entity.ExamID; 
            }
        }
    
        [Display(Name = "PrintsForOneModel")]
        public int? PrintsForOneModel
        {            
            set{ this.entity.PrintsForOneModel = value; }
            get{ return  this.entity == null? 0 : this.entity.PrintsForOneModel; }
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
    
        
        [Display(Name = "OperationStatusID")]
        public int? OperationStatusID
        {            
            set{ this.entity.OperationStatusID = value; }
            get{ return this.entity.OperationStatusID; }
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
              context.BookPrintingOperations.InsertOnSubmit(this.entity);
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
              context.BookPrintingOperations.DeleteOnSubmit(this.entity);
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
      