
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
    public partial class BookRepackPackageItem : EntityBase<Qiyas.DataAccessLayer.BookRepackPackageItem>
    {

        #region Constructors
        public BookRepackPackageItem()
        {
            this.entity = new Qiyas.DataAccessLayer.BookRepackPackageItem();
            isNew = true;
        }

        public BookRepackPackageItem(int BookRepackPackageItemID)
        {
            this.entity = context.BookRepackPackageItems.Where(p => p.BookRepackPackageItemID == BookRepackPackageItemID).FirstOrDefault();  
        }
    
        internal BookRepackPackageItem(Qiyas.DataAccessLayer.BookRepackPackageItem entity)
        {
            this.entity = entity;
        }

        #endregion

        #region Properties
    
        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "BookRepackPackageItemID")]
        public int BookRepackPackageItemID
        {            
            set{ this.entity.BookRepackPackageItemID = value; }
            get{ return this.entity.BookRepackPackageItemID; }
        }
    
        [Display(Name = "BookRepackPackageID")]
        public int? BookRepackPackageID
        {            
            set{ this.entity.BookRepackPackageID = value; }
            get{ return this.entity.BookRepackPackageID; }
        }
    
        [Display(Name = "BookPackItemID")]
        public int? BookPackItemID
        {            
            set{ this.entity.BookPackItemID = value; }
            get{ return this.entity.BookPackItemID; }
        }
    
        #endregion    
    
        #region Methods
        internal override bool? Save(Qiyas.DataAccessLayer.QiyasLinqDataContext context, bool commit)
        {
            if (isNew)
            {
              context.BookRepackPackageItems.InsertOnSubmit(this.entity);
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
              context.BookRepackPackageItems.DeleteOnSubmit(this.entity);
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
      