
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
    public partial class CountryRegion : EntityBase<Qiyas.DataAccessLayer.CountryRegion>
    {

        #region Constructors
        public CountryRegion()
        {
            this.entity = new Qiyas.DataAccessLayer.CountryRegion();
            isNew = true;
        }

        public CountryRegion(string CountryRegionCode)
        {
            this.entity = context.CountryRegions.Where(p => p.CountryRegionCode == CountryRegionCode).FirstOrDefault();  
        }
    
        internal CountryRegion(Qiyas.DataAccessLayer.CountryRegion entity)
        {
            this.entity = entity;
        }

        #endregion

        #region Properties
    
        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "CountryRegionCode")]
        public string CountryRegionCode
        {            
            set{ this.entity.CountryRegionCode = value; }
            get{ return this.entity.CountryRegionCode; }
        }
    
        [Display(Name = "CountryRegionName")]
        public string Name
        {            
            set{ this.entity.Name = value; }
            get{ return this.entity.Name; }
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
              context.CountryRegions.InsertOnSubmit(this.entity);
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
              context.CountryRegions.DeleteOnSubmit(this.entity);
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
      