
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
    public partial class BookPackItem : EntityBase<Qiyas.DataAccessLayer.BookPackItem>
    {

        #region Constructors
        public BookPackItem()
        {
            this.entity = new Qiyas.DataAccessLayer.BookPackItem();
            isNew = true;
        }

        public BookPackItem(int BookPackItemID)
        {
            this.entity = context.BookPackItems.Where(p => p.BookPackItemID == BookPackItemID).FirstOrDefault();  
        }
    
        internal BookPackItem(Qiyas.DataAccessLayer.BookPackItem entity)
        {
            this.entity = entity;
        }

        #endregion

        #region Properties
    
        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "BookPackItemID")]
        public int BookPackItemID
        {            
            set{ this.entity.BookPackItemID = value; }
            get{ return this.entity.BookPackItemID; }
        }
    
        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "BookPackingOperationID")]
        public int? BookPackingOperationID
        {            
            set{ this.entity.BookPackingOperationID = value; }
            get{ return this.entity.BookPackingOperationID; }
        }
    
        [Display(Name = "PackCode")]
        public string PackCode
        {            
            set{ this.entity.PackCode = value; }
            get{ return this.entity.PackCode; }
        }
    
        [Display(Name = "Weight")]
        public decimal? Weight
        {            
            set{ this.entity.Weight = value; }
            get{ return this.entity.Weight; }
        }
    
        [Display(Name = "OperationStatusID")]
        public int? OperationStatusID
        {            
            set{ this.entity.OperationStatusID = value; }
            get{ return this.entity.OperationStatusID; }
        }
    
        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "ParentID")]
        public int? ParentID
        {            
            set{ this.entity.ParentID = value; }
            get{ return this.entity.ParentID; }
        }

        [Required(ErrorMessage = "PackSerial")]
        [Display(Name = "PackSerial")]
        public int? PackSerial
        {
            set { this.entity.PackSerial = value; }
            get { return this.entity.PackSerial; }
        }

        
        [Display(Name = "StartBookSerial")]
        public int? StartBookSerial
        {
            set { this.entity.StartBookSerial = value; }
            get { return this.entity.StartBookSerial; }
        }

        [Display(Name = "LastBookSerial")]
        public int? LastBookSerial
        {
            set { this.entity.LastBookSerial = value; }
            get { return this.entity.LastBookSerial; }
        }


        public int? BookPackItemOperationID
        {
            set { this.entity.BookPackItemOperationID = value; }
            get { return this.entity.BookPackItemOperationID; }
        }
    
        #endregion    
    
        #region Methods
        internal override bool? Save(Qiyas.DataAccessLayer.QiyasLinqDataContext context, bool commit)
        {
            if (isNew)
            {
              context.BookPackItems.InsertOnSubmit(this.entity);
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
              context.BookPackItems.DeleteOnSubmit(this.entity);
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
      