
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
    public partial class ShippingBag : EntityBase<Qiyas.DataAccessLayer.ShippingBag>
    {

        #region Constructors
        public ShippingBag()
        {
            this.entity = new Qiyas.DataAccessLayer.ShippingBag();
            isNew = true;
        }

        public ShippingBag(int ShippingBagID)
        {
            this.entity = context.ShippingBags.Where(p => p.ShippingBagID == ShippingBagID).FirstOrDefault();  
        }
    
        internal ShippingBag(Qiyas.DataAccessLayer.ShippingBag entity)
        {
            this.entity = entity;
        }

        #endregion

        #region Properties
    
        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "ShippingBagID")]
        public int ShippingBagID
        {            
            set{ this.entity.ShippingBagID = value; }
            get{ return this.entity.ShippingBagID; }
        }
    
        
        [Display(Name = "ExamCenterRequiredExamsID")]
        public int? ExamCenterRequiredExamsID
        {            
            set{ this.entity.ExamCenterRequiredExamsID = value; }
            get{ return this.entity.ExamCenterRequiredExamsID; }
        }
    
        [Display(Name = "ShippingBagCode")]
        public string ShippingBagCode
        {            
            set{ this.entity.ShippingBagCode = value; }
            get{ return this.entity.ShippingBagCode; }
        }


        [Display(Name = "ShippingBagSerial")]
        public int? ShippingBagSerial
        {
            set { this.entity.ShippingBagSerial = value; }
            get { return this.entity.ShippingBagSerial; }
        }

        [Display(Name = "BookCount")]
        public int? BookCount
        {
            set { this.entity.BookCount = value; }
            get { return this.entity.BookCount; }
        }

        [Display(Name = "PackCount")]
        public int? PackCount
        {
            set { this.entity.PackCount = value; }
            get { return this.entity.PackCount; }
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
    
        #endregion    
    
        #region Methods
        internal override bool? Save(Qiyas.DataAccessLayer.QiyasLinqDataContext context, bool commit)
        {
            if (isNew)
            {
              context.ShippingBags.InsertOnSubmit(this.entity);
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
              context.ShippingBags.DeleteOnSubmit(this.entity);
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
      