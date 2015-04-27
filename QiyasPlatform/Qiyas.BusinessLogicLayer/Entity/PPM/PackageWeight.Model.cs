
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
    public partial class PackageWeight : EntityBase<Qiyas.DataAccessLayer.PackageWeight>
    {

        #region Constructors
        public PackageWeight()
        {
            this.entity = new Qiyas.DataAccessLayer.PackageWeight();
            isNew = true;
        }

        public PackageWeight(int PackageWeightID)
        {
            this.entity = context.PackageWeights.Where(p => p.PackageWeightID == PackageWeightID).FirstOrDefault();  
        }
    
        internal PackageWeight(Qiyas.DataAccessLayer.PackageWeight entity)
        {
            this.entity = entity;
        }

        #endregion

        #region Properties
    
        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "PackageWeightID")]
        public int PackageWeightID
        {            
            set{ this.entity.PackageWeightID = value; }
            get{ return this.entity.PackageWeightID; }
        }
    
        [Display(Name = "PackageWeightName")]
        public string Name
        {            
            set{ this.entity.Name = value; }
            get{ return this.entity.Name; }
        }

        [Required(ErrorMessage = "RequiredValidation")]
        [Display(Name = "Weight")]
        public decimal? Weight
        {            
            set{ this.entity.Weight = value; }
            get { return this.entity.Weight; }
        }

        [Required(ErrorMessage = "RequiredValidation")] 
        [Display(Name = "PackageCode")]
        public int? PackageCode
        {
            set { this.entity.PackageCode = value; }
            get { return this.entity.PackageCode; }
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
              context.PackageWeights.InsertOnSubmit(this.entity);
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
              context.PackageWeights.DeleteOnSubmit(this.entity);
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
      