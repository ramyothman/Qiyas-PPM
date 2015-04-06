
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Qiyas.BusinessLogicLayer.Entity;
using System.ComponentModel.DataAnnotations;

namespace Qiyas.BusinessLogicLayer.Entity.Persons
{
    [DataObject(true)]
    [Serializable]
    public partial class StateProvince : EntityBase<Qiyas.DataAccessLayer.StateProvince>
    {

        #region Constructors
        public StateProvince()
        {
            this.entity = new Qiyas.DataAccessLayer.StateProvince();
            isNew = true;
        }

        public StateProvince(int StateProvinceId)
        {
            this.entity = context.StateProvinces.Where(p => p.StateProvinceId == StateProvinceId).FirstOrDefault();  
        }
    
        internal StateProvince(Qiyas.DataAccessLayer.StateProvince entity)
        {
            this.entity = entity;
        }

        #endregion

        #region Properties
    
        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "StateProvinceId")]
        public int StateProvinceId
        {            
            set{ this.entity.StateProvinceId = value; }
            get{ return this.entity.StateProvinceId; }
        }
    
        [Display(Name = "StateProvinceCode")]
        public string StateProvinceCode
        {            
            set{ this.entity.StateProvinceCode = value; }
            get{ return this.entity.StateProvinceCode; }
        }
    
        [Display(Name = "CountryRegionCode")]
        public string CountryRegionCode
        {            
            set{ this.entity.CountryRegionCode = value; }
            get{ return this.entity.CountryRegionCode; }
        }
    
        [Display(Name = "IsOnlyStateProvince")]
        public bool? IsOnlyStateProvince
        {            
            set{ this.entity.IsOnlyStateProvince = value; }
            get{ return this.entity.IsOnlyStateProvince; }
        }
    
        [Display(Name = "StateProvinceName")]
        public string Name
        {            
            set{ this.entity.Name = value; }
            get{ return this.entity.Name; }
        }
    
        [Display(Name = "RowGuid")]
        public Guid RowGuid
        {            
            set{ this.entity.RowGuid = value; }
            get{ return this.entity.RowGuid; }
        }
    
        [Display(Name = "ModifiedDate")]
        public DateTime ModifiedDate
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
              context.StateProvinces.InsertOnSubmit(this.entity);
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
              context.StateProvinces.DeleteOnSubmit(this.entity);
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
      