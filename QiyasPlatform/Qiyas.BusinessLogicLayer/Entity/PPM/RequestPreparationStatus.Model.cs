
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
    public partial class RequestPreparationStatu : EntityBase<Qiyas.DataAccessLayer.RequestPreparationStatus>
    {

        #region Constructors
        public RequestPreparationStatu()
        {
            this.entity = new Qiyas.DataAccessLayer.RequestPreparationStatus();
            isNew = true;
        }

        public RequestPreparationStatu(int RequestPreparationStatusID)
        {
            this.entity = context.RequestPreparationStatus.Where(p => p.RequestPreparationStatusID == RequestPreparationStatusID).FirstOrDefault();  
        }
    
        internal RequestPreparationStatu(Qiyas.DataAccessLayer.RequestPreparationStatus entity)
        {
            this.entity = entity;
        }

        #endregion

        #region Properties
    
        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "RequestPreparationStatusID")]
        public int RequestPreparationStatusID
        {            
            set{ this.entity.RequestPreparationStatusID = value; }
            get{ return this.entity.RequestPreparationStatusID; }
        }
    
        [Display(Name = "RequestPreparationStatusName")]
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
              context.RequestPreparationStatus.InsertOnSubmit(this.entity);
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
              context.RequestPreparationStatus.DeleteOnSubmit(this.entity);
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
      