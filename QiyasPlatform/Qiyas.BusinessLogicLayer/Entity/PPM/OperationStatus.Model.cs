
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
    public partial class OperationStatu : EntityBase<Qiyas.DataAccessLayer.OperationStatus>
    {

        #region Constructors
        public OperationStatu()
        {
            this.entity = new Qiyas.DataAccessLayer.OperationStatus();
            isNew = true;
        }

        public OperationStatu(int OperationStatusID)
        {
            this.entity = context.OperationStatus.Where(p => p.OperationStatusID == OperationStatusID).FirstOrDefault();  
        }
    
        internal OperationStatu(Qiyas.DataAccessLayer.OperationStatus entity)
        {
            this.entity = entity;
        }

        #endregion

        #region Properties
    
        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "OperationStatusID")]
        public int OperationStatusID
        {            
            set{ this.entity.OperationStatusID = value; }
            get{ return this.entity.OperationStatusID; }
        }
    
        [Display(Name = "OperationStatusName")]
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
              context.OperationStatus.InsertOnSubmit(this.entity);
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
              context.OperationStatus.DeleteOnSubmit(this.entity);
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
      