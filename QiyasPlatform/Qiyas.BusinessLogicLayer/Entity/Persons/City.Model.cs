
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
    public partial class City : EntityBase<Qiyas.DataAccessLayer.City>
    {

        #region Constructors
        public City()
        {
            this.entity = new Qiyas.DataAccessLayer.City();
            isNew = true;
        }

        public City(int CityID)
        {
            this.entity = context.Cities.Where(p => p.CityID == CityID).FirstOrDefault();  
        }
    
        internal City(Qiyas.DataAccessLayer.City entity)
        {
            this.entity = entity;
        }

        #endregion

        #region Properties
    
        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "CityID")]
        public int CityID
        {            
            set{ this.entity.CityID = value; }
            get{ return this.entity.CityID; }
        }
    
        [Display(Name = "StateRegionID")]
        public int? StateRegionID
        {            
            set{ this.entity.StateRegionID = value; }
            get{ return this.entity.StateRegionID; }
        }
    
        [Display(Name = "CityName")]
        public string Name
        {            
            set{ this.entity.Name = value; }
            get{ return this.entity.Name; }
        }
    
        [Display(Name = "IsActive")]
        public bool? IsActive
        {            
            set{ this.entity.IsActive = value; }
            get{ return this.entity.IsActive; }
        }
    
        #endregion    
    
        #region Methods
        internal override bool? Save(Qiyas.DataAccessLayer.QiyasLinqDataContext context, bool commit)
        {
            if (isNew)
            {
              context.Cities.InsertOnSubmit(this.entity);
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
              context.Cities.DeleteOnSubmit(this.entity);
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
      