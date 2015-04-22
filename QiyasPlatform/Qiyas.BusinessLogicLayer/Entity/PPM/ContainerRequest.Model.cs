
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
    public partial class ContainerRequest : EntityBase<Qiyas.DataAccessLayer.ContainerRequest>
    {

        #region Constructors
        public ContainerRequest()
        {
            this.entity = new Qiyas.DataAccessLayer.ContainerRequest();
            isNew = true;
        }

        public ContainerRequest(int ContainerRequestID)
        {
            this.entity = context.ContainerRequests.Where(p => p.ContainerRequestID == ContainerRequestID).FirstOrDefault();  
        }
    
        internal ContainerRequest(Qiyas.DataAccessLayer.ContainerRequest entity)
        {
            this.entity = entity;
        }

        #endregion

        #region Properties
    
        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "ContainerRequestID")]
        public int ContainerRequestID
        {            
            set{ this.entity.ContainerRequestID = value; }
            get{ return this.entity.ContainerRequestID; }
        }
    
        [Required(ErrorMessage="RequiredValidation")] 
        [Display(Name = "ExamCenterRequiredExamsID")]
        public int? ExamCenterRequiredExamsID
        {            
            set{ this.entity.ExamCenterRequiredExamsID = value; }
            get{ return this.entity.ExamCenterRequiredExamsID; }
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
              context.ContainerRequests.InsertOnSubmit(this.entity);
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
              context.ContainerRequests.DeleteOnSubmit(this.entity);
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
      