
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
    public partial class PackingCalculationType : EntityBase<Qiyas.DataAccessLayer.PackingCalculationType>
    {

        #region Constructors
        public PackingCalculationType()
        {
            this.entity = new Qiyas.DataAccessLayer.PackingCalculationType();
            isNew = true;
        }

        public PackingCalculationType(int PackingCalculationTypeID)
        {
            this.entity = context.PackingCalculationTypes.Where(p => p.PackingCalculationTypeID == PackingCalculationTypeID).FirstOrDefault();  
        }
    
        internal PackingCalculationType(Qiyas.DataAccessLayer.PackingCalculationType entity)
        {
            this.entity = entity;
        }

        #endregion

        #region Properties
    
        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "PackingCalculationTypeID")]
        public int PackingCalculationTypeID
        {            
            set{ this.entity.PackingCalculationTypeID = value; }
            get{ return this.entity.PackingCalculationTypeID; }
        }
    
        [Display(Name = "PackingCalculationTypeName")]
        public string Name
        {            
            set{ this.entity.Name = value; }
            get{ return this.entity.Name; }
        }
    
        #endregion    
    
        #region Methods
        internal override bool? Save(Qiyas.DataAccessLayer.QiyasLinqDataContext context, bool commit)
        {
            if (isNew)
            {
              context.PackingCalculationTypes.InsertOnSubmit(this.entity);
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
              context.PackingCalculationTypes.DeleteOnSubmit(this.entity);
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
      