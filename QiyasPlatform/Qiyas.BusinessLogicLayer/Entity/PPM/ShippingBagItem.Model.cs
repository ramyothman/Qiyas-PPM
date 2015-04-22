
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
    public partial class ShippingBagItem : EntityBase<Qiyas.DataAccessLayer.ShippingBagItem>
    {

        #region Constructors
        public ShippingBagItem()
        {
            this.entity = new Qiyas.DataAccessLayer.ShippingBagItem();
            isNew = true;
        }

        public ShippingBagItem(int ShippingBagItemID)
        {
            this.entity = context.ShippingBagItems.Where(p => p.ShippingBagItemID == ShippingBagItemID).FirstOrDefault();  
        }
    
        internal ShippingBagItem(Qiyas.DataAccessLayer.ShippingBagItem entity)
        {
            this.entity = entity;
        }

        #endregion

        #region Properties
    
        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "ShippingBagItemID")]
        public int ShippingBagItemID
        {            
            set{ this.entity.ShippingBagItemID = value; }
            get{ return this.entity.ShippingBagItemID; }
        }
    
        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "ShippingBagID")]
        public int? ShippingBagID
        {            
            set{ this.entity.ShippingBagID = value; }
            get{ return this.entity.ShippingBagID; }
        }
    
        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "BookPackItemID")]
        public int? BookPackItemID
        {            
            set{ this.entity.BookPackItemID = value; }
            get{ return this.entity.BookPackItemID; }
        }
    
        [Display(Name = "CreatedDate")]
        public DateTime? CreatedDate
        {            
            set{ this.entity.CreatedDate = value; }
            get{ return this.entity.CreatedDate; }
        }
    
        [Display(Name = "ModifiedDate")]
        public DateTime? ModifiedDate
        {            
            set{ this.entity.ModifiedDate = value; }
            get{ return this.entity.ModifiedDate; }
        }
    
        [Display(Name = "CreatedBy")]
        public int? CreatedBy
        {            
            set{ this.entity.CreatedBy = value; }
            get{ return this.entity.CreatedBy; }
        }
    
        [Display(Name = "ModifiedBy")]
        public int? ModifiedBy
        {            
            set{ this.entity.ModifiedBy = value; }
            get{ return this.entity.ModifiedBy; }
        }
    
        #endregion    
    
        #region Methods
        internal override bool? Save(Qiyas.DataAccessLayer.QiyasLinqDataContext context, bool commit)
        {
            if (isNew)
            {
              context.ShippingBagItems.InsertOnSubmit(this.entity);
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
              context.ShippingBagItems.DeleteOnSubmit(this.entity);
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
      